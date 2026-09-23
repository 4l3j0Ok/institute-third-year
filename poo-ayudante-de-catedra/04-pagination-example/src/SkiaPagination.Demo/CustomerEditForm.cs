using SkiaPagination.Demo.Models;

namespace SkiaPagination.Demo;

/// <summary>Diálogo de Alta/Modificación de un cliente. Reutilizable para ambos casos:
/// se instancia sin argumentos para Alta, o con un <see cref="Models.Customer"/> existente para Modificación.</summary>
public partial class CustomerEditForm : Form
{
    private readonly long _id;

    /// <summary>Cliente resultante, disponible cuando <see cref="Form.DialogResult"/> es <see cref="DialogResult.OK"/>.</summary>
    public Customer? Customer { get; private set; }

    public CustomerEditForm() : this(null)
    {
    }

    public CustomerEditForm(Customer? customer)
    {
        InitializeComponent();
        _id = customer?.Id ?? 0;
        Text = customer is null ? "Nuevo cliente" : "Editar cliente";

        if (customer is not null)
        {
            _nameBox.Text = customer.Name;
            _emailBox.Text = customer.Email;
            _cityBox.Text = customer.City;
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

        Customer = new Customer(_id, name, email, city);
        DialogResult = DialogResult.OK;
        Close();
    }
}
