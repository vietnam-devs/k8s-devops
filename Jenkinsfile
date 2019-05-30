node {

    def scmVars = checkout scm
    def gitShortCommit = scmVars.GIT_COMMIT[0..6]

    try {
        stage('Detect changes') {
            sh """
                git log -m -1 --name-only --pretty=format:'' scmVars.GIT_COMMIT
            """            
        }

        docker.image('microsoft/dotnet:2.2.100-sdk-alpine').inside {
            stage('Build') {
                sh """
                    echo 'Hello Jenkins'                    
                """
            }            
        }         
    }
    catch(e) {
        throw e
    }
}