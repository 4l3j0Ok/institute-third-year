#nullable disable

namespace CuentaBancariaWinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpCuenta = new GroupBox();
            lblSaldo = new Label();
            lblSaldoEtiqueta = new Label();
            lblTitular = new Label();
            lblTitularEtiqueta = new Label();
            grpOperacion = new GroupBox();
            btnRetirar = new Button();
            btnDepositar = new Button();
            txtImporte = new TextBox();
            lblImporteEtiqueta = new Label();
            lblEstado = new Label();
            grpCuenta.SuspendLayout();
            grpOperacion.SuspendLayout();
            SuspendLayout();
            // 
            // grpCuenta
            // 
            grpCuenta.Controls.Add(lblSaldo);
            grpCuenta.Controls.Add(lblSaldoEtiqueta);
            grpCuenta.Controls.Add(lblTitular);
            grpCuenta.Controls.Add(lblTitularEtiqueta);
            grpCuenta.Dock = DockStyle.Top;
            grpCuenta.Location = new Point(20, 20);
            grpCuenta.Name = "grpCuenta";
            grpCuenta.Size = new Size(319, 100);
            grpCuenta.TabIndex = 0;
            grpCuenta.TabStop = false;
            grpCuenta.Text = "Cuenta bancaria";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSaldo.Location = new Point(90, 63);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(24, 15);
            lblSaldo.TabIndex = 3;
            lblSaldo.Text = "$ 0";
            // 
            // lblSaldoEtiqueta
            // 
            lblSaldoEtiqueta.AutoSize = true;
            lblSaldoEtiqueta.Location = new Point(15, 63);
            lblSaldoEtiqueta.Name = "lblSaldoEtiqueta";
            lblSaldoEtiqueta.Size = new Size(39, 15);
            lblSaldoEtiqueta.TabIndex = 2;
            lblSaldoEtiqueta.Text = "Saldo:";
            // 
            // lblTitular
            // 
            lblTitular.AutoSize = true;
            lblTitular.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitular.Location = new Point(90, 31);
            lblTitular.Name = "lblTitular";
            lblTitular.Size = new Size(43, 15);
            lblTitular.TabIndex = 1;
            lblTitular.Text = "Titular";
            // 
            // lblTitularEtiqueta
            // 
            lblTitularEtiqueta.AutoSize = true;
            lblTitularEtiqueta.Location = new Point(15, 31);
            lblTitularEtiqueta.Name = "lblTitularEtiqueta";
            lblTitularEtiqueta.Size = new Size(44, 15);
            lblTitularEtiqueta.TabIndex = 0;
            lblTitularEtiqueta.Text = "Titular:";
            // 
            // grpOperacion
            // 
            grpOperacion.Controls.Add(btnRetirar);
            grpOperacion.Controls.Add(btnDepositar);
            grpOperacion.Controls.Add(txtImporte);
            grpOperacion.Controls.Add(lblImporteEtiqueta);
            grpOperacion.Dock = DockStyle.Top;
            grpOperacion.Location = new Point(20, 120);
            grpOperacion.Name = "grpOperacion";
            grpOperacion.Size = new Size(319, 110);
            grpOperacion.TabIndex = 1;
            grpOperacion.TabStop = false;
            grpOperacion.Text = "Operación";
            // 
            // btnRetirar
            // 
            btnRetirar.Anchor = AnchorStyles.Right;
            btnRetirar.Location = new Point(218, 62);
            btnRetirar.Name = "btnRetirar";
            btnRetirar.Size = new Size(94, 27);
            btnRetirar.TabIndex = 3;
            btnRetirar.Text = "Retirar";
            btnRetirar.UseVisualStyleBackColor = true;
            btnRetirar.Click += btnRetirar_Click;
            // 
            // btnDepositar
            // 
            btnDepositar.Anchor = AnchorStyles.Right;
            btnDepositar.Location = new Point(118, 62);
            btnDepositar.Name = "btnDepositar";
            btnDepositar.Size = new Size(94, 27);
            btnDepositar.TabIndex = 2;
            btnDepositar.Text = "Depositar";
            btnDepositar.UseVisualStyleBackColor = true;
            btnDepositar.Click += btnDepositar_Click;
            // 
            // txtImporte
            // 
            txtImporte.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtImporte.Location = new Point(90, 28);
            txtImporte.MaxLength = 9;
            txtImporte.Name = "txtImporte";
            txtImporte.Size = new Size(222, 23);
            txtImporte.TabIndex = 1;
            txtImporte.KeyPress += txtImporte_KeyPress;
            // 
            // lblImporteEtiqueta
            // 
            lblImporteEtiqueta.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblImporteEtiqueta.AutoSize = true;
            lblImporteEtiqueta.Location = new Point(15, 31);
            lblImporteEtiqueta.Name = "lblImporteEtiqueta";
            lblImporteEtiqueta.Size = new Size(52, 15);
            lblImporteEtiqueta.TabIndex = 0;
            lblImporteEtiqueta.Text = "Importe:";
            // 
            // lblEstado
            // 
            lblEstado.Dock = DockStyle.Top;
            lblEstado.Location = new Point(20, 230);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(319, 36);
            lblEstado.TabIndex = 2;
            lblEstado.Text = "Ingresá un importe y elegí una operación.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(359, 276);
            Controls.Add(lblEstado);
            Controls.Add(grpOperacion);
            Controls.Add(grpCuenta);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimumSize = new Size(375, 315);
            Name = "Form1";
            Padding = new Padding(20);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Implementación de clases";
            grpCuenta.ResumeLayout(false);
            grpCuenta.PerformLayout();
            grpOperacion.ResumeLayout(false);
            grpOperacion.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpCuenta;
        private System.Windows.Forms.Label lblTitularEtiqueta;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.Label lblSaldoEtiqueta;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.GroupBox grpOperacion;
        private System.Windows.Forms.Label lblImporteEtiqueta;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Button btnDepositar;
        private System.Windows.Forms.Button btnRetirar;
        private System.Windows.Forms.Label lblEstado;
    }
}
