namespace Presentacion.Core.AbonarConsumicion
{
    partial class _101_Elegir_FormaDePago
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
            this.btnPagarEfectivo = new System.Windows.Forms.Button();
            this.btnCuentaCorriente = new System.Windows.Forms.Button();
            this.PBCtaCte = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBCtaCte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPagarEfectivo
            // 
            this.btnPagarEfectivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(99)))), ((int)(((byte)(178)))));
            this.btnPagarEfectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagarEfectivo.Location = new System.Drawing.Point(65, 14);
            this.btnPagarEfectivo.Name = "btnPagarEfectivo";
            this.btnPagarEfectivo.Size = new System.Drawing.Size(141, 32);
            this.btnPagarEfectivo.TabIndex = 4;
            this.btnPagarEfectivo.Text = "Efectivo";
            this.btnPagarEfectivo.UseVisualStyleBackColor = false;
            this.btnPagarEfectivo.Click += new System.EventHandler(this.btnPagarEfectivo_Click);
            // 
            // btnCuentaCorriente
            // 
            this.btnCuentaCorriente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(99)))), ((int)(((byte)(178)))));
            this.btnCuentaCorriente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCuentaCorriente.Location = new System.Drawing.Point(65, 72);
            this.btnCuentaCorriente.Name = "btnCuentaCorriente";
            this.btnCuentaCorriente.Size = new System.Drawing.Size(141, 32);
            this.btnCuentaCorriente.TabIndex = 5;
            this.btnCuentaCorriente.Text = "Cuenta Corriente";
            this.btnCuentaCorriente.UseVisualStyleBackColor = false;
            this.btnCuentaCorriente.Click += new System.EventHandler(this.btnCuentaCorriente_Click);
            // 
            // PBCtaCte
            // 
            this.PBCtaCte.Image = global::Presentacion.Core.Properties.Resources.Cta_Corriente;
            this.PBCtaCte.Location = new System.Drawing.Point(2, 62);
            this.PBCtaCte.Name = "PBCtaCte";
            this.PBCtaCte.Size = new System.Drawing.Size(57, 53);
            this.PBCtaCte.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBCtaCte.TabIndex = 3;
            this.PBCtaCte.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Presentacion.Core.Properties.Resources.Moneda;
            this.pictureBox1.Location = new System.Drawing.Point(2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // _101_Elegir_FormaDePago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(81)))));
            this.ClientSize = new System.Drawing.Size(210, 118);
            this.Controls.Add(this.btnCuentaCorriente);
            this.Controls.Add(this.btnPagarEfectivo);
            this.Controls.Add(this.PBCtaCte);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_101_Elegir_FormaDePago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forma De Pago";
            ((System.ComponentModel.ISupportInitialize)(this.PBCtaCte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox PBCtaCte;
        private System.Windows.Forms.Button btnPagarEfectivo;
        private System.Windows.Forms.Button btnCuentaCorriente;
    }
}