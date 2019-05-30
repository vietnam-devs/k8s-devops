node {

    def scmVars = checkout scm
    def gitShortCommit = scmVars.GIT_COMMIT[0..6]
    def shouldBuildAPI

    try {
        stage('Detect changes') {
          
            shouldBuildAPI = sh (
                script: """                    
                    git log -m -1 --name-only ${scmVars.GIT_COMMIT}
                """,
                returnStdout: true
            ).trim()

            sh label: '', script: """  git log -m -1 --name-only ${scmVars.GIT_COMMIT} | grep src/ddd
            """
           

            echo "aaa ${shouldBuildAPI}  ssss"

            // if (shouldBuildAPI != "") {
            //     echo "should build"
            // }            
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