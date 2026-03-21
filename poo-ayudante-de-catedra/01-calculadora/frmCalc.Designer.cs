namespace poo_ayudante_de_catedra;

partial class frmCalc
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalc));
        tlpButtons = new TableLayoutPanel();
        btnNegate = new Button();
        btnCloseParenthesis = new Button();
        btnOpenParenthesis = new Button();
        btnClearAll = new Button();
        btnClear = new Button();
        btnZero = new Button();
        btnComa = new Button();
        btnOne = new Button();
        btnTwo = new Button();
        btnThree = new Button();
        btnEqual = new Button();
        btnFour = new Button();
        btnFive = new Button();
        btnSix = new Button();
        btnSeven = new Button();
        btnEight = new Button();
        btnNine = new Button();
        btnPlus = new Button();
        btnMinus = new Button();
        btnMultiply = new Button();
        btnDivide = new Button();
        btnPercentage = new Button();
        tlpDisplays = new TableLayoutPanel();
        tbCalc = new TextBox();
        tbResult = new TextBox();
        tlpButtons.SuspendLayout();
        tlpDisplays.SuspendLayout();
        SuspendLayout();
        // 
        // tlpButtons
        // 
        tlpButtons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tlpButtons.ColumnCount = 6;
        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666718F));
        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tlpButtons.Controls.Add(btnNegate, 4, 4);
        tlpButtons.Controls.Add(btnCloseParenthesis, 4, 3);
        tlpButtons.Controls.Add(btnOpenParenthesis, 4, 2);
        tlpButtons.Controls.Add(btnClearAll, 4, 1);
        tlpButtons.Controls.Add(btnClear, 4, 0);
        tlpButtons.Controls.Add(btnZero, 0, 4);
        tlpButtons.Controls.Add(btnComa, 2, 4);
        tlpButtons.Controls.Add(btnOne, 0, 3);
        tlpButtons.Controls.Add(btnTwo, 1, 3);
        tlpButtons.Controls.Add(btnThree, 2, 3);
        tlpButtons.Controls.Add(btnEqual, 3, 3);
        tlpButtons.Controls.Add(btnFour, 0, 2);
        tlpButtons.Controls.Add(btnFive, 1, 2);
        tlpButtons.Controls.Add(btnSix, 2, 2);
        tlpButtons.Controls.Add(btnSeven, 0, 1);
        tlpButtons.Controls.Add(btnEight, 1, 1);
        tlpButtons.Controls.Add(btnNine, 2, 1);
        tlpButtons.Controls.Add(btnPlus, 3, 1);
        tlpButtons.Controls.Add(btnMinus, 3, 0);
        tlpButtons.Controls.Add(btnMultiply, 2, 0);
        tlpButtons.Controls.Add(btnDivide, 1, 0);
        tlpButtons.Controls.Add(btnPercentage, 0, 0);
        tlpButtons.Location = new Point(20, 152);
        tlpButtons.Name = "tlpButtons";
        tlpButtons.RowCount = 5;
        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tlpButtons.Size = new Size(494, 297);
        tlpButtons.TabIndex = 0;
        // 
        // btnNegate
        // 
        btnNegate.BackColor = Color.FromArgb(49, 49, 49);
        tlpButtons.SetColumnSpan(btnNegate, 2);
        btnNegate.Dock = DockStyle.Fill;
        btnNegate.FlatAppearance.BorderSize = 0;
        btnNegate.FlatStyle = FlatStyle.Flat;
        btnNegate.Font = new Font("Arial", 15F);
        btnNegate.ForeColor = Color.White;
        btnNegate.Location = new Point(331, 239);
        btnNegate.Name = "btnNegate";
        btnNegate.Size = new Size(160, 55);
        btnNegate.TabIndex = 24;
        btnNegate.Text = "+/-";
        btnNegate.UseVisualStyleBackColor = false;
        btnNegate.Click += btnNegateClick;
        // 
        // btnCloseParenthesis
        // 
        btnCloseParenthesis.BackColor = Color.FromArgb(49, 49, 49);
        tlpButtons.SetColumnSpan(btnCloseParenthesis, 2);
        btnCloseParenthesis.Dock = DockStyle.Fill;
        btnCloseParenthesis.FlatAppearance.BorderSize = 0;
        btnCloseParenthesis.FlatStyle = FlatStyle.Flat;
        btnCloseParenthesis.Font = new Font("Arial", 15F);
        btnCloseParenthesis.ForeColor = Color.White;
        btnCloseParenthesis.Location = new Point(331, 180);
        btnCloseParenthesis.Name = "btnCloseParenthesis";
        btnCloseParenthesis.Size = new Size(160, 53);
        btnCloseParenthesis.TabIndex = 23;
        btnCloseParenthesis.Text = ")";
        btnCloseParenthesis.UseVisualStyleBackColor = false;
        btnCloseParenthesis.Click += btnAppendTextClick;
        // 
        // btnOpenParenthesis
        // 
        btnOpenParenthesis.BackColor = Color.FromArgb(49, 49, 49);
        tlpButtons.SetColumnSpan(btnOpenParenthesis, 2);
        btnOpenParenthesis.Dock = DockStyle.Fill;
        btnOpenParenthesis.FlatAppearance.BorderSize = 0;
        btnOpenParenthesis.FlatStyle = FlatStyle.Flat;
        btnOpenParenthesis.Font = new Font("Arial", 15F);
        btnOpenParenthesis.ForeColor = Color.White;
        btnOpenParenthesis.Location = new Point(331, 121);
        btnOpenParenthesis.Name = "btnOpenParenthesis";
        btnOpenParenthesis.Size = new Size(160, 53);
        btnOpenParenthesis.TabIndex = 22;
        btnOpenParenthesis.Text = "(";
        btnOpenParenthesis.UseVisualStyleBackColor = false;
        btnOpenParenthesis.Click += btnAppendTextClick;
        // 
        // btnClearAll
        // 
        btnClearAll.BackColor = Color.FromArgb(49, 49, 49);
        tlpButtons.SetColumnSpan(btnClearAll, 2);
        btnClearAll.Dock = DockStyle.Fill;
        btnClearAll.FlatAppearance.BorderSize = 0;
        btnClearAll.FlatStyle = FlatStyle.Flat;
        btnClearAll.Font = new Font("Arial", 15F);
        btnClearAll.ForeColor = Color.White;
        btnClearAll.Location = new Point(331, 62);
        btnClearAll.Name = "btnClearAll";
        btnClearAll.Size = new Size(160, 53);
        btnClearAll.TabIndex = 21;
        btnClearAll.Text = "AC";
        btnClearAll.UseVisualStyleBackColor = false;
        btnClearAll.Click += btnClearAllClick;
        // 
        // btnClear
        // 
        btnClear.BackColor = Color.FromArgb(49, 49, 49);
        tlpButtons.SetColumnSpan(btnClear, 2);
        btnClear.Dock = DockStyle.Fill;
        btnClear.FlatAppearance.BorderSize = 0;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Arial", 15F);
        btnClear.ForeColor = Color.White;
        btnClear.Location = new Point(331, 3);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(160, 53);
        btnClear.TabIndex = 20;
        btnClear.Text = "C";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Click += btnClearClick;
        // 
        // btnZero
        // 
        btnZero.BackColor = Color.FromArgb(49, 49, 49);
        tlpButtons.SetColumnSpan(btnZero, 2);
        btnZero.Dock = DockStyle.Fill;
        btnZero.FlatAppearance.BorderSize = 0;
        btnZero.FlatStyle = FlatStyle.Flat;
        btnZero.Font = new Font("Arial", 15F);
        btnZero.ForeColor = Color.White;
        btnZero.Location = new Point(3, 239);
        btnZero.Name = "btnZero";
        btnZero.Size = new Size(158, 55);
        btnZero.TabIndex = 19;
        btnZero.Text = "0";
        btnZero.UseVisualStyleBackColor = false;
        btnZero.Click += btnAppendTextClick;
        // 
        // btnComa
        // 
        btnComa.BackColor = Color.FromArgb(49, 49, 49);
        btnComa.Dock = DockStyle.Fill;
        btnComa.FlatAppearance.BorderSize = 0;
        btnComa.FlatStyle = FlatStyle.Flat;
        btnComa.Font = new Font("Arial", 15F);
        btnComa.ForeColor = Color.White;
        btnComa.Location = new Point(167, 239);
        btnComa.Name = "btnComa";
        btnComa.Size = new Size(76, 55);
        btnComa.TabIndex = 17;
        btnComa.Text = ",";
        btnComa.UseVisualStyleBackColor = false;
        btnComa.Click += btnAppendTextClick;
        // 
        // btnOne
        // 
        btnOne.BackColor = Color.FromArgb(49, 49, 49);
        btnOne.Dock = DockStyle.Fill;
        btnOne.FlatAppearance.BorderSize = 0;
        btnOne.FlatStyle = FlatStyle.Flat;
        btnOne.Font = new Font("Arial", 15F);
        btnOne.ForeColor = Color.White;
        btnOne.Location = new Point(3, 180);
        btnOne.Name = "btnOne";
        btnOne.Size = new Size(76, 53);
        btnOne.TabIndex = 15;
        btnOne.Text = "1";
        btnOne.UseVisualStyleBackColor = false;
        btnOne.Click += btnAppendTextClick;
        // 
        // btnTwo
        // 
        btnTwo.BackColor = Color.FromArgb(49, 49, 49);
        btnTwo.Dock = DockStyle.Fill;
        btnTwo.FlatAppearance.BorderSize = 0;
        btnTwo.FlatStyle = FlatStyle.Flat;
        btnTwo.Font = new Font("Arial", 15F);
        btnTwo.ForeColor = Color.White;
        btnTwo.Location = new Point(85, 180);
        btnTwo.Name = "btnTwo";
        btnTwo.Size = new Size(76, 53);
        btnTwo.TabIndex = 14;
        btnTwo.Text = "2";
        btnTwo.UseVisualStyleBackColor = false;
        btnTwo.Click += btnAppendTextClick;
        // 
        // btnThree
        // 
        btnThree.BackColor = Color.FromArgb(49, 49, 49);
        btnThree.Dock = DockStyle.Fill;
        btnThree.FlatAppearance.BorderSize = 0;
        btnThree.FlatStyle = FlatStyle.Flat;
        btnThree.Font = new Font("Arial", 15F);
        btnThree.ForeColor = Color.White;
        btnThree.Location = new Point(167, 180);
        btnThree.Name = "btnThree";
        btnThree.Size = new Size(76, 53);
        btnThree.TabIndex = 13;
        btnThree.Text = "3";
        btnThree.UseVisualStyleBackColor = false;
        btnThree.Click += btnAppendTextClick;
        // 
        // btnEqual
        // 
        btnEqual.BackColor = Color.FromArgb(49, 49, 49);
        btnEqual.Dock = DockStyle.Fill;
        btnEqual.FlatAppearance.BorderSize = 0;
        btnEqual.FlatStyle = FlatStyle.Flat;
        btnEqual.Font = new Font("Arial", 15F);
        btnEqual.ForeColor = Color.White;
        btnEqual.Location = new Point(249, 180);
        btnEqual.Name = "btnEqual";
        tlpButtons.SetRowSpan(btnEqual, 2);
        btnEqual.Size = new Size(76, 114);
        btnEqual.TabIndex = 12;
        btnEqual.Text = "=";
        btnEqual.UseVisualStyleBackColor = false;
        btnEqual.Click += btnEqualClick;
        // 
        // btnFour
        // 
        btnFour.BackColor = Color.FromArgb(49, 49, 49);
        btnFour.Dock = DockStyle.Fill;
        btnFour.FlatAppearance.BorderSize = 0;
        btnFour.FlatStyle = FlatStyle.Flat;
        btnFour.Font = new Font("Arial", 15F);
        btnFour.ForeColor = Color.White;
        btnFour.Location = new Point(3, 121);
        btnFour.Name = "btnFour";
        btnFour.Size = new Size(76, 53);
        btnFour.TabIndex = 11;
        btnFour.Text = "4";
        btnFour.UseVisualStyleBackColor = false;
        btnFour.Click += btnAppendTextClick;
        // 
        // btnFive
        // 
        btnFive.BackColor = Color.FromArgb(49, 49, 49);
        btnFive.Dock = DockStyle.Fill;
        btnFive.FlatAppearance.BorderSize = 0;
        btnFive.FlatStyle = FlatStyle.Flat;
        btnFive.Font = new Font("Arial", 15F);
        btnFive.ForeColor = Color.White;
        btnFive.Location = new Point(85, 121);
        btnFive.Name = "btnFive";
        btnFive.Size = new Size(76, 53);
        btnFive.TabIndex = 10;
        btnFive.Text = "5";
        btnFive.UseVisualStyleBackColor = false;
        btnFive.Click += btnAppendTextClick;
        // 
        // btnSix
        // 
        btnSix.BackColor = Color.FromArgb(49, 49, 49);
        btnSix.Dock = DockStyle.Fill;
        btnSix.FlatAppearance.BorderSize = 0;
        btnSix.FlatStyle = FlatStyle.Flat;
        btnSix.Font = new Font("Arial", 15F);
        btnSix.ForeColor = Color.White;
        btnSix.Location = new Point(167, 121);
        btnSix.Name = "btnSix";
        btnSix.Size = new Size(76, 53);
        btnSix.TabIndex = 9;
        btnSix.Text = "6";
        btnSix.UseVisualStyleBackColor = false;
        btnSix.Click += btnAppendTextClick;
        // 
        // btnSeven
        // 
        btnSeven.BackColor = Color.FromArgb(49, 49, 49);
        btnSeven.Dock = DockStyle.Fill;
        btnSeven.FlatAppearance.BorderSize = 0;
        btnSeven.FlatStyle = FlatStyle.Flat;
        btnSeven.Font = new Font("Arial", 15F);
        btnSeven.ForeColor = Color.White;
        btnSeven.Location = new Point(3, 62);
        btnSeven.Name = "btnSeven";
        btnSeven.Size = new Size(76, 53);
        btnSeven.TabIndex = 7;
        btnSeven.Text = "7";
        btnSeven.UseVisualStyleBackColor = false;
        btnSeven.Click += btnAppendTextClick;
        // 
        // btnEight
        // 
        btnEight.BackColor = Color.FromArgb(49, 49, 49);
        btnEight.Dock = DockStyle.Fill;
        btnEight.FlatAppearance.BorderSize = 0;
        btnEight.FlatStyle = FlatStyle.Flat;
        btnEight.Font = new Font("Arial", 15F);
        btnEight.ForeColor = Color.White;
        btnEight.Location = new Point(85, 62);
        btnEight.Name = "btnEight";
        btnEight.Size = new Size(76, 53);
        btnEight.TabIndex = 6;
        btnEight.Text = "8";
        btnEight.UseVisualStyleBackColor = false;
        btnEight.Click += btnAppendTextClick;
        // 
        // btnNine
        // 
        btnNine.BackColor = Color.FromArgb(49, 49, 49);
        btnNine.Dock = DockStyle.Fill;
        btnNine.FlatAppearance.BorderSize = 0;
        btnNine.FlatStyle = FlatStyle.Flat;
        btnNine.Font = new Font("Arial", 15F);
        btnNine.ForeColor = Color.White;
        btnNine.Location = new Point(167, 62);
        btnNine.Name = "btnNine";
        btnNine.Size = new Size(76, 53);
        btnNine.TabIndex = 5;
        btnNine.Text = "9";
        btnNine.UseVisualStyleBackColor = false;
        btnNine.Click += btnAppendTextClick;
        // 
        // btnPlus
        // 
        btnPlus.BackColor = Color.FromArgb(49, 49, 49);
        btnPlus.Dock = DockStyle.Fill;
        btnPlus.FlatAppearance.BorderSize = 0;
        btnPlus.FlatStyle = FlatStyle.Flat;
        btnPlus.Font = new Font("Arial", 15F);
        btnPlus.ForeColor = Color.White;
        btnPlus.Location = new Point(249, 62);
        btnPlus.Name = "btnPlus";
        tlpButtons.SetRowSpan(btnPlus, 2);
        btnPlus.Size = new Size(76, 112);
        btnPlus.TabIndex = 4;
        btnPlus.Text = "+";
        btnPlus.UseVisualStyleBackColor = false;
        btnPlus.Click += btnAppendTextClick;
        // 
        // btnMinus
        // 
        btnMinus.BackColor = Color.FromArgb(49, 49, 49);
        btnMinus.Dock = DockStyle.Fill;
        btnMinus.FlatAppearance.BorderSize = 0;
        btnMinus.FlatStyle = FlatStyle.Flat;
        btnMinus.Font = new Font("Arial", 15F);
        btnMinus.ForeColor = Color.White;
        btnMinus.Location = new Point(249, 3);
        btnMinus.Name = "btnMinus";
        btnMinus.Size = new Size(76, 53);
        btnMinus.TabIndex = 3;
        btnMinus.Text = "-";
        btnMinus.UseVisualStyleBackColor = false;
        btnMinus.Click += btnAppendTextClick;
        // 
        // btnMultiply
        // 
        btnMultiply.BackColor = Color.FromArgb(49, 49, 49);
        btnMultiply.Dock = DockStyle.Fill;
        btnMultiply.FlatAppearance.BorderSize = 0;
        btnMultiply.FlatStyle = FlatStyle.Flat;
        btnMultiply.Font = new Font("Arial", 15F);
        btnMultiply.ForeColor = Color.White;
        btnMultiply.Location = new Point(167, 3);
        btnMultiply.Name = "btnMultiply";
        btnMultiply.Size = new Size(76, 53);
        btnMultiply.TabIndex = 2;
        btnMultiply.Text = "*";
        btnMultiply.UseVisualStyleBackColor = false;
        btnMultiply.Click += btnAppendTextClick;
        // 
        // btnDivide
        // 
        btnDivide.BackColor = Color.FromArgb(49, 49, 49);
        btnDivide.Dock = DockStyle.Fill;
        btnDivide.FlatAppearance.BorderSize = 0;
        btnDivide.FlatStyle = FlatStyle.Flat;
        btnDivide.Font = new Font("Arial", 15F);
        btnDivide.ForeColor = Color.White;
        btnDivide.Location = new Point(85, 3);
        btnDivide.Name = "btnDivide";
        btnDivide.Size = new Size(76, 53);
        btnDivide.TabIndex = 1;
        btnDivide.Text = "/";
        btnDivide.UseVisualStyleBackColor = false;
        btnDivide.Click += btnAppendTextClick;
        // 
        // btnPercentage
        // 
        btnPercentage.BackColor = Color.FromArgb(49, 49, 49);
        btnPercentage.Dock = DockStyle.Fill;
        btnPercentage.FlatAppearance.BorderSize = 0;
        btnPercentage.FlatStyle = FlatStyle.Flat;
        btnPercentage.Font = new Font("Arial", 15F);
        btnPercentage.ForeColor = Color.White;
        btnPercentage.Location = new Point(3, 3);
        btnPercentage.Name = "btnPercentage";
        btnPercentage.Size = new Size(76, 53);
        btnPercentage.TabIndex = 0;
        btnPercentage.Text = "%";
        btnPercentage.UseVisualStyleBackColor = false;
        btnPercentage.Click += btnAppendTextClick;
        // 
        // tlpDisplays
        // 
        tlpDisplays.ColumnCount = 1;
        tlpDisplays.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tlpDisplays.Controls.Add(tbCalc, 0, 0);
        tlpDisplays.Controls.Add(tbResult, 0, 1);
        tlpDisplays.Dock = DockStyle.Top;
        tlpDisplays.Location = new Point(20, 20);
        tlpDisplays.Name = "tlpDisplays";
        tlpDisplays.RowCount = 2;
        tlpDisplays.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
        tlpDisplays.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
        tlpDisplays.Size = new Size(494, 129);
        tlpDisplays.TabIndex = 1;
        // 
        // tbCalc
        // 
        tbCalc.BackColor = Color.FromArgb(49, 49, 49);
        tbCalc.BorderStyle = BorderStyle.None;
        tbCalc.Dock = DockStyle.Fill;
        tbCalc.Font = new Font("Arial", 20F);
        tbCalc.ForeColor = Color.White;
        tbCalc.Location = new Point(3, 3);
        tbCalc.Name = "tbCalc";
        tbCalc.ReadOnly = true;
        tbCalc.Size = new Size(488, 31);
        tbCalc.TabIndex = 5;
        tbCalc.TextAlign = HorizontalAlignment.Right;
        // 
        // tbResult
        // 
        tbResult.BackColor = Color.FromArgb(49, 49, 49);
        tbResult.BorderStyle = BorderStyle.None;
        tbResult.Dock = DockStyle.Fill;
        tbResult.Font = new Font("Arial", 35F);
        tbResult.ForeColor = Color.White;
        tbResult.Location = new Point(3, 54);
        tbResult.Name = "tbResult";
        tbResult.ReadOnly = true;
        tbResult.Size = new Size(488, 54);
        tbResult.TabIndex = 4;
        tbResult.TextAlign = HorizontalAlignment.Right;
        // 
        // frmCalc
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(44, 44, 44);
        ClientSize = new Size(534, 481);
        Controls.Add(tlpDisplays);
        Controls.Add(tlpButtons);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "frmCalc";
        Padding = new Padding(20);
        StartPosition = FormStartPosition.Manual;
        Text = "Calculadora";
        tlpButtons.ResumeLayout(false);
        tlpDisplays.ResumeLayout(false);
        tlpDisplays.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tlpButtons;
    private Button btnZero;
    private Button btnComa;
    private Button btnOne;
    private Button btnTwo;
    private Button btnThree;
    private Button btnEqual;
    private Button btnFour;
    private Button btnFive;
    private Button btnSix;
    private Button btnSeven;
    private Button btnEight;
    private Button btnNine;
    private Button btnPlus;
    private Button btnMinus;
    private Button btnMultiply;
    private Button btnDivide;
    private Button btnPercentage;
    private Button btnNegate;
    private Button btnCloseParenthesis;
    private Button btnOpenParenthesis;
    private Button btnClearAll;
    private Button btnClear;
    private TableLayoutPanel tlpDisplays;
    private TextBox tbCalc;
    private TextBox tbResult;
}
