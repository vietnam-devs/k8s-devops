node {

    def scmVars = checkout scm
    def gitShortCommit = scmVars.GIT_COMMIT[0..6]
    def shouldBuildAPI

    try {
        stage('Detect changes') {
            shouldBuildAPI = sh (
                script: """
                    git log -m -1 --name-only --pretty=format:"" ${scmVars.GIT_COMMIT} -- src/BiMonetaryApi'
                """,
                returnStdout: true
            ).trim()

            echo "should build ${shouldBuildAPI}"
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