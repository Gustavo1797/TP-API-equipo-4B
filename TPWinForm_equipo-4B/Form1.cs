using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
namespace TPWinForm_equipo_4B
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;
        private int indiceAuxArticulo, cantImagenes;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Código");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Precio");
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulo = negocio.Listar();
            dgvArticulo.DataSource = listaArticulo;
            cargarImagen(listaArticulo[0].Imagen[0].ImagenUrl);
            //dgvArticulo.Columns["IdCategoria"].Visible = false;
            //dgvArticulo.Columns["IdMarca"].Visible = false;
            //dgvArticulo.Columns["imagen"].Visible = false;
            ocultarColumnas();
            //CAMBIAR NOMBRES DE COLUMNAS DEL DGV

            //dgvArticulo.Columns["Descripcion"].HeaderText = "Descripción";
            visualizarBotonesImagenes(listaArticulo[0]);
        }

        private void ocultarColumnas()
        {
            dgvArticulo.Columns["Descripcion"].Visible = false;
            dgvArticulo.Columns["Id"].Visible = false;
            dgvArticulo.Columns["IdMarca"].HeaderText = "Marca";
            dgvArticulo.Columns["IdCategoria"].HeaderText = "Categoría";
            dgvArticulo.Columns["Codigo"].HeaderText = "Código";

        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow != null)
            {
                Articulo Selecionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                cargarImagen(Selecionado.Imagen[0].ImagenUrl);
                visualizarBotonesImagenes(Selecionado);
            }
        }

        private void cargarImagen(String imagen)
        {
            try
            {
                pcbArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pcbArticulo.Load("https://e1.pngegg.com/pngimages/50/931/png-clipart-through-the-ages-empty-street-thumbnail.png");
            }
        }

        private void bttAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo alta = new frmAgregarArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            frmAgregarArticulo modificar = new frmAgregarArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("CONFIRMAR ELIMINACION", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                { 
                    seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);
                    MessageBox.Show("Eliminado exitosamente.");
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

            frmArticuloDetalle detalle = new frmArticuloDetalle(articuloSeleccionado);
            
            detalle.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
          

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listafiltrada;
            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                listafiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()));
            }
            else { listafiltrada = listaArticulo; }


            dgvArticulo.DataSource = null;
            dgvArticulo.DataSource = listafiltrada;
            ocultarColumnas();

        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Código" || opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");

            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                ArticuloNegocio negocio = new ArticuloNegocio();
                dgvArticulo.DataSource = negocio.Filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            


        }

        private void btnImgDer_Click(object sender, EventArgs e)
        {
            if (indiceAuxArticulo == cantImagenes - 1)
            {
                indiceAuxArticulo = 0;
            }
            else
            {
                indiceAuxArticulo++;
            }
            Articulo Selecionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            cargarImagen(Selecionado.Imagen[indiceAuxArticulo].ImagenUrl);
        }

        private void btnImgIzq_Click(object sender, EventArgs e)
        {
            if (indiceAuxArticulo == 0)
            {
                indiceAuxArticulo = cantImagenes - 1;
            }
            else
            {
                indiceAuxArticulo--;
            }
            Articulo Selecionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            cargarImagen(Selecionado.Imagen[indiceAuxArticulo].ImagenUrl);
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmABMMarcas frmABMMarcas = new frmABMMarcas();
            frmABMMarcas.ShowDialog();
            cargar();
        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnListarCategorias_Click(object sender, EventArgs e)
        {
            frmABMCategorias categorias = new frmABMCategorias();
            categorias.ShowDialog();
        }

        private void btnListarMarcas_Click(object sender, EventArgs e)
        {
            frmABMMarcas marcas = new frmABMMarcas();
            marcas.ShowDialog();
        }

        private void visualizarBotonesImagenes(Articulo articulo)
        {
            indiceAuxArticulo = 0;
            try
            {
                cantImagenes = articulo.Imagen.Count;
                if (cantImagenes == 1)
                {
                    btnImgDer.Visible = false;
                    btnImgIzq.Visible = false;
                }
                else
                {
                    btnImgDer.Visible = true;
                    btnImgIzq.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}



