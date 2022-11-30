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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private RepositorioDeOrtoedros repositorio;
        private List<Ortoedro> lista;

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmOrtoedrosAE frmOrtoedro = new frmOrtoedrosAE() { Text = "Agregar Ortoedro" };
            DialogResult dr = frmOrtoedro.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            
            Ortoedro ortoedro = frmOrtoedro.GetOrtoedro();
            
            repositorio.Agregar(ortoedro);
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, ortoedro);
            AgregarFila(r);

            MostrarCantidad(repositorio.GetCantidad());
        }

        private void MostrarCantidad(int cantidad)
        {
            CantidadTextBox.Text = cantidad.ToString();
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Ortoedro ortoedro)
        {
            r.Cells[colAristaA.Index].Value = ortoedro.AristaA;
            r.Cells[colAristaB.Index].Value = ortoedro.AristaB;
            r.Cells[colAristaC.Index].Value = ortoedro.AristaC;
            r.Cells[colRelleno.Index].Value = ortoedro.Relleno;
            r.Cells[colArea.Index].Value = ortoedro.Area();
            r.Cells[colVolumen.Index].Value = ortoedro.Volumen();

            r.Tag = ortoedro;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            repositorio.GuardarEnArchivoSecuencial();
            Application.Exit();
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Ortoedro ortoedro = (Ortoedro)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar el siguiente Ortoedro: Arista A ({ortoedro.AristaA}), Arista B ({ortoedro.AristaB}) y Arista C ({ortoedro.AristaC})?",
                "Confirmar Operacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.No)
            {
                return;
            }

            repositorio.Borrar(ortoedro);
            dgvDatos.Rows.Remove(r);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }

            var r = dgvDatos.SelectedRows[0];
            Ortoedro ortoedro = (Ortoedro)r.Tag;


            frmOrtoedrosAE frm = new frmOrtoedrosAE() { Text = "Editar Ortoedro" };
            frm.SetOrtoedro(ortoedro);

            DialogResult dr = frm.ShowDialog(this);

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            ortoedro = frm.GetOrtoedro();
            repositorio.Editar(ortoedro);
            SetearFila(r, ortoedro);
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            CargarDatosEnFiltro();
            repositorio = new RepositorioDeOrtoedros();
            int cantidad = repositorio.GetCantidad();
            MostrarCantidad(cantidad);
            if (cantidad > 0)
            {
                lista = repositorio.GetLista();
                MostrarDatosEnGrilla();
            }
        }

        private void CargarDatosEnFiltro()
        {
            var listaColores = Enum.GetValues(typeof(Relleno)).Cast<Relleno>().ToList();
            foreach (var relleno in listaColores)
            {
                FiltroToolStripComboBox.Items.Add(relleno);
            }
        }

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (var ortoedro in lista)
            {
                var r = ConstruirFila();
                SetearFila(r, ortoedro);
                AgregarFila(r);
            }
        }

        private void ascendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetOrdenAscendente();
            MostrarDatosEnGrilla();
        }

        private void tsbRefrescar_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetLista();
            MostrarDatosEnGrilla();
        }

        private void descendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetOrdenDescendente();
            MostrarDatosEnGrilla();
        }

        private void FiltroToolStripComboBox_Click(object sender, EventArgs e)
        {

        }

        private void FiltroToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var colorFiltrar = (Relleno)FiltroToolStripComboBox.ComboBox.SelectedItem;
            lista = repositorio.FiltrarPorColor(colorFiltrar);
            MostrarDatosEnGrilla();
        }
    }
}
