using System.ComponentModel;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace SkiaPagination.WinForms;

/// <summary>Paginador dibujado con SkiaSharp. Expone páginas candidatas y el cambio de página.
/// Editable desde el diseñador de WinForms: arrástralo a un formulario y ajusta sus propiedades
/// (Estilo, Página actual, Tamaño de página, Total de elementos) desde la ventana de Propiedades.</summary>
[ToolboxItem(true)]
[DesignerCategory("Code")]
[DefaultProperty(nameof(CurrentPage))]
[DefaultEvent(nameof(PageChanged))]
[Description("Control de paginación dibujado con SkiaSharp.")]
public sealed class SkiaPaginator : SKControl
{
    private readonly List<ButtonHit> _hits = [];
    private int _totalItems;
    private int _pageSize = 10;
    private int _currentPage = 1;

    [Category("Acción")]
    [Description("Se produce cuando el usuario cambia de página.")]
    public event EventHandler<PageChangedEventArgs>? PageChanged;

    [Category("Apariencia")]
    [Description("Paleta de colores, radio de esquina y tamaño de fuente de los botones.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaginationStyle Style { get; set; } = new();

    [Category("Datos")]
    [Description("Número total de elementos a paginar.")]
    [DefaultValue(0)]
    public int TotalItems { get => _totalItems; set { _totalItems = Math.Max(0, value); ClampPage(); Invalidate(); } }

    [Category("Datos")]
    [Description("Cantidad de elementos que se muestran por página.")]
    [DefaultValue(10)]
    public int PageSize { get => _pageSize; set { _pageSize = Math.Max(1, value); ClampPage(); Invalidate(); } }

    [Category("Comportamiento")]
    [Description("Página actualmente seleccionada (1-based).")]
    [DefaultValue(1)]
    public int CurrentPage { get => _currentPage; set => GoTo(value); }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TotalPages => Math.Max(1, (int)Math.Ceiling((double)TotalItems / PageSize));

    public SkiaPaginator()
    {
        DoubleBuffered = true;
        Height = 40;
        MinimumSize = new Size(240, 40);
        Cursor = Cursors.Hand;
    }

    /// <summary>Calcula un rango de páginas visible: 1, 2, …, 10, 11 según el total y la página actual.</summary>
    public IReadOnlyList<int?> GetTentativePages(int maxButtons = 7)
    {
        if (TotalPages <= maxButtons) return Enumerable.Range(1, TotalPages).Select(x => (int?)x).ToList();
        var pages = new List<int?> { 1 };
        var start = Math.Max(2, CurrentPage - 2);
        var end = Math.Min(TotalPages - 1, CurrentPage + 2);
        if (start > 2) pages.Add(null);
        for (var page = start; page <= end; page++) pages.Add(page);
        if (end < TotalPages - 1) pages.Add(null);
        pages.Add(TotalPages);
        return pages;
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);
        var canvas = e.Surface.Canvas;
        canvas.Clear(new SKColor(BackColor.R, BackColor.G, BackColor.B, BackColor.A));
        _hits.Clear();
        var scale = e.Info.Width / (float)Math.Max(1, Width);
        canvas.Scale(scale);
        using var text = new SKPaint { IsAntialias = true, TextSize = Math.Max(1f, Style.FontSize), Typeface = SKTypeface.FromFamilyName("Segoe UI", SKFontStyleWeight.SemiBold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright), TextAlign = SKTextAlign.Center };
        var x = 0f;
        x = DrawButton(canvas, text, x, "‹", CurrentPage > 1, () => GoTo(CurrentPage - 1));
        foreach (var item in GetTentativePages())
            x = item is null ? DrawEllipsis(canvas, text, x) : DrawButton(canvas, text, x, item.Value.ToString(), true, () => GoTo(item.Value), item.Value == CurrentPage);
        DrawButton(canvas, text, x, "›", CurrentPage < TotalPages, () => GoTo(CurrentPage + 1));
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        var hit = _hits.FirstOrDefault(x => x.Bounds.Contains(e.Location));
        hit?.Action();
    }

    private float DrawButton(SKCanvas canvas, SKPaint text, float x, string label, bool enabled, Action action, bool selected = false)
    {
        const float width = 36, height = 34, gap = 6;
        var rect = new SKRoundRect(new SKRect(x, 3, x + width, 3 + height), Math.Max(0f, Style.CornerRadius));
        using var fill = new SKPaint { IsAntialias = true, Color = ToSKColor(selected ? Style.Accent : Style.Surface), Style = SKPaintStyle.Fill };
        using var stroke = new SKPaint { IsAntialias = true, Color = ToSKColor(selected ? Style.Accent : Style.Border), Style = SKPaintStyle.Stroke, StrokeWidth = 1 };
        canvas.DrawRoundRect(rect, fill); canvas.DrawRoundRect(rect, stroke);
        text.Color = ToSKColor(!enabled ? Style.DisabledText : selected ? Style.AccentText : Style.Text);
        var baseline = 3 + (height - (text.FontMetrics.Descent - text.FontMetrics.Ascent)) / 2 - text.FontMetrics.Ascent;
        canvas.DrawText(label, x + width / 2, baseline, text);
        if (enabled) _hits.Add(new ButtonHit(Rectangle.Round(new RectangleF(rect.Rect.Left, rect.Rect.Top, rect.Rect.Width, rect.Rect.Height)), action));
        return x + width + gap;
    }

    private float DrawEllipsis(SKCanvas canvas, SKPaint text, float x)
    {
        text.Color = ToSKColor(Style.Text); canvas.DrawText("…", x + 14, 25, text); return x + 30;
    }

    private static SKColor ToSKColor(Color color) => new(color.R, color.G, color.B, color.A);

    private void GoTo(int page)
    {
        var target = Math.Clamp(page, 1, TotalPages);
        if (target == _currentPage) return;
        _currentPage = target; Invalidate(); PageChanged?.Invoke(this, new PageChangedEventArgs(target));
    }
    private void ClampPage() => _currentPage = Math.Clamp(_currentPage, 1, TotalPages);
    private sealed record ButtonHit(Rectangle Bounds, Action Action);
}
