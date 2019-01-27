def label = "worker-${UUID.randomUUID().toString()}"

podTemplate(label: label, serviceAccount: 'jenkins', containers: [
  containerTemplate(name: 'netcore22', image: 'microsoft/dotnet:2.2.100-sdk-alpine', ttyEnabled: true),
  containerTemplate(name: 'docker', image: 'docker', command: 'cat', ttyEnabled: true),  
  containerTemplate(name: 'kubectl', image: 'lachlanevenson/k8s-kubectl:latest', command: 'cat', ttyEnabled: true, privileged: false)  
],
volumes: [
  hostPathVolume(mountPath: '/var/run/docker.sock', hostPath: '/var/run/docker.sock'),
  hostPathVolume(mountPath: '/home/jenkins/.nuget/packages', hostPath: '/home/.nuget/packages/')
]){
    node(label) {
        parameters {
            string(name: 'DEPLOY_TO_ENV', defaultValue: 'false', description: 'Do you want to deploy?')
            string(name: 'ENV', defaultValue: 'false', description: 'Which environment?')                    
        }

        def myRepo = checkout scm
        def gitCommit = myRepo.GIT_COMMIT
        def gitBranch = myRepo.GIT_BRANCH
        def shortGitCommit = "v-${gitCommit[0..6]}"

        stage('Build') {
            container('netcore22') {
                sh """
                    dotnet restore
                    dotnet build k8s-devops.sln --no-restore -nowarn:msb3202,nu1503
                """
            }
        }

        stage('Run unittest') {             
            println "Comming soon!"
        }

        if(params.DEPLOY_TO_ENV == 'true') {
            stage('Buid docker image') {
                container('docker') {
                    withCredentials([[$class: 'UsernamePasswordMultiBinding', credentialsId: 'nexus_key',
                        usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD']]) {
                        sh """
                            docker --version
                            echo $shortGitCommit
                            echo $REGISTRY_URL
                            
                            docker login -u $USERNAME -p $PASSWORD $REGISTRY_URL

                            docker build -f src/BiMonetaryApi/Dockerfile -t $REGISTRY_URL/bimonetary-api:$shortGitCommit -t $REGISTRY_URL/bimonetary-api:latest .                            
                            docker push $REGISTRY_URL/bimonetary-api:$shortGitCommit
                            docker push $REGISTRY_URL/bimonetary-api:latest

                            docker build -f src/ExchangeService/Dockerfile -t $REGISTRY_URL/exchange-service:$shortGitCommit -t $REGISTRY_URL/exchange-service:latest .                            
                            docker push $REGISTRY_URL/exchange-service:$shortGitCommit
                            docker push $REGISTRY_URL/exchange-service:latest
                        """
                    }                    
                }
            }

            stage('Deploy') {
                container('kubectl') {
                    sh """
                        kubectl set image deployments bimonetary-api-v1 *=192.168.1.4:8082/bimonetary-api:$shortGitCommit -n $params.ENV
                        kubectl set image deployments exchange *=192.168.1.4:8082/exchange-service:$shortGitCommit -n $params.ENV
                    """                
                }
            }
        }        
    }    
}