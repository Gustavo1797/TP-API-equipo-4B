using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_equipo_4B
{
    public partial class frmABMMarcas : Form
    {
        private List<Marca> listMarcas;
        public frmABMMarcas()
        {
            InitializeComponent();
        }

        private void frmABMMarcas_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            listMarcas = negocio.listar();
            dgvMarcas.DataSource = listMarcas;
            ocultarColumnas();
        }

        private void ocultarColumnas()
        {
            dgvMarcas.Columns["Id"].Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbMarca.Text))
                {
                    MessageBox.Show("No se puede guardar una marca con campo vacio");
                }
                else
                {
                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.Agregar(tbMarca.Text);
                    tbMarca.Text = string.Empty;
                    MessageBox.Show("Marca nueva generada");
                    cargarDGV();                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (listMarcas.Count > 0)
                {
                    Marca marca = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.Eliminar(marca.Id);
                    MessageBox.Show("Marca borrada");
                    cargarDGV();
                }
                else
                {
                    MessageBox.Show("No hay marca para borrar");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al borrar la marca: " + ex.Message);
            }
        }

        private void cargarDGV()
        {
            try
            {
                dgvMarcas.DataSource = null;
                MarcaNegocio negocio = new MarcaNegocio();
                listMarcas = negocio.listar();
                dgvMarcas.DataSource = listMarcas;

                ocultarColumnas();

                if (listMarcas.Count == 0)
                {
                    dgvMarcas.ClearSelection();
                    tbMarca.Text = string.Empty;
                    return;
                }

                if (dgvMarcas.Rows.Count == listMarcas.Count)
                {
                    int ultimoIndiceList = listMarcas.Count - 1;
                    int colImagenUrl = dgvMarcas.Columns["Descripcion"].Index;
                    dgvMarcas.CurrentCell = dgvMarcas[colImagenUrl, ultimoIndiceList];
                    dgvMarcas.FirstDisplayedScrollingRowIndex = ultimoIndiceList;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
