#!/bin/bash

# Solicitar datos al usuario
read -p "Ingrese el nombre del servidor (HOSTNAME): " HOSTNAME
read -p "Ingrese el nombre de usuario: " USER
read -sp "Ingrese la contraseña: " PASSWORD
echo

# Crear copia del template para no modificar el original
cp binnacle.template.md binnacle.md

# Reemplazar las variables en el archivo
sed -i "s/\${HOSTNAME}/${HOSTNAME}/g" binnacle.md
sed -i "s/\${USER}/${USER}/g" binnacle.md
sed -i "s/\${PASSWORD}/${PASSWORD}/g" binnacle.md

echo "Archivo binnacle.md generado correctamente."
