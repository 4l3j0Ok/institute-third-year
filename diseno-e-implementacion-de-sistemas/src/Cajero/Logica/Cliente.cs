namespace Cajero.Logica;

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

    public string Dni
    {
        get => _dni;
        set => _dni = value;
    }
}
