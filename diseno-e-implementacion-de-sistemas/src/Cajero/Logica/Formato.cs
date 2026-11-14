using System.Globalization;

namespace Cajero.Logica;

public static class Formato
{
    private static readonly CultureInfo s_cultura = CrearCultura();

    /// <summary>Formatea un monto como <c>$1.234,56</c>.</summary>
    public static string Dinero(decimal valor)
    {
        return "$" + valor.ToString("N2", s_cultura);
    }

    private static CultureInfo CrearCultura()
    {
        var cultura = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        cultura.NumberFormat.NumberDecimalSeparator = ",";
        cultura.NumberFormat.NumberGroupSeparator = ".";
        return cultura;
    }
}
