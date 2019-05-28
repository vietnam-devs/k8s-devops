node {

    def scmVars = checkout scm
    def gitShortCommit = scmVars.GIT_COMMIT[0..6]

    try {
        // docker.image('mcr.microsoft.com/dotnet/core/sdk:2.2.300-alpine').inside {
        //     stage('Build') {
        //         sh '''
        //             echo ${gitShortCommit}
        //             dotnet restore
        //             dotnet build k8s-devops.sln --no-restore -nowarn:msb3202,nu1503
        //         '''
        //     }

        //     stage('Run unittest') {
        //         println('Unit test!!!')
        //     }
        // }

        // stage('Build Docker Image') {
        //     docker.image('docker:18.09').inside {
        //         withCredentials([[$class: 'UsernamePasswordMultiBinding', credentialsId: 'nexus_docker_registry_login', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD']]) {

        //             sh """
        //                 docker login -u $USERNAME -p $PASSWORD $REGISTRY_URL

        //                 docker build -f src/BiMonetaryApi/Dockerfile -t $REGISTRY_URL/bimonetary-api:latest -t bimonetary-api:${gitShortCommit} .                    

        //                 docker push $REGISTRY_URL/bimonetary-api:latest
        //                 docker push $REGISTRY_URL/bimonetary-api:${gitShortCommit}

        //                 docker logout
        //             """
                    
        //         }
        //     }                                   
        // }

        stage('Deploy') {
            docker.image('bitnami/kubectl:1.12.8').inside('-v ~/.kube:/config/.kube') {
                sh """
                       kubectl version

                       kubectl get nodes
                    """
            }                                   
        }
    }
    catch(e) {
        throw e
    }
}