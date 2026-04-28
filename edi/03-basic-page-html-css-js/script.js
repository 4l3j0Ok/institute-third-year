/*
 * ACTIVIDAD: Mi Perfil Web
 * Archivo: script.js
 *
 * Bibliografía: Ortúñez, M.A. (2025). Programación Web:
 *               HTML, CSS, JavaScript. URJC.
 *               Cap. 4: JavaScript
 *               Cap. 5: DOM – Document Object Model
 *
 * Conceptos cubiertos en este archivo:
 *   - Variables con let y const (Cap. 4.9)
 *   - Funciones (Cap. 4 – Introducción)
 *   - Sentencias de control: if / else (Cap. 4.10)
 *   - DOM: getElementById, classList, innerHTML (Cap. 5.1 y 5.3)
 *   - Eventos: onclick desde HTML y addEventListener (Cap. 5.2)
 *   - Tipos de datos: number, string, boolean (Cap. 4.9)
 *   - console.log para depuración (Cap. 4.2)
 */


/* ============================================================
   SECCIÓN A: CONTADOR INTERACTIVO
   Conceptos: variables, funciones, if/else, DOM
   Ref.: Cap. 4.9 (variables), Cap. 4.10 (control),
         Cap. 5.1 (getElementById), Cap. 5.3 (innerHTML)
   ============================================================ */

/*
 * let → variable que puede cambiar de valor (Cap. 4.9)
 * const → variable que NO cambia (constante)
 *
 * Usamos let porque el contador va a modificarse.
 * El valor inicial es 0 (tipo number – Cap. 4.9).
 */
let valorContador = 0;

/*
 * Función sumar()
 * Se llama desde el botón "+ Sumar" del HTML (onclick="sumar()")
 * Ref.: Cap. 5.2 – Eventos
 */
function sumar() {
    valorContador++;   // equivale a: valorContador = valorContador + 1

    // Mostramos en consola para depuración (Cap. 4.2)
    console.log("Contador aumentó a:", valorContador);

    actualizarContador();
}

/*
 * Función restar()
 * Se llama desde el botón "− Restar" del HTML
 */
function restar() {
    valorContador--;   // equivale a: valorContador = valorContador - 1
    console.log("Contador disminuyó a:", valorContador);
    actualizarContador();
}

/*
 * Función reiniciar()
 * Vuelve el contador a cero
 */
function reiniciar() {
    valorContador = 0;
    console.log("Contador reiniciado a 0");
    actualizarContador();
}

/*
 * Función actualizarContador()
 * Función auxiliar que centraliza la actualización del DOM.
 * Es buena práctica no duplicar código: la llaman sumar(), restar() y reiniciar().
 *
 * DOM – getElementById:
 *   document.getElementById("id") devuelve el elemento HTML con ese id.
 *   Ref.: Cap. 5.1 – Introducción al DOM
 *
 * innerHTML:
 *   Cambia el contenido HTML de un elemento.
 *   Ref.: Cap. 5.3 – Contenido de un elemento
 *
 * classList:
 *   Permite agregar, quitar o verificar clases CSS de un elemento.
 *   Ref.: Cap. 5.1
 */
function actualizarContador() {
    // 1. Obtenemos el elemento del DOM por su id
    const elementoValor   = document.getElementById("valor-contador");
    const elementoMensaje = document.getElementById("mensaje-contador");

    // 2. Actualizamos el texto visible (innerHTML)
    elementoValor.innerHTML = valorContador;

    // 3. Removemos las clases de color antes de aplicar una nueva
    elementoValor.classList.remove("contador-positivo", "contador-negativo");

    // 4. Sentencia if / else if / else (Cap. 4.10)
    //    Cambiamos el color y el mensaje según el valor
    if (valorContador > 0) {
        // Valor positivo: verde
        elementoValor.classList.add("contador-positivo");
        elementoMensaje.innerHTML = "¡El contador está en positivo! 🟢";
        elementoMensaje.style.color = "#1E5631";

    } else if (valorContador < 0) {
        // Valor negativo: rojo
        elementoValor.classList.add("contador-negativo");
        elementoMensaje.innerHTML = "El contador está en negativo. 🔴";
        elementoMensaje.style.color = "#C00000";

    } else {
        // Valor igual a cero: neutro
        elementoMensaje.innerHTML = "El contador está en cero. ⚪";
        elementoMensaje.style.color = "#666666";
    }
}


/* ============================================================
   SECCIÓN B: FORMULARIO DE CONTACTO
   Conceptos: eventos, lectura de valores, manipulación DOM
   Ref.: Cap. 1.7 (formularios HTML),
         Cap. 5.2 (eventos – onsubmit),
         Cap. 5.3 (innerHTML, value)
   ============================================================ */

/*
 * Función procesarFormulario(event)
 * Se llama cuando el usuario envía el formulario (onsubmit).
 *
 * Parámetro event:
 *   El navegador pasa automáticamente el objeto del evento.
 *   event.preventDefault() evita que el formulario recargue la página.
 *   Ref.: Cap. 5.2 – Eventos
 */
