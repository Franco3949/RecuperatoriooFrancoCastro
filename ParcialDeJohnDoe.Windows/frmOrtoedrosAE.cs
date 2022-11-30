using ParcialDeJohnDoe.Datos;
using ParcialDeJohnDoe.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialDeJohnDoe.Windows
{
    public partial class frmOrtoedrosAE : Form
    {
        public frmOrtoedrosAE()
        {
            InitializeComponent();
        }

        private Ortoedro ortoedro = new Ortoedro();
        RepositorioDeOrtoedros listaOrtoedros;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (ortoedro != null)
            {
                AristaATextBox.Text = ortoedro.AristaA.ToString();
                AristaBTextBox.Text = ortoedro.AristaB.ToString();
                AristaCTextBox.Text = ortoedro.AristaC.ToString();
                RellenoComboBox.SelectedValue = ortoedro.Relleno;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (ortoedro == null)
                {
                    ortoedro = new Ortoedro();
                }
                
                ortoedro.AristaA = int.Parse(AristaATextBox.Text);
                ortoedro.AristaB = int.Parse(AristaBTextBox.Text);
                ortoedro.AristaC = int.Parse(AristaCTextBox.Text);
                ortoedro.Relleno = (Relleno)RellenoComboBox.SelectedIndex;

                if (ortoedro.Validar())
                {
                    DialogResult = DialogResult.OK;
                }

                errorProvider1.SetError(AristaATextBox, "Las 3 aristas no pueden tener la misma medida y deben ser valores positivos");

            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();

            if (!int.TryParse(AristaATextBox.Text, out int AristaA))
            {
                valido = false;
                errorProvider1.SetError(AristaATextBox, "Debe ingresar un numero Real");
            }
            if (!int.TryParse(AristaBTextBox.Text, out int AristaB))
            {
                valido = false;
                errorProvider1.SetError(AristaBTextBox, "Debe ingresar un numero Real");
            }
            if (!int.TryParse(AristaCTextBox.Text, out int AristaC))
            {
                valido = false;
                errorProvider1.SetError(AristaCTextBox, "Debe ingresar un numero Real");
            }
            if (RellenoComboBox.SelectedItem == null)
            {
                valido = false;
                errorProvider1.SetError(RellenoComboBox, "Debe seleccionar una opcion");
            }

            return valido;
        }
        internal Ortoedro GetOrtoedro()
        {
            return ortoedro;
        }
        internal void SetOrtoedro(Ortoedro ortoedro)
        {
            this.ortoedro = ortoedro;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
