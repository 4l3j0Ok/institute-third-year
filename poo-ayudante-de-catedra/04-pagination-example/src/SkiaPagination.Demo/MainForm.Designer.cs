namespace SkiaPagination.Demo;

partial class MainForm
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
        _grid = new DataGridView();
        _status = new Label();
        _paginator = new SkiaPagination.WinForms.SkiaPaginator();
        _footer = new TableLayoutPanel();
        _toolbar = new FlowLayoutPanel();
        _addButton = new Button();
        _editButton = new Button();
        _deleteButton = new Button();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _footer.SuspendLayout();
        _toolbar.SuspendLayout();
        SuspendLayout();
        //
        // _grid
        //
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _grid.BackgroundColor = Color.White;
        _grid.BorderStyle = BorderStyle.None;
        _grid.Dock = DockStyle.Fill;
        _grid.Location = new Point(0, 44);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(884, 461);
        _grid.TabIndex = 1;
        //
        // _status
        //
        _status.AutoSize = true;
        _status.Dock = DockStyle.Fill;
        _status.ForeColor = Color.FromArgb(71, 84, 103);
        _status.Location = new Point(583, 8);
        _status.Name = "_status";
        _status.Size = new Size(301, 40);
        _status.TabIndex = 1;
        _status.TextAlign = ContentAlignment.MiddleRight;
        //
        // _paginator
        //
        _paginator.CurrentPage = 1;
        _paginator.Dock = DockStyle.Fill;
        _paginator.Location = new Point(20, 8);
        _paginator.MinimumSize = new Size(240, 40);
        _paginator.Name = "_paginator";
        _paginator.PageSize = 10;
        _paginator.Size = new Size(563, 40);
        _paginator.TabIndex = 0;
        _paginator.TotalItems = 0;
        //
        // _footer
        //
        _footer.ColumnCount = 2;
        _footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        _footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        _footer.Controls.Add(_paginator, 0, 0);
        _footer.Controls.Add(_status, 1, 0);
        _footer.Dock = DockStyle.Bottom;
        _footer.Location = new Point(0, 505);
        _footer.Name = "_footer";
        _footer.Padding = new Padding(20, 8, 20, 8);
        _footer.RowCount = 1;
        _footer.Size = new Size(884, 56);
        _footer.TabIndex = 2;
        //
        // _toolbar
        //
        _toolbar.Controls.Add(_addButton);
        _toolbar.Controls.Add(_editButton);
        _toolbar.Controls.Add(_deleteButton);
        _toolbar.Dock = DockStyle.Top;
        _toolbar.Location = new Point(0, 0);
        _toolbar.Name = "_toolbar";
        _toolbar.Padding = new Padding(20, 8, 20, 8);
        _toolbar.Size = new Size(884, 44);
        _toolbar.TabIndex = 0;
        //
        // _addButton
        //
        _addButton.Location = new Point(23, 11);
        _addButton.Name = "_addButton";
        _addButton.Size = new Size(96, 28);
        _addButton.TabIndex = 0;
        _addButton.Text = "Nuevo";
        _addButton.UseVisualStyleBackColor = true;
        //
        // _editButton
        //
        _editButton.Enabled = false;
        _editButton.Location = new Point(125, 11);
        _editButton.Name = "_editButton";
        _editButton.Size = new Size(96, 28);
        _editButton.TabIndex = 1;
        _editButton.Text = "Editar";
        _editButton.UseVisualStyleBackColor = true;
        //
        // _deleteButton
        //
        _deleteButton.Enabled = false;
        _deleteButton.Location = new Point(227, 11);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.Size = new Size(96, 28);
        _deleteButton.TabIndex = 2;
        _deleteButton.Text = "Eliminar";
        _deleteButton.UseVisualStyleBackColor = true;
        //
        // MainForm
        //
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(884, 561);
        Controls.Add(_grid);
        Controls.Add(_toolbar);
        Controls.Add(_footer);
        MinimumSize = new Size(720, 480);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SkiaPagination · Demo";
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _footer.ResumeLayout(false);
        _toolbar.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private DataGridView _grid;
    private Label _status;
    private SkiaPagination.WinForms.SkiaPaginator _paginator;
    private TableLayoutPanel _footer;
    private FlowLayoutPanel _toolbar;
    private Button _addButton;
    private Button _editButton;
    private Button _deleteButton;
}
