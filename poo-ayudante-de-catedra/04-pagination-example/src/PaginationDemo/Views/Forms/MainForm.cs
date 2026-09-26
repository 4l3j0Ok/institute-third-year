using Pagination.Demo.Data;
using Pagination.Demo.Models;

namespace Pagination.Demo.Views.Forms;

public partial class MainForm : Form
{
    private const int PageSize = 10;
    private readonly ClienteRepository _repository;
    private int _currentPage = 1;

    public MainForm()
    {
        InitializeComponent();

        var connectionString = Environment.GetEnvironmentVariable("PAGINATION_DEMO_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Creá src/PaginationDemo/.env con la conexión a la base de datos SQL Server.");

        _repository = new ClienteRepository(connectionString);
        _repository.Initialize();

        // Los botones numéricos usan el número de página guardado en Tag.
        _previousButton.Click += (_, _) => LoadPage(_currentPage - 1);
        _previousPageButton.Click += PageButtonClicked;
        _currentPageButton.Click += PageButtonClicked;
        _nextPageButton.Click += PageButtonClicked;
        _nextButton.Click += (_, _) => LoadPage(_currentPage + 1);
        _grid.SelectionChanged += (_, _) => UpdateActionButtons();
        _grid.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) EditSelected(); };
        _addButton.Click += (_, _) => AddCliente();
        _editButton.Click += (_, _) => EditSelected();
        _deleteButton.Click += (_, _) => DeleteSelected();
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
        UpdatePageButtons(totalPages);
        _previousButton.Enabled = _currentPage > 1;
        _nextButton.Enabled = _currentPage < totalPages;
        UpdateActionButtons();
    }

    // Carga la página indicada por el botón numérico que se presionó.
    private void PageButtonClicked(object? sender, EventArgs e)
    {
        if (sender is Button { Tag: int page }) LoadPage(page);
    }

    // Muestra una ventana de tres páginas alrededor de la página actual.
    private void UpdatePageButtons(int totalPages)
    {
        var firstPage = Math.Clamp(_currentPage - 1, 1, Math.Max(1, totalPages - 2));
        ConfigurePageButton(_previousPageButton, firstPage, totalPages);
        ConfigurePageButton(_currentPageButton, firstPage + 1, totalPages);
        ConfigurePageButton(_nextPageButton, firstPage + 2, totalPages);
    }

    // Oculta los números que no existen cuando hay menos de tres páginas.
    private static void ConfigurePageButton(Button button, int page, int totalPages)
    {
        button.Tag = page;
        button.Text = page.ToString();
        button.Visible = page <= totalPages;
    }

    // Activa Editar y Eliminar únicamente cuando la grilla tiene una fila seleccionada.
    private void UpdateActionButtons()
    {
        var hasSelection = SelectedCliente() is not null;
        _editButton.Enabled = hasSelection;
        _deleteButton.Enabled = hasSelection;
    }

    private Cliente? SelectedCliente() => _grid.CurrentRow?.DataBoundItem as Cliente;

    // Abre un diálogo vacío y agrega el cliente si se confirma.
    private void AddCliente()
    {
        using var dialog = new ClienteEditForm();
        if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Cliente is null) return;

        _repository.Insert(dialog.Cliente);
        LoadPage(_currentPage);
    }

    // Edita los datos de la fila seleccionada.
    private void EditSelected()
    {
        var cliente = SelectedCliente();
        if (cliente is null) return;

        using var dialog = new ClienteEditForm(cliente);
        if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Cliente is null) return;

        _repository.Update(dialog.Cliente);
        LoadPage(_currentPage);
    }

    // Pide confirmación antes de eliminar la fila seleccionada.
    private void DeleteSelected()
    {
        var cliente = SelectedCliente();
        if (cliente is null) return;

        var result = MessageBox.Show(this, $"¿Eliminar a \"{cliente.Name}\"?", "Eliminar cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes) return;

        _repository.Delete(cliente.Id);
        LoadPage(_currentPage);
    }

}
