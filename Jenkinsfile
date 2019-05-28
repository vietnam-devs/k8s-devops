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

        stage('Build Docker Image') {
            docker.image('docker:18.09').inside {
                withCredentials([[$class: 'UsernamePasswordMultiBinding', credentialsId: 'nexus_docker_registry_login', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD']]) {

                    sh """
                        docker login -u $USERNAME -p $PASSWORD $REGISTRY_URL

                        docker build -f src/BiMonetaryApi/Dockerfile -t $REGISTRY_URL/bimonetary-api:latest -t bimonetary-api:${gitShortCommit} .                    

                        docker push $REGISTRY_URL/bimonetary-api:latest
                    """
                    
                }

                // docker.withRegistry("${env.REGISTRY_URL}", 'nexus_docker_registry_login') {
                //     // def bimonetaryImage = docker.build("bimonetary-api:${gitShortCommit}", "-f src/BiMonetaryApi/Dockerfile .")

                //     // bimonetaryImage.push()
                //     sh """
                //         docker --version

                //         docker build -f src/BiMonetaryApi/Dockerfile -t $REGISTRY_URL/bimonetary-api:latest -t bimonetary-api:${gitShortCommit} .                    

                //         docker push $REGISTRY_URL/bimonetary-api:latest
                //     """
                // }
            }                        
            // sh """
            //     docker --version

            //     docker build -f src/BiMonetaryApi/Dockerfile -t bimonetary-api:latest -t bimonetary-api:${gitShortCommit} .                    
            // """
        }

        // docker.image('docker:18.09').inside {
        //     stage('Build Docker Image') {
        //         docker.build
        //         // sh """
        //         //     docker --version

        //         //     docker build -f src/BiMonetaryApi/Dockerfile -t bimonetary-api:latest -t bimonetary-api:${gitShortCommit} .                    
        //         // """
        //     }
        // }
    }
    catch(e) {
        throw e
    }
}