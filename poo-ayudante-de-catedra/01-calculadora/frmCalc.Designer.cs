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
        tableLayoutPanel1 = new TableLayoutPanel();
        button22 = new Button();
        button21 = new Button();
        button20 = new Button();
        button18 = new Button();
        button12 = new Button();
        button17 = new Button();
        button19 = new Button();
        button13 = new Button();
        button14 = new Button();
        button15 = new Button();
        button16 = new Button();
        button9 = new Button();
        button10 = new Button();
        button11 = new Button();
        button5 = new Button();
        button6 = new Button();
        button7 = new Button();
        button8 = new Button();
        button4 = new Button();
        button3 = new Button();
        button2 = new Button();
        button1 = new Button();
        tableLayoutPanel2 = new TableLayoutPanel();
        tbCalc = new TextBox();
        tbResult = new TextBox();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tableLayoutPanel1.ColumnCount = 6;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666718F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
        tableLayoutPanel1.Controls.Add(button22, 4, 4);
        tableLayoutPanel1.Controls.Add(button21, 4, 3);
        tableLayoutPanel1.Controls.Add(button20, 4, 2);
        tableLayoutPanel1.Controls.Add(button18, 4, 1);
        tableLayoutPanel1.Controls.Add(button12, 4, 0);
        tableLayoutPanel1.Controls.Add(button17, 0, 4);
        tableLayoutPanel1.Controls.Add(button19, 2, 4);
        tableLayoutPanel1.Controls.Add(button13, 0, 3);
        tableLayoutPanel1.Controls.Add(button14, 1, 3);
        tableLayoutPanel1.Controls.Add(button15, 2, 3);
        tableLayoutPanel1.Controls.Add(button16, 3, 3);
        tableLayoutPanel1.Controls.Add(button9, 0, 2);
        tableLayoutPanel1.Controls.Add(button10, 1, 2);
        tableLayoutPanel1.Controls.Add(button11, 2, 2);
        tableLayoutPanel1.Controls.Add(button5, 0, 1);
        tableLayoutPanel1.Controls.Add(button6, 1, 1);
        tableLayoutPanel1.Controls.Add(button7, 2, 1);
        tableLayoutPanel1.Controls.Add(button8, 3, 1);
        tableLayoutPanel1.Controls.Add(button4, 3, 0);
        tableLayoutPanel1.Controls.Add(button3, 2, 0);
        tableLayoutPanel1.Controls.Add(button2, 1, 0);
        tableLayoutPanel1.Controls.Add(button1, 0, 0);
        tableLayoutPanel1.Location = new Point(20, 152);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 5;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel1.Size = new Size(494, 297);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // button22
        // 
        button22.BackColor = Color.FromArgb(49, 49, 49);
        tableLayoutPanel1.SetColumnSpan(button22, 2);
        button22.Dock = DockStyle.Fill;
        button22.FlatAppearance.BorderSize = 0;
        button22.FlatStyle = FlatStyle.Flat;
        button22.Font = new Font("Arial", 15F);
        button22.ForeColor = Color.White;
        button22.Location = new Point(331, 239);
        button22.Name = "button22";
        button22.Size = new Size(160, 55);
        button22.TabIndex = 24;
        button22.Text = "+/-";
        button22.UseVisualStyleBackColor = false;
        button22.Click += btnNegateClick;
        // 
        // button21
        // 
        button21.BackColor = Color.FromArgb(49, 49, 49);
        tableLayoutPanel1.SetColumnSpan(button21, 2);
        button21.Dock = DockStyle.Fill;
        button21.FlatAppearance.BorderSize = 0;
        button21.FlatStyle = FlatStyle.Flat;
        button21.Font = new Font("Arial", 15F);
        button21.ForeColor = Color.White;
        button21.Location = new Point(331, 180);
        button21.Name = "button21";
        button21.Size = new Size(160, 53);
        button21.TabIndex = 23;
        button21.Text = ")";
        button21.UseVisualStyleBackColor = false;
        button21.Click += btnAppendTextClick;
        // 
        // button20
        // 
        button20.BackColor = Color.FromArgb(49, 49, 49);
        tableLayoutPanel1.SetColumnSpan(button20, 2);
        button20.Dock = DockStyle.Fill;
        button20.FlatAppearance.BorderSize = 0;
        button20.FlatStyle = FlatStyle.Flat;
        button20.Font = new Font("Arial", 15F);
        button20.ForeColor = Color.White;
        button20.Location = new Point(331, 121);
        button20.Name = "button20";
        button20.Size = new Size(160, 53);
        button20.TabIndex = 22;
        button20.Text = "(";
        button20.UseVisualStyleBackColor = false;
        button20.Click += btnAppendTextClick;
        // 
        // button18
        // 
        button18.BackColor = Color.FromArgb(49, 49, 49);
        tableLayoutPanel1.SetColumnSpan(button18, 2);
        button18.Dock = DockStyle.Fill;
        button18.FlatAppearance.BorderSize = 0;
        button18.FlatStyle = FlatStyle.Flat;
        button18.Font = new Font("Arial", 15F);
        button18.ForeColor = Color.White;
        button18.Location = new Point(331, 62);
        button18.Name = "button18";
        button18.Size = new Size(160, 53);
        button18.TabIndex = 21;
        button18.Text = "AC";
        button18.UseVisualStyleBackColor = false;
        button18.Click += btnClearAllClick;
        // 
        // button12
        // 
        button12.BackColor = Color.FromArgb(49, 49, 49);
        tableLayoutPanel1.SetColumnSpan(button12, 2);
        button12.Dock = DockStyle.Fill;
        button12.FlatAppearance.BorderSize = 0;
        button12.FlatStyle = FlatStyle.Flat;
        button12.Font = new Font("Arial", 15F);
        button12.ForeColor = Color.White;
        button12.Location = new Point(331, 3);
        button12.Name = "button12";
        button12.Size = new Size(160, 53);
        button12.TabIndex = 20;
        button12.Text = "C";
        button12.UseVisualStyleBackColor = false;
        button12.Click += btnClearClick;
        // 
        // button17
        // 
        button17.BackColor = Color.FromArgb(49, 49, 49);
        tableLayoutPanel1.SetColumnSpan(button17, 2);
        button17.Dock = DockStyle.Fill;
        button17.FlatAppearance.BorderSize = 0;
        button17.FlatStyle = FlatStyle.Flat;
        button17.Font = new Font("Arial", 15F);
        button17.ForeColor = Color.White;
        button17.Location = new Point(3, 239);
        button17.Name = "button17";
        button17.Size = new Size(158, 55);
        button17.TabIndex = 19;
        button17.Text = "0";
        button17.UseVisualStyleBackColor = false;
        button17.Click += btnAppendTextClick;
        // 
        // button19
        // 
        button19.BackColor = Color.FromArgb(49, 49, 49);
        button19.Dock = DockStyle.Fill;
        button19.FlatAppearance.BorderSize = 0;
        button19.FlatStyle = FlatStyle.Flat;
        button19.Font = new Font("Arial", 15F);
        button19.ForeColor = Color.White;
        button19.Location = new Point(167, 239);
        button19.Name = "button19";
        button19.Size = new Size(76, 55);
        button19.TabIndex = 17;
        button19.Text = ",";
        button19.UseVisualStyleBackColor = false;
        button19.Click += btnAppendTextClick;
        // 
        // button13
        // 
        button13.BackColor = Color.FromArgb(49, 49, 49);
        button13.Dock = DockStyle.Fill;
        button13.FlatAppearance.BorderSize = 0;
        button13.FlatStyle = FlatStyle.Flat;
        button13.Font = new Font("Arial", 15F);
        button13.ForeColor = Color.White;
        button13.Location = new Point(3, 180);
        button13.Name = "button13";
        button13.Size = new Size(76, 53);
        button13.TabIndex = 15;
        button13.Text = "1";
        button13.UseVisualStyleBackColor = false;
        button13.Click += btnAppendTextClick;
        // 
        // button14
        // 
        button14.BackColor = Color.FromArgb(49, 49, 49);
        button14.Dock = DockStyle.Fill;
        button14.FlatAppearance.BorderSize = 0;
        button14.FlatStyle = FlatStyle.Flat;
        button14.Font = new Font("Arial", 15F);
        button14.ForeColor = Color.White;
        button14.Location = new Point(85, 180);
        button14.Name = "button14";
        button14.Size = new Size(76, 53);
        button14.TabIndex = 14;
        button14.Text = "2";
        button14.UseVisualStyleBackColor = false;
        button14.Click += btnAppendTextClick;
        // 
        // button15
        // 
        button15.BackColor = Color.FromArgb(49, 49, 49);
        button15.Dock = DockStyle.Fill;
        button15.FlatAppearance.BorderSize = 0;
        button15.FlatStyle = FlatStyle.Flat;
        button15.Font = new Font("Arial", 15F);
        button15.ForeColor = Color.White;
        button15.Location = new Point(167, 180);
        button15.Name = "button15";
        button15.Size = new Size(76, 53);
        button15.TabIndex = 13;
        button15.Text = "3";
        button15.UseVisualStyleBackColor = false;
        button15.Click += btnAppendTextClick;
        // 
        // button16
        // 
        button16.BackColor = Color.FromArgb(49, 49, 49);
        button16.Dock = DockStyle.Fill;
        button16.FlatAppearance.BorderSize = 0;
        button16.FlatStyle = FlatStyle.Flat;
        button16.Font = new Font("Arial", 15F);
        button16.ForeColor = Color.White;
        button16.Location = new Point(249, 180);
        button16.Name = "button16";
        tableLayoutPanel1.SetRowSpan(button16, 2);
        button16.Size = new Size(76, 114);
        button16.TabIndex = 12;
        button16.Text = "=";
        button16.UseVisualStyleBackColor = false;
        button16.Click += btnEqualClick;
        // 
        // button9
        // 
        button9.BackColor = Color.FromArgb(49, 49, 49);
        button9.Dock = DockStyle.Fill;
        button9.FlatAppearance.BorderSize = 0;
        button9.FlatStyle = FlatStyle.Flat;
        button9.Font = new Font("Arial", 15F);
        button9.ForeColor = Color.White;
        button9.Location = new Point(3, 121);
        button9.Name = "button9";
        button9.Size = new Size(76, 53);
        button9.TabIndex = 11;
        button9.Text = "4";
        button9.UseVisualStyleBackColor = false;
        button9.Click += btnAppendTextClick;
        // 
        // button10
        // 
        button10.BackColor = Color.FromArgb(49, 49, 49);
        button10.Dock = DockStyle.Fill;
        button10.FlatAppearance.BorderSize = 0;
        button10.FlatStyle = FlatStyle.Flat;
        button10.Font = new Font("Arial", 15F);
        button10.ForeColor = Color.White;
        button10.Location = new Point(85, 121);
        button10.Name = "button10";
        button10.Size = new Size(76, 53);
        button10.TabIndex = 10;
        button10.Text = "5";
        button10.UseVisualStyleBackColor = false;
        button10.Click += btnAppendTextClick;
        // 
        // button11
        // 
        button11.BackColor = Color.FromArgb(49, 49, 49);
        button11.Dock = DockStyle.Fill;
        button11.FlatAppearance.BorderSize = 0;
        button11.FlatStyle = FlatStyle.Flat;
        button11.Font = new Font("Arial", 15F);
        button11.ForeColor = Color.White;
        button11.Location = new Point(167, 121);
        button11.Name = "button11";
        button11.Size = new Size(76, 53);
        button11.TabIndex = 9;
        button11.Text = "6";
        button11.UseVisualStyleBackColor = false;
        button11.Click += btnAppendTextClick;
        // 
        // button5
        // 
        button5.BackColor = Color.FromArgb(49, 49, 49);
        button5.Dock = DockStyle.Fill;
        button5.FlatAppearance.BorderSize = 0;
        button5.FlatStyle = FlatStyle.Flat;
        button5.Font = new Font("Arial", 15F);
        button5.ForeColor = Color.White;
        button5.Location = new Point(3, 62);
        button5.Name = "button5";
        button5.Size = new Size(76, 53);
        button5.TabIndex = 7;
        button5.Text = "7";
        button5.UseVisualStyleBackColor = false;
        button5.Click += btnAppendTextClick;
        // 
        // button6
        // 
        button6.BackColor = Color.FromArgb(49, 49, 49);
        button6.Dock = DockStyle.Fill;
        button6.FlatAppearance.BorderSize = 0;
        button6.FlatStyle = FlatStyle.Flat;
        button6.Font = new Font("Arial", 15F);
        button6.ForeColor = Color.White;
        button6.Location = new Point(85, 62);
        button6.Name = "button6";
        button6.Size = new Size(76, 53);
        button6.TabIndex = 6;
        button6.Text = "8";
        button6.UseVisualStyleBackColor = false;
        button6.Click += btnAppendTextClick;
        // 
        // button7
        // 
        button7.BackColor = Color.FromArgb(49, 49, 49);
        button7.Dock = DockStyle.Fill;
        button7.FlatAppearance.BorderSize = 0;
        button7.FlatStyle = FlatStyle.Flat;
        button7.Font = new Font("Arial", 15F);
        button7.ForeColor = Color.White;
        button7.Location = new Point(167, 62);
        button7.Name = "button7";
        button7.Size = new Size(76, 53);
        button7.TabIndex = 5;
        button7.Text = "9";
        button7.UseVisualStyleBackColor = false;
        button7.Click += btnAppendTextClick;
        // 
        // button8
        // 
        button8.BackColor = Color.FromArgb(49, 49, 49);
        button8.Dock = DockStyle.Fill;
        button8.FlatAppearance.BorderSize = 0;
        button8.FlatStyle = FlatStyle.Flat;
        button8.Font = new Font("Arial", 15F);
        button8.ForeColor = Color.White;
        button8.Location = new Point(249, 62);
        button8.Name = "button8";
        tableLayoutPanel1.SetRowSpan(button8, 2);
        button8.Size = new Size(76, 112);
        button8.TabIndex = 4;
        button8.Text = "+";
        button8.UseVisualStyleBackColor = false;
        button8.Click += btnAppendTextClick;
        // 
        // button4
        // 
        button4.BackColor = Color.FromArgb(49, 49, 49);
        button4.Dock = DockStyle.Fill;
        button4.FlatAppearance.BorderSize = 0;
        button4.FlatStyle = FlatStyle.Flat;
        button4.Font = new Font("Arial", 15F);
        button4.ForeColor = Color.White;
        button4.Location = new Point(249, 3);
        button4.Name = "button4";
        button4.Size = new Size(76, 53);
        button4.TabIndex = 3;
        button4.Text = "-";
        button4.UseVisualStyleBackColor = false;
        button4.Click += btnAppendTextClick;
        // 
        // button3
        // 
        button3.BackColor = Color.FromArgb(49, 49, 49);
        button3.Dock = DockStyle.Fill;
        button3.FlatAppearance.BorderSize = 0;
        button3.FlatStyle = FlatStyle.Flat;
        button3.Font = new Font("Arial", 15F);
        button3.ForeColor = Color.White;
        button3.Location = new Point(167, 3);
        button3.Name = "button3";
        button3.Size = new Size(76, 53);
        button3.TabIndex = 2;
        button3.Text = "*";
        button3.UseVisualStyleBackColor = false;
        button3.Click += btnAppendTextClick;
        // 
        // button2
        // 
        button2.BackColor = Color.FromArgb(49, 49, 49);
        button2.Dock = DockStyle.Fill;
        button2.FlatAppearance.BorderSize = 0;
        button2.FlatStyle = FlatStyle.Flat;
        button2.Font = new Font("Arial", 15F);
        button2.ForeColor = Color.White;
        button2.Location = new Point(85, 3);
        button2.Name = "button2";
        button2.Size = new Size(76, 53);
        button2.TabIndex = 1;
        button2.Text = "/";
        button2.UseVisualStyleBackColor = false;
        button2.Click += btnAppendTextClick;
        // 
        // button1
        // 
        button1.BackColor = Color.FromArgb(49, 49, 49);
        button1.Dock = DockStyle.Fill;
        button1.FlatAppearance.BorderSize = 0;
        button1.FlatStyle = FlatStyle.Flat;
        button1.Font = new Font("Arial", 15F);
        button1.ForeColor = Color.White;
        button1.Location = new Point(3, 3);
        button1.Name = "button1";
        button1.Size = new Size(76, 53);
        button1.TabIndex = 0;
        button1.Text = "%";
        button1.UseVisualStyleBackColor = false;
        button1.Click += btnAppendTextClick;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 1;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.Controls.Add(tbCalc, 0, 0);
        tableLayoutPanel2.Controls.Add(tbResult, 0, 1);
        tableLayoutPanel2.Dock = DockStyle.Top;
        tableLayoutPanel2.Location = new Point(20, 20);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 2;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
        tableLayoutPanel2.Size = new Size(494, 129);
        tableLayoutPanel2.TabIndex = 1;
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
        Controls.Add(tableLayoutPanel2);
        Controls.Add(tableLayoutPanel1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "frmCalc";
        Padding = new Padding(20);
        StartPosition = FormStartPosition.Manual;
        Text = "Calculadora";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private Button button17;
    private Button button19;
    private Button button13;
    private Button button14;
    private Button button15;
    private Button button16;
    private Button button9;
    private Button button10;
    private Button button11;
    private Button button5;
    private Button button6;
    private Button button7;
    private Button button8;
    private Button button4;
    private Button button3;
    private Button button2;
    private Button button1;
    private Button button22;
    private Button button21;
    private Button button20;
    private Button button18;
    private Button button12;
    private TableLayoutPanel tableLayoutPanel2;
    private TextBox tbCalc;
    private TextBox tbResult;
}