function procesarFormulario(event) {
    // Prevenimos el comportamiento por defecto del formulario (recargar la página)
    event.preventDefault();

    // 1. Leemos los valores de los campos con .value (Cap. 5.3)
    const nombre  = document.getElementById("campo-nombre").value;
    const email   = document.getElementById("campo-email").value;
    const mensaje = document.getElementById("campo-mensaje").value;

    // 2. Mostramos los datos en la consola (muy útil para depurar)
    console.log("Formulario enviado:");
    console.log("  Nombre:", nombre);
    console.log("  Email:", email);
    console.log("  Mensaje:", mensaje);

    // 3. Validación básica con if (Cap. 4.10)
    //    Aunque el atributo 'required' del HTML ya valida,
    //    mostramos cómo hacerlo también desde JavaScript.
    if (nombre === "" || email === "" || mensaje === "") {
        alert("Por favor, completá todos los campos.");
        return;   // salimos de la función sin continuar
    }

    // 4. Armamos el texto de confirmación usando template literals
    //    Template literal: texto entre backticks `` con ${variable} (Cap. 4.11)
    const textoConfirmacion = `
        ¡Gracias, ${nombre}! 🎉
        Tu mensaje fue recibido correctamente.
        Te responderemos a ${email} a la brevedad.
    `;

    // 5. Mostramos la confirmación en el DOM
    const zonaConfirmacion = document.getElementById("confirmacion");
    zonaConfirmacion.innerHTML = textoConfirmacion;

    // Quitamos la clase 'oculto' para que el div sea visible (Cap. 5.1)
    zonaConfirmacion.classList.remove("oculto");

    // 6. Limpiamos el formulario
    document.getElementById("formulario-contacto").reset();

    // 7. Scroll suave hacia la confirmación
    zonaConfirmacion.scrollIntoView({ behavior: "smooth" });
}


/* ============================================================
   SECCIÓN C: SALUDO DINÁMICO EN EL ENCABEZADO
   Conceptos: Date, cadenas de texto, DOM
   Ref.: Cap. 4.9 (tipos), Cap. 5.1 (DOM), Cap. 5.3 (innerHTML)
         Cap. 6.2 – Fecha y Hora en JavaScript
   ============================================================ */

/*
 * Función saludarSegunHorario()
 * Lee la hora actual y cambia el subtítulo del encabezado
 * con un saludo acorde al momento del día.
 *
 * new Date() crea un objeto con la fecha y hora actuales.
 * .getHours() devuelve la hora (0 a 23) como número.
 * Ref.: Cap. 6.2 – Fecha y Hora en JavaScript
 */
function saludarSegunHorario() {
    const ahora = new Date();
    const hora  = ahora.getHours();   // número entre 0 y 23

    // Variable que guardará el saludo (tipo string – Cap. 4.9)
    let saludo;

    // Cadena de if / else if para determinar el saludo (Cap. 4.10)
    if (hora >= 6 && hora < 12) {
        saludo = "☀️ ¡Buenos días! Bienvenido/a a mi perfil.";
    } else if (hora >= 12 && hora < 18) {
        saludo = "🌤️ ¡Buenas tardes! Bienvenido/a a mi perfil.";
    } else if (hora >= 18 && hora < 22) {
        saludo = "🌆 ¡Buenas noches! Bienvenido/a a mi perfil.";
    } else {
        saludo = "🌙 Trabajando de madrugada... ¡Admirable! Bienvenido/a.";
    }

    // Actualizamos el subtítulo del encabezado en el DOM
    const elementoSubtitulo = document.querySelector(".subtitulo");
    if (elementoSubtitulo) {
        elementoSubtitulo.innerHTML = saludo;
    }
}


/* ============================================================
   SECCIÓN D: EVENTO DOMContentLoaded
   Concepto: addEventListener – ejecutar código cuando la página cargó
   Ref.: Cap. 5.2 – Eventos
   ============================================================ */

/*
 * addEventListener("DOMContentLoaded", función)
 * Espera a que el HTML esté completamente cargado antes de
 * ejecutar el código. Es la forma correcta de inicializar JS.
 *
 * La función flecha (arrow function) () => { } es una forma
 * moderna y concisa de escribir funciones anónimas.
 * Ref.: Cap. 4 – Introducción a JavaScript (ECMAScript 6)
 */
document.addEventListener("DOMContentLoaded", () => {
    console.log("📄 La página cargó correctamente.");
    console.log("🕐 Hora actual:", new Date().toLocaleTimeString("es-AR"));

    // Llamamos a la función de saludo al cargar la página
    saludarSegunHorario();

    // TODO 9 – DESAFÍO JAVASCRIPT:
    // Agregá aquí un addEventListener sobre el elemento con id "nombre-principal"
    // para que al hacer clic sobre tu nombre, aparezca un alert()
    // con el texto: "Hola, soy [tu nombre]!"
    //
    // Pista:
    // const elem = document.getElementById("nombre-principal");
    // elem.addEventListener("click", () => {
    //     alert("¡Hola! Soy ...");
    // });
    const elemNombre = document.getElementById("nombre-principal");
    const nombre = elemNombre ? elemNombre.textContent : "Tu Nombre";
    if (elemNombre) {
        elemNombre.addEventListener("click", () => {
            alert(`¡Hola! Soy ${nombre}!`);
        });
    }

    // TODO 10 – DESAFÍO JAVASCRIPT:
    // Creá una función contarCaracteres() que lea el valor de
    // #campo-mensaje y muestre en la consola cuántos caracteres tiene.
    // Vincular al evento "input" del textarea.
    //
    // Pista:
    // document.getElementById("campo-mensaje").addEventListener("input", () => {
    //     const texto = document.getElementById("campo-mensaje").value;
    //     console.log("Caracteres escritos:", texto.length);
    // });
    const campoMensaje = document.getElementById("campo-mensaje");
    if (campoMensaje) {
        campoMensaje.addEventListener("input", () => {
            const texto = campoMensaje.value;
            console.log("Caracteres escritos:", texto.length);
        });
    }
});
