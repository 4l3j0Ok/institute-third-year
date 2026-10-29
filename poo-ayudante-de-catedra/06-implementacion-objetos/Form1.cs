using CuentaBancariaWinForms.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CuentaBancariaWinForms;

public partial class Form1 : Form
{
    // Creamos dos instancias de clases globales para poder usarlas en los métodos de la clase Form1.
    private readonly CuentaBancaria cuenta = new CuentaBancaria(
        titular: "Juan Pérez",
        saldoInicial: 10000
    );
    // CultureInfo para formatear los números en formato de moneda de Argentina ($X.XXX)
    private readonly CultureInfo culturaArgentina = CultureInfo.GetCultureInfo("es-AR");

    public Form1()
    {
        InitializeComponent();
        ActualizarPantalla();
    }

    // Actualiza los labels con la información de la cuenta bancaria (titular y saldo).
    private void ActualizarPantalla()
    {
        lblTitular.Text = cuenta.Titular;
        lblSaldo.Text = cuenta.Saldo.ToString("C0", culturaArgentina);
    }

    // Solo se aceptan dígitos (y Backspace), por lo que el texto siempre es un número válido.
    private void txtImporte_KeyPress(object? sender, KeyPressEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\b]$");
    }

    // Convierte el texto del TextBox a int. Un TextBox vacío equivale a 0 (la cuenta lo rechaza).
    private int ObtenerImporte() =>
        txtImporte.Text.Length == 0 ? 0 : int.Parse(txtImporte.Text);

    private void btnDepositar_Click(object? sender, EventArgs e)
    {
        try
        {
            cuenta.Depositar(ObtenerImporte());
            OperacionExitosa("Depósito realizado correctamente.");
        }
        catch (ArgumentException ex)
        {
            MostrarEstado(ex.Message, false);
        }
    }

    private void btnRetirar_Click(object? sender, EventArgs e)
    {
        try
        {
            cuenta.Retirar(ObtenerImporte());
            OperacionExitosa("Retiro realizado correctamente.");
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            MostrarEstado(ex.Message, false);
        }
    }

    private void OperacionExitosa(string mensaje)
    {
        ActualizarPantalla();
        txtImporte.Clear();
        txtImporte.Focus();
        MostrarEstado(mensaje, true);
    }

    private void MostrarEstado(string mensaje, bool exito)
    {
        lblEstado.Text = mensaje;
        lblEstado.ForeColor = exito ? Color.Green : Color.Red;
    }
}
