namespace Cajero.Logic;

public class Cuenta
{
    private readonly string _cbu;
    private decimal _saldoDisponible;
    private decimal _saldoTotal;

    public Cuenta(string cbu, decimal saldoDisponible, decimal saldoTotal)
    {
        _cbu = cbu;
        _saldoDisponible = saldoDisponible;
        _saldoTotal = saldoTotal;
    }

    public string Cbu
    {
        get => _cbu;
    }

    public decimal SaldoDisponible
    {
        get => _saldoDisponible;
        set => _saldoDisponible = value;
    }

    public decimal SaldoTotal
    {
        get => _saldoTotal;
        set => _saldoTotal = value;
    }

    public void ActualizarSaldo(decimal disponible, decimal total)
    {
        _saldoDisponible = disponible;
        _saldoTotal = total;
    }
}
