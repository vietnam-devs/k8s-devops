node {

    def scmVars = checkout scm
    def gitShortCommit = scmVars.GIT_COMMIT[0..6]
    def shouldBuildAPI

    try {
        stage('Detect changes') {
            def command = $/"git log -m -1 ${scmVars.GIT_COMMIT}"/$
            
            // res = sh(returnStdout: true, script: command).trim()

            // shouldBuildAPI = sh (
            //     script: "git log -m -1 --name-only ${scmVars.GIT_COMMIT} | grep src/BiMonetaryApi",
            //     returnStdout: true
            // ).trim()
            shouldBuildAPI = sh (
                script: """                    
                    git log -m -1 --name-only ${scmVars.GIT_COMMIT}
                """,
                returnStdout: true
            ).trim()

            res = sh (
                returnStdout: true, 
                script: command
            ).trim()

            echo "aaa ${res}  ssss"

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