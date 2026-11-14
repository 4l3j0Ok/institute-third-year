using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cajero.Logica;

namespace Cajero.Presentacion;

public partial class FormCajero : Form
{
    private const int CantidadOpciones = 8;

    private static readonly Color s_colorBotonLateralActivo = Color.FromArgb(0xD9, 0xDD, 0xE4);
    private static readonly Color s_colorBotonInactivo = Color.FromArgb(0x6A, 0x70, 0x7B);
    private static readonly Color s_colorBotonTarjetaActivo = Color.FromArgb(0x2A, 0x63, 0xB8);

    private static readonly TarjetaDemo[] s_tarjetasDemo =
    [
        new("Tarjeta válida (PIN 1234)", "1234567890123456", new DateTime(2030, 12, 31)),
        new("Tarjeta bloqueada", "1111222233334444", new DateTime(2030, 12, 31)),
        new("Tarjeta vencida", "5555666677778888", new DateTime(2020, 1, 31)),
    ];

    private readonly Button[] _botonesLaterales;
    private readonly Label[] _etiquetasLaterales;
    private readonly Action?[] _accionesLaterales = new Action?[CantidadOpciones];

    private Sesion _sesion = new();
    private Entrada? _entrada;
    private string _cbuDestino = "";
    private decimal _montoTransferencia;

    public FormCajero()
    {
        InitializeComponent();

        _botonesLaterales = [btnLateral0, btnLateral1, btnLateral2, btnLateral3, btnLateral4, btnLateral5, btnLateral6, btnLateral7];
        _etiquetasLaterales = [lblEtiqueta0, lblEtiqueta1, lblEtiqueta2, lblEtiqueta3, lblEtiqueta4, lblEtiqueta5, lblEtiqueta6, lblEtiqueta7];

        cmbTarjeta.Items.AddRange(s_tarjetasDemo);
        cmbTarjeta.SelectedIndex = 0;

        MostrarInicio();
    }

    private record TarjetaDemo(string Texto, string Numero, DateTime Vencimiento)
    {
        public override string ToString()
        {
            return Texto;
        }
    }

    private record Opcion(string Texto, Action Accion);

    private record Entrada(string Titulo, int Largo, Action<string> AlAceptar, Action AlCancelar, bool EsOculta = false, bool EsDinero = false)
    {
        public string Texto { get; set; } = "";
    }

    #region Pantalla

    private void MostrarPantalla(string mensaje, Opcion?[]? opciones = null, Entrada? entrada = null)
    {
        _entrada = entrada;
        lblMensaje.Text = mensaje;
        for (var indice = 0; indice < CantidadOpciones; indice++)
        {
            var opcion = opciones is not null && indice < opciones.Length ? opciones[indice] : null;
            _accionesLaterales[indice] = opcion?.Accion;
            _botonesLaterales[indice].Enabled = opcion is not null;
            _botonesLaterales[indice].BackColor = opcion is not null ? s_colorBotonLateralActivo : s_colorBotonInactivo;
            _etiquetasLaterales[indice].Text = opcion is null ? "" : indice < 4 ? "◄ " + opcion.Texto : opcion.Texto + " ►";
        }
    }

    private void RefrescarEntrada()
    {
        var entrada = _entrada!;
        var visible = entrada.EsOculta ? new string('●', entrada.Texto.Length) : entrada.Texto;
        if (entrada.EsDinero && entrada.Texto.Length > 0)
        {
            visible = Formato.Dinero(decimal.Parse(entrada.Texto));
        }
        lblMensaje.Text = $"{entrada.Titulo}\n\n{(visible.Length == 0 ? "_" : visible)}";
    }

    private void SolicitarEntrada(string titulo, int largo, Action<string> alAceptar, Action alCancelar, bool esOculta = false, bool esDinero = false)
    {
        MostrarPantalla("", entrada: new Entrada(titulo, largo, alAceptar, alCancelar, esOculta, esDinero));
        RefrescarEntrada();
    }

    private void ActualizarBotonTarjeta(string texto, bool habilitado)
    {
        btnTarjeta.Text = texto;
        btnTarjeta.Enabled = habilitado;
        btnTarjeta.BackColor = habilitado ? s_colorBotonTarjetaActivo : s_colorBotonInactivo;
    }

    #endregion

    #region Eventos de la interfaz

    private void AlPresionarBotonLateral(object? sender, EventArgs e)
    {
        var indice = int.Parse((string)((Control)sender!).Tag!);
        _accionesLaterales[indice]?.Invoke();
    }

    private void AlPresionarDigito(object? sender, EventArgs e)
    {
        if (_entrada is { } entrada && entrada.Texto.Length < entrada.Largo)
        {
            entrada.Texto += ((Button)sender!).Text;
            RefrescarEntrada();
        }
    }

    private void AlPresionarCorregir(object? sender, EventArgs e)
    {
        if (_entrada is { } entrada)
        {
            entrada.Texto = "";
            RefrescarEntrada();
        }
    }

    private void AlPresionarAceptar(object? sender, EventArgs e)
    {
        _entrada?.AlAceptar(_entrada.Texto);
    }

    private void AlPresionarCancelar(object? sender, EventArgs e)
    {
        if (_entrada is { } entrada)
        {
            entrada.AlCancelar();
        }
        else if (_sesion.Estado == EstadoSesion.Operando)
        {
            PreguntarTicket();
        }
    }

    private void AlPresionarTarjeta(object? sender, EventArgs e)
    {
        if (_sesion.Estado != EstadoSesion.Iniciada)
        {
            MostrarInicio();
            return;
        }
        var tarjeta = (TarjetaDemo)cmbTarjeta.SelectedItem!;
        cmbTarjeta.Enabled = false;
        ActualizarBotonTarjeta("Retirar tarjeta", habilitado: false);
        if (_sesion.IngresarTarjeta(tarjeta.Numero, tarjeta.Vencimiento))
        {
            SolicitarPin();
        }
        else
        {
            FinalizarSesion(_sesion.Mensaje);
        }
    }

    #endregion

    #region Flujo

    private void MostrarInicio()
    {
        _sesion = new Sesion();
        lblTicket.Text = "";
        lblEfectivo.Text = "";
        cmbTarjeta.Enabled = true;
        ActualizarBotonTarjeta("Insertar tarjeta", habilitado: true);
        MostrarPantalla("BIENVENIDO\n\nInserte su tarjeta");
    }

    private void SolicitarPin()
    {
        SolicitarEntrada("INGRESE SU PIN", 4, AutenticarPin, () => FinalizarSesion("Operación cancelada"), esOculta: true);
    }

    private void AutenticarPin(string pin)
    {
        if (pin.Length != 4)
        {
            return;
        }
        if (_sesion.Autenticar(pin))
        {
            MostrarMenu();
        }
        else if (_sesion.Estado == EstadoSesion.Cerrando)
        {
            FinalizarSesion(_sesion.Mensaje);
        }
        else
        {
            MostrarPantalla(_sesion.Mensaje, [new("Reintentar", SolicitarPin)]);
        }
    }

    private void MostrarMenu()
    {
        MostrarPantalla("SELECCIONE UNA OPERACIÓN",
        [
            new("Consultar saldo", ConsultarSaldo), new("Extraer efectivo", MostrarMenuExtraccion),
            null, null,
            new("Transferir", SolicitarCbuDestino), new("Finalizar", PreguntarTicket),
        ]);
    }

    private void MostrarResultado(bool exitosa, Action alReintentar)
    {
        if (exitosa)
        {
            MostrarPantalla(_sesion.Mensaje, [new("Otra operación", MostrarMenu), null, null, null, new("Finalizar", PreguntarTicket)]);
        }
        else
        {
            MostrarPantalla($"ERROR\n\n{_sesion.Mensaje}", [new("Reintentar", alReintentar), new("Menú", MostrarMenu)]);
        }
    }

