// Jenkinsfile para el proyecto de Chat con SignalR

pipeline {
    // Definimos que Jenkins puede usar cualquier agente disponible para ejecutar este pipeline
    agent any

    // Las 'stages' son las etapas lógicas de nuestro proceso de CI/CD
    stages {
        
        // Etapa 1: Construir las imágenes de Docker
        stage('Build') {
            steps {
                script {
                    echo 'Construyendo las imágenes de Docker con docker-compose...'
                    // Usamos docker-compose para construir las imágenes definidas en el yml.
                    // --no-cache asegura que se construya todo desde cero, ideal para un entorno de CI.
                    bat 'docker-compose build --no-cache'
                }
            }
        }
        
        // Etapa 2: Ejecutar Pruebas Unitarias
        stage('Unit Tests') {
            steps {
                script {
                    echo 'Ejecutando pruebas unitarias dentro del contenedor de la API...'
                    // Este comando inicia temporalmente el servicio 'chatapi', ejecuta 'dotnet test'
                    // y luego lo elimina gracias a --rm. Asume que tienes un proyecto de pruebas.
                    bat 'docker-compose run --rm chatapi dotnet test'
                }
            }
        }
        
        // Etapa 3: "Desplegar" la aplicación (levantar los servicios)
        stage('Deploy to Staging') {
            steps {
                script {
                    echo 'Levantando la aplicación completa en modo detached...'
                    // -d (detached) hace que los contenedores corran en segundo plano.
                    bat 'docker-compose up -d'
                    
                    echo 'La aplicación debería estar disponible en los puertos configurados.'
                    // Se podría agregar un paso de validación aquí (ej. curl a la API)
                }
            }
        }
    }
    
    // El bloque 'post' define acciones que se ejecutan después de que el pipeline termina
    post {
        // 'always' se ejecuta siempre, haya fallado o no el pipeline
        always {
            script {
                echo 'Limpiando el entorno. Deteniendo y eliminando contenedores...'
                // Es una buena práctica dejar el entorno limpio después de cada ejecución.
                bat 'docker-compose down'
            }
        }
    }
}