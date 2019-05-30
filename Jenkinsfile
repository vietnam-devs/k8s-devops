node {

    def scmVars = checkout scm
    def gitShortCommit = scmVars.GIT_COMMIT[0..6]    

    try {
        
        // docker.image('microsoft/dotnet:2.2.100-sdk-alpine').inside {
        //     stage('Build') {
        //         sh """
        //             dotnet restore
        //             dotnet build k8s-devops.sln --no-restore -nowarn:msb3202,nu1503                 
        //         """
        //     }     

        //     stage('Run unittest') {
        //         sh """
        //              dotnet test src/BiMonetary.Tests/NetCoreKit.Samples.Tests.csproj
        //         """
        //     }       
        // }  

        stage('Build Docker Image') {
            docker.image('docker:18.09').inside {
                withCredentials([[$class: 'UsernamePasswordMultiBinding', credentialsId: 'nexus_docker_registry_login', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD']]) {

                    sh """
                        docker login -u $USERNAME -p $PASSWORD $REGISTRY_URL

                        docker build -f src/BiMonetaryApi/Dockerfile -t $REGISTRY_URL/bimonetary-api:latest -t $REGISTRY_URL/bimonetary-api:${gitShortCommit} .
                        docker push $REGISTRY_URL/bimonetary-api:latest
                        docker push $REGISTRY_URL/bimonetary-api:${gitShortCommit}

                        docker build -f src/ExchangeService/Dockerfile -t $REGISTRY_URL/exchange-service:latest -t $REGISTRY_URL/exchange-service:${gitShortCommit} .
                        docker push $REGISTRY_URL/exchange-service:latest
                        docker push $REGISTRY_URL/exchange-service:${gitShortCommit}

                        docker logout
                    """
                    
                }
            }                                   
        }       
    }
    catch(e) {
        throw e
    }
}