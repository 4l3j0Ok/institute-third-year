
<div style="display: flex; justify-content: center; align-items: center; flex-direction: column; text-align: center;">
<img src="./logo.png" alt="Logo" width="500"/>

# Programación Orientada a Objetos
### Prime Systems: Notas de revisión de programas
</div>


<div style="page-break-after: always;"></div>

---
> **Profesor**: Leandro Pini  
> **Ayudante de cátedra**: Alejo Sarmiento  
> **Fecha de la clase virtual**: 26/06/2026  
---

## Introducción
Este documento presenta un resumen de los puntos más comunes que se notaron durante la revisión de los programas presentados por los alumnos. Se busca que estas notas sirvan como guía para la correcta implementación del sistema solicitado, evitando errores comunes y promoviendo buenas prácticas de programación.

## Baja lógica

Al eliminar un elemento del programa, la baja debe ser lógica: no se debe borrar físicamente de la base de datos, sino marcarlo como eliminado. Así se conserva el historial y se facilita su recuperación si fuera necesario.

### ¿Por qué?

Imaginemos que tenemos dos tablas: `Vendedor` y `Ventas`, donde cada venta es realizada por un vendedor.
El flujo de una venta es el siguiente:
1. Se da de alta un vendedor. Queda con id 1.
2. El vendedor se loguea a la aplicación y realiza una venta. La venta queda registrada con id 1 y el id del vendedor es 1.
3. La venta queda registrada en la tabla `Ventas` con el id del vendedor que la realizó.

Luego, el vendedor es eliminado del sistema. Si la eliminación es física, el registro de la venta quedaría huérfano, ya que el vendedor con id 1 ya no existe. Esto puede generar inconsistencias en la base de datos y problemas al momento de consultar las ventas realizadas por un vendedor eliminado.

<div style="page-break-after: always;"></div>

#### Solución

La solución a este problema es implementar una baja lógica, es decir, agregar un campo en la tabla `Vendedor` que indique si el vendedor está vigente. Nombres sugeridos para este campo podrían ser `activo`, `habilitado`, `vigente`, etc. Este campo puede ser de tipo booleano, donde `true` indica que el vendedor está activo y `false` indica que está eliminado.

> [!NOTE]
> Recordar que a la base de datos proporcionada por la cátedra se le pueden cambiar los campos sugeridos, adaptándolos a la lógica de la aplicación, pero no se pueden eliminar las tablas.

En la aplicación, al momento de eliminar el vendedor, en lugar de ejecutar un `DELETE`, se debe ejecutar un `UPDATE` para cambiar el valor del campo `activo` a `false`. De esta manera, el registro del vendedor sigue existiendo en la base de datos, pero se considera eliminado desde la perspectiva de la aplicación.

Al hacer el `SELECT` de los vendedores, filtramos por los activos, es decir, aquellos cuyo campo `activo` sea `true`. Esto permite mantener la integridad de los datos y evitar inconsistencias en la base de datos.

> [!TIP]
> En SQL Server, el tipo de dato booleano no existe, por lo que se puede utilizar un tipo de dato `BIT`, donde `1` representa `true` y `0` representa `false`.

<div style="page-break-after: always;"></div>

## Módulo de Recursos Humanos

Se notó confusión con la implementación de este módulo y en qué se diferencia del módulo de clientes, proveedores y usuarios, por lo que cabe aclarar la diferencia entre ellos y cómo se relacionan.

### Usuarios

Este módulo gestiona los usuarios del sistema, es decir, los empleados que utilizan la aplicación. Cada usuario tiene un perfil que define sus permisos y funcionalidades dentro del sistema.

### Clientes

Este módulo gestiona la información de los clientes de la empresa/negocio a los que se le vende los artículos. Cada cliente tiene un perfil que define sus datos de contacto, historial de compras o cuenta corriente.

### Proveedores

Este módulo gestiona la información de los proveedores de la empresa/negocio a los que el negocio le compra los artículos. Nuevamente, cada proveedor tiene un perfil que define sus datos de contacto, historial de ventas o cuenta corriente.

### Opciones de implementación

Las siguientes son algunas opciones de implementación del módulo de Recursos Humanos y su relación con los módulos de clientes, proveedores y usuarios:

1. Recursos Humanos es un módulo. Clientes, proveedores y usuarios son **submódulos** de Recursos Humanos, siendo Recursos Humanos el módulo principal.
2. Recursos Humanos es un módulo, al igual que clientes, proveedores y usuarios. 
  Cada uno de estos módulos tiene su propia funcionalidad y no dependen entre sí. Recursos Humanos, a diferencia de usuarios, puede gestionar empleados de la empresa/negocio que no necesariamente son usuarios del sistema, como por ejemplo, un empleado que trabaja en el área de logística y no tiene acceso a la aplicación.
3. Recursos Humanos no existe. Clientes, proveedores y usuarios son módulos independientes que gestionan la información de cada uno de estos actores por separado.

Obviamente otras opciones de implementación son posibles, pero estas son las recomendadas por nuestra parte.

---

Cualquier duda es bienvenida y puede ser consultada con el ayudante de cátedra o el profesor.

Saludos.
