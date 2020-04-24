namespace Presentacion.Core.ControlesUsuarios
{
    partial class UserControlMesa
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlEstado = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abrirMesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarMesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarDeMesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservarMesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.habilitarMesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarTemporalmenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelarVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblNumeroMesa = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEstado
            // 
            this.pnlEstado.ContextMenuStrip = this.menu;
            this.pnlEstado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEstado.Location = new System.Drawing.Point(0, 0);
            this.pnlEstado.Name = "pnlEstado";
            this.pnlEstado.Size = new System.Drawing.Size(98, 19);
            this.pnlEstado.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(81)))));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirMesaToolStripMenuItem,
            this.cerrarMesaToolStripMenuItem,
            this.cambiarDeMesaToolStripMenuItem,
            this.reservarMesaToolStripMenuItem,
            this.habilitarMesaToolStripMenuItem,
            this.cerrarTemporalmenteToolStripMenuItem,
            this.cancelarVentaToolStripMenuItem});
            this.menu.Name = "contextMenuStrip1";
            this.menu.ShowImageMargin = false;
            this.menu.Size = new System.Drawing.Size(168, 180);
            // 
            // abrirMesaToolStripMenuItem
            // 
            this.abrirMesaToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(81)))));
            this.abrirMesaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.abrirMesaToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.abrirMesaToolStripMenuItem.Name = "abrirMesaToolStripMenuItem";
            this.abrirMesaToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.abrirMesaToolStripMenuItem.Text = "Abrir Mesa";
            this.abrirMesaToolStripMenuItem.Click += new System.EventHandler(this.abrirMesaToolStripMenuItem_Click);
            // 
            // cerrarMesaToolStripMenuItem
            // 
            this.cerrarMesaToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(81)))));
            this.cerrarMesaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cerrarMesaToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.cerrarMesaToolStripMenuItem.Name = "cerrarMesaToolStripMenuItem";
            this.cerrarMesaToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.cerrarMesaToolStripMenuItem.Text = "Cerrar Mesa";
            this.cerrarMesaToolStripMenuItem.Click += new System.EventHandler(this.cerrarMesaToolStripMenuItem_Click);
            // 
            // cambiarDeMesaToolStripMenuItem
            // 
            this.cambiarDeMesaToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(81)))));
            this.cambiarDeMesaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cambiarDeMesaToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.cambiarDeMesaToolStripMenuItem.Name = "cambiarDeMesaToolStripMenuItem";
            this.cambiarDeMesaToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.cambiarDeMesaToolStripMenuItem.Text = "Cambiar de Mesa";
            this.cambiarDeMesaToolStripMenuItem.Click += new System.EventHandler(this.cambiarDeMesaToolStripMenuItem_Click);
            // 
            // reservarMesaToolStripMenuItem
            // 
            this.reservarMesaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reservarMesaToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.reservarMesaToolStripMenuItem.Name = "reservarMesaToolStripMenuItem";
            this.reservarMesaToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.reservarMesaToolStripMenuItem.Text = "Reservar Mesa";
            this.reservarMesaToolStripMenuItem.Click += new System.EventHandler(this.reservarMesaToolStripMenuItem_Click);
            // 
            // habilitarMesaToolStripMenuItem
            // 
            this.habilitarMesaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.habilitarMesaToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.habilitarMesaToolStripMenuItem.Name = "habilitarMesaToolStripMenuItem";
            this.habilitarMesaToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.habilitarMesaToolStripMenuItem.Text = "Habilitar Mesa";
            this.habilitarMesaToolStripMenuItem.Click += new System.EventHandler(this.habilitarMesaToolStripMenuItem_Click);
            // 
            // cerrarTemporalmenteToolStripMenuItem
            // 
            this.cerrarTemporalmenteToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cerrarTemporalmenteToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.cerrarTemporalmenteToolStripMenuItem.Name = "cerrarTemporalmenteToolStripMenuItem";
            this.cerrarTemporalmenteToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.cerrarTemporalmenteToolStripMenuItem.Text = "Cerrar Temporalmente";
            this.cerrarTemporalmenteToolStripMenuItem.Click += new System.EventHandler(this.cerrarTemporalmenteToolStripMenuItem_Click);
            // 
            // cancelarVentaToolStripMenuItem
            // 
            this.cancelarVentaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelarVentaToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.cancelarVentaToolStripMenuItem.Name = "cancelarVentaToolStripMenuItem";
            this.cancelarVentaToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.cancelarVentaToolStripMenuItem.Text = "Cancelar Venta";
            this.cancelarVentaToolStripMenuItem.Click += new System.EventHandler(this.cancelarVentaToolStripMenuItem_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(81)))));
            this.lblTotal.ContextMenuStrip = this.menu;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTotal.Location = new System.Drawing.Point(0, 94);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(98, 29);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumeroMesa
            // 
            this.lblNumeroMesa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(81)))));
            this.lblNumeroMesa.ContextMenuStrip = this.menu;
            this.lblNumeroMesa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumeroMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroMesa.ForeColor = System.Drawing.Color.DarkGray;
            this.lblNumeroMesa.Location = new System.Drawing.Point(0, 19);
            this.lblNumeroMesa.Name = "lblNumeroMesa";
            this.lblNumeroMesa.Size = new System.Drawing.Size(98, 75);
            this.lblNumeroMesa.TabIndex = 2;
            this.lblNumeroMesa.Text = "1";
            this.lblNumeroMesa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNumeroMesa.DoubleClick += new System.EventHandler(this.lblNumeroMesa_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.ContextMenuStrip = this.menu;
            this.panel1.Location = new System.Drawing.Point(0, 89);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(98, 2);
            this.panel1.TabIndex = 3;
            // 
            // UserControlMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblNumeroMesa);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.pnlEstado);
            this.Name = "UserControlMesa";
            this.Size = new System.Drawing.Size(98, 123);
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEstado;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.ContextMenuStrip menu;
        public System.Windows.Forms.Label lblNumeroMesa;
        private System.Windows.Forms.ToolStripMenuItem abrirMesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarMesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarDeMesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservarMesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habilitarMesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarTemporalmenteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelarVentaToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}
