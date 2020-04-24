using System;
using System.Windows.Forms;
using Presentacion.Base.Varios;

namespace Presentacion.Base
{
    public partial class FormularioBase : Form
    {
        public long? EntidadId { get; set; }

        public FormularioBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metodo para Limpiar el contenido los controles
        /// </summary>
        /// <param name="obj">Objeto contenedor</param>
        public virtual void LimpiarDatos(object obj)
        {
            if (obj is Form)
            {
                foreach (var item in ((Form)obj).Controls)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Clear();
                    }

                    if (item is NumericUpDown)
                    {
                        ((NumericUpDown)item).Value = ((NumericUpDown)item).Minimum;
                    }

                    if (item is RichTextBox)
                    {
                        ((RichTextBox)item).Clear();
                    }

                    if (item is ComboBox)
                    {
                        if (((ComboBox)item).Items.Count > 0)
                        {
                            ((ComboBox)item).SelectedIndex = 0;
                        }
                    }

                    if (item is CheckBox)
                    {
                        ((CheckBox)item).Checked = false;
                    }

                    if (item is PictureBox)
                    {
                        ((PictureBox)item).Image = Constante.ImagenControl.ImagenDeFondo;
                    }

                    if (item is Panel)
                    {
                        LimpiarDatos(item);
                    }

                    if (item is UserControl)
                    {
                        LimpiarDatos(item);
                    }

                    if (item is GroupBox)
                    {
                        LimpiarDatos(item);
                    }
                }
            }
            else if (obj is Panel)
            {
                foreach (var item in ((Panel)obj).Controls)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Clear();
                    }

                    if (item is NumericUpDown)
                    {
                        ((NumericUpDown)item).Value = ((NumericUpDown)item).Minimum;
                    }

                    if (item is RichTextBox)
                    {
                        ((RichTextBox)item).Clear();
                    }

                    if (item is ComboBox)
                    {
                        if (((ComboBox)item).Items.Count > 0)
                        {
                            ((ComboBox)item).SelectedIndex = 0;
                        }
                    }

                    if (item is CheckBox)
                    {
                        ((CheckBox)item).Checked = false;
                    }

                    if (item is PictureBox)
                    {
                        ((PictureBox)item).Image = Constante.ImagenControl.ImagenDeFondo;
                    }

                    if (item is Panel)
                    {
                        LimpiarDatos(item);
                    }

                    if (item is Control)
                    {
                        LimpiarDatos(item);
                    }

