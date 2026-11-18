using System.Globalization;

namespace Cajero.Logic;

public static class FormateadorMonedas
{
    private static readonly CultureInfo s_culture = CrearCultura();

    /// <summary>Formatea un monto como <c>$1.234,56</c> (separadores argentinos).</summary>
    public static string Formatear(decimal valor)
    {
        return "$" + valor.ToString("N2", s_culture);
    }

    private static CultureInfo CrearCultura()
    {
        var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        culture.NumberFormat.NumberDecimalSeparator = ",";
        culture.NumberFormat.NumberGroupSeparator = ".";
        return culture;
    }
}
