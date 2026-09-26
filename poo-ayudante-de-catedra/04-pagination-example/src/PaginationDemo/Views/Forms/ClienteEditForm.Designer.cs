namespace Pagination.Demo.Views.Forms;

partial class ClienteEditForm
{
    private void InitializeComponent()
    {
        _layout = new TableLayoutPanel();
        _nameBox = new TextBox();
        _emailBox = new TextBox();
        _cityBox = new TextBox();
        _saveButton = new Button();
        _cancelButton = new Button();
        _layout.SuspendLayout();
        SuspendLayout();
        _layout.ColumnCount = 2;
        _layout.ColumnStyles.Add(new ColumnStyle());
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _layout.Controls.Add(new Label { AutoSize = true, Text = "Nombre" }, 0, 0);
        _layout.Controls.Add(_nameBox, 1, 0);
        _layout.Controls.Add(new Label { AutoSize = true, Text = "Email" }, 0, 1);
        _layout.Controls.Add(_emailBox, 1, 1);
        _layout.Controls.Add(new Label { AutoSize = true, Text = "Ciudad" }, 0, 2);
        _layout.Controls.Add(_cityBox, 1, 2);
        _layout.Controls.Add(_saveButton, 0, 3);
        _layout.Controls.Add(_cancelButton, 1, 3);
        _layout.Dock = DockStyle.Fill;
        _layout.Padding = new Padding(12);
        _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        _nameBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _emailBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cityBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _saveButton.Text = "Guardar";
        _saveButton.AutoSize = true;
        _cancelButton.DialogResult = DialogResult.Cancel;
        _cancelButton.Text = "Cancelar";
        _cancelButton.AutoSize = true;
        AcceptButton = _saveButton;
        CancelButton = _cancelButton;
        ClientSize = new Size(380, 190);
        Controls.Add(_layout);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        _layout.ResumeLayout(false);
        _layout.PerformLayout();
        ResumeLayout(false);
    }

    private TableLayoutPanel _layout;
    private TextBox _nameBox;
    private TextBox _emailBox;
    private TextBox _cityBox;
    private Button _saveButton;
    private Button _cancelButton;
}
