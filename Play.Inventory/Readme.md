# Informacion general

Este servicio expone los endpoints para la gestion del inventario de un usuario. 

# Pasos para la configuracion del servicio.

##### Configuracion archivo appsettings:

1. **MongoDbSettings** - Configuracion de la conexion de base de datos.
- **Host:** Host donde esta hospedado el cluster para los contenedores de Docker.
- **Port:** Puerto del Host donde esta hospedado el cluster para los contenedores de Docker

2. **ServiceSettings** - Configuracion del servicio.
- **ServiceName:** Nombre del servicio este funciona tambien como identificador para el registro de la aplicacion RabbitMq y el nombre de la base de datos que creara la informacion que reciba el servicio.
- 

3. **RabbitMqSettings** - Configuracion de RabbitMQ
- **Host:** Host donde esta hospedado el servidor de RabbitMQ.