using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Pagination.Demo.Views.UserControls;

/// <summary>Control de paginación reutilizable, compuesto por botones nativos de WinForms.
/// Editable desde el diseñador de Visual Studio: arrástralo a un formulario y ajustá sus propiedades
/// (Estilo, Página actual, Tamaño de página, Total de elementos) desde la ventana de Propiedades.
/// El diseño visual (botones "‹"/"›" y el panel donde se agregan los números de página) vive en
/// <see cref="Paginator.Designer.cs"/>; este archivo solo contiene el comportamiento.</summary>
[ToolboxItem(true)]
[DefaultProperty(nameof(CurrentPage))]
[DefaultEvent(nameof(PageChanged))]
[Description("Control de paginación con botones nativos de WinForms.")]
public partial class Paginator : UserControl
{
    private PaginationStyle _style = new();
    private Font? _buttonFont;
    private int _totalItems;
    private int _pageSize = 10;
    private int _currentPage = 1;

    public Paginator()
    {
        InitializeComponent();
        _style.Changed += OnStyleChanged;
        _previousButton.Click += (_, _) => GoTo(CurrentPage - 1);
        _nextButton.Click += (_, _) => GoTo(CurrentPage + 1);
        Rebuild();
    }

    [Category("Acción")]
    [Description("Se produce cuando el usuario cambia de página.")]
    public event EventHandler<PageChangedEventArgs>? PageChanged;

    [Category("Apariencia")]
    [Description("Paleta de colores, radio de esquina y tamaño de fuente de los botones.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaginationStyle Style
    {
        get => _style;
        set
        {
            var style = value ?? new PaginationStyle();
            if (ReferenceEquals(_style, style)) return;
            _style.Changed -= OnStyleChanged;
            _style = style;
            _style.Changed += OnStyleChanged;
            ApplyStyle();
        }
    }

    [Category("Datos")]
    [Description("Número total de elementos a paginar.")]
    [DefaultValue(0)]
    public int TotalItems { get => _totalItems; set { _totalItems = Math.Max(0, value); ClampPage(); Rebuild(); } }

    [Category("Datos")]
    [Description("Cantidad de elementos que se muestran por página.")]
    [DefaultValue(10)]
    public int PageSize { get => _pageSize; set { _pageSize = Math.Max(1, value); ClampPage(); Rebuild(); } }

    [Category("Comportamiento")]
    [Description("Página actualmente seleccionada (1-based).")]
    [DefaultValue(1)]
    public int CurrentPage { get => _currentPage; set => GoTo(value); }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TotalPages => Math.Max(1, (int)Math.Ceiling((double)TotalItems / PageSize));

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

    private void GoTo(int page)
    {
        var target = Math.Clamp(page, 1, TotalPages);
        if (target == _currentPage) return;
        _currentPage = target;
        Rebuild();
        PageChanged?.Invoke(this, new PageChangedEventArgs(target));
    }

    private void ClampPage() => _currentPage = Math.Clamp(_currentPage, 1, TotalPages);

    /// <summary>Recrea los botones numéricos y las elipsis según <see cref="GetTentativePages"/>,
    /// y actualiza el estado habilitado/deshabilitado de las flechas.</summary>
    private void Rebuild()
    {
        _pagesPanel.SuspendLayout();
        foreach (Control control in _pagesPanel.Controls.Cast<Control>().ToList())
        {
            _pagesPanel.Controls.Remove(control);
            control.Dispose();
        }
        foreach (var item in GetTentativePages())
            _pagesPanel.Controls.Add(item is null ? CreateEllipsis() : CreatePageButton(item.Value));
        _pagesPanel.ResumeLayout();

        _previousButton.Enabled = CurrentPage > 1;
        _nextButton.Enabled = CurrentPage < TotalPages;
        ApplyStyle();
    }

    private Button CreatePageButton(int page)
    {
        var button = new Button
        {
            Text = page.ToString(),
            Size = new Size(36, 34),
            Margin = new Padding(0, 3, 6, 3),
            FlatStyle = FlatStyle.Flat,
            UseVisualStyleBackColor = false,
            Tag = page,
        };
        button.Click += (_, _) => GoTo(page);
        return button;
    }

    private Label CreateEllipsis() => new()
    {
        Text = "…",
        AutoSize = false,
        Size = new Size(24, 34),
        Margin = new Padding(0, 3, 6, 3),
        TextAlign = ContentAlignment.MiddleCenter,
    };

    private void OnStyleChanged(object? sender, EventArgs e) => ApplyStyle();

    /// <summary>Aplica los colores, fuente y radio de esquina de <see cref="Style"/> a las flechas
    /// y a los botones de página actualmente creados.</summary>
    private void ApplyStyle()
    {
        _buttonFont?.Dispose();
        _buttonFont = new Font(Font.FontFamily, Math.Max(1f, Style.FontSize), FontStyle.Bold);

        ApplyButtonStyle(_previousButton, selected: false);
        ApplyButtonStyle(_nextButton, selected: false);
        foreach (Button button in _pagesPanel.Controls.OfType<Button>())
            ApplyButtonStyle(button, selected: button.Tag is int page && page == CurrentPage);
        foreach (Label label in _pagesPanel.Controls.OfType<Label>())
        {
            label.Font = _buttonFont;
            label.ForeColor = Style.Text;
        }
    }

    private void ApplyButtonStyle(Button button, bool selected)
    {
        button.Font = _buttonFont;
        button.BackColor = selected ? Style.Accent : Style.Surface;
        button.ForeColor = !button.Enabled ? Style.DisabledText : selected ? Style.AccentText : Style.Text;
        button.FlatAppearance.BorderColor = selected ? Style.Accent : Style.Border;
        button.FlatAppearance.BorderSize = 1;
        button.Region = Style.CornerRadius > 0 ? RoundedRegion(button.Size, Style.CornerRadius) : null;
    }

    private static Region RoundedRegion(Size size, float radius)
    {
        var diameter = Math.Min(radius * 2, Math.Min(size.Width, size.Height));
        using var path = new GraphicsPath();
        var rect = new RectangleF(0, 0, size.Width, size.Height);
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return new Region(path);
    }
}
