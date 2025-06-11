// Jenkinsfile
pipeline {
    agent any

    environment {
        DOTNET_VERSION = '6.0'
        DOCKER_REGISTRY = '' //  registro Docker privado
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build .NET') {
            steps {
                script {
                    docker.image("mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}").inside {
                        sh 'dotnet restore'
                        sh 'dotnet build --no-restore --configuration Release'
                    }
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    docker.image("mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}").inside {
                        sh 'dotnet test --no-build --verbosity normal'
                    }
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    docker.build("chatapi:${env.BUILD_ID}", "--build-arg BUILD_CONFIGURATION=Release .")
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    //Para desarrollo, usamos docker-compose
                    //sh 'docker-compose up -d --build chatapi'
                    
                    // Para producción 
                     docker.withRegistry(...) { ... }
                }
            }
        }
    }

    post {
        always {
            echo 'Pipeline completado. Limpieza...'
            // Limpiar contenedores temporales
            sh 'docker system prune -f'
        }
        success {
            echo '¡Despliegue exitoso!'
            // Notificaciones opcionales (Slack, email, etc.)
        }
        failure {
            echo 'Pipeline falló. Revisar logs.'
        }
    }
}