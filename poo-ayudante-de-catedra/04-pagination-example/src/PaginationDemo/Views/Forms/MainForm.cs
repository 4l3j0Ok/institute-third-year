using Pagination.Demo.Data;

namespace Pagination.Demo.Views.Forms;

public partial class MainForm : Form
{
    private const int PageSize = 10;
    private readonly ClienteRepository _repository;
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

}
