#!/bin/bash
read -p "Ingresa la contraseña para la base de datos: " password
cp ./.env.example ./.env
sed -i "s/SA_PASSWORD.*/SA_PASSWORD=$password/" ./.env
echo "Archivo .env creado y contraseña actualizada."
