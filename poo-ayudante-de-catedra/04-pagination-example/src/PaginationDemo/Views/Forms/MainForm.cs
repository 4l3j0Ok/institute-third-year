using Pagination.Demo.Data;
using Pagination.Demo.Models;

namespace Pagination.Demo.Views.Forms;

public partial class MainForm : Form
{
    private const int PageSize = 10;
    private readonly ClienteRepository _repository;

    public MainForm()
    {
        InitializeComponent();

        var connectionString = Environment.GetEnvironmentVariable("PAGINATION_DEMO_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Creá src/PaginationDemo/.env con la conexión a la base de datos SQL Server.");

        _repository = new ClienteRepository(connectionString);
        _repository.Initialize();

        _paginator.PageChanged += (_, args) => LoadPage(args.Page);
        _grid.SelectionChanged += (_, _) => UpdateButtonsState();
        _grid.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) EditSelected(); };
        _addButton.Click += (_, _) => AddNew();
        _editButton.Click += (_, _) => EditSelected();
        _deleteButton.Click += (_, _) => DeleteSelected();
        Shown += (_, _) => LoadPage(1);
    }

    private void LoadPage(int page)
    {
        var total = _repository.Count();
        _paginator.TotalItems = total;
        if (_paginator.CurrentPage != page) { _paginator.CurrentPage = page; return; }
        _grid.DataSource = _repository.GetPage(page, PageSize);
        var from = total == 0 ? 0 : (page - 1) * PageSize + 1;
        var to = Math.Min(page * PageSize, total);
        _status.Text = $"Mostrando {from}–{to} de {total}";
        UpdateButtonsState();
    }

    private void UpdateButtonsState()
    {
        var hasSelection = GetSelectedCliente() is not null;
        _editButton.Enabled = hasSelection;
        _deleteButton.Enabled = hasSelection;
    }

    private Cliente? GetSelectedCliente() => _grid.CurrentRow?.DataBoundItem as Cliente;

    /// <summary>Alta: abre el diálogo de creación y, si se confirma, inserta y navega a la última página.</summary>
    private void AddNew()
    {
        using var dialog = new ClienteEditForm();
        if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Cliente is null) return;

        _repository.Insert(dialog.Cliente);
        var total = _repository.Count();
        var lastPage = Math.Max(1, (int)Math.Ceiling((double)total / PageSize));
        LoadPage(lastPage);
    }

    /// <summary>Modificación: abre el diálogo con los datos del cliente seleccionado.</summary>
    private void EditSelected()
    {
        var selected = GetSelectedCliente();
        if (selected is null) return;

        using var dialog = new ClienteEditForm(selected);
        if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Cliente is null) return;

        _repository.Update(dialog.Cliente);
        LoadPage(_paginator.CurrentPage);
    }

    /// <summary>Baja: pide confirmación y elimina al cliente seleccionado.</summary>
    private void DeleteSelected()
    {
        var selected = GetSelectedCliente();
        if (selected is null) return;

        var confirm = MessageBox.Show(
            this,
            $"¿Eliminar a \"{selected.Name}\"? Esta acción no se puede deshacer.",
            "Confirmar eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);
        if (confirm != DialogResult.Yes) return;

        _repository.Delete(selected.Id);
        LoadPage(_paginator.CurrentPage);
    }
}
