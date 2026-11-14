using System.Globalization;

namespace Atm.Logic;

public static class MoneyFormatter
{
    private static readonly CultureInfo s_culture = CreateCulture();

    /// <summary>Formats an amount as <c>$1.234,56</c> (Argentine separators).</summary>
    public static string Format(decimal value)
    {
        return "$" + value.ToString("N2", s_culture);
    }

    private static CultureInfo CreateCulture()
    {
        var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        culture.NumberFormat.NumberDecimalSeparator = ",";
        culture.NumberFormat.NumberGroupSeparator = ".";
        return culture;
    }
}
