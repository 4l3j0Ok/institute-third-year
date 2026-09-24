using Pagination.Demo.Models;

namespace Pagination.Demo;

/// <summary>Diálogo de Alta/Modificación de un cliente. Reutilizable para ambos casos:
/// se instancia sin argumentos para Alta, o con un <see cref="Models.Cliente"/> existente para Modificación.</summary>
public partial class ClienteEditForm : Form
{
    private readonly long _id;

    /// <summary>Cliente resultante, disponible cuando <see cref="Form.DialogResult"/> es <see cref="DialogResult.OK"/>.</summary>
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

        _saveButton.Click += OnSave;
    }

    private void OnSave(object? sender, EventArgs e)
    {
        _errorProvider.Clear();
        var name = _nameBox.Text.Trim();
        var email = _emailBox.Text.Trim();
        var city = _cityBox.Text.Trim();
        var valid = true;

        if (string.IsNullOrEmpty(name))
        {
            _errorProvider.SetError(_nameBox, "El nombre es obligatorio.");
            valid = false;
        }
        if (string.IsNullOrEmpty(email) || !email.Contains('@') || !email.Contains('.'))
        {
            _errorProvider.SetError(_emailBox, "Ingresá un email válido.");
            valid = false;
        }
        if (string.IsNullOrEmpty(city))
        {
            _errorProvider.SetError(_cityBox, "La ciudad es obligatoria.");
            valid = false;
        }

        if (!valid) return;

        Cliente = new Cliente(_id, name, email, city);
        DialogResult = DialogResult.OK;
        Close();
    }
}
