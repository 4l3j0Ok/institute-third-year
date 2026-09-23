namespace Pagination.Demo;

partial class ClienteEditForm
{
    /// <summary>Variable del diseñador necesaria.</summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>Limpiar los recursos que se estén usando.</summary>
    /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Código generado por el Diseñador de Windows Forms

    /// <summary>
    /// Método necesario para admitir el Diseñador. No se puede modificar
    /// el contenido de este método con el editor de código.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        _errorProvider = new ErrorProvider(components);
        _layout = new TableLayoutPanel();
        _nameLabel = new Label();
        _nameBox = new TextBox();
        _emailLabel = new Label();
        _emailBox = new TextBox();
        _cityLabel = new Label();
        _cityBox = new TextBox();
        _buttons = new FlowLayoutPanel();
        _saveButton = new Button();
        _cancelButton = new Button();
        ((System.ComponentModel.ISupportInitialize)_errorProvider).BeginInit();
        _layout.SuspendLayout();
        _buttons.SuspendLayout();
        SuspendLayout();
        //
        // _errorProvider
        //
        _errorProvider.ContainerControl = this;
        //
        // _layout
        //
        _layout.ColumnCount = 2;
        _layout.ColumnStyles.Add(new ColumnStyle());
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _layout.Controls.Add(_nameLabel, 0, 0);
        _layout.Controls.Add(_nameBox, 1, 0);
        _layout.Controls.Add(_emailLabel, 0, 1);
        _layout.Controls.Add(_emailBox, 1, 1);
        _layout.Controls.Add(_cityLabel, 0, 2);
        _layout.Controls.Add(_cityBox, 1, 2);
        _layout.Controls.Add(_buttons, 1, 3);
        _layout.Dock = DockStyle.Fill;
        _layout.Location = new Point(16, 16);
        _layout.Name = "_layout";
        _layout.Padding = new Padding(4);
        _layout.RowCount = 4;
        _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        _layout.Size = new Size(348, 179);
        _layout.TabIndex = 0;
        //
        // _nameLabel
        //
        _nameLabel.Anchor = AnchorStyles.Left;
        _nameLabel.AutoSize = true;
        _nameLabel.Location = new Point(7, 12);
        _nameLabel.Name = "_nameLabel";
        _nameLabel.Size = new Size(51, 15);
        _nameLabel.TabIndex = 0;
        _nameLabel.Text = "Nombre";
        //
        // _nameBox
        //
        _nameBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _nameBox.Location = new Point(84, 9);
        _nameBox.MaxLength = 120;
        _nameBox.Name = "_nameBox";
        _nameBox.Size = new Size(253, 23);
        _nameBox.TabIndex = 1;
        //
        // _emailLabel
        //
        _emailLabel.Anchor = AnchorStyles.Left;
        _emailLabel.AutoSize = true;
        _emailLabel.Location = new Point(7, 52);
        _emailLabel.Name = "_emailLabel";
        _emailLabel.Size = new Size(38, 15);
        _emailLabel.TabIndex = 2;
        _emailLabel.Text = "Email";
        //
        // _emailBox
        //
        _emailBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _emailBox.Location = new Point(84, 49);
        _emailBox.MaxLength = 160;
        _emailBox.Name = "_emailBox";
        _emailBox.Size = new Size(253, 23);
        _emailBox.TabIndex = 3;
        //
        // _cityLabel
        //
        _cityLabel.Anchor = AnchorStyles.Left;
        _cityLabel.AutoSize = true;
        _cityLabel.Location = new Point(7, 92);
        _cityLabel.Name = "_cityLabel";
        _cityLabel.Size = new Size(37, 15);
        _cityLabel.TabIndex = 4;
        _cityLabel.Text = "Ciudad";
        //
        // _cityBox
        //
        _cityBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cityBox.Location = new Point(84, 89);
        _cityBox.MaxLength = 120;
        _cityBox.Name = "_cityBox";
        _cityBox.Size = new Size(253, 23);
        _cityBox.TabIndex = 5;
        //
        // _buttons
        //
        _buttons.Anchor = AnchorStyles.Right;
        _buttons.AutoSize = true;
        _buttons.Controls.Add(_cancelButton);
        _buttons.Controls.Add(_saveButton);
        _buttons.FlowDirection = FlowDirection.RightToLeft;
        _buttons.Location = new Point(150, 132);
        _buttons.Name = "_buttons";
        _buttons.Size = new Size(187, 32);
        _buttons.TabIndex = 6;
        //
        // _saveButton
        //
        _saveButton.Location = new Point(103, 3);
        _saveButton.Name = "_saveButton";
        _saveButton.Size = new Size(81, 26);
        _saveButton.TabIndex = 0;
        _saveButton.Text = "Guardar";
        _saveButton.UseVisualStyleBackColor = true;
        //
        // _cancelButton
        //
        _cancelButton.DialogResult = DialogResult.Cancel;
        _cancelButton.Location = new Point(16, 3);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.Size = new Size(81, 26);
        _cancelButton.TabIndex = 1;
        _cancelButton.Text = "Cancelar";
        _cancelButton.UseVisualStyleBackColor = true;
        //
        // ClienteEditForm
        //
        AcceptButton = _saveButton;
        CancelButton = _cancelButton;
        ClientSize = new Size(380, 211);
        Controls.Add(_layout);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ClienteEditForm";
        Padding = new Padding(16);
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Cliente";
        ((System.ComponentModel.ISupportInitialize)_errorProvider).EndInit();
        _layout.ResumeLayout(false);
        _layout.PerformLayout();
        _buttons.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private ErrorProvider _errorProvider;
    private TableLayoutPanel _layout;
    private Label _nameLabel;
    private TextBox _nameBox;
    private Label _emailLabel;
    private TextBox _emailBox;
    private Label _cityLabel;
    private TextBox _cityBox;
    private FlowLayoutPanel _buttons;
    private Button _saveButton;
    private Button _cancelButton;
}
