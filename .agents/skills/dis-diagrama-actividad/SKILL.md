---
name: dis-diagrama-actividad
description: Crear, revisar o corregir el Diagrama de Actividad del SISTEMA completo del Cajero Automático en diagrams/08_actividad_sistema.puml (validación de tarjeta, PIN con contador de intentos, selección de operación, ticket y cierre), con calles, guardas entre corchetes y nodos de fusión. Usar cuando se hable de diagrama de actividad, flujo de control, bifurcación, bucle, calles (swimlanes), guardas o de los rombos sin texto.
---

# Diagrama de Actividad del sistema

Ítem 9 de la consigna. Lee primero `dis-convenciones`. Modelo del docente: `ejemploDiagramaActividad.html` (CU «Operar Cajero»).

## Fuentes
- `Guia_DiagramaActividad_DIS_2026.pdf` (notación §3, errores §8, checklist §9), `ejemploDiagramaActividad.html`, `resources/CU_*.png`, Debrauwer cap. 9, EIDOS cap. 6 pp. 72-78, consigna ítem 9.

## Reglas
1. **Un nodo inicial** y al menos un nodo final. Es el flujo de todo el sistema: tarjeta → PIN → operaciones → ticket → devolución, no solo «Operar Cajero».
2. **Actividades** con verbo en infinitivo + objeto específico, esquinas redondeadas: `Validar tarjeta`, no `Tarjeta` ni `Procesar`. Cada una con al menos una entrada y una salida.
3. **Calles (swimlanes)**, como el ejemplo del docente: `Cliente`, `Cajero`, `Sucursal`, `Red Central`. Cada actividad pertenece a una sola calle; las transiciones cruzan calles.
4. **Bifurcación** (rombo): la pregunta va en el rombo y las guardas **entre corchetes** sobre las flechas de salida: `¿Tarjeta válida?` → `[sí]` / `[no]`. Guardas **excluyentes y exhaustivas**; usar `else` para el resto.
5. **Rombos sin texto** = **fusión de alternativa** (Debrauwer cap. 9): reúnen ramas tras una bifurcación o abren un bucle. Son correctos. PlantUML los pone en cada `endif`; incluir una leyenda que los explique.
6. **PIN con contador de intentos**: inicializar el contador, pedir y validar el PIN, incrementar al fallar y repetir mientras `[PIN incorrecto e intentos < 3]`. Salen **como máximo 3 intentos** (incrementar antes de evaluar la condición, no después). Al tercer fallo: bloquear tarjeta y avisar.
7. **Selección de operación** con `switch`: Consultar saldo, Extraer efectivo, Transferir, según cada `CU_*.png`; después `¿Realizar otra operación?` con bucle.
8. **Transferencia**: bucle de validación del CBU (22 dígitos) antes de pedir el monto; resumen y confirmación; ejecución.
9. Pregunta del ticket **solo si hubo sesión** (CU Emitir Ticket); tarjeta inválida o PIN bloqueado van directo a `Devolver tarjeta`. Al final: `Cerrar sesión` borra los datos de autenticación.
10. Fork/join (barra) solo si hay actividades realmente concurrentes. No inventarlas.
11. Los bucles muestran su condición de salida.

## Sintaxis (PlantUML, calles)
```
@startuml 08_actividad_sistema
|Cliente|
start
:Insertar tarjeta;
|Cajero|
:Leer datos de la tarjeta;
if (¿Tarjeta válida?) then ([sí])
  repeat
    :Solicitar PIN;
  repeat while (¿PIN incorrecto e intentos < 3?) is ([sí]) not ([no])
else ([no])
  :Mostrar error de tarjeta;
endif
stop
@enduml
```
`backward:` dentro de `repeat` dibuja una actividad en el camino de retorno del bucle. El script de verificación exige que toda guarda de `then`, `else`, `is` y `not` esté entre corchetes.

## Errores frecuentes (Guía §8)
- Guardas no exhaustivas o solapadas.
- Actividad sin salida, o diagrama sin nodo inicial/final.
- Nombres vagos.
- Rombo para flujos paralelos o barra para alternativos.
- Contar los intentos del PIN mal (el bucle permite 4).

## Verificación
`python3 scripts/verificar_diagramas.py` (inicio, final, guardas con corchetes, calles). Mirar el PNG: se leen las calles y los rombos tienen su leyenda. Recorrer a mano los caminos: tarjeta inválida, 3 PIN erróneos, cada operación aceptada y rechazada, con y sin ticket.
