using Pagination.Demo.Models;

namespace Pagination.Demo.Views.Forms;

public partial class ClienteEditForm : Form
{
    private readonly long _id;

    public Cliente? Cliente { get; private set; }

    public ClienteEditForm() : this(null)
    {
    }

    public ClienteEditForm(Cliente? cliente)
    {
        InitializeComponent();
        _id = cliente?.Id ?? 0;
        Text = cliente is null ? "Nuevo cliente" : "Editar cliente";

        if (cliente is not null)
        {
            _nameBox.Text = cliente.Name;
            _emailBox.Text = cliente.Email;
            _cityBox.Text = cliente.City;
        }

        _saveButton.Click += (_, _) => SaveCliente();
    }

    // Valida los campos y devuelve el cliente al formulario principal.
    private void SaveCliente()
    {
        var name = _nameBox.Text.Trim();
        var email = _emailBox.Text.Trim();
        var city = _cityBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(city))
        {
            MessageBox.Show(this, "Completá nombre, email y ciudad.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        Cliente = new Cliente(_id, name, email, city);
        DialogResult = DialogResult.OK;
    }
}
