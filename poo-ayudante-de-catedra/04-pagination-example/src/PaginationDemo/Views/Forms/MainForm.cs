using Pagination.Demo.Data;

namespace Pagination.Demo.Views.Forms;

public class MainForm : Form
{
    private const int PageSize = 10;
    private readonly ClienteRepository _repository;
    private readonly DataGridView _grid = new();
    private readonly Label _status = new();
    private readonly Button _previousButton = new() { Text = "<" };
    private readonly Button _page4Button = new() { Text = "4" };
    private readonly Button _page5Button = new() { Text = "5" };
    private readonly Button _page6Button = new() { Text = "6" };
    private readonly Button _nextButton = new() { Text = ">" };
    private int _currentPage = 5;

    public MainForm()
    {
        InitializeComponent();

        var connectionString = Environment.GetEnvironmentVariable("PAGINATION_DEMO_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Creá src/PaginationDemo/.env con la conexión a la base de datos SQL Server.");

        _repository = new ClienteRepository(connectionString);
        _repository.Initialize();

        // Cada boton indica directamente la pagina que debe cargarse.
        _previousButton.Click += (_, _) => LoadPage(_currentPage - 1);
        _page4Button.Click += (_, _) => LoadPage(4);
        _page5Button.Click += (_, _) => LoadPage(5);
        _page6Button.Click += (_, _) => LoadPage(6);
        _nextButton.Click += (_, _) => LoadPage(_currentPage + 1);
        Shown += (_, _) => LoadPage(_currentPage);
    }

    // Consulta la pagina elegida y actualiza la grilla y el estado del pie.
    private void LoadPage(int page)
    {
        var total = _repository.Count();
        var totalPages = Math.Max(1, (int)Math.Ceiling((double)total / PageSize));
        _currentPage = Math.Clamp(page, 1, totalPages);
        _grid.DataSource = _repository.GetPage(_currentPage, PageSize);
        var from = total == 0 ? 0 : (_currentPage - 1) * PageSize + 1;
        var to = Math.Min(_currentPage * PageSize, total);
        _status.Text = $"Mostrando {from}–{to} de {total}";
        _previousButton.Enabled = _currentPage > 1;
        _nextButton.Enabled = _currentPage < totalPages;
    }

    // Crea todos los controles del formulario sin depender de un UserControl ni del Designer.
    private void InitializeComponent()
    {
        var paginator = new FlowLayoutPanel
        {
            AutoSize = true,
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };
        var footer = new TableLayoutPanel
        {
            ColumnCount = 1,
            Dock = DockStyle.Bottom,
            Padding = new Padding(20, 8, 20, 8),
            RowCount = 2,
            Size = new Size(884, 80)
        };

        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        footer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        footer.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));

        foreach (var button in new[] { _previousButton, _page4Button, _page5Button, _page6Button, _nextButton })
        {
            button.AutoSize = true;
            paginator.Controls.Add(button);
        }

        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _grid.BackgroundColor = Color.White;
        _grid.BorderStyle = BorderStyle.None;
        _grid.Dock = DockStyle.Fill;
        _grid.ReadOnly = true;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        _status.AutoSize = true;
        _status.Dock = DockStyle.Fill;
        _status.ForeColor = Color.FromArgb(71, 84, 103);
        _status.TextAlign = ContentAlignment.MiddleCenter;

        footer.Controls.Add(paginator, 0, 0);
        footer.Controls.Add(_status, 0, 1);
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(884, 585);
        Controls.Add(_grid);
        Controls.Add(footer);
        MinimumSize = new Size(720, 480);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Pagination · Demo";
    }
}
