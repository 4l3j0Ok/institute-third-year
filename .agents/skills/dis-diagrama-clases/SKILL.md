---
name: dis-diagrama-clases
description: Crear, revisar o corregir el Diagrama de Clases del Cajero Automático en diagrams/04_clases.puml (8 clases con atributos y métodos tipados, herencia desde Operation, composición y asociaciones con multiplicidad) y mantenerlo sincronizado con el código C# de src/Atm/Logic. Usar cuando se hable de clases, atributos, métodos, visibilidad, multiplicidad, herencia, composición, asociación o de traducir UML a C#.
---

# Diagrama de Clases

Ítem 5 de la consigna. Lee primero `dis-convenciones`. El diagrama es la **especificación del código** (Guía U4 §4.1): si cambia uno, cambia el otro.

## Fuentes
- Debrauwer cap. 6 (singular, `+ - # ~`, tipos, firma, asociaciones, cardinalidad, navegación, composición). Guía U3 §3.3. Consigna ítem 5 e ítem 10.

## Las 8 clases (capa Lógica)
`Customer`, `Card`, `Account`, `Session`, `Operation` (abstracta), `BalanceInquiry`, `CashWithdrawal`, `Transfer` (equivalencias con los nombres de la consigna en `dis-convenciones/references/glosario.md`; el diagrama lleva una nota con la tabla). `BranchDB`, `CentralDB` (capa `Data`) y `AtmForm` (capa `Presentation`) **no** se dibujan: son los participantes `:BranchDB`, `:CentralDB` y `:AtmForm` de las secuencias. La misma nota lo aclara.

## Reglas
1. **Tres compartimentos**: nombre en singular y PascalCase, atributos, métodos. `Operation` va como `abstract class` (nombre en cursiva).
2. **Atributo**: `visibilidad _camelCase : tipo`. Privado `-` con guión bajo (consigna ítem 10); protegido `#` en `Operation`. Marcar `{readOnly}` si la propiedad no tiene `set`.
3. **Método**: `visibilidad Nombre(parametro : tipo) : retorno`. Constructores sin retorno: `+ Card(number : string, expirationDate : DateTime)`. Método abstracto `{abstract}`.
4. **Tipos de C#**: `string`, `bool`, `int`, `decimal` (dinero), `DateTime`, `List<Operation>`. Los mismos del código.
5. **Herencia** `Operation <|-- BalanceInquiry / CashWithdrawal / Transfer`, con `Execute()` redefinido en cada hija.
6. **Composición** `Session *-- Operation` (las operaciones del log no existen sin la sesión).
7. **Multiplicidad en TODOS los extremos** de toda asociación y composición, con el sentido de lectura (`registra >`). Flecha de navegación solo donde el código tiene la referencia.
8. **Cada relación del diagrama existe en el código** (propiedad o campo) y cada referencia entre clases del código está en el diagrama.
9. **Propiedades públicas**: no se dibujan; cada atributo `_x` implica una propiedad `X` (regla de `dis-convenciones`).
10. Los nombres de **objetos subrayados** se resuelven con `dis-diagrama-objetos` (PlantUML no los dibuja en clases).

## Sintaxis
```
abstract class Operation {
  # _amount : decimal
  # _fecha : DateTime
  + {abstract} Execute() : bool
}
class CashWithdrawal {
  + CashWithdrawal(central : CentralDB, card : Card, account : Account, amount : decimal)
  + Execute() : bool
}
Operation <|-- CashWithdrawal
Session "1" *-- "0..*" Operation : records >
Operation "0..*" --> "1" Account : affects >
```
`skinparam classAttributeIconSize 0` para ver `+ - #` en lugar de íconos.

## Errores frecuentes
- Atributo sin tipo o método sin retorno.
- Relación con multiplicidad en un solo extremo, o sin multiplicidad.
- Nombres de atributo sin `_`, en snake_case o en español.
- Dibujar getters y setters como métodos (en C# son propiedades).
- Clases en el diagrama que no existen en el código (o al revés).

## Verificación
`python3 scripts/verificar_diagramas.py` (8 clases, visibilidad, tipos, `_camelCase`, multiplicidad en ambos extremos). Luego comparar a mano con `src/Atm/Logic/*.cs`.
