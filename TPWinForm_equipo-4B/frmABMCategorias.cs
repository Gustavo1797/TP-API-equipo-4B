using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TPWinForm_equipo_4B
{
    public partial class frmABMCategorias : Form
    {
        private List<Categoria> listaCategorias;
        public frmABMCategorias()
        {
            InitializeComponent();
        }

        private void frmABMCategorias_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            listaCategorias = negocio.listar();
            dgvCategorias.DataSource = listaCategorias;
            ocultarColumnas();
        }
        private void ocultarColumnas()
        {
            dgvCategorias.Columns["Id"].Visible = false;
        }

        private void btnAgregarCat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbCategoria.Text))
                {
                    MessageBox.Show("No se puede guardar una marca con campo vacío.");
                }
                else
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.Agregar(tbCategoria.Text);
                    tbCategoria.Text = string.Empty;
                    MessageBox.Show("Categoria nueva generada.");
                    cargarDGV();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnEditarCat_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarCat_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaCategorias.Count > 0)
                {
                    Categoria categoria = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.Eliminar(categoria.Id);
                    MessageBox.Show("Marca borrada.");
                    cargarDGV();
                }
                else
                {
                    MessageBox.Show("No hay marca para borrar.");
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
                dgvCategorias.DataSource = null;
                CategoriaNegocio negocio = new CategoriaNegocio();
                listaCategorias = negocio.listar();
                dgvCategorias.DataSource = listaCategorias;

                ocultarColumnas();

                if (listaCategorias.Count == 0)
                {
                    dgvCategorias.ClearSelection();
                    tbCategoria.Text = string.Empty;
                    return;
                }

                if (dgvCategorias.Rows.Count == listaCategorias.Count)
                {
                    int ultimoIndiceList = listaCategorias.Count - 1;
                    int colImagenUrl = dgvCategorias.Columns["Descripcion"].Index;
                    dgvCategorias.CurrentCell = dgvCategorias[colImagenUrl, ultimoIndiceList];
                    dgvCategorias.FirstDisplayedScrollingRowIndex = ultimoIndiceList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
