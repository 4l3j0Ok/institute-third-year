# Despliegue con docker compose de SQL Server

Este ejemplo contiene una definición de despliegue para SQL Server y el cliente web Cloud Beaver.

## Estructura

Consta de tres archivos:

```sh
.
├── compose.yml    # Archivo compose. Contiene la declaración de servicios y su configuración.
├── create-env.sh  # Script para crear el archivo .env necesario en base al .env.example
└── .env.example   # Plantilla de variables de entorno para el contenedor de SQL Server.
```

## Levantar el stack

Para levantar el stack, es necesario crear el archivo `.env` necesario. Este archivo contendrá las variables de entorno para el contenedor de SQL Server.  
`.env.example` es la plantilla base con las variables necesarias de acuerdo a la documentación de la imagen para poder levantar el contenedor.

Se puede utilizar el archivo [`create-env.sh`](./create-env.sh) para crearlo automaticamente con valores aleatorios. Para ejecutar el script, ejecutarlo con bash:

```sh
bash ./create-env.sh
```

Si también desea, puede crear el archivo `.env` a mano y ponerle las variables manualmente en base a la plantilla.

Una vez creado el archivo `.env`, se puede levantar el contenedor con `docker compose`:

```sh
docker compose up -d
```

## Detener el servicio

Se puede detener el stack con el siguiente comando:

```sh
docker compose down
```

## Monitoreo y resolución de errores

Se pueden ver los logs de los servicios con el siguiente comando:

```sh
docker compose logs -f
```
> `CTRL` + `C` para detenerlo.

## Accesos

SQL Server se levanta en el puerto `1433`, se puede modificar el puerto modificando el archivo [`compose.yml`](./compose.yml`) y es solo accesible mediante un cliente de SQL Server.

Cloud Beaver se levanta en el puerto `8978`, como es una aplicación web, se puede acceder mediante el navegador mediante http://localhost:8978 o http://127.0.0.1:8978