    private void ConsultarSaldo()
    {
        MostrarResultado(_sesion.Ejecutar(_sesion.NuevaConsulta()), MostrarMenu);
    }

    private void MostrarMenuExtraccion()
    {
        decimal[] montos = [1000, 5000, 10000, 20000];
        var opciones = montos
            .Select(monto => new Opcion(Formato.Dinero(monto).Split(',')[0], () => ExtraerEfectivo(monto)))
            .Cast<Opcion?>()
            .ToList();
        opciones.AddRange([new Opcion("Otro monto", SolicitarMontoExtraccion), null, null, new Opcion("Volver", MostrarMenu)]);
        MostrarPantalla("EXTRAER EFECTIVO\n\nElija el monto", [.. opciones]);
    }

    private void SolicitarMontoExtraccion()
    {
        SolicitarEntrada(
            "MONTO A RETIRAR", 7,
            texto =>
            {
                if (texto.Length > 0)
                {
                    ExtraerEfectivo(decimal.Parse(texto));
                }
            },
            MostrarMenuExtraccion,
            esDinero: true);
    }

    private void ExtraerEfectivo(decimal monto)
    {
        var exitosa = _sesion.Ejecutar(_sesion.NuevaExtraccion(monto));
        if (exitosa)
        {
            lblEfectivo.Text = $"▼ {Formato.Dinero(monto)} ▼";
        }
        MostrarResultado(exitosa, MostrarMenuExtraccion);
    }

    private void SolicitarCbuDestino()
    {
        SolicitarEntrada("CBU DESTINO (22 dígitos)", 22, AlIngresarCbuDestino, MostrarMenu);
    }

    private void AlIngresarCbuDestino(string cbuDestino)
    {
        if (cbuDestino.Length == 22)
        {
            _cbuDestino = cbuDestino;
            SolicitarEntrada("MONTO A TRANSFERIR", 7, AlIngresarMontoTransferencia, MostrarMenu, esDinero: true);
        }
    }

    private void AlIngresarMontoTransferencia(string texto)
    {
        if (texto.Length == 0 || decimal.Parse(texto) <= 0)
        {
            return;
        }
        _montoTransferencia = decimal.Parse(texto);
        MostrarPantalla(
            $"CONFIRME LA OPERACIÓN\n\nDestino: {_cbuDestino[..11]}\n{_cbuDestino[11..]}\nMonto: {Formato.Dinero(_montoTransferencia)}",
            [new("Confirmar", Transferir), null, null, null, new("Cancelar", MostrarMenu)]);
        // ACEPTAR en el teclado también confirma la transferencia
        _entrada = new Entrada("", 0, _ => Transferir(), MostrarMenu);
    }

    private void Transferir()
    {
        MostrarResultado(_sesion.Ejecutar(_sesion.NuevaTransferencia(_cbuDestino, _montoTransferencia)), SolicitarCbuDestino);
    }

    private void PreguntarTicket()
    {
        _sesion.Finalizar();
        MostrarPantalla("¿DESEA COMPROBANTE IMPRESO?",
        [
            new("Sí", () => FinalizarSesion("¡Gracias!", conTicket: true)), null, null, null,
            new("No", () => FinalizarSesion("¡Gracias!")),
        ]);
    }

    private void FinalizarSesion(string mensaje, bool conTicket = false)
    {
        if (_sesion.Estado != EstadoSesion.Cerrando)
        {
            _sesion.Finalizar();
        }
        if (conTicket)
        {
            lblTicket.Text = _sesion.GenerarTicket();
        }
        _sesion.Cerrar();
        ActualizarBotonTarjeta("Retirar tarjeta", habilitado: true);
        MostrarPantalla($"{mensaje}\n\nRETIRE SU TARJETA");
    }

    #endregion
}
