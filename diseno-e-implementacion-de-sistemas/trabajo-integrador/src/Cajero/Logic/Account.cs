namespace Cajero.Logic;

public class Account
{
    private readonly string _cbu;
    private decimal _availableBalance;
    private decimal _totalBalance;

    public Account(string cbu, decimal availableBalance, decimal totalBalance)
    {
        _cbu = cbu;
        _availableBalance = availableBalance;
        _totalBalance = totalBalance;
    }

    public string Cbu
    {
        get => _cbu;
    }

    public decimal AvailableBalance
    {
        get => _availableBalance;
        set => _availableBalance = value;
    }

    public decimal TotalBalance
    {
        get => _totalBalance;
        set => _totalBalance = value;
    }

    public void UpdateBalance(decimal available, decimal total)
    {
        _availableBalance = available;
        _totalBalance = total;
    }
}
