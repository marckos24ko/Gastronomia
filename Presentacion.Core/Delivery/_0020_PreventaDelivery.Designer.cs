namespace Presentacion.Core.Delivery
{
    partial class _0020_PreventaDelivery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlGriila = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.imgBuscar = new System.Windows.Forms.PictureBox();
            this.dgvgrilla = new System.Windows.Forms.DataGridView();
            this.Menu = new System.Windows.Forms.ToolStrip();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnContinuar = new System.Windows.Forms.ToolStripButton();
            this.btnNuevaVenta = new System.Windows.Forms.ToolStripButton();
            this.btnVentasAbiertas = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUsuarioLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlGriila.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBuscar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvgrilla)).BeginInit();
            this.Menu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGriila
            // 
            this.pnlGriila.Controls.Add(this.label1);
            this.pnlGriila.Controls.Add(this.txtBuscar);
            this.pnlGriila.Controls.Add(this.btnBuscar);
            this.pnlGriila.Controls.Add(this.lblBuscar);
            this.pnlGriila.Controls.Add(this.imgBuscar);
            this.pnlGriila.Controls.Add(this.dgvgrilla);
            this.pnlGriila.Enabled = false;
            this.pnlGriila.Location = new System.Drawing.Point(0, 55);
            this.pnlGriila.Name = "pnlGriila";
            this.pnlGriila.Size = new System.Drawing.Size(613, 259);
            this.pnlGriila.TabIndex = 10;
            this.pnlGriila.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 27);
            this.label1.TabIndex = 13;
            this.label1.Text = "Seleccione un cliente de la lista";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(61, 228);
            this.txtBuscar.MaxLength = 250;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(457, 22);
            this.txtBuscar.TabIndex = 11;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(99)))), ((int)(((byte)(178)))));
            this.btnBuscar.Location = new System.Drawing.Point(524, 227);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(87, 23);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblBuscar
            // 
            this.lblBuscar.BackColor = System.Drawing.Color.Transparent;
            this.lblBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblBuscar.Location = new System.Drawing.Point(61, 198);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(343, 27);
            this.lblBuscar.TabIndex = 10;
            this.lblBuscar.Text = "Busqueda";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imgBuscar
            // 
            this.imgBuscar.BackColor = System.Drawing.Color.Transparent;
            this.imgBuscar.ErrorImage = global::Presentacion.Core.Properties.Resources.Buscar;
            this.imgBuscar.Image = global::Presentacion.Core.Properties.Resources.Buscar;
            this.imgBuscar.Location = new System.Drawing.Point(6, 198);
            this.imgBuscar.Name = "imgBuscar";
            this.imgBuscar.Size = new System.Drawing.Size(49, 52);
            this.imgBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgBuscar.TabIndex = 9;
            this.imgBuscar.TabStop = false;
            // 
            // dgvgrilla
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(65)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgvgrilla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvgrilla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvgrilla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvgrilla.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(65)))), ((int)(((byte)(91)))));
            this.dgvgrilla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(99)))), ((int)(((byte)(178)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvgrilla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvgrilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvgrilla.EnableHeadersVisualStyles = false;
            this.dgvgrilla.Location = new System.Drawing.Point(3, 36);
            this.dgvgrilla.Name = "dgvgrilla";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(65)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvgrilla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(65)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvgrilla.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvgrilla.Size = new System.Drawing.Size(605, 156);
            this.dgvgrilla.TabIndex = 6;
            this.dgvgrilla.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvgrilla_CellEnter);
            this.dgvgrilla.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvgrilla_RowEnter);
            // 
            // Menu
            // 
            this.Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(99)))), ((int)(((byte)(178)))));
            this.Menu.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCancelar,
            this.btnContinuar,
            this.btnNuevaVenta,
            this.btnVentasAbiertas});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(613, 52);
            this.Menu.TabIndex = 9;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = global::Presentacion.Core.Properties.Resources.Cancelar;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(57, 49);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnContinuar
            // 
            this.btnContinuar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnContinuar.ForeColor = System.Drawing.Color.White;
            this.btnContinuar.Image = global::Presentacion.Core.Properties.Resources.Grabar;
            this.btnContinuar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(64, 49);
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnContinuar.Visible = false;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // btnNuevaVenta
            // 
            this.btnNuevaVenta.ForeColor = System.Drawing.Color.White;
            this.btnNuevaVenta.Image = global::Presentacion.Core.Properties.Resources.Moneda;
            this.btnNuevaVenta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevaVenta.Name = "btnNuevaVenta";
            this.btnNuevaVenta.Size = new System.Drawing.Size(77, 49);
            this.btnNuevaVenta.Text = "Nueva Venta";
            this.btnNuevaVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevaVenta.Click += new System.EventHandler(this.btnNuevaVenta_Click);
            // 
            // btnVentasAbiertas
            // 
            this.btnVentasAbiertas.ForeColor = System.Drawing.Color.White;
            this.btnVentasAbiertas.Image = global::Presentacion.Core.Properties.Resources.dinero;
            this.btnVentasAbiertas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVentasAbiertas.Name = "btnVentasAbiertas";
            this.btnVentasAbiertas.Size = new System.Drawing.Size(91, 49);
            this.btnVentasAbiertas.Text = "Ventas Abiertas";
            this.btnVentasAbiertas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVentasAbiertas.Click += new System.EventHandler(this.btnVentasAbiertas_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUsuarioLogin});
            this.statusStrip1.Location = new System.Drawing.Point(0, 317);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(613, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblUsuarioLogin
            // 
            this.lblUsuarioLogin.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuarioLogin.ForeColor = System.Drawing.Color.Black;
            this.lblUsuarioLogin.Name = "lblUsuarioLogin";
            this.lblUsuarioLogin.Size = new System.Drawing.Size(50, 17);
            this.lblUsuarioLogin.Text = "Usuario:";
            // 
            // _0020_PreventaDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(81)))));
            this.ClientSize = new System.Drawing.Size(613, 339);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlGriila);
            this.Controls.Add(this.Menu);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(629, 378);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(629, 378);
            this.Name = "_0020_PreventaDelivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preventa Delivery";
            this.pnlGriila.ResumeLayout(false);
            this.pnlGriila.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBuscar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvgrilla)).EndInit();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlGriila;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.PictureBox imgBuscar;
        private System.Windows.Forms.DataGridView dgvgrilla;
        public System.Windows.Forms.ToolStrip Menu;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnContinuar;
        private System.Windows.Forms.ToolStripButton btnNuevaVenta;
        private System.Windows.Forms.ToolStripButton btnVentasAbiertas;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel lblUsuarioLogin;
        private System.Windows.Forms.Label label1;
    }
}