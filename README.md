# n5Changelle
Challenge Net 7 - ReactJs
# Configuración de `appsettings.json`

El archivo `appsettings.json` es esencial para la configuración de la aplicación. Aquí se describen los pasos para configurar las secciones relacionadas con Elasticsearch y Kafka.

## Elasticsearch

En la sección `"ElasticSearch"` del archivo `appsettings.json`, puedes configurar la conexión a Elasticsearch. Los valores clave a configurar son:

- `"EndPoint"`: Esta propiedad define la dirección y el puerto del servidor de Elasticsearch. Por defecto, se establece en `"http://localhost:9200"`. Asegúrate de especificar la dirección correcta de tu servidor Elasticsearch si no se ejecuta en `localhost`.

- `"Index"`: Define el nombre del índice de Elasticsearch en el que se registrarán los eventos de permisos. Por defecto, se establece en `"permissions_index"`. Puedes cambiar este valor según tus necesidades.

Ejemplo:

```json
"ElasticSearch": {
    "EndPoint": "http://localhost:9200",
    "Index": "permissions_index"
}
```
## Kafka

La sección `"Kafka"` se utiliza para configurar la conexión a un clúster de Kafka. Aquí están los valores clave a configurar:

- `"BootstrapServers"`: Esta propiedad define las direcciones y puertos de los servidores de Kafka. Por defecto, se establece en `"localhost:9092"`. 

- `"ClientId"`: Puedes definir un identificador de cliente que se utilizará para identificar esta aplicación en el clúster de Kafka. Puedes cambiar este valor según tus necesidades.

- `"Topic"`: Define el nombre del tema de Kafka al que se suscribirán los eventos de permisos. Asegúrate de que coincida con el nombre del tema correcto en tu clúster de Kafka.

Ejemplo:

```json
"Kafka": {
    "BootstrapServers": "localhost:9092",
    "ClientId": "n5.Permissions",
    "Topic": "permissions-topic"
}
```
## Configuración de `appsettings` para Pruebas de Integración

El proyecto de pruebas de integración (n5.permissions.IntegrationTest) de nuestra aplicación requiere una configuración específica para ejecutar pruebas que interactúen con los componentes del sistema. Para lograr esto, debe configurar la sección ConnectionStrings:
```json
"ConnectionStrings": {
    "Default": "Server=localhost; Database=PermissionTestDb ;User ID=sa;Password=sa;TrustServerCertificate=True"
  }
```
## Configuración de FrontEnd
En el proyecto ReactJs se debe configurar el API en la ruta src/api. La configuración se realiza con la url Base.El ejemplo siguiente:

```
 baseURL:'https://localhost:44340/api'
```