                    if (item is GroupBox)
                    {
                        LimpiarDatos(item);
                    }
                }
            }
            else if (obj is GroupBox)
            {
                foreach (var item in ((GroupBox)obj).Controls)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Clear();
                    }

                    if (item is NumericUpDown)
                    {
                        ((NumericUpDown)item).Value = ((NumericUpDown)item).Minimum;
                    }

                    if (item is RichTextBox)
                    {
                        ((RichTextBox)item).Clear();
                    }

                    if (item is ComboBox)
                    {
                        if (((ComboBox)item).Items.Count > 0)
                        {
                            ((ComboBox)item).SelectedIndex = 0;
                        }
                    }

                    if (item is CheckBox)
                    {
                        ((CheckBox)item).Checked = false;
                    }

                    if (item is PictureBox)
                    {
                        ((PictureBox)item).Image = Constante.ImagenControl.ImagenDeFondo;
                    }

                    if (item is Panel)
                    {
                        LimpiarDatos(item);
                    }

                    if (item is Control)
                    {
                        LimpiarDatos(item);
                    }
                    if (item is GroupBox)
                    {
                        LimpiarDatos(item);
                    }
                }
            }
            else if (obj is UserControl)
            {
                foreach (var item in ((Control)obj).Controls)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Clear();
                    }

                    if (item is NumericUpDown)
                    {
                        ((NumericUpDown)item).Value = ((NumericUpDown)item).Minimum;
                    }

                    if (item is RichTextBox)
                    {
                        ((RichTextBox)item).Clear();
                    }

                    if (item is ComboBox)
                    {
                        if (((ComboBox)item).Items.Count > 0)
                        {
                            ((ComboBox)item).SelectedIndex = 0;
                        }
                    }

                    if (item is CheckBox)
                    {
                        ((CheckBox)item).Checked = false;
                    }

                    if (item is PictureBox)
                    {
                        ((PictureBox)item).Image = Constante.ImagenControl.ImagenDeFondo;
                    }

                    if (item is Panel)
                    {
                        LimpiarDatos(item);
                    }

                    if (item is UserControl)
                    {
                        LimpiarDatos(item);
                    }
                    if (item is GroupBox)
                    {
                        LimpiarDatos(item);
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para Activar o Desactivar los controles
        /// </summary>
        /// <param name="obj">objeto contenedor</param>
        /// <param name="estado">Estado del Control: True o False</param>
        public void ActivarControles(object obj, bool estado)
        {
            if (obj is Form)
            {
                foreach (var item in ((Form)obj).Controls)
                {
                    if (item is Button)
                    {
                        ((Button)item).Enabled = estado;
                    }

                    if (item is NumericUpDown)
                    {
                        ((NumericUpDown)item).Enabled = estado;
                    }

                    if (item is DateTimePicker)
                    {
                        ((DateTimePicker)item).Enabled = estado;
                    }

                    if (item is TextBox)
                    {
                        ((TextBox)item).Enabled = estado;
                    }

                    if (item is RichTextBox)
                    {
                        ((RichTextBox)item).Enabled = estado;
                    }

                    if (item is ComboBox)
                    {
                        ((ComboBox)item).Enabled = estado;
                    }

                    if (item is CheckBox)
                    {
                        ((CheckBox)item).Enabled = estado;
                    }

                    if (item is Panel)
                    {
                        ActivarControles(item, estado);
                    }

                    if (item is UserControl)
                    {
                        ActivarControles(item, estado);
                    }

                    if (item is GroupBox)
                    {
                        ActivarControles(item, estado);
                    }
                }
            }
            else if (obj is Panel)
            {
                foreach (var item in ((Panel)obj).Controls)
                {
                    if (item is DateTimePicker)
                    {
                        ((DateTimePicker)item).Enabled = estado;
                    }

                    if (item is Button)
                    {
                        ((Button)item).Enabled = estado;
                    }

                    if (item is TextBox)
                    {
                        ((TextBox)item).Enabled = estado;
                    }

                    if (item is NumericUpDown)
                    {
                        ((NumericUpDown)item).Enabled = estado;
                    }

                    if (item is RichTextBox)
                    {
                        ((RichTextBox)item).Enabled = estado;
                    }

                    if (item is ComboBox)
                    {
                        ((ComboBox)item).Enabled = estado;
                    }

                    if (item is CheckBox)
                    {
                        ((CheckBox)item).Enabled = estado;
                    }

                    if (item is Panel)
                    {
                        ActivarControles(item, estado);
                    }

                    if (item is UserControl)
                    {
                        ActivarControles(item, estado);
                    }

                    if (item is GroupBox)
                    {
                        ActivarControles(item, estado);
                    }
                }
            }
            else if (obj is GroupBox)
            {
                foreach (var item in ((GroupBox)obj).Controls)
                {
                    if (item is DateTimePicker)
                    {
                        ((DateTimePicker)item).Enabled = estado;
                    }

                    if (item is Button)
                    {
                        ((Button)item).Enabled = estado;
                    }

                    if (item is TextBox)
                    {
                        ((TextBox)item).Enabled = estado;
                    }

                    if (item is NumericUpDown)
                    {
                        ((NumericUpDown)item).Enabled = estado;
                    }

                    if (item is RichTextBox)
                    {
                        ((RichTextBox)item).Enabled = estado;
                    }

                    if (item is ComboBox)
                    {
                        ((ComboBox)item).Enabled = estado;
                    }

                    if (item is CheckBox)
                    {
                        ((CheckBox)item).Enabled = estado;
                    }

                    if (item is Panel)
                    {
                        ActivarControles(item, estado);
                    }

                    if (item is UserControl)
                    {
                        ActivarControles(item, estado);
                    }
                    if (item is GroupBox)
                    {
                        ActivarControles(item, estado);
                    }
                }
            }
            else if (obj is UserControl)
            {
                foreach (var item in ((Control)obj).Controls)
                {
                    if (item is DateTimePicker)
                    {
                        ((DateTimePicker)item).Enabled = estado;
                    }

                    if (item is Button)
                    {
                        ((Button)item).Enabled = estado;
                    }

                    if (item is TextBox)
                    {
                        ((TextBox)item).Enabled = estado;
                    }

                    if (item is NumericUpDown)
                    {
                        ((NumericUpDown)item).Enabled = estado;
                    }

                    if (item is RichTextBox)
                    {
                        ((RichTextBox)item).Enabled = estado;
                    }

                    if (item is ComboBox)
                    {
                        ((ComboBox)item).Enabled = estado;
                    }

                    if (item is CheckBox)
                    {
                        ((CheckBox)item).Enabled = estado;
                    }

                    if (item is Panel)
                    {
                        ActivarControles(item, estado);
                    }

                    if (item is UserControl)
                    {
                        ActivarControles(item, estado);
                    }
                    if (item is GroupBox)
                    {
                        ActivarControles(item, estado);
                    }
                }
            }
        }

        public void txt_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Constante.ColorControl.ColorSinFoco;
            }
            else if (sender is NumericUpDown)
            {
                ((NumericUpDown)sender).BackColor = Constante.ColorControl.ColorSinFoco;
            }
        }

        public void txt_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).BackColor = Constante.ColorControl.ColorConFoco;
            }
            else if (sender is NumericUpDown)
            {
                ((NumericUpDown)sender).BackColor = Constante.ColorControl.ColorConFoco;
            }
        }

        public void PoblarComboBox(ComboBox cmb, object obj, string display, string valorDevuelto)
        {
            cmb.DataSource = obj;
            cmb.DisplayMember = display;
            cmb.ValueMember = valorDevuelto;
        }

        private void FormularioBase_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormularioBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }

        public virtual void FormatearGrilla(DataGridView dgvGrilla)
        {
            for (int i = 0; i < dgvGrilla.ColumnCount; i++)
            {
                dgvGrilla.Columns[i].Visible = false;
                dgvGrilla.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}
