#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cajero.Presentacion;

partial class FormCajero
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
        lblMarca = new Label();
        pnlBisel = new Panel();
        pnlPantalla = new Panel();
        lblMensaje = new Label();
        lblEtiqueta0 = new Label();
        lblEtiqueta1 = new Label();
        lblEtiqueta2 = new Label();
        lblEtiqueta3 = new Label();
        lblEtiqueta4 = new Label();
        lblEtiqueta5 = new Label();
        lblEtiqueta6 = new Label();
        lblEtiqueta7 = new Label();
        btnLateral0 = new Button();
        btnLateral1 = new Button();
        btnLateral2 = new Button();
        btnLateral3 = new Button();
        btnLateral4 = new Button();
        btnLateral5 = new Button();
        btnLateral6 = new Button();
        btnLateral7 = new Button();
        btnDigito1 = new Button();
        btnDigito2 = new Button();
        btnDigito3 = new Button();
        btnDigito4 = new Button();
        btnDigito5 = new Button();
        btnDigito6 = new Button();
        btnDigito7 = new Button();
        btnDigito8 = new Button();
        btnDigito9 = new Button();
        btnDigito0 = new Button();
        btnCorregir = new Button();
        btnAceptar = new Button();
        btnCancelar = new Button();
        lblRotuloTarjeta = new Label();
        cmbTarjeta = new ComboBox();
        btnTarjeta = new Button();
        lblRotuloComprobante = new Label();
        lblTicket = new Label();
        lblRotuloEfectivo = new Label();
        lblEfectivo = new Label();
        pnlBisel.SuspendLayout();
        pnlPantalla.SuspendLayout();
        SuspendLayout();
        // 
        // lblMarca
        // 
        lblMarca.AutoSize = false;
        lblMarca.Location = new Point(22, 12);
        lblMarca.Size = new Size(516, 36);
        lblMarca.Text = "BANCO ISFT 93";
        lblMarca.TextAlign = ContentAlignment.MiddleCenter;
        lblMarca.ForeColor = System.Drawing.ColorTranslator.FromHtml("#14305C");
        lblMarca.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblMarca.Name = "lblMarca";
        // 
        // pnlBisel
        // 
        pnlBisel.Location = new Point(22, 56);
        pnlBisel.Size = new Size(516, 402);
        pnlBisel.BackColor = System.Drawing.ColorTranslator.FromHtml("#20242C");
        pnlBisel.Name = "pnlBisel";
        // 
        // pnlPantalla
        // 
        pnlPantalla.Location = new Point(64, 10);
        pnlPantalla.Size = new Size(388, 382);
        pnlPantalla.BackColor = System.Drawing.ColorTranslator.FromHtml("#06222B");
        pnlPantalla.Name = "pnlPantalla";
        // 
        // lblMensaje
        // 
        lblMensaje.AutoSize = false;
        lblMensaje.Location = new Point(0, 0);
        lblMensaje.Size = new Size(388, 190);
        lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
        lblMensaje.ForeColor = System.Drawing.ColorTranslator.FromHtml("#7DFFC0");
        lblMensaje.Font = new Font("Consolas", 13F, FontStyle.Bold);
        lblMensaje.Padding = new Padding(14);
        lblMensaje.Name = "lblMensaje";
        // 
        // lblEtiqueta0
        // 
        lblEtiqueta0.AutoSize = false;
        lblEtiqueta0.Location = new Point(0, 190);
        lblEtiqueta0.Size = new Size(194, 48);
        lblEtiqueta0.TextAlign = ContentAlignment.MiddleLeft;
        lblEtiqueta0.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblEtiqueta0.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblEtiqueta0.Padding = new Padding(8, 0, 8, 0);
        lblEtiqueta0.Name = "lblEtiqueta0";
        // 
        // lblEtiqueta1
        // 
        lblEtiqueta1.AutoSize = false;
        lblEtiqueta1.Location = new Point(0, 238);
        lblEtiqueta1.Size = new Size(194, 48);
        lblEtiqueta1.TextAlign = ContentAlignment.MiddleLeft;
        lblEtiqueta1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblEtiqueta1.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblEtiqueta1.Padding = new Padding(8, 0, 8, 0);
        lblEtiqueta1.Name = "lblEtiqueta1";
        // 
        // lblEtiqueta2
        // 
        lblEtiqueta2.AutoSize = false;
        lblEtiqueta2.Location = new Point(0, 286);
        lblEtiqueta2.Size = new Size(194, 48);
        lblEtiqueta2.TextAlign = ContentAlignment.MiddleLeft;
        lblEtiqueta2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblEtiqueta2.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblEtiqueta2.Padding = new Padding(8, 0, 8, 0);
        lblEtiqueta2.Name = "lblEtiqueta2";
        // 
        // lblEtiqueta3
        // 
        lblEtiqueta3.AutoSize = false;
        lblEtiqueta3.Location = new Point(0, 334);
        lblEtiqueta3.Size = new Size(194, 48);
        lblEtiqueta3.TextAlign = ContentAlignment.MiddleLeft;
        lblEtiqueta3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblEtiqueta3.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblEtiqueta3.Padding = new Padding(8, 0, 8, 0);
        lblEtiqueta3.Name = "lblEtiqueta3";
        // 
        // lblEtiqueta4
        // 
        lblEtiqueta4.AutoSize = false;
        lblEtiqueta4.Location = new Point(194, 190);
        lblEtiqueta4.Size = new Size(194, 48);
        lblEtiqueta4.TextAlign = ContentAlignment.MiddleRight;
        lblEtiqueta4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblEtiqueta4.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblEtiqueta4.Padding = new Padding(8, 0, 8, 0);
        lblEtiqueta4.Name = "lblEtiqueta4";
        // 
        // lblEtiqueta5
        // 
        lblEtiqueta5.AutoSize = false;
        lblEtiqueta5.Location = new Point(194, 238);
        lblEtiqueta5.Size = new Size(194, 48);
        lblEtiqueta5.TextAlign = ContentAlignment.MiddleRight;
        lblEtiqueta5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblEtiqueta5.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblEtiqueta5.Padding = new Padding(8, 0, 8, 0);
        lblEtiqueta5.Name = "lblEtiqueta5";
        // 
        // lblEtiqueta6
        // 
        lblEtiqueta6.AutoSize = false;
        lblEtiqueta6.Location = new Point(194, 286);
        lblEtiqueta6.Size = new Size(194, 48);
        lblEtiqueta6.TextAlign = ContentAlignment.MiddleRight;
        lblEtiqueta6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblEtiqueta6.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblEtiqueta6.Padding = new Padding(8, 0, 8, 0);
        lblEtiqueta6.Name = "lblEtiqueta6";
        // 
        // lblEtiqueta7
        // 
        lblEtiqueta7.AutoSize = false;
        lblEtiqueta7.Location = new Point(194, 334);
        lblEtiqueta7.Size = new Size(194, 48);
        lblEtiqueta7.TextAlign = ContentAlignment.MiddleRight;
        lblEtiqueta7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BFF6FF");
        lblEtiqueta7.Font = new Font("Consolas", 10F, FontStyle.Bold);
        lblEtiqueta7.Padding = new Padding(8, 0, 8, 0);
        lblEtiqueta7.Name = "lblEtiqueta7";
        // 
        // btnLateral0
        // 
        btnLateral0.Location = new Point(10, 207);
        btnLateral0.Size = new Size(46, 34);
        btnLateral0.Tag = "0";
        btnLateral0.UseVisualStyleBackColor = false;
        btnLateral0.FlatStyle = FlatStyle.Flat;
        btnLateral0.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnLateral0.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnLateral0.FlatAppearance.BorderSize = 2;
        btnLateral0.TabStop = false;
        btnLateral0.Name = "btnLateral0";
        btnLateral0.Click += AlPresionarBotonLateral;
        // 
        // btnLateral1
        // 
        btnLateral1.Location = new Point(10, 255);
        btnLateral1.Size = new Size(46, 34);
        btnLateral1.Tag = "1";
        btnLateral1.UseVisualStyleBackColor = false;
        btnLateral1.FlatStyle = FlatStyle.Flat;
        btnLateral1.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnLateral1.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnLateral1.FlatAppearance.BorderSize = 2;
        btnLateral1.TabStop = false;
        btnLateral1.Name = "btnLateral1";
        btnLateral1.Click += AlPresionarBotonLateral;
        // 
        // btnLateral2
        // 
        btnLateral2.Location = new Point(10, 303);
        btnLateral2.Size = new Size(46, 34);
        btnLateral2.Tag = "2";
        btnLateral2.UseVisualStyleBackColor = false;
        btnLateral2.FlatStyle = FlatStyle.Flat;
        btnLateral2.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnLateral2.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnLateral2.FlatAppearance.BorderSize = 2;
        btnLateral2.TabStop = false;
        btnLateral2.Name = "btnLateral2";
        btnLateral2.Click += AlPresionarBotonLateral;
        // 
        // btnLateral3
        // 
        btnLateral3.Location = new Point(10, 351);
        btnLateral3.Size = new Size(46, 34);
        btnLateral3.Tag = "3";
        btnLateral3.UseVisualStyleBackColor = false;
        btnLateral3.FlatStyle = FlatStyle.Flat;
        btnLateral3.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnLateral3.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnLateral3.FlatAppearance.BorderSize = 2;
        btnLateral3.TabStop = false;
        btnLateral3.Name = "btnLateral3";
        btnLateral3.Click += AlPresionarBotonLateral;
        // 
        // btnLateral4
        // 
        btnLateral4.Location = new Point(460, 207);
        btnLateral4.Size = new Size(46, 34);
        btnLateral4.Tag = "4";
        btnLateral4.UseVisualStyleBackColor = false;
        btnLateral4.FlatStyle = FlatStyle.Flat;
        btnLateral4.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnLateral4.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnLateral4.FlatAppearance.BorderSize = 2;
        btnLateral4.TabStop = false;
        btnLateral4.Name = "btnLateral4";
        btnLateral4.Click += AlPresionarBotonLateral;
        // 
        // btnLateral5
        // 
        btnLateral5.Location = new Point(460, 255);
        btnLateral5.Size = new Size(46, 34);
        btnLateral5.Tag = "5";
        btnLateral5.UseVisualStyleBackColor = false;
        btnLateral5.FlatStyle = FlatStyle.Flat;
        btnLateral5.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnLateral5.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnLateral5.FlatAppearance.BorderSize = 2;
        btnLateral5.TabStop = false;
        btnLateral5.Name = "btnLateral5";
        btnLateral5.Click += AlPresionarBotonLateral;
        // 
        // btnLateral6
        // 
        btnLateral6.Location = new Point(460, 303);
        btnLateral6.Size = new Size(46, 34);
        btnLateral6.Tag = "6";
        btnLateral6.UseVisualStyleBackColor = false;
        btnLateral6.FlatStyle = FlatStyle.Flat;
        btnLateral6.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnLateral6.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnLateral6.FlatAppearance.BorderSize = 2;
        btnLateral6.TabStop = false;
        btnLateral6.Name = "btnLateral6";
        btnLateral6.Click += AlPresionarBotonLateral;
        // 
        // btnLateral7
        // 
        btnLateral7.Location = new Point(460, 351);
        btnLateral7.Size = new Size(46, 34);
        btnLateral7.Tag = "7";
        btnLateral7.UseVisualStyleBackColor = false;
        btnLateral7.FlatStyle = FlatStyle.Flat;
        btnLateral7.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9DDE4");
        btnLateral7.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4A515E");
        btnLateral7.FlatAppearance.BorderSize = 2;
        btnLateral7.TabStop = false;
        btnLateral7.Name = "btnLateral7";
        btnLateral7.Click += AlPresionarBotonLateral;
        // 
        // btnDigito1
        // 
        btnDigito1.Location = new Point(22, 478);
        btnDigito1.Size = new Size(92, 48);
        btnDigito1.Text = "1";
        btnDigito1.UseVisualStyleBackColor = false;
        btnDigito1.FlatStyle = FlatStyle.Flat;
        btnDigito1.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito1.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito1.FlatAppearance.BorderSize = 2;
        btnDigito1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito1.TabStop = false;
        btnDigito1.Name = "btnDigito1";
        btnDigito1.Click += AlPresionarDigito;
        // 
        // btnDigito2
        // 
        btnDigito2.Location = new Point(122, 478);
        btnDigito2.Size = new Size(92, 48);
        btnDigito2.Text = "2";
        btnDigito2.UseVisualStyleBackColor = false;
        btnDigito2.FlatStyle = FlatStyle.Flat;
        btnDigito2.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito2.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito2.FlatAppearance.BorderSize = 2;
        btnDigito2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito2.TabStop = false;
        btnDigito2.Name = "btnDigito2";
        btnDigito2.Click += AlPresionarDigito;
        // 
        // btnDigito3
        // 
        btnDigito3.Location = new Point(222, 478);
        btnDigito3.Size = new Size(92, 48);
        btnDigito3.Text = "3";
        btnDigito3.UseVisualStyleBackColor = false;
        btnDigito3.FlatStyle = FlatStyle.Flat;
        btnDigito3.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito3.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito3.FlatAppearance.BorderSize = 2;
        btnDigito3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito3.TabStop = false;
        btnDigito3.Name = "btnDigito3";
        btnDigito3.Click += AlPresionarDigito;
        // 
        // btnDigito4
        // 
        btnDigito4.Location = new Point(22, 534);
        btnDigito4.Size = new Size(92, 48);
        btnDigito4.Text = "4";
        btnDigito4.UseVisualStyleBackColor = false;
        btnDigito4.FlatStyle = FlatStyle.Flat;
        btnDigito4.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito4.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito4.FlatAppearance.BorderSize = 2;
        btnDigito4.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito4.TabStop = false;
        btnDigito4.Name = "btnDigito4";
        btnDigito4.Click += AlPresionarDigito;
        // 
        // btnDigito5
        // 
        btnDigito5.Location = new Point(122, 534);
        btnDigito5.Size = new Size(92, 48);
        btnDigito5.Text = "5";
        btnDigito5.UseVisualStyleBackColor = false;
        btnDigito5.FlatStyle = FlatStyle.Flat;
        btnDigito5.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito5.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito5.FlatAppearance.BorderSize = 2;
        btnDigito5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito5.TabStop = false;
        btnDigito5.Name = "btnDigito5";
        btnDigito5.Click += AlPresionarDigito;
        // 
        // btnDigito6
        // 
        btnDigito6.Location = new Point(222, 534);
        btnDigito6.Size = new Size(92, 48);
        btnDigito6.Text = "6";
        btnDigito6.UseVisualStyleBackColor = false;
        btnDigito6.FlatStyle = FlatStyle.Flat;
        btnDigito6.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito6.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito6.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito6.FlatAppearance.BorderSize = 2;
        btnDigito6.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito6.TabStop = false;
        btnDigito6.Name = "btnDigito6";
        btnDigito6.Click += AlPresionarDigito;
        // 
        // btnDigito7
        // 
        btnDigito7.Location = new Point(22, 590);
        btnDigito7.Size = new Size(92, 48);
        btnDigito7.Text = "7";
        btnDigito7.UseVisualStyleBackColor = false;
        btnDigito7.FlatStyle = FlatStyle.Flat;
        btnDigito7.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito7.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito7.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito7.FlatAppearance.BorderSize = 2;
        btnDigito7.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito7.TabStop = false;
        btnDigito7.Name = "btnDigito7";
        btnDigito7.Click += AlPresionarDigito;
        // 
        // btnDigito8
        // 
        btnDigito8.Location = new Point(122, 590);
        btnDigito8.Size = new Size(92, 48);
        btnDigito8.Text = "8";
        btnDigito8.UseVisualStyleBackColor = false;
        btnDigito8.FlatStyle = FlatStyle.Flat;
        btnDigito8.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito8.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito8.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito8.FlatAppearance.BorderSize = 2;
        btnDigito8.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito8.TabStop = false;
        btnDigito8.Name = "btnDigito8";
        btnDigito8.Click += AlPresionarDigito;
        // 
        // btnDigito9
        // 
        btnDigito9.Location = new Point(222, 590);
        btnDigito9.Size = new Size(92, 48);
        btnDigito9.Text = "9";
        btnDigito9.UseVisualStyleBackColor = false;
        btnDigito9.FlatStyle = FlatStyle.Flat;
        btnDigito9.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito9.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito9.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito9.FlatAppearance.BorderSize = 2;
        btnDigito9.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito9.TabStop = false;
        btnDigito9.Name = "btnDigito9";
        btnDigito9.Click += AlPresionarDigito;
        // 
        // btnDigito0
        // 
        btnDigito0.Location = new Point(122, 646);
        btnDigito0.Size = new Size(92, 48);
        btnDigito0.Text = "0";
        btnDigito0.UseVisualStyleBackColor = false;
        btnDigito0.FlatStyle = FlatStyle.Flat;
        btnDigito0.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E7EC");
        btnDigito0.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnDigito0.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnDigito0.FlatAppearance.BorderSize = 2;
        btnDigito0.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        btnDigito0.TabStop = false;
        btnDigito0.Name = "btnDigito0";
        btnDigito0.Click += AlPresionarDigito;
        // 
        // btnCorregir
        // 
        btnCorregir.Location = new Point(22, 646);
        btnCorregir.Size = new Size(92, 48);
        btnCorregir.Text = "BORRAR";
        btnCorregir.UseVisualStyleBackColor = false;
        btnCorregir.FlatStyle = FlatStyle.Flat;
        btnCorregir.BackColor = System.Drawing.ColorTranslator.FromHtml("#E6B422");
        btnCorregir.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1C1F26");
        btnCorregir.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnCorregir.FlatAppearance.BorderSize = 2;
        btnCorregir.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        btnCorregir.TabStop = false;
        btnCorregir.Name = "btnCorregir";
        btnCorregir.Click += AlPresionarCorregir;
        // 
        // btnAceptar
        // 
        btnAceptar.Location = new Point(222, 646);
        btnAceptar.Size = new Size(92, 48);
        btnAceptar.Text = "ACEPTAR";
        btnAceptar.UseVisualStyleBackColor = false;
        btnAceptar.FlatStyle = FlatStyle.Flat;
        btnAceptar.BackColor = System.Drawing.ColorTranslator.FromHtml("#2FA84F");
        btnAceptar.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnAceptar.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnAceptar.FlatAppearance.BorderSize = 2;
        btnAceptar.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        btnAceptar.TabStop = false;
        btnAceptar.Name = "btnAceptar";
        btnAceptar.Click += AlPresionarAceptar;
        // 
        // btnCancelar
        // 
        btnCancelar.Location = new Point(22, 702);
        btnCancelar.Size = new Size(292, 48);
        btnCancelar.Text = "CANCELAR";
        btnCancelar.UseVisualStyleBackColor = false;
        btnCancelar.FlatStyle = FlatStyle.Flat;
        btnCancelar.BackColor = System.Drawing.ColorTranslator.FromHtml("#D1432F");
        btnCancelar.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnCancelar.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#444B57");
        btnCancelar.FlatAppearance.BorderSize = 2;
        btnCancelar.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        btnCancelar.TabStop = false;
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Click += AlPresionarCancelar;
        // 
        // lblRotuloTarjeta
        // 
        lblRotuloTarjeta.Location = new Point(332, 478);
        lblRotuloTarjeta.Size = new Size(206, 16);
        lblRotuloTarjeta.Text = "TARJETA";
        lblRotuloTarjeta.ForeColor = System.Drawing.ColorTranslator.FromHtml("#14305C");
        lblRotuloTarjeta.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblRotuloTarjeta.AutoSize = false;
        lblRotuloTarjeta.Name = "lblRotuloTarjeta";
        // 
        // cmbTarjeta
        // 
        cmbTarjeta.Location = new Point(332, 496);
        cmbTarjeta.Size = new Size(206, 23);
        cmbTarjeta.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTarjeta.FormattingEnabled = true;
        cmbTarjeta.Name = "cmbTarjeta";
        // 
        // btnTarjeta
        // 
        btnTarjeta.Location = new Point(332, 526);
        btnTarjeta.Size = new Size(206, 34);
        btnTarjeta.Text = "Insertar tarjeta";
        btnTarjeta.UseVisualStyleBackColor = false;
        btnTarjeta.FlatStyle = FlatStyle.Flat;
        btnTarjeta.BackColor = System.Drawing.ColorTranslator.FromHtml("#2A63B8");
        btnTarjeta.ForeColor = Color.White;
        btnTarjeta.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#17376B");
        btnTarjeta.FlatAppearance.BorderSize = 2;
        btnTarjeta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTarjeta.TabStop = false;
        btnTarjeta.Name = "btnTarjeta";
        btnTarjeta.Click += AlPresionarTarjeta;
        // 
        // lblRotuloComprobante
        // 
        lblRotuloComprobante.Location = new Point(332, 572);
        lblRotuloComprobante.Size = new Size(206, 16);
        lblRotuloComprobante.Text = "COMPROBANTE";
        lblRotuloComprobante.ForeColor = System.Drawing.ColorTranslator.FromHtml("#14305C");
        lblRotuloComprobante.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblRotuloComprobante.AutoSize = false;
        lblRotuloComprobante.Name = "lblRotuloComprobante";
        // 
        // lblTicket
        // 
        lblTicket.AutoSize = false;
        lblTicket.Location = new Point(332, 590);
        lblTicket.Size = new Size(206, 120);
        lblTicket.BackColor = System.Drawing.ColorTranslator.FromHtml("#FDFDF6");
        lblTicket.ForeColor = System.Drawing.ColorTranslator.FromHtml("#222222");
        lblTicket.BorderStyle = BorderStyle.FixedSingle;
        lblTicket.Padding = new Padding(6);
        lblTicket.Font = new Font("Consolas", 8.25F);
        lblTicket.Name = "lblTicket";
        // 
        // lblRotuloEfectivo
        // 
        lblRotuloEfectivo.Location = new Point(332, 716);
        lblRotuloEfectivo.Size = new Size(206, 16);
        lblRotuloEfectivo.Text = "SALIDA DE EFECTIVO";
        lblRotuloEfectivo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#14305C");
        lblRotuloEfectivo.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        lblRotuloEfectivo.AutoSize = false;
        lblRotuloEfectivo.Name = "lblRotuloEfectivo";
        // 
        // lblEfectivo
        // 
        lblEfectivo.AutoSize = false;
        lblEfectivo.Location = new Point(332, 734);
        lblEfectivo.Size = new Size(206, 34);
        lblEfectivo.BackColor = System.Drawing.ColorTranslator.FromHtml("#101215");
        lblEfectivo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6DFF8A");
        lblEfectivo.TextAlign = ContentAlignment.MiddleCenter;
        lblEfectivo.Font = new Font("Consolas", 11.25F, FontStyle.Bold);
        lblEfectivo.Name = "lblEfectivo";
        // 
        // pnlPantalla
        // 
        pnlPantalla.Controls.Add(lblMensaje);
        pnlPantalla.Controls.Add(lblEtiqueta0);
        pnlPantalla.Controls.Add(lblEtiqueta1);
        pnlPantalla.Controls.Add(lblEtiqueta2);
        pnlPantalla.Controls.Add(lblEtiqueta3);
        pnlPantalla.Controls.Add(lblEtiqueta4);
        pnlPantalla.Controls.Add(lblEtiqueta5);
        pnlPantalla.Controls.Add(lblEtiqueta6);
        pnlPantalla.Controls.Add(lblEtiqueta7);
        // 
        // pnlBisel
        // 
        pnlBisel.Controls.Add(pnlPantalla);
        pnlBisel.Controls.Add(btnLateral0);
        pnlBisel.Controls.Add(btnLateral1);
        pnlBisel.Controls.Add(btnLateral2);
        pnlBisel.Controls.Add(btnLateral3);
        pnlBisel.Controls.Add(btnLateral4);
        pnlBisel.Controls.Add(btnLateral5);
        pnlBisel.Controls.Add(btnLateral6);
        pnlBisel.Controls.Add(btnLateral7);
        // 
        // FormCajero
        // 
        AutoScaleMode = AutoScaleMode.None;
        BackColor = System.Drawing.ColorTranslator.FromHtml("#B8BEC9");
        ClientSize = new Size(560, 790);
        Controls.Add(lblMarca);
        Controls.Add(pnlBisel);
        Controls.Add(btnDigito1);
        Controls.Add(btnDigito2);
        Controls.Add(btnDigito3);
        Controls.Add(btnDigito4);
        Controls.Add(btnDigito5);
        Controls.Add(btnDigito6);
        Controls.Add(btnDigito7);
        Controls.Add(btnDigito8);
        Controls.Add(btnDigito9);
        Controls.Add(btnDigito0);
        Controls.Add(btnCorregir);
        Controls.Add(btnAceptar);
        Controls.Add(btnCancelar);
        Controls.Add(lblRotuloTarjeta);
        Controls.Add(cmbTarjeta);
        Controls.Add(btnTarjeta);
        Controls.Add(lblRotuloComprobante);
        Controls.Add(lblTicket);
        Controls.Add(lblRotuloEfectivo);
        Controls.Add(lblEfectivo);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormCajero";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Cajero Automático - Banco ISFT 93";
        pnlPantalla.ResumeLayout(false);
        pnlBisel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Label lblMarca;
    private Panel pnlBisel;
    private Panel pnlPantalla;
    private Label lblMensaje;
    private Label lblEtiqueta0;
    private Label lblEtiqueta1;
    private Label lblEtiqueta2;
    private Label lblEtiqueta3;
    private Label lblEtiqueta4;
    private Label lblEtiqueta5;
    private Label lblEtiqueta6;
    private Label lblEtiqueta7;
    private Button btnLateral0;
    private Button btnLateral1;
    private Button btnLateral2;
    private Button btnLateral3;
    private Button btnLateral4;
    private Button btnLateral5;
    private Button btnLateral6;
    private Button btnLateral7;
    private Button btnDigito1;
    private Button btnDigito2;
    private Button btnDigito3;
    private Button btnDigito4;
    private Button btnDigito5;
    private Button btnDigito6;
    private Button btnDigito7;
    private Button btnDigito8;
    private Button btnDigito9;
    private Button btnDigito0;
    private Button btnCorregir;
    private Button btnAceptar;
    private Button btnCancelar;
    private Label lblRotuloTarjeta;
    private ComboBox cmbTarjeta;
    private Button btnTarjeta;
    private Label lblRotuloComprobante;
    private Label lblTicket;
    private Label lblRotuloEfectivo;
    private Label lblEfectivo;
}
