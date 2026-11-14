#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cajero.Presentation;

partial class CajeroForm
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
        components = new System.ComponentModel.Container();
        pnlMarquee = new Panel();
        pnlBezel = new Panel();
        pnlScreen = new Panel();
        lblMessage = new Label();
        lblInput = new Label();
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
        pnlKeypad = new Panel();
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
        pnlCardReader = new Panel();
        cmbCard = new ComboBox();
        btnCard = new Button();
        lblReceiptCaption = new Label();
        pnlPrinter = new Panel();
        lblReceipt = new Label();
        lblCashCaption = new Label();
        pnlDispenser = new Panel();
        lblCash = new Label();
        tmrBlink = new System.Windows.Forms.Timer(components);
        pnlBezel.SuspendLayout();
        pnlScreen.SuspendLayout();
        pnlKeypad.SuspendLayout();
        pnlDispenser.SuspendLayout();
        SuspendLayout();
        // 
        // pnlMarquee
        // 
        pnlMarquee.Location = new Point(0, 0);
        pnlMarquee.Size = new Size(620, 70);
        pnlMarquee.Name = "pnlMarquee";
        pnlMarquee.Paint += OnMarqueePaint;
        // 
        // pnlBezel
        // 
        pnlBezel.Location = new Point(24, 88);
        pnlBezel.Size = new Size(572, 428);
        pnlBezel.BackColor = Color.Transparent;
        pnlBezel.Name = "pnlBezel";
        pnlBezel.Paint += OnBezelPaint;
        // 
        // pnlScreen
        // 
        pnlScreen.Location = new Point(72, 18);
        pnlScreen.Size = new Size(428, 392);
        pnlScreen.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        pnlScreen.Name = "pnlScreen";
        pnlScreen.Paint += OnScreenPaint;
        // 
        // lblMessage
        // 
        lblMessage.AutoSize = false;
        lblMessage.Location = new Point(0, 44);
        lblMessage.Size = new Size(428, 150);
        lblMessage.BackColor = Color.Transparent;
        lblMessage.TextAlign = ContentAlignment.MiddleCenter;
        lblMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B3B3B");
        lblMessage.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblMessage.Padding = new Padding(16, 6, 16, 6);
        lblMessage.Name = "lblMessage";
        // 
        // lblInput
        // 
        lblInput.AutoSize = false;
        lblInput.Location = new Point(44, 142);
        lblInput.Size = new Size(340, 44);
        lblInput.BackColor = Color.White;
        lblInput.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EC0000");
        lblInput.TextAlign = ContentAlignment.MiddleCenter;
        lblInput.Font = new Font("Consolas", 16F, FontStyle.Bold);
        lblInput.Visible = false;
        lblInput.Name = "lblInput";
        lblInput.Paint += OnInputPaint;
        // 
        // lblLabel0
        // 
        lblLabel0.AutoSize = false;
        lblLabel0.Location = new Point(0, 196);
        lblLabel0.Size = new Size(214, 44);
        lblLabel0.BackColor = Color.Transparent;
        lblLabel0.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLabel0.Name = "lblLabel0";
        lblLabel0.Paint += OnOptionLabelPaint;
        // 
        // lblLabel1
        // 
        lblLabel1.AutoSize = false;
        lblLabel1.Location = new Point(0, 240);
        lblLabel1.Size = new Size(214, 44);
        lblLabel1.BackColor = Color.Transparent;
        lblLabel1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLabel1.Name = "lblLabel1";
        lblLabel1.Paint += OnOptionLabelPaint;
        // 
        // lblLabel2
        // 
        lblLabel2.AutoSize = false;
        lblLabel2.Location = new Point(0, 284);
        lblLabel2.Size = new Size(214, 44);
        lblLabel2.BackColor = Color.Transparent;
        lblLabel2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLabel2.Name = "lblLabel2";
        lblLabel2.Paint += OnOptionLabelPaint;
        // 
        // lblLabel3
        // 
        lblLabel3.AutoSize = false;
        lblLabel3.Location = new Point(0, 328);
        lblLabel3.Size = new Size(214, 44);
        lblLabel3.BackColor = Color.Transparent;
        lblLabel3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLabel3.Name = "lblLabel3";
        lblLabel3.Paint += OnOptionLabelPaint;
        // 
        // lblLabel4
        // 
        lblLabel4.AutoSize = false;
        lblLabel4.Location = new Point(214, 196);
        lblLabel4.Size = new Size(214, 44);
        lblLabel4.BackColor = Color.Transparent;
        lblLabel4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLabel4.Name = "lblLabel4";
        lblLabel4.Paint += OnOptionLabelPaint;
        // 
        // lblLabel5
        // 
        lblLabel5.AutoSize = false;
        lblLabel5.Location = new Point(214, 240);
        lblLabel5.Size = new Size(214, 44);
        lblLabel5.BackColor = Color.Transparent;
        lblLabel5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLabel5.Name = "lblLabel5";
        lblLabel5.Paint += OnOptionLabelPaint;
        // 
        // lblLabel6
        // 
        lblLabel6.AutoSize = false;
        lblLabel6.Location = new Point(214, 284);
        lblLabel6.Size = new Size(214, 44);
        lblLabel6.BackColor = Color.Transparent;
        lblLabel6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLabel6.Name = "lblLabel6";
        lblLabel6.Paint += OnOptionLabelPaint;
        // 
        // lblLabel7
        // 
        lblLabel7.AutoSize = false;
        lblLabel7.Location = new Point(214, 328);
        lblLabel7.Size = new Size(214, 44);
        lblLabel7.BackColor = Color.Transparent;
        lblLabel7.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLabel7.Name = "lblLabel7";
        lblLabel7.Paint += OnOptionLabelPaint;
        // 
        // btnSide0
        // 
        btnSide0.Location = new Point(16, 218);
        btnSide0.Size = new Size(46, 36);
        btnSide0.Tag = "0";
        btnSide0.UseVisualStyleBackColor = false;
        btnSide0.FlatStyle = FlatStyle.Flat;
        btnSide0.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5CAD1");
        btnSide0.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5A606B");
        btnSide0.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#D6DAE0");
        btnSide0.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9AA0A9");
        btnSide0.FlatAppearance.BorderSize = 2;
        btnSide0.TabStop = false;
        btnSide0.Name = "btnSide0";
        btnSide0.Paint += OnSideButtonPaint;
        btnSide0.Click += OnSideButtonClick;
        // 
        // btnSide1
        // 
        btnSide1.Location = new Point(16, 262);
        btnSide1.Size = new Size(46, 36);
        btnSide1.Tag = "1";
        btnSide1.UseVisualStyleBackColor = false;
        btnSide1.FlatStyle = FlatStyle.Flat;
        btnSide1.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5CAD1");
        btnSide1.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5A606B");
        btnSide1.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#D6DAE0");
        btnSide1.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9AA0A9");
        btnSide1.FlatAppearance.BorderSize = 2;
        btnSide1.TabStop = false;
        btnSide1.Name = "btnSide1";
        btnSide1.Paint += OnSideButtonPaint;
        btnSide1.Click += OnSideButtonClick;
        // 
        // btnSide2
        // 
        btnSide2.Location = new Point(16, 306);
        btnSide2.Size = new Size(46, 36);
        btnSide2.Tag = "2";
        btnSide2.UseVisualStyleBackColor = false;
        btnSide2.FlatStyle = FlatStyle.Flat;
        btnSide2.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5CAD1");
        btnSide2.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5A606B");
        btnSide2.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#D6DAE0");
        btnSide2.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9AA0A9");
        btnSide2.FlatAppearance.BorderSize = 2;
        btnSide2.TabStop = false;
        btnSide2.Name = "btnSide2";
        btnSide2.Paint += OnSideButtonPaint;
        btnSide2.Click += OnSideButtonClick;
        // 
        // btnSide3
        // 
        btnSide3.Location = new Point(16, 350);
        btnSide3.Size = new Size(46, 36);
        btnSide3.Tag = "3";
        btnSide3.UseVisualStyleBackColor = false;
        btnSide3.FlatStyle = FlatStyle.Flat;
        btnSide3.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5CAD1");
        btnSide3.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5A606B");
        btnSide3.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#D6DAE0");
        btnSide3.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9AA0A9");
        btnSide3.FlatAppearance.BorderSize = 2;
        btnSide3.TabStop = false;
        btnSide3.Name = "btnSide3";
        btnSide3.Paint += OnSideButtonPaint;
        btnSide3.Click += OnSideButtonClick;
        // 
        // btnSide4
        // 
        btnSide4.Location = new Point(510, 218);
        btnSide4.Size = new Size(46, 36);
        btnSide4.Tag = "4";
        btnSide4.UseVisualStyleBackColor = false;
        btnSide4.FlatStyle = FlatStyle.Flat;
        btnSide4.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5CAD1");
        btnSide4.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5A606B");
        btnSide4.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#D6DAE0");
        btnSide4.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9AA0A9");
        btnSide4.FlatAppearance.BorderSize = 2;
        btnSide4.TabStop = false;
        btnSide4.Name = "btnSide4";
        btnSide4.Paint += OnSideButtonPaint;
        btnSide4.Click += OnSideButtonClick;
        // 
        // btnSide5
        // 
        btnSide5.Location = new Point(510, 262);
        btnSide5.Size = new Size(46, 36);
        btnSide5.Tag = "5";
        btnSide5.UseVisualStyleBackColor = false;
        btnSide5.FlatStyle = FlatStyle.Flat;
        btnSide5.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5CAD1");
        btnSide5.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5A606B");
        btnSide5.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#D6DAE0");
        btnSide5.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9AA0A9");
        btnSide5.FlatAppearance.BorderSize = 2;
        btnSide5.TabStop = false;
        btnSide5.Name = "btnSide5";
        btnSide5.Paint += OnSideButtonPaint;
        btnSide5.Click += OnSideButtonClick;
        // 
        // btnSide6
        // 
        btnSide6.Location = new Point(510, 306);
        btnSide6.Size = new Size(46, 36);
        btnSide6.Tag = "6";
        btnSide6.UseVisualStyleBackColor = false;
        btnSide6.FlatStyle = FlatStyle.Flat;
        btnSide6.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5CAD1");
        btnSide6.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5A606B");
        btnSide6.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#D6DAE0");
        btnSide6.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9AA0A9");
        btnSide6.FlatAppearance.BorderSize = 2;
        btnSide6.TabStop = false;
        btnSide6.Name = "btnSide6";
        btnSide6.Paint += OnSideButtonPaint;
        btnSide6.Click += OnSideButtonClick;
        // 
        // btnSide7
        // 
        btnSide7.Location = new Point(510, 350);
        btnSide7.Size = new Size(46, 36);
        btnSide7.Tag = "7";
        btnSide7.UseVisualStyleBackColor = false;
        btnSide7.FlatStyle = FlatStyle.Flat;
        btnSide7.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5CAD1");
        btnSide7.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5A606B");
        btnSide7.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#D6DAE0");
        btnSide7.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9AA0A9");
        btnSide7.FlatAppearance.BorderSize = 2;
        btnSide7.TabStop = false;
        btnSide7.Name = "btnSide7";
        btnSide7.Paint += OnSideButtonPaint;
        btnSide7.Click += OnSideButtonClick;
        // 
        // pnlKeypad
        // 
        pnlKeypad.Location = new Point(24, 540);
        pnlKeypad.Size = new Size(336, 268);
        pnlKeypad.BackColor = Color.Transparent;
        pnlKeypad.Name = "pnlKeypad";
        pnlKeypad.Paint += OnKeypadPaint;
        // 
        // btnDigit1
        // 
        btnDigit1.Location = new Point(16, 16);
        btnDigit1.Size = new Size(70, 54);
        btnDigit1.Text = "1";
        btnDigit1.UseVisualStyleBackColor = false;
        btnDigit1.FlatStyle = FlatStyle.Flat;
        btnDigit1.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit1.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit1.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit1.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit1.FlatAppearance.BorderSize = 2;
        btnDigit1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit1.TabStop = false;
        btnDigit1.Name = "btnDigit1";
        btnDigit1.Click += OnDigitClick;
        // 
        // btnDigit2
        // 
        btnDigit2.Location = new Point(94, 16);
        btnDigit2.Size = new Size(70, 54);
        btnDigit2.Text = "2";
        btnDigit2.UseVisualStyleBackColor = false;
        btnDigit2.FlatStyle = FlatStyle.Flat;
        btnDigit2.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit2.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit2.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit2.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit2.FlatAppearance.BorderSize = 2;
        btnDigit2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit2.TabStop = false;
        btnDigit2.Name = "btnDigit2";
        btnDigit2.Click += OnDigitClick;
        // 
        // btnDigit3
        // 
        btnDigit3.Location = new Point(172, 16);
        btnDigit3.Size = new Size(70, 54);
        btnDigit3.Text = "3";
        btnDigit3.UseVisualStyleBackColor = false;
        btnDigit3.FlatStyle = FlatStyle.Flat;
        btnDigit3.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit3.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit3.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit3.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit3.FlatAppearance.BorderSize = 2;
        btnDigit3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit3.TabStop = false;
        btnDigit3.Name = "btnDigit3";
        btnDigit3.Click += OnDigitClick;
        // 
        // btnDigit4
        // 
        btnDigit4.Location = new Point(16, 78);
        btnDigit4.Size = new Size(70, 54);
        btnDigit4.Text = "4";
        btnDigit4.UseVisualStyleBackColor = false;
        btnDigit4.FlatStyle = FlatStyle.Flat;
        btnDigit4.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit4.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit4.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit4.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit4.FlatAppearance.BorderSize = 2;
        btnDigit4.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit4.TabStop = false;
        btnDigit4.Name = "btnDigit4";
        btnDigit4.Click += OnDigitClick;
        // 
        // btnDigit5
        // 
        btnDigit5.Location = new Point(94, 78);
        btnDigit5.Size = new Size(70, 54);
        btnDigit5.Text = "5";
        btnDigit5.UseVisualStyleBackColor = false;
        btnDigit5.FlatStyle = FlatStyle.Flat;
        btnDigit5.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit5.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit5.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit5.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit5.FlatAppearance.BorderSize = 2;
        btnDigit5.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit5.TabStop = false;
        btnDigit5.Name = "btnDigit5";
        btnDigit5.Click += OnDigitClick;
        // 
        // btnDigit6
        // 
        btnDigit6.Location = new Point(172, 78);
        btnDigit6.Size = new Size(70, 54);
        btnDigit6.Text = "6";
        btnDigit6.UseVisualStyleBackColor = false;
        btnDigit6.FlatStyle = FlatStyle.Flat;
        btnDigit6.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit6.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit6.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit6.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit6.FlatAppearance.BorderSize = 2;
        btnDigit6.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit6.TabStop = false;
        btnDigit6.Name = "btnDigit6";
        btnDigit6.Click += OnDigitClick;
        // 
        // btnDigit7
        // 
        btnDigit7.Location = new Point(16, 140);
        btnDigit7.Size = new Size(70, 54);
        btnDigit7.Text = "7";
        btnDigit7.UseVisualStyleBackColor = false;
        btnDigit7.FlatStyle = FlatStyle.Flat;
        btnDigit7.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit7.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit7.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit7.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit7.FlatAppearance.BorderSize = 2;
        btnDigit7.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit7.TabStop = false;
        btnDigit7.Name = "btnDigit7";
        btnDigit7.Click += OnDigitClick;
        // 
        // btnDigit8
        // 
        btnDigit8.Location = new Point(94, 140);
        btnDigit8.Size = new Size(70, 54);
        btnDigit8.Text = "8";
        btnDigit8.UseVisualStyleBackColor = false;
        btnDigit8.FlatStyle = FlatStyle.Flat;
        btnDigit8.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit8.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit8.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit8.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit8.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit8.FlatAppearance.BorderSize = 2;
        btnDigit8.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit8.TabStop = false;
        btnDigit8.Name = "btnDigit8";
        btnDigit8.Click += OnDigitClick;
        // 
        // btnDigit9
        // 
        btnDigit9.Location = new Point(172, 140);
        btnDigit9.Size = new Size(70, 54);
        btnDigit9.Text = "9";
        btnDigit9.UseVisualStyleBackColor = false;
        btnDigit9.FlatStyle = FlatStyle.Flat;
        btnDigit9.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit9.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit9.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit9.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit9.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit9.FlatAppearance.BorderSize = 2;
        btnDigit9.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit9.TabStop = false;
        btnDigit9.Name = "btnDigit9";
        btnDigit9.Click += OnDigitClick;
        // 
        // btnDigit0
        // 
        btnDigit0.Location = new Point(94, 202);
        btnDigit0.Size = new Size(70, 54);
        btnDigit0.Text = "0";
        btnDigit0.UseVisualStyleBackColor = false;
        btnDigit0.FlatStyle = FlatStyle.Flat;
        btnDigit0.BackColor = System.Drawing.ColorTranslator.FromHtml("#CDD2D8");
        btnDigit0.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnDigit0.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6B717C");
        btnDigit0.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DDE1E6");
        btnDigit0.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#A3A9B2");
        btnDigit0.FlatAppearance.BorderSize = 2;
        btnDigit0.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        btnDigit0.TabStop = false;
        btnDigit0.Name = "btnDigit0";
        btnDigit0.Click += OnDigitClick;
        // 
        // btnClear
        // 
        btnClear.Location = new Point(250, 78);
        btnClear.Size = new Size(70, 54);
        btnClear.Text = "BORRAR";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2C230");
        btnClear.ForeColor = System.Drawing.ColorTranslator.FromHtml("#16191E");
        btnClear.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#8A6A0E");
        btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#F7D25C");
        btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#C99C1A");
        btnClear.FlatAppearance.BorderSize = 2;
        btnClear.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
        btnClear.Padding = new Padding(0);
        btnClear.TabStop = false;
        btnClear.Name = "btnClear";
        btnClear.Click += OnClearClick;
        // 
        // btnAccept
        // 
        btnAccept.Location = new Point(250, 140);
        btnAccept.Size = new Size(70, 54);
        btnAccept.Text = "ACEPTAR";
        btnAccept.UseVisualStyleBackColor = false;
        btnAccept.FlatStyle = FlatStyle.Flat;
        btnAccept.BackColor = System.Drawing.ColorTranslator.FromHtml("#2E9E4A");
        btnAccept.ForeColor = Color.White;
        btnAccept.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#17582A");
        btnAccept.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#3DB75B");
        btnAccept.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#22793A");
        btnAccept.FlatAppearance.BorderSize = 2;
        btnAccept.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
        btnAccept.Padding = new Padding(0);
        btnAccept.TabStop = false;
        btnAccept.Name = "btnAccept";
        btnAccept.Click += OnAcceptClick;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(250, 16);
        btnCancel.Size = new Size(70, 54);
        btnCancel.Text = "CANCELAR";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.BackColor = System.Drawing.ColorTranslator.FromHtml("#C8322B");
        btnCancel.ForeColor = Color.White;
        btnCancel.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#6E1A16");
        btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.ColorTranslator.FromHtml("#DA4A42");
        btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.ColorTranslator.FromHtml("#9E2520");
        btnCancel.FlatAppearance.BorderSize = 2;
        btnCancel.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
        btnCancel.Padding = new Padding(0);
        btnCancel.TabStop = false;
        btnCancel.Name = "btnCancel";
        btnCancel.Click += OnCancelClick;
        // 
        // lblCardCaption
        // 
        lblCardCaption.Location = new Point(380, 540);
        lblCardCaption.Size = new Size(216, 16);
        lblCardCaption.Text = "TARJETA";
        lblCardCaption.BackColor = Color.Transparent;
        lblCardCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2E343C");
        lblCardCaption.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblCardCaption.AutoSize = false;
        lblCardCaption.Name = "lblCardCaption";
        // 
        // pnlCardReader
        // 
        pnlCardReader.Location = new Point(380, 558);
        pnlCardReader.Size = new Size(216, 64);
        pnlCardReader.BackColor = Color.Transparent;
        pnlCardReader.Name = "pnlCardReader";
        pnlCardReader.Paint += OnCardReaderPaint;
        // 
        // cmbCard
        // 
        cmbCard.Location = new Point(380, 630);
        cmbCard.Size = new Size(216, 23);
        cmbCard.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCard.FormattingEnabled = true;
        cmbCard.Name = "cmbCard";
        // 
        // btnCard
        // 
        btnCard.Location = new Point(380, 658);
        btnCard.Size = new Size(216, 34);
        btnCard.Text = "Insertar tarjeta";
        btnCard.UseVisualStyleBackColor = false;
        btnCard.FlatStyle = FlatStyle.Flat;
        btnCard.BackColor = System.Drawing.ColorTranslator.FromHtml("#EC0000");
        btnCard.ForeColor = Color.White;
        btnCard.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#A60000");
        btnCard.FlatAppearance.BorderSize = 2;
        btnCard.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnCard.TabStop = false;
        btnCard.Name = "btnCard";
        btnCard.Click += OnCardClick;
        // 
        // lblReceiptCaption
        // 
        lblReceiptCaption.Location = new Point(380, 704);
        lblReceiptCaption.Size = new Size(216, 16);
        lblReceiptCaption.Text = "COMPROBANTE";
        lblReceiptCaption.BackColor = Color.Transparent;
        lblReceiptCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2E343C");
        lblReceiptCaption.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblReceiptCaption.AutoSize = false;
        lblReceiptCaption.Name = "lblReceiptCaption";
        // 
        // pnlPrinter
        // 
        pnlPrinter.Location = new Point(380, 722);
        pnlPrinter.Size = new Size(216, 24);
        pnlPrinter.BackColor = Color.Transparent;
        pnlPrinter.Name = "pnlPrinter";
        pnlPrinter.Paint += OnPrinterPaint;
        // 
        // lblReceipt
        // 
        lblReceipt.AutoSize = false;
        lblReceipt.Location = new Point(392, 742);
        lblReceipt.Size = new Size(192, 150);
        lblReceipt.BackColor = System.Drawing.ColorTranslator.FromHtml("#FBFAF3");
        lblReceipt.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2B2B2B");
        lblReceipt.Padding = new Padding(6);
        lblReceipt.Font = new Font("Consolas", 7.5F);
        lblReceipt.Visible = false;
        lblReceipt.Name = "lblReceipt";
        // 
        // lblCashCaption
        // 
        lblCashCaption.Location = new Point(24, 820);
        lblCashCaption.Size = new Size(336, 16);
        lblCashCaption.Text = "RETIRE SU DINERO";
        lblCashCaption.BackColor = Color.Transparent;
        lblCashCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2E343C");
        lblCashCaption.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblCashCaption.AutoSize = false;
        lblCashCaption.Name = "lblCashCaption";
        // 
        // pnlDispenser
        // 
        pnlDispenser.Location = new Point(24, 838);
        pnlDispenser.Size = new Size(336, 64);
        pnlDispenser.BackColor = Color.Transparent;
        pnlDispenser.Name = "pnlDispenser";
        pnlDispenser.Paint += OnDispenserPaint;
        // 
        // lblCash
        // 
        lblCash.AutoSize = false;
        lblCash.Location = new Point(28, 20);
        lblCash.Size = new Size(280, 24);
        lblCash.BackColor = System.Drawing.ColorTranslator.FromHtml("#0B0C0E");
        lblCash.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2F5A2A");
        lblCash.TextAlign = ContentAlignment.MiddleCenter;
        lblCash.Font = new Font("Consolas", 11.25F, FontStyle.Bold);
        lblCash.Name = "lblCash";
        // 
        // tmrBlink
        // 
        tmrBlink.Interval = 500;
        tmrBlink.Tick += OnBlinkTimerTick;
        // 
        // pnlScreen
        // 
        pnlScreen.Controls.Add(lblInput);
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
        // pnlKeypad
        // 
        pnlKeypad.Controls.Add(btnDigit1);
        pnlKeypad.Controls.Add(btnDigit2);
        pnlKeypad.Controls.Add(btnDigit3);
        pnlKeypad.Controls.Add(btnDigit4);
        pnlKeypad.Controls.Add(btnDigit5);
        pnlKeypad.Controls.Add(btnDigit6);
        pnlKeypad.Controls.Add(btnDigit7);
        pnlKeypad.Controls.Add(btnDigit8);
        pnlKeypad.Controls.Add(btnDigit9);
        pnlKeypad.Controls.Add(btnDigit0);
        pnlKeypad.Controls.Add(btnCancel);
        pnlKeypad.Controls.Add(btnClear);
        pnlKeypad.Controls.Add(btnAccept);
        // 
        // pnlDispenser
        // 
        pnlDispenser.Controls.Add(lblCash);
        // 
        // CajeroForm
        // 
        AutoScaleMode = AutoScaleMode.None;
        BackColor = System.Drawing.ColorTranslator.FromHtml("#C3C8CF");
        ClientSize = new Size(620, 920);
        DoubleBuffered = true;
        Controls.Add(pnlMarquee);
        Controls.Add(pnlBezel);
        Controls.Add(pnlKeypad);
        Controls.Add(lblCardCaption);
        Controls.Add(pnlCardReader);
        Controls.Add(cmbCard);
        Controls.Add(btnCard);
        Controls.Add(lblReceiptCaption);
        Controls.Add(pnlPrinter);
        Controls.Add(lblReceipt);
        Controls.Add(lblCashCaption);
        Controls.Add(pnlDispenser);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "CajeroForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Cajero Automático Santander";
        Paint += OnFormPaint;
        pnlDispenser.ResumeLayout(false);
        pnlKeypad.ResumeLayout(false);
        pnlScreen.ResumeLayout(false);
        pnlBezel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlMarquee;
    private Panel pnlBezel;
    private Panel pnlScreen;
    private Label lblMessage;
    private Label lblInput;
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
    private Panel pnlKeypad;
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
    private Panel pnlCardReader;
    private ComboBox cmbCard;
    private Button btnCard;
    private Label lblReceiptCaption;
    private Panel pnlPrinter;
    private Label lblReceipt;
    private Label lblCashCaption;
    private Panel pnlDispenser;
    private Label lblCash;
    private System.Windows.Forms.Timer tmrBlink;
}
