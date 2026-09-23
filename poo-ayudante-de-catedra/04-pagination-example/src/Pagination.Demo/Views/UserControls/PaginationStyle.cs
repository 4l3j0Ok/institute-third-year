using System.ComponentModel;

namespace Pagination.Demo.Views.UserControls;

/// <summary>Paleta reutilizable para <see cref="Paginator"/>. Editable desde el diseñador de WinForms
/// a través de la propiedad <see cref="Paginator.Style"/> (Property Grid expandible). Cada cambio dispara
/// <see cref="Changed"/> para que el control vuelva a pintar sus botones con los nuevos valores.</summary>
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
    private float _cornerRadius = 8f;
    private float _fontSize = 9f;

    /// <summary>Se produce cuando cualquier propiedad cambia, para que <see cref="Paginator"/> refresque sus botones.</summary>
    public event EventHandler? Changed;

    [Description("Color de fondo del botón de página seleccionada.")]
    public Color Accent { get => _accent; set => SetAndRaise(ref _accent, value); }
    private bool ShouldSerializeAccent() => _accent != DefaultAccent;
    private void ResetAccent() => Accent = DefaultAccent;

    [Description("Color del texto sobre el botón de página seleccionada.")]
    public Color AccentText { get => _accentText; set => SetAndRaise(ref _accentText, value); }
    private bool ShouldSerializeAccentText() => _accentText != DefaultAccentText;
    private void ResetAccentText() => AccentText = DefaultAccentText;

    [Description("Color de fondo de los botones no seleccionados.")]
    public Color Surface { get => _surface; set => SetAndRaise(ref _surface, value); }
    private bool ShouldSerializeSurface() => _surface != DefaultSurface;
    private void ResetSurface() => Surface = DefaultSurface;

    [Description("Color del borde de los botones no seleccionados.")]
    public Color Border { get => _border; set => SetAndRaise(ref _border, value); }
    private bool ShouldSerializeBorder() => _border != DefaultBorder;
    private void ResetBorder() => Border = DefaultBorder;

    [Description("Color del texto de los botones habilitados.")]
    public Color Text { get => _text; set => SetAndRaise(ref _text, value); }
    private bool ShouldSerializeText() => _text != DefaultText;
    private void ResetText() => Text = DefaultText;

    [Description("Color del texto de los botones deshabilitados (p. ej. ‹ y › en los extremos).")]
    public Color DisabledText { get => _disabledText; set => SetAndRaise(ref _disabledText, value); }
    private bool ShouldSerializeDisabledText() => _disabledText != DefaultDisabledText;
    private void ResetDisabledText() => DisabledText = DefaultDisabledText;

    [Description("Radio de las esquinas de cada botón, en píxeles.")]
    [DefaultValue(8f)]
    public float CornerRadius { get => _cornerRadius; set => SetAndRaise(ref _cornerRadius, value); }

    [Description("Tamaño de fuente del texto de los botones, en puntos.")]
    [DefaultValue(9f)]
    public float FontSize { get => _fontSize; set => SetAndRaise(ref _fontSize, value); }

    private void SetAndRaise(ref Color field, Color value)
    {
        if (field == value) return;
        field = value;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    private void SetAndRaise(ref float field, float value)
    {
        if (field.Equals(value)) return;
        field = value;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>Representación legible en la Property Grid cuando la fila no está expandida.</summary>
    public override string ToString() => string.Empty;
}
