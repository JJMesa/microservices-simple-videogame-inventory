# Informacion general

Este proyecto es pequeño set APIs con .Net 8 y arquitectura de microservicios, pensada para la gestion de un muy pequeño inventario de un videojuego o cualquier otro caso en que pueda aplicarse (Basada en el tutorial de FreeCodeCamp 'https://www.youtube.com/watch?v=CqCDOosvZIk&t=759s&ab_channel=freeCodeCamp.org').

# Guia de directorios.

1. **packages** - Directorio donde se encuentran el NuGet's con la logica comun de los servicios.

- **Play.Catalog** - Servicio para el control de los items.

- **Play.Common** - Servicio que contiene toda la logica comun de los servicios (Extensiones para implementación de MassTransit, Mongo).

- **Play.Infra** - Carpeta que aloja el docker-compose para el levantamiento de las imagenes Mongo y RabbitMQ en Docker.

- **Play.Inventory** - Servicio para el control del inventario de personajes.
