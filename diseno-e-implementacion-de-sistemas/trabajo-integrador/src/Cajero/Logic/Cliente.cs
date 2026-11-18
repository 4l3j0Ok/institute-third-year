namespace Cajero.Logic;

public class Cliente
{
    private string _nombre;
    private string _dni;

    public Cliente(string nombre, string dni)
    {
        _nombre = nombre;
        _dni = dni;
    }

    public string Nombre
    {
        get => _nombre;
        set => _nombre = value;
    }

    /// <summary>DNI, el documento nacional de identidad argentino.</summary>
    public string Dni
    {
        get => _dni;
        set => _dni = value;
    }
}
