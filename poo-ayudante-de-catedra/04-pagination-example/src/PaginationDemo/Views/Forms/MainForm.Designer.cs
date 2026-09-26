namespace Pagination.Demo.Views.Forms;

partial class MainForm
{
    private void InitializeComponent()
    {
        _grid = new DataGridView();
        _footer = new TableLayoutPanel();
        _paginator = new FlowLayoutPanel();
        _previousButton = new Button();
        _previousPageButton = new Button();
        _currentPageButton = new Button();
        _nextPageButton = new Button();
        _nextButton = new Button();
        _status = new Label();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _footer.SuspendLayout();
        _paginator.SuspendLayout();
        SuspendLayout();
        // _grid
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _grid.BackgroundColor = Color.White;
        _grid.BorderStyle = BorderStyle.None;
        _grid.Dock = DockStyle.Fill;
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.TabIndex = 0;
        // _footer
        _footer.ColumnCount = 3;
        _footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _footer.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _footer.Controls.Add(_paginator, 1, 0);
        _footer.Controls.Add(_status, 0, 1);
        _footer.Dock = DockStyle.Bottom;
        _footer.Name = "_footer";
        _footer.Padding = new Padding(20, 8, 20, 8);
        _footer.RowCount = 2;
        _footer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _footer.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        _footer.Size = new Size(884, 80);
        _footer.TabIndex = 1;
        _footer.SetColumnSpan(_status, 3);
        // _paginator
        _paginator.AutoSize = true;
        _paginator.Controls.Add(_previousButton);
        _paginator.Controls.Add(_previousPageButton);
        _paginator.Controls.Add(_currentPageButton);
        _paginator.Controls.Add(_nextPageButton);
        _paginator.Controls.Add(_nextButton);
        _paginator.FlowDirection = FlowDirection.LeftToRight;
        _paginator.Name = "_paginator";
        _paginator.TabIndex = 0;
        _paginator.WrapContents = false;
        // _previousButton
        _previousButton.AutoSize = true;
        _previousButton.Name = "_previousButton";
        _previousButton.TabIndex = 0;
        _previousButton.Text = "<";
        _previousButton.UseVisualStyleBackColor = true;
        // _previousPageButton
        _previousPageButton.AutoSize = true;
        _previousPageButton.Name = "_previousPageButton";
        _previousPageButton.TabIndex = 1;
        _previousPageButton.UseVisualStyleBackColor = true;
        // _currentPageButton
        _currentPageButton.AutoSize = true;
        _currentPageButton.Name = "_currentPageButton";
        _currentPageButton.TabIndex = 2;
        _currentPageButton.UseVisualStyleBackColor = true;
        // _nextPageButton
        _nextPageButton.AutoSize = true;
        _nextPageButton.Name = "_nextPageButton";
        _nextPageButton.TabIndex = 3;
        _nextPageButton.UseVisualStyleBackColor = true;
        // _nextButton
        _nextButton.AutoSize = true;
        _nextButton.Name = "_nextButton";
        _nextButton.TabIndex = 4;
        _nextButton.Text = ">";
        _nextButton.UseVisualStyleBackColor = true;
        // _status
        _status.AutoSize = true;
        _status.Dock = DockStyle.Fill;
        _status.ForeColor = Color.FromArgb(71, 84, 103);
        _status.Name = "_status";
        _status.TabIndex = 1;
        _status.TextAlign = ContentAlignment.MiddleCenter;
        // MainForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(884, 585);
        Controls.Add(_grid);
        Controls.Add(_footer);
        MinimumSize = new Size(720, 480);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Pagination · Demo";
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _footer.ResumeLayout(false);
        _footer.PerformLayout();
        _paginator.ResumeLayout(false);
        _paginator.PerformLayout();
        ResumeLayout(false);
    }

    private DataGridView _grid;
    private TableLayoutPanel _footer;
    private FlowLayoutPanel _paginator;
    private Button _previousButton;
    private Button _previousPageButton;
    private Button _currentPageButton;
    private Button _nextPageButton;
    private Button _nextButton;
    private Label _status;
}
