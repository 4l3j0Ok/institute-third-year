namespace Atm.Logic;

public class Customer
{
    private string _name;
    private string _nationalId;

    public Customer(string name, string nationalId)
    {
        _name = name;
        _nationalId = nationalId;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>DNI, the Argentine national identity document number.</summary>
    public string NationalId
    {
        get => _nationalId;
        set => _nationalId = value;
    }
}
