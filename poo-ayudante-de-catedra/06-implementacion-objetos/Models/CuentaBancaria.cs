namespace CuentaBancariaWinForms.Models;

// Representa una cuenta y protege las reglas de su saldo.
public class CuentaBancaria
{
    // Creamos dos propiedades.
 
    // Titular es pública, se puede leer y modificar desde fuera de la clase.
    // Saldo es privada, solo se puede leer desde fuera de la clase, no se puede modificar (encapsulamiento).

    // No se podría actualizar el saldo en la instancia, solo en la clase con el método que creamos más abajo:

    // cuenta.Saldo = 1000  // NO VÁLIDO
    // cuenta.Depositar(1000) // VÁLIDO

    public string Titular { get; set; }
    public int Saldo { get; private set; }

    public CuentaBancaria(string titular, int saldoInicial)
    {
        if (string.IsNullOrWhiteSpace(titular))
            throw new ArgumentException("El titular es obligatorio.", nameof(titular));
        if (saldoInicial < 0)
            throw new ArgumentException("El saldo inicial no puede ser negativo.", nameof(saldoInicial));

        Titular = titular;
        Saldo = saldoInicial;
    }

    /// <summary>
    /// Deposita un importe en l
    public void Depositar(int importe)
    {
        if (importe <= 0)
            throw new ArgumentException("El importe debe ser mayor a cero.");
        Saldo += importe;
    }

    public void Retirar(int importe)
    {
        if (importe <= 0)
            throw new ArgumentException("El importe debe ser mayor a cero.");
        if (importe > Saldo)
            throw new InvalidOperationException("No hay saldo suficiente para retirar ese importe.");
        Saldo -= importe;
    }
}
