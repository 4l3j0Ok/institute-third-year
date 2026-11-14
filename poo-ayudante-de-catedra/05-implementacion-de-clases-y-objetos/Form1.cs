namespace _05_implementacion_de_clases_y_objetos;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }
}

public class Continente
{
    public string Nombre { get; set; }
    public int NumeroPaises { get; set; }
}

public class Pais
{
    public string Nombre { get; set; }
    public Continente Continente { get; set; }
    public int NumeroHabitantes { get; set; }
}

public class Ciudad
{
    public string Nombre { get; set; }
    public Pais Pais { get; set; }
    // get permite leer el valor y set permite asignarlo o modificarlo desde fuera de la clase.
    public int NumeroHabitantes
    {
        get;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("El número de habitantes no puede ser negativo.");
            }
            field = value;
        }
    }

}
