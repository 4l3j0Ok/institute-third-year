using System.ComponentModel;

namespace SkiaPagination.WinForms;

/// <summary>Paleta reutilizable para controles Skia del producto. Editable desde el diseñador de WinForms
/// a través de la propiedad <see cref="SkiaPaginator.Style"/> (Property Grid expandible).</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public sealed class PaginationStyle
{
    private static readonly Color DefaultAccent = ColorTranslator.FromHtml("#2563EB");
    private static readonly Color DefaultAccentText = Color.White;
    private static readonly Color DefaultSurface = Color.White;
    private static readonly Color DefaultBorder = ColorTranslator.FromHtml("#D0D5DD");
    private static readonly Color DefaultText = ColorTranslator.FromHtml("#344054");
    private static readonly Color DefaultDisabledText = ColorTranslator.FromHtml("#98A2B3");

    private Color _accent = DefaultAccent;
    private Color _accentText = DefaultAccentText;
    private Color _surface = DefaultSurface;
    private Color _border = DefaultBorder;
    private Color _text = DefaultText;
    private Color _disabledText = DefaultDisabledText;

    [Description("Color de fondo del botón de página seleccionada.")]
    public Color Accent { get => _accent; set => _accent = value; }
    private bool ShouldSerializeAccent() => _accent != DefaultAccent;
    private void ResetAccent() => _accent = DefaultAccent;

    [Description("Color del texto sobre el botón de página seleccionada.")]
    public Color AccentText { get => _accentText; set => _accentText = value; }
    private bool ShouldSerializeAccentText() => _accentText != DefaultAccentText;
    private void ResetAccentText() => _accentText = DefaultAccentText;

    [Description("Color de fondo de los botones no seleccionados.")]
    public Color Surface { get => _surface; set => _surface = value; }
    private bool ShouldSerializeSurface() => _surface != DefaultSurface;
    private void ResetSurface() => _surface = DefaultSurface;

    [Description("Color del borde de los botones no seleccionados.")]
    public Color Border { get => _border; set => _border = value; }
    private bool ShouldSerializeBorder() => _border != DefaultBorder;
    private void ResetBorder() => _border = DefaultBorder;

    [Description("Color del texto de los botones habilitados.")]
    public Color Text { get => _text; set => _text = value; }
    private bool ShouldSerializeText() => _text != DefaultText;
    private void ResetText() => _text = DefaultText;

    [Description("Color del texto de los botones deshabilitados (p. ej. ‹ y › en los extremos).")]
    public Color DisabledText { get => _disabledText; set => _disabledText = value; }
    private bool ShouldSerializeDisabledText() => _disabledText != DefaultDisabledText;
    private void ResetDisabledText() => _disabledText = DefaultDisabledText;

    [Description("Radio de las esquinas de cada botón, en píxeles.")]
    [DefaultValue(8f)]
    public float CornerRadius { get; set; } = 8f;

    [Description("Tamaño de fuente del texto de los botones, en puntos Skia.")]
    [DefaultValue(13f)]
    public float FontSize { get; set; } = 13f;

    /// <summary>Representación legible en la Property Grid cuando la fila no está expandida.</summary>
    public override string ToString() => string.Empty;
}
