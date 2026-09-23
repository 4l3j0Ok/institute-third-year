using SkiaPagination.Demo.Data;
using SkiaPagination.Demo.Models;

namespace SkiaPagination.Demo;

public partial class MainForm : Form
{
    private const int PageSize = 10;
    private readonly CustomerRepository _repository;

    public MainForm()
    {
        InitializeComponent();

        var connectionString = Environment.GetEnvironmentVariable("PAGINATION_DEMO_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Definí PAGINATION_DEMO_CONNECTION_STRING con la conexión a la base de datos SQL Server.");

        _repository = new CustomerRepository(connectionString);
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
        var hasSelection = GetSelectedCustomer() is not null;
        _editButton.Enabled = hasSelection;
        _deleteButton.Enabled = hasSelection;
    }

    private Customer? GetSelectedCustomer() => _grid.CurrentRow?.DataBoundItem as Customer;

    /// <summary>Alta: abre el diálogo de creación y, si se confirma, inserta y navega a la última página.</summary>
    private void AddNew()
    {
        using var dialog = new CustomerEditForm();
        if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Customer is null) return;

        _repository.Insert(dialog.Customer);
        var total = _repository.Count();
        var lastPage = Math.Max(1, (int)Math.Ceiling((double)total / PageSize));
        LoadPage(lastPage);
    }

    /// <summary>Modificación: abre el diálogo con los datos del cliente seleccionado.</summary>
    private void EditSelected()
    {
        var selected = GetSelectedCustomer();
        if (selected is null) return;

        using var dialog = new CustomerEditForm(selected);
        if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Customer is null) return;

        _repository.Update(dialog.Customer);
        LoadPage(_paginator.CurrentPage);
    }

    /// <summary>Baja: pide confirmación y elimina al cliente seleccionado.</summary>
    private void DeleteSelected()
    {
        var selected = GetSelectedCustomer();
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
