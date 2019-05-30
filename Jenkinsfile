node {

    def scmVars = checkout scm
    def gitShortCommit = scmVars.GIT_COMMIT[0..6]    

    try {
        
        docker.image('microsoft/dotnet:2.2.100-sdk-alpine').inside {
            stage('Build') {
                sh """
                    dotnet restore
                    dotnet build k8s-devops.sln --no-restore -nowarn:msb3202,nu1503                 
                """
            }            
        }         
    }
    catch(e) {
        throw e
    }
}