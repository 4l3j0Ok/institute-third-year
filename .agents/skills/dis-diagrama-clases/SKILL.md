---
name: dis-diagrama-clases
description: Crear, revisar o corregir el Diagrama de Clases del Cajero Automático en diagrams/04_clases.puml (8 clases con atributos y métodos tipados, herencia desde Operacion, composición y asociaciones con multiplicidad) y mantenerlo sincronizado con el código C# de src/Cajero/Logica. Usar cuando se hable de clases, atributos, métodos, visibilidad, multiplicidad, herencia, composición, asociación o de traducir UML a C#.
---

# Diagrama de Clases

Ítem 5 de la consigna. Lee primero `dis-convenciones`. El diagrama es la **especificación del código** (Guía U4 §4.1): si cambia uno, cambia el otro.

## Fuentes
- Debrauwer cap. 6 (singular, `+ - # ~`, tipos, firma, asociaciones, cardinalidad, navegación, composición). Guía U3 §3.3. Consigna ítem 5 e ítem 10.

## Las 8 clases (capa Lógica)
`Cliente`, `Tarjeta`, `Cuenta`, `Sesion`, `Operacion` (abstracta), `Consulta`, `Extraccion`, `Transferencia`. `SucursalDB`, `CentralDB` (capa Datos) y `FormCajero` (Presentación) **no** se dibujan: son los participantes `:SucursalDB`, `:CentralDB` y `:PantallaCajero` de las secuencias. Una nota lo aclara.

## Reglas
1. **Tres compartimentos**: nombre en singular y PascalCase, atributos, métodos. `Operacion` va como `abstract class` (nombre en cursiva).
2. **Atributo**: `visibilidad _camelCase : tipo`. Privado `-` con guión bajo (consigna ítem 10); protegido `#` en `Operacion`. Marcar `{readOnly}` si la propiedad no tiene `set`.
3. **Método**: `visibilidad Nombre(parametro : tipo) : retorno`. Constructores sin retorno: `+ Tarjeta(numero : string, vencimiento : DateTime)`. Método abstracto `{abstract}`.
4. **Tipos de C#**: `string`, `bool`, `int`, `decimal` (dinero), `DateTime`, `List<Operacion>`. Los mismos del código.
5. **Herencia** `Operacion <|-- Consulta / Extraccion / Transferencia`, con `Ejecutar()` redefinido en cada hija.
6. **Composición** `Sesion *-- Operacion` (las operaciones del log no existen sin la sesión).
7. **Multiplicidad en TODOS los extremos** de toda asociación y composición, con el sentido de lectura (`registra >`). Flecha de navegación solo donde el código tiene la referencia.
8. **Cada relación del diagrama existe en el código** (propiedad o campo) y cada referencia entre clases del código está en el diagrama.
9. **Propiedades públicas**: no se dibujan; cada atributo `_x` implica una propiedad `X` (regla de `dis-convenciones`).
10. Los nombres de **objetos subrayados** se resuelven con `dis-diagrama-objetos` (PlantUML no los dibuja en clases).

## Sintaxis
```
abstract class Operacion {
  # _monto : decimal
  # _fecha : DateTime
  + {abstract} Ejecutar() : bool
}
class Extraccion {
  - _montoRetirado : decimal
  + Extraccion(monto : decimal)
  + Ejecutar() : bool
}
Operacion <|-- Extraccion
Sesion "1" *-- "0..*" Operacion : registra >
Operacion "0..*" --> "1" Cuenta : afecta >
```
`skinparam classAttributeIconSize 0` para ver `+ - #` en lugar de íconos.

## Errores frecuentes
- Atributo sin tipo o método sin retorno.
- Relación con multiplicidad en un solo extremo, o sin multiplicidad.
- Nombres de atributo sin `_` o en snake_case.
- Dibujar getters y setters como métodos (en C# son propiedades).
- Clases en el diagrama que no existen en el código (o al revés).

## Verificación
`python3 scripts/verificar_diagramas.py` (8 clases, visibilidad, tipos, `_camelCase`, multiplicidad en ambos extremos). Luego comparar a mano con `src/Cajero/Logica/*.cs`.
