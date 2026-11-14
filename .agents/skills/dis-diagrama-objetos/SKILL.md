---
name: dis-diagrama-objetos
description: Crear o corregir el Diagrama de Objetos del Cajero Automático en diagrams/09_objetos.puml, que muestra instancias con el nombre subrayado, sus valores y sus vínculos en un momento de la ejecución. Usar cuando la consigna pida que los nombres de los objetos vayan subrayados, cuando se hable de instancias, objetos, vínculos o diagrama de objetos, o para completar el ítem 5 sin pelear con PlantUML.
---

# Diagrama de Objetos

Complementa el ítem 5 de la consigna («los nombres de los OBJETOS van subrayados»). PlantUML no subraya nada en un diagrama de clases; en un diagrama de objetos el subrayado se pide con `<u>…</u>` (la versión 1.2024.3 no lo aplica sola). El programa de la materia incluye este diagrama (Toledo 2025, Unidad 3). Lee primero `dis-convenciones` y haz antes `dis-diagrama-clases`.

## Fuentes
- Debrauwer cap. 6 «Los objetos o instancias»: instancia en rectángulo con nombre subrayado `nombreInstancia : nombreClase`, valores `atributo = valor`, vínculos como líneas continuas.
- Guía U3 §3.1 (objeto = instancia con valores concretos) y §3.3.

## Reglas
1. **Un escenario concreto**: una sesión a mitad de ejecución, por ejemplo tras autenticar y extraer. Es una foto, no una definición.
2. Nombre de instancia en **camelCase** + `: Clase` con una clase del diagrama de clases (`sesion1 : Sesion`), siempre **subrayado** con `<u>…</u>` en el nombre del `object`.
3. Atributos como `_nombreAtributo = valor`, con los mismos nombres que en el diagrama de clases y el código, y valores coherentes con las reglas del negocio (PIN de 4 dígitos, número de tarjeta de 16, CBU de 22).
4. Cada vínculo corresponde a una asociación del diagrama de clases. Nombre del rol o de la asociación sin subrayar; se puede omitir.
5. Incluir un objeto de cada rama de la herencia que se use (`extraccion1 : Extraccion`), no clases abstractas (`Operacion` no se instancia).

## Sintaxis
```
@startuml 09_objetos
title Diagrama de Objetos - Sesión con una extracción
object "<u>sesion1 : Sesion</u>" as sesion1 {
  _estado = Operando
}
object "tarjeta1 : Tarjeta" as tarjeta1 {
  _numero = "1234567890123456"
}
sesion1 --> tarjeta1 : usa
@enduml
```

## Errores frecuentes
- Instanciar la clase abstracta `Operacion`.
- Valores incompatibles con el código (estado `Abierta` que no existe en `EstadoSesion`).
- Vínculos que no existen en el diagrama de clases.
- Nombres de objeto en PascalCase (son instancias: `sesion1`, no `Sesion1`).

## Verificación
Renderizar y comprobar en el PNG que los nombres salen subrayados. Cotejar atributos y vínculos con `04_clases.puml`.
