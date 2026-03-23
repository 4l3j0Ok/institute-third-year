using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace poo_ayudante_de_catedra;

public partial class frmCalc : Form
{
    const List<string> operations = new List<string> { "+", "-", "*", "/", "%", ",", "(", ")" };
    const List<string> numbers = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    const string SYNTAX_ERROR = "Error de sintaxis";
    public frmCalc()
    {
        InitializeComponent();
    }

    private void btnAppendTextClick(object sender, EventArgs e)
    {
        // El sender es el botón que se hizo click, entonces convertimos 
        // el sender a un botón para acceder a su texto
        Button btn = (Button)sender;
        // Si el texto del botón es un número, lo agregamos al textbox
        if (numbers.Contains(btn.Text))
        {
            tbCalc.Text += btn.Text;
            return;
        }
        // Si en cambio, es una operación, hay que verificar varias cosas
        if (operations.Contains(btn.Text))
        {

            // Si el último carácter del textbox es una operación, se reemplaza por la nueva operación
            if (tbCalc.Text.Length > 0)
            {
                if (operations.Contains(tbCalc.Text.Substring(tbCalc.Text.Length - 1)))
                {
                    tbCalc.Text = tbCalc.Text.Substring(0, tbCalc.Text.Length - 1) + btn.Text;
                    return;
                }
            }
            // Si no cumple ninguna de las condiciones anteriores, se agrega la operación al textbox
            // (No hace falta else porque hacemos return en caso de que se cumplan)
            tbCalc.Text += btn.Text;
        }
    }

    private void btnEqualClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        tbResult.Text = string.Empty;
        try
        {
            // Para evaluar la expresión usamos el método Compute de la clase DataTable,
            // que evalúa una expresión matemática en formato string y devuelve el resultado

            // Antes de evaluar la expresión, verificamos que no haya paréntesis sin cerrar,
            // ya que eso causaría un error de sintaxis.
            if (tbCalc.Text.Contains("(") && !tbCalc.Text.Contains(")")) {
                if (tbCalc.Text.EndsWith("(")) {
                    btnClearClick(sender, e);
                }
                else
                {
                    tbCalc.Text = tbCalc.Text += ")";
                }
            }
            // Convertimos porcentajes tipo 50%100 (cincuenta porciento de cien) 
            // en (100*50)/100 para que Compute lo pueda interpretar bien
            string calcText = tbCalc.Text;
            // El método Regex.Replace nos permite reemplazar partes de un string
            // que coincidan con una expresión regular.
            // La expresión regular que usamos es @"(\d+)%(\d+)", que busca un número
            // seguido de un porcentaje y otro número.
            // El reemplazo que hacemos es "($2*$1)/100", que toma el
            // segundo número (el número después del porcentaje) y lo multiplica por el
            // primer número (el número antes del porcentaje), y luego lo divide por 100
            // para obtener el resultado del porcentaje.
            calcText = Regex.Replace(
                tbCalc.Text,
                @"(\d+)%(\d+)",
                "($2*$1)/100"
            );
            // Reemplazamos las comas por puntos para que el DataTable.Compute pueda evaluar números decimales
            calcText = calcText.Replace(",", ".");
            DataTable dt = new DataTable();
            // El ? en la declaración del tipo de dato significa que la variable PUEDE ser nula
            string? result = dt.Compute(
                calcText,
                String.Empty
            ).ToString();
            tbResult.Text = result;
        }
        catch (DataException ex)
        {
            // Debug.WriteLine es una función que nos permite imprimir mensajes
            // en la consola de depuración de Visual Studio, lo cuales útil para
            // ver qué errores ocurren sin mostrarle al usuario un mensaje de error.
            Debug.WriteLine( ex.Message );
            // Si ocurre un error al evaluar la expresión, se muestra el mensaje de error de sintaxis
            // (que definimos como constante al principio de la clase)
            tbResult.Text = SYNTAX_ERROR;
        }
    }

    private void btnClearClick(object sender, EventArgs e)
    {
        // Si el textbox está vacío, no hacemos nada
        if (!(tbCalc.Text.Length > 0)) return;
        // Si no, eliminamos el último carácter del textbox
        tbCalc.Text = tbCalc.Text.Substring(0, tbCalc.Text.Length - 1);
        return;
    }

    private void btnClearAllClick(object sender, EventArgs e)
    {
        // Vaciamos ambos textboxes
        tbCalc.Text = string.Empty;
        tbResult.Text = string.Empty;
    }

    private void btnNegateClick(object sender, EventArgs e)
    {
        // Para negar el resultado, primero evaluamos la expresión para asegurarnos de que no haya un error de sintaxis
        btnEqualClick(sender, e);
        // Si hay un error de sintaxis, no hacemos nada.
        if (tbResult.Text == SYNTAX_ERROR) return;
        // Si el resultado es negativo, lo hacemos positivo, y si es positivo, lo hacemos negativo.
        // Para esto, verificamos el primer carácter del resultado con el método StartsWith,
        // que devuelve true si el string comienza con el carácter especificado,
        // y dependiendo de eso, modificamos el resultado en consecuencia.
        if (!tbResult.Text.StartsWith("-")) tbResult.Text = "-" + tbResult.Text;
        else tbResult.Text = tbResult.Text.Substring(1, tbResult.Text.Length - 1);
        return;
    }
}
