namespace Pagination.Demo.Views.UserControls;

partial class Paginator
{
    /// <summary>Variable del diseñador necesaria.</summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>Limpiar los recursos que se estén usando.</summary>
    /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _style.Changed -= OnStyleChanged;
            _buttonFont?.Dispose();
            components?.Dispose();
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
        _layout = new TableLayoutPanel();
        _previousButton = new Button();
        _pagesPanel = new FlowLayoutPanel();
        _nextButton = new Button();
        _layout.SuspendLayout();
        SuspendLayout();
        // 
        // _layout
        // 
        _layout.ColumnCount = 3;
        _layout.ColumnStyles.Add(new ColumnStyle());
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _layout.ColumnStyles.Add(new ColumnStyle());
        _layout.Controls.Add(_previousButton, 0, 0);
        _layout.Controls.Add(_pagesPanel, 1, 0);
        _layout.Controls.Add(_nextButton, 2, 0);
        _layout.Dock = DockStyle.Fill;
        _layout.Location = new Point(0, 0);
        _layout.Name = "_layout";
        _layout.RowCount = 1;
        _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layout.Size = new Size(240, 40);
        _layout.TabIndex = 0;
        // 
        // _previousButton
        // 
        _previousButton.FlatStyle = FlatStyle.Flat;
        _previousButton.Location = new Point(0, 3);
        _previousButton.Margin = new Padding(0, 3, 6, 3);
        _previousButton.Name = "_previousButton";
        _previousButton.Size = new Size(36, 34);
        _previousButton.TabIndex = 0;
        _previousButton.Text = "‹";
        _previousButton.UseVisualStyleBackColor = false;
        // 
        // _pagesPanel
        // 
        _pagesPanel.Anchor = AnchorStyles.None;
        _pagesPanel.AutoSize = true;
        _pagesPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pagesPanel.Location = new Point(42, 0);
        _pagesPanel.Margin = new Padding(0);
        _pagesPanel.Name = "_pagesPanel";
        _pagesPanel.Size = new Size(156, 40);
        _pagesPanel.TabIndex = 1;
        _pagesPanel.WrapContents = false;
        // 
        // _nextButton
        // 
        _nextButton.FlatStyle = FlatStyle.Flat;
        _nextButton.Location = new Point(204, 3);
        _nextButton.Margin = new Padding(6, 3, 0, 3);
        _nextButton.Name = "_nextButton";
        _nextButton.Size = new Size(36, 34);
        _nextButton.TabIndex = 2;
        _nextButton.Text = "›";
        _nextButton.UseVisualStyleBackColor = false;
        // 
        // Paginator
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_layout);
        Cursor = Cursors.Hand;
        MinimumSize = new Size(240, 40);
        Name = "Paginator";
        Size = new Size(240, 40);
        _layout.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _layout;
    private Button _previousButton;
    private FlowLayoutPanel _pagesPanel;
    private Button _nextButton;
}
