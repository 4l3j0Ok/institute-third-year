# BIOS

- Se desactivo el secure boot.
- Se habilito el arranque 18:45.
- Se habilito el arranque cuando vuelve la energia.
- Se habilito el arranque con el teclado (Ctrl/Esc).
- Se habilito el arranque pciexpress.

> [!WARNING]
> Si la PC pierde energía, toda la configuración de la BIOS se pierde. Esto puede deberse a la pila de energía que se encarga de mantener la energía activa.


# Instalacion de Ubuntu Server

- Versión elegida Ubuntu Server 24.04
- Se configuro inicial mente sin RED (por consecuencia no se instalaron actualizaciones).
- Se selecciono el SSD como disco para el SO.
- Se activo LVM.
- En la instalación se habilito SSH.

# Configuración de Usuario Administrado

- Nombre del Servidor: `${HOSTNAME}`
- Nombre usuario: `${USER}`
- Contraseña: `${PASSWORD}`
