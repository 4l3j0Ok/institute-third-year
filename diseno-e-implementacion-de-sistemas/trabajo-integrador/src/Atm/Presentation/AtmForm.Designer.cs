#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Atm.Presentation;

partial class AtmForm
{
    /// <summary>Required designer variable.</summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
    private void InitializeComponent()
    {
        lblBrand = new Label();
        pnlBezel = new Panel();
        pnlScreen = new Panel();
        lblMessage = new Label();
        lblLabel0 = new Label();
        lblLabel1 = new Label();
        lblLabel2 = new Label();
        lblLabel3 = new Label();
        lblLabel4 = new Label();
        lblLabel5 = new Label();
        lblLabel6 = new Label();
        lblLabel7 = new Label();
        btnSide0 = new Button();
        btnSide1 = new Button();
        btnSide2 = new Button();
        btnSide3 = new Button();
        btnSide4 = new Button();
        btnSide5 = new Button();
        btnSide6 = new Button();
        btnSide7 = new Button();
        btnDigit1 = new Button();
        btnDigit2 = new Button();
        btnDigit3 = new Button();
        btnDigit4 = new Button();
        btnDigit5 = new Button();
        btnDigit6 = new Button();
        btnDigit7 = new Button();
        btnDigit8 = new Button();
        btnDigit9 = new Button();
        btnDigit0 = new Button();
        btnClear = new Button();
        btnAccept = new Button();
        btnCancel = new Button();
        lblCardCaption = new Label();
        cmbCard = new ComboBox();
        btnCard = new Button();
        lblReceiptCaption = new Label();
        lblReceipt = new Label();
        lblCashCaption = new Label();
        lblCash = new Label();
        pnlBezel.SuspendLayout();
        pnlScreen.SuspendLayout();
        SuspendLayout();
        // 
        // lblBrand
        // 
        lblBrand.AutoSize = false;
        lblBrand.Location = new Point(22, 12);
        lblBrand.Size = new Size(516, 36);
        lblBrand.Text = "BANCO ISFT 93";
        lblBrand.TextAlign = ContentAlignment.MiddleCenter;
        lblBrand.ForeColor = System.Drawing.ColorTranslator.FromHtml("#14305C");
        lblBrand.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblBrand.Name = "lblBrand";
        // 
        // pnlBezel
        // 
        pnlBezel.Location = new Point(22, 56);
        pnlBezel.Size = new Size(516, 402);
        pnlBezel.BackColor = System.Drawing.ColorTranslator.FromHtml("#20242C");
        pnlBezel.Name = "pnlBezel";
        // 
        // pnlScreen
        // 
        pnlScreen.Location = new Point(64, 10);
        pnlScreen.Size = new Size(388, 382);
        pnlScreen.BackColor = System.Drawing.ColorTranslator.FromHtml("#06222B");
        pnlScreen.Name = "pnlScreen";
        // 
        // lblMessage
        // 
        lblMessage.AutoSize = false;
        lblMessage.Location = new Point(0, 0);
        lblMessage.Size = new Size(388, 190);
        lblMessage.TextAlign = ContentAlignment.MiddleCenter;
        lblMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#7DFFC0");
        lblMessage.Font = new Font("Consolas", 13F, FontStyle.Bold);
        lblMessage.Padding = new Padding(14);
        lblMessage.Name = "lblMessage";
        // 
        // lblLabel0
        // 
        lblLabel0.AutoSize = false;
        lblLabel0.Location = new Point(0, 190);
        lblLabel0.Size = new Size(194, 48);
        lblLabel0.TextAlign = ContentAlignment.MiddleLeft;
        lblLabel0.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblLabel0.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblLabel0.Padding = new Padding(8, 0, 8, 0);
        lblLabel0.Name = "lblLabel0";
        // 
        // lblLabel1
        // 
        lblLabel1.AutoSize = false;
        lblLabel1.Location = new Point(0, 238);
        lblLabel1.Size = new Size(194, 48);
        lblLabel1.TextAlign = ContentAlignment.MiddleLeft;
        lblLabel1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblLabel1.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblLabel1.Padding = new Padding(8, 0, 8, 0);
        lblLabel1.Name = "lblLabel1";
        // 
        // lblLabel2
        // 
        lblLabel2.AutoSize = false;
        lblLabel2.Location = new Point(0, 286);
        lblLabel2.Size = new Size(194, 48);
        lblLabel2.TextAlign = ContentAlignment.MiddleLeft;
        lblLabel2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblLabel2.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblLabel2.Padding = new Padding(8, 0, 8, 0);
        lblLabel2.Name = "lblLabel2";
        // 
        // lblLabel3
        // 
        lblLabel3.AutoSize = false;
        lblLabel3.Location = new Point(0, 334);
        lblLabel3.Size = new Size(194, 48);
        lblLabel3.TextAlign = ContentAlignment.MiddleLeft;
        lblLabel3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblLabel3.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblLabel3.Padding = new Padding(8, 0, 8, 0);
        lblLabel3.Name = "lblLabel3";
        // 
        // lblLabel4
        // 
        lblLabel4.AutoSize = false;
        lblLabel4.Location = new Point(194, 190);
        lblLabel4.Size = new Size(194, 48);
        lblLabel4.TextAlign = ContentAlignment.MiddleRight;
        lblLabel4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblLabel4.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblLabel4.Padding = new Padding(8, 0, 8, 0);
        lblLabel4.Name = "lblLabel4";
        // 
        // lblLabel5
        // 
        lblLabel5.AutoSize = false;
        lblLabel5.Location = new Point(194, 238);
        lblLabel5.Size = new Size(194, 48);
        lblLabel5.TextAlign = ContentAlignment.MiddleRight;
        lblLabel5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblLabel5.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblLabel5.Padding = new Padding(8, 0, 8, 0);
        lblLabel5.Name = "lblLabel5";
        // 
        // lblLabel6
        // 
        lblLabel6.AutoSize = false;
        lblLabel6.Location = new Point(194, 286);
        lblLabel6.Size = new Size(194, 48);
        lblLabel6.TextAlign = ContentAlignment.MiddleRight;
        lblLabel6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblLabel6.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblLabel6.Padding = new Padding(8, 0, 8, 0);
        lblLabel6.Name = "lblLabel6";
        // 
        // lblLabel7
        // 
        lblLabel7.AutoSize = false;
        lblLabel7.Location = new Point(194, 334);
        lblLabel7.Size = new Size(194, 48);
        lblLabel7.TextAlign = ContentAlignment.MiddleRight;
        lblLabel7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblLabel7.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblLabel7.Padding = new Padding(8, 0, 8, 0);
        lblLabel7.Name = "lblLabel7";
        // 
        // btnSide0
        // 
        btnSide0.Location = new Point(10, 207);
        btnSide0.Size = new Size(46, 34);
        btnSide0.Tag = "0";
        btnSide0.UseVisualStyleBackColor = false;
        btnSide0.FlatStyle = FlatStyle.Flat;
        btnSide0.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnSide0.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnSide0.FlatAppearance.BorderSize = 2;
        btnSide0.TabStop = false;
        btnSide0.Name = "btnSide0";
        btnSide0.Click += OnSideButtonClick;
        // 
        // btnSide1
        // 
        btnSide1.Location = new Point(10, 255);
        btnSide1.Size = new Size(46, 34);
        btnSide1.Tag = "1";
        btnSide1.UseVisualStyleBackColor = false;
        btnSide1.FlatStyle = FlatStyle.Flat;
        btnSide1.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnSide1.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnSide1.FlatAppearance.BorderSize = 2;
        btnSide1.TabStop = false;
        btnSide1.Name = "btnSide1";
        btnSide1.Click += OnSideButtonClick;
        // 
        // btnSide2
        // 
        btnSide2.Location = new Point(10, 303);
        btnSide2.Size = new Size(46, 34);
        btnSide2.Tag = "2";
        btnSide2.UseVisualStyleBackColor = false;
        btnSide2.FlatStyle = FlatStyle.Flat;
        btnSide2.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnSide2.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnSide2.FlatAppearance.BorderSize = 2;
        btnSide2.TabStop = false;
        btnSide2.Name = "btnSide2";
        btnSide2.Click += OnSideButtonClick;
        // 
        // btnSide3
        // 
        btnSide3.Location = new Point(10, 351);
        btnSide3.Size = new Size(46, 34);
        btnSide3.Tag = "3";
        btnSide3.UseVisualStyleBackColor = false;
        btnSide3.FlatStyle = FlatStyle.Flat;
        btnSide3.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnSide3.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnSide3.FlatAppearance.BorderSize = 2;
        btnSide3.TabStop = false;
        btnSide3.Name = "btnSide3";
        btnSide3.Click += OnSideButtonClick;
        // 
        // btnSide4
        // 
        btnSide4.Location = new Point(460, 207);
        btnSide4.Size = new Size(46, 34);
        btnSide4.Tag = "4";
        btnSide4.UseVisualStyleBackColor = false;
        btnSide4.FlatStyle = FlatStyle.Flat;
        btnSide4.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnSide4.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnSide4.FlatAppearance.BorderSize = 2;
        btnSide4.TabStop = false;
        btnSide4.Name = "btnSide4";
        btnSide4.Click += OnSideButtonClick;
        // 
        // btnSide5
        // 
        btnSide5.Location = new Point(460, 255);
        btnSide5.Size = new Size(46, 34);
        btnSide5.Tag = "5";
        btnSide5.UseVisualStyleBackColor = false;
        btnSide5.FlatStyle = FlatStyle.Flat;
        btnSide5.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnSide5.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnSide5.FlatAppearance.BorderSize = 2;
        btnSide5.TabStop = false;
        btnSide5.Name = "btnSide5";
        btnSide5.Click += OnSideButtonClick;
        // 
        // btnSide6
        // 
        btnSide6.Location = new Point(460, 303);
        btnSide6.Size = new Size(46, 34);
        btnSide6.Tag = "6";
        btnSide6.UseVisualStyleBackColor = false;
        btnSide6.FlatStyle = FlatStyle.Flat;
        btnSide6.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnSide6.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnSide6.FlatAppearance.BorderSize = 2;
        btnSide6.TabStop = false;
        btnSide6.Name = "btnSide6";
        btnSide6.Click += OnSideButtonClick;
        // 
        // btnSide7
        // 
        btnSide7.Location = new Point(460, 351);
        btnSide7.Size = new Size(46, 34);
        btnSide7.Tag = "7";
        btnSide7.UseVisualStyleBackColor = false;
        btnSide7.FlatStyle = FlatStyle.Flat;
        btnSide7.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnSide7.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnSide7.FlatAppearance.BorderSize = 2;
        btnSide7.TabStop = false;
        btnSide7.Name = "btnSide7";
        btnSide7.Click += OnSideButtonClick;
        // 
        // btnDigit1
        // 
        btnDigit1.Location = new Point(22, 478);
        btnDigit1.Size = new Size(92, 48);
        btnDigit1.Text = "1";
        btnDigit1.UseVisualStyleBackColor = false;
        btnDigit1.FlatStyle = FlatStyle.Flat;
        btnDigit1.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit1.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit1.FlatAppearance.BorderSize = 2;
        btnDigit1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit1.TabStop = false;
        btnDigit1.Name = "btnDigit1";
        btnDigit1.Click += OnDigitClick;
        // 
        // btnDigit2
        // 
        btnDigit2.Location = new Point(122, 478);
        btnDigit2.Size = new Size(92, 48);
        btnDigit2.Text = "2";
        btnDigit2.UseVisualStyleBackColor = false;
        btnDigit2.FlatStyle = FlatStyle.Flat;
        btnDigit2.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit2.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit2.FlatAppearance.BorderSize = 2;
        btnDigit2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit2.TabStop = false;
        btnDigit2.Name = "btnDigit2";
        btnDigit2.Click += OnDigitClick;
        // 
        // btnDigit3
        // 
        btnDigit3.Location = new Point(222, 478);
        btnDigit3.Size = new Size(92, 48);
        btnDigit3.Text = "3";
        btnDigit3.UseVisualStyleBackColor = false;
        btnDigit3.FlatStyle = FlatStyle.Flat;
        btnDigit3.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit3.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit3.FlatAppearance.BorderSize = 2;
        btnDigit3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit3.TabStop = false;
        btnDigit3.Name = "btnDigit3";
        btnDigit3.Click += OnDigitClick;
        // 
        // btnDigit4
        // 
        btnDigit4.Location = new Point(22, 534);
        btnDigit4.Size = new Size(92, 48);
        btnDigit4.Text = "4";
        btnDigit4.UseVisualStyleBackColor = false;
        btnDigit4.FlatStyle = FlatStyle.Flat;
        btnDigit4.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit4.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit4.FlatAppearance.BorderSize = 2;
        btnDigit4.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit4.TabStop = false;
        btnDigit4.Name = "btnDigit4";
        btnDigit4.Click += OnDigitClick;
        // 
        // btnDigit5
        // 
        btnDigit5.Location = new Point(122, 534);
        btnDigit5.Size = new Size(92, 48);
        btnDigit5.Text = "5";
        btnDigit5.UseVisualStyleBackColor = false;
        btnDigit5.FlatStyle = FlatStyle.Flat;
        btnDigit5.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit5.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit5.FlatAppearance.BorderSize = 2;
        btnDigit5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit5.TabStop = false;
        btnDigit5.Name = "btnDigit5";
        btnDigit5.Click += OnDigitClick;
        // 
        // btnDigit6
        // 
        btnDigit6.Location = new Point(222, 534);
        btnDigit6.Size = new Size(92, 48);
        btnDigit6.Text = "6";
        btnDigit6.UseVisualStyleBackColor = false;
        btnDigit6.FlatStyle = FlatStyle.Flat;
        btnDigit6.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit6.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit6.FlatAppearance.BorderSize = 2;
        btnDigit6.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit6.TabStop = false;
        btnDigit6.Name = "btnDigit6";
        btnDigit6.Click += OnDigitClick;
        // 
        // btnDigit7
        // 
        btnDigit7.Location = new Point(22, 590);
        btnDigit7.Size = new Size(92, 48);
        btnDigit7.Text = "7";
        btnDigit7.UseVisualStyleBackColor = false;
        btnDigit7.FlatStyle = FlatStyle.Flat;
        btnDigit7.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit7.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit7.FlatAppearance.BorderSize = 2;
        btnDigit7.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit7.TabStop = false;
        btnDigit7.Name = "btnDigit7";
        btnDigit7.Click += OnDigitClick;
        // 
        // btnDigit8
        // 
        btnDigit8.Location = new Point(122, 590);
        btnDigit8.Size = new Size(92, 48);
        btnDigit8.Text = "8";
        btnDigit8.UseVisualStyleBackColor = false;
        btnDigit8.FlatStyle = FlatStyle.Flat;
        btnDigit8.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit8.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit8.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit8.FlatAppearance.BorderSize = 2;
        btnDigit8.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit8.TabStop = false;
        btnDigit8.Name = "btnDigit8";
        btnDigit8.Click += OnDigitClick;
        // 
        // btnDigit9
        // 
        btnDigit9.Location = new Point(222, 590);
        btnDigit9.Size = new Size(92, 48);
        btnDigit9.Text = "9";
        btnDigit9.UseVisualStyleBackColor = false;
        btnDigit9.FlatStyle = FlatStyle.Flat;
        btnDigit9.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit9.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit9.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit9.FlatAppearance.BorderSize = 2;
        btnDigit9.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit9.TabStop = false;
        btnDigit9.Name = "btnDigit9";
        btnDigit9.Click += OnDigitClick;
        // 
        // btnDigit0
        // 
        btnDigit0.Location = new Point(122, 646);
        btnDigit0.Size = new Size(92, 48);
        btnDigit0.Text = "0";
        btnDigit0.UseVisualStyleBackColor = false;
        btnDigit0.FlatStyle = FlatStyle.Flat;
        btnDigit0.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigit0.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigit0.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigit0.FlatAppearance.BorderSize = 2;
        btnDigit0.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigit0.TabStop = false;
        btnDigit0.Name = "btnDigit0";
        btnDigit0.Click += OnDigitClick;
        // 
        // btnClear
        // 
        btnClear.Location = new Point(22, 646);
        btnClear.Size = new Size(92, 48);
        btnClear.Text = "BORRAR";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.BackColor = System.Drawing.ColorTranslator.FromHtml("#E6B422");
        btnClear.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnClear.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnClear.FlatAppearance.BorderSize = 2;
        btnClear.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        btnClear.TabStop = false;
        btnClear.Name = "btnClear";
        btnClear.Click += OnClearClick;
        // 
        // btnAccept
        // 
        btnAccept.Location = new Point(222, 646);
        btnAccept.Size = new Size(92, 48);
        btnAccept.Text = "ACEPTAR";
        btnAccept.UseVisualStyleBackColor = false;
        btnAccept.FlatStyle = FlatStyle.Flat;
        btnAccept.BackColor = System.Drawing.ColorTranslator.FromHtml("#2FA84F");
        btnAccept.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnAccept.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnAccept.FlatAppearance.BorderSize = 2;
        btnAccept.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        btnAccept.TabStop = false;
        btnAccept.Name = "btnAccept";
        btnAccept.Click += OnAcceptClick;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(22, 702);
        btnCancel.Size = new Size(292, 48);
        btnCancel.Text = "CANCELAR";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.BackColor = System.Drawing.ColorTranslator.FromHtml("#D1432F");
        btnCancel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnCancel.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnCancel.FlatAppearance.BorderSize = 2;
        btnCancel.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        btnCancel.TabStop = false;
        btnCancel.Name = "btnCancel";
        btnCancel.Click += OnCancelClick;
        // 
        // lblCardCaption
        // 
        lblCardCaption.Location = new Point(332, 478);
        lblCardCaption.Size = new Size(206, 16);
        lblCardCaption.Text = "TARJETA";
        lblCardCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#14305C");
        lblCardCaption.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblCardCaption.AutoSize = false;
        lblCardCaption.Name = "lblCardCaption";
        // 
        // cmbCard
        // 
        cmbCard.Location = new Point(332, 496);
        cmbCard.Size = new Size(206, 23);
        cmbCard.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCard.FormattingEnabled = true;
        cmbCard.Name = "cmbCard";
        // 
        // btnCard
        // 
        btnCard.Location = new Point(332, 526);
        btnCard.Size = new Size(206, 34);
        btnCard.Text = "Insertar tarjeta";
        btnCard.UseVisualStyleBackColor = false;
        btnCard.FlatStyle = FlatStyle.Flat;
        btnCard.BackColor = System.Drawing.ColorTranslator.FromHtml("#2A63B8");
        btnCard.ForeColor = Color.White;
        btnCard.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#17376B");
        btnCard.FlatAppearance.BorderSize = 2;
        btnCard.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnCard.TabStop = false;
        btnCard.Name = "btnCard";
        btnCard.Click += OnCardClick;
        // 
        // lblReceiptCaption
        // 
        lblReceiptCaption.Location = new Point(332, 572);
        lblReceiptCaption.Size = new Size(206, 16);
        lblReceiptCaption.Text = "COMPROBANTE";
        lblReceiptCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#14305C");
        lblReceiptCaption.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblReceiptCaption.AutoSize = false;
        lblReceiptCaption.Name = "lblReceiptCaption";
        // 
        // lblReceipt
        // 
        lblReceipt.AutoSize = false;
        lblReceipt.Location = new Point(332, 590);
        lblReceipt.Size = new Size(206, 120);
        lblReceipt.BackColor = System.Drawing.ColorTranslator.FromHtml("#FDFDF6");
        lblReceipt.ForeColor = System.Drawing.ColorTranslator.FromHtml("#222222");
        lblReceipt.BorderStyle = BorderStyle.FixedSingle;
        lblReceipt.Padding = new Padding(6);
        lblReceipt.Font = new Font("Consolas", 8.25F);
        lblReceipt.Name = "lblReceipt";
        // 
        // lblCashCaption
        // 
        lblCashCaption.Location = new Point(332, 716);
        lblCashCaption.Size = new Size(206, 16);
        lblCashCaption.Text = "SALIDA DE EFECTIVO";
        lblCashCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#14305C");
        lblCashCaption.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblCashCaption.AutoSize = false;
        lblCashCaption.Name = "lblCashCaption";
        // 
        // lblCash
        // 
        lblCash.AutoSize = false;
        lblCash.Location = new Point(332, 734);
        lblCash.Size = new Size(206, 34);
        lblCash.BackColor = System.Drawing.ColorTranslator.FromHtml("#101215");
        lblCash.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6DFF8A");
        lblCash.TextAlign = ContentAlignment.MiddleCenter;
        lblCash.Font = new Font("Consolas", 11.25F, FontStyle.Bold);
        lblCash.Name = "lblCash";
        // 
        // pnlScreen
        // 
        pnlScreen.Controls.Add(lblMessage);
        pnlScreen.Controls.Add(lblLabel0);
        pnlScreen.Controls.Add(lblLabel1);
        pnlScreen.Controls.Add(lblLabel2);
        pnlScreen.Controls.Add(lblLabel3);
        pnlScreen.Controls.Add(lblLabel4);
        pnlScreen.Controls.Add(lblLabel5);
        pnlScreen.Controls.Add(lblLabel6);
        pnlScreen.Controls.Add(lblLabel7);
        // 
        // pnlBezel
        // 
        pnlBezel.Controls.Add(pnlScreen);
        pnlBezel.Controls.Add(btnSide0);
        pnlBezel.Controls.Add(btnSide1);
        pnlBezel.Controls.Add(btnSide2);
        pnlBezel.Controls.Add(btnSide3);
        pnlBezel.Controls.Add(btnSide4);
        pnlBezel.Controls.Add(btnSide5);
        pnlBezel.Controls.Add(btnSide6);
        pnlBezel.Controls.Add(btnSide7);
        // 
        // AtmForm
        // 
        AutoScaleMode = AutoScaleMode.None;
        BackColor = System.Drawing.ColorTranslator.FromHtml("#B8BEC9");
        ClientSize = new Size(560, 790);
        Controls.Add(lblBrand);
        Controls.Add(pnlBezel);
        Controls.Add(btnDigit1);
        Controls.Add(btnDigit2);
        Controls.Add(btnDigit3);
        Controls.Add(btnDigit4);
        Controls.Add(btnDigit5);
        Controls.Add(btnDigit6);
        Controls.Add(btnDigit7);
        Controls.Add(btnDigit8);
        Controls.Add(btnDigit9);
        Controls.Add(btnDigit0);
        Controls.Add(btnClear);
        Controls.Add(btnAccept);
        Controls.Add(btnCancel);
        Controls.Add(lblCardCaption);
        Controls.Add(cmbCard);
        Controls.Add(btnCard);
        Controls.Add(lblReceiptCaption);
        Controls.Add(lblReceipt);
        Controls.Add(lblCashCaption);
        Controls.Add(lblCash);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "AtmForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Cajero Automático - Banco ISFT 93";
        pnlScreen.ResumeLayout(false);
        pnlBezel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Label lblBrand;
    private Panel pnlBezel;
    private Panel pnlScreen;
    private Label lblMessage;
    private Label lblLabel0;
    private Label lblLabel1;
    private Label lblLabel2;
    private Label lblLabel3;
    private Label lblLabel4;
    private Label lblLabel5;
    private Label lblLabel6;
    private Label lblLabel7;
    private Button btnSide0;
    private Button btnSide1;
    private Button btnSide2;
    private Button btnSide3;
    private Button btnSide4;
    private Button btnSide5;
    private Button btnSide6;
    private Button btnSide7;
    private Button btnDigit1;
    private Button btnDigit2;
    private Button btnDigit3;
    private Button btnDigit4;
    private Button btnDigit5;
    private Button btnDigit6;
    private Button btnDigit7;
    private Button btnDigit8;
    private Button btnDigit9;
    private Button btnDigit0;
    private Button btnClear;
    private Button btnAccept;
    private Button btnCancel;
    private Label lblCardCaption;
    private ComboBox cmbCard;
    private Button btnCard;
    private Label lblReceiptCaption;
    private Label lblReceipt;
    private Label lblCashCaption;
    private Label lblCash;
}
