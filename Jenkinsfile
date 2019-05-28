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
            docker.withRegistry($REGISTRY_URL, 'nexus_docker_registry_login') {
                def bimonetaryImage = docker.build("bimonetary-api:${gitShortCommit}", "src/BiMonetaryApi")

                bimonetaryImage.push()
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