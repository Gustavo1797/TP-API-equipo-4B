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
using static System.Net.Mime.MediaTypeNames;

namespace TPWinForm_equipo_4B
{
    public partial class frmAgregarArticulo : Form
    {
        private Articulo articulo = null;
        private List<Imagen> listImagen = new List<Imagen>();
        private List<int> listUrlImgBorrar = new List<int>();

        public frmAgregarArticulo()
        {

            InitializeComponent();
            dgvArticulo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
            dgvArticulo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();
            ImagenNegocio negocioImg = new ImagenNegocio();
            int idArticulo = 0;

            try
            {   
                if (articulo == null) articulo = new Articulo();

                articulo.Codigo = txtCodigoArticulo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.IdMarca = (Marca)cbMarca.SelectedItem;
                articulo.IdCategoria = (Categoria)cbCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                List<String> listUrlImg = new List<String>();
                listUrlImg = cargarImagenParaCrear();

                if (articulo.Id != 0){

                    negocio.Modificar(articulo);

                    if (listUrlImg.Count > 0) negocioImg.Agregar(listUrlImg, articulo.Id);
                    if (listUrlImgBorrar.Count > 0) negocioImg.Eliminar(listUrlImgBorrar);                    

                    MessageBox.Show("Modificado exitosamente.");

                }
                else{

                    negocio.Agregar(articulo);
                    idArticulo = negocio.ObtenerIdArticulo(articulo);
                    negocioImg.Agregar(listUrlImg, idArticulo);
                    
                    MessageBox.Show("Agregado exitosamente.");

                }

                
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private List<String> cargarImagenParaCrear()
        {
            List <String> listUrlImgAux = new List<String>();
            int cantImagen = listImagen.Count;
            try 
            {
                for (int i = 0; i < cantImagen; i++)
                {
                    if (listImagen[i].ID == -1)
                    {
                        listUrlImgAux.Add(listImagen[i].ImagenUrl);
                    }
                }
                return listUrlImgAux;
            }
            catch(Exception ex) 
            { 
                throw ex;
            }            
        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();


            try
            {
                cbCategoria.DataSource = categoriaNegocio.listar();
                cbCategoria.ValueMember = "id";
                cbCategoria.DisplayMember = "Descripcion";
                cbMarca.DataSource = marcaNegocio.listar();
                cbMarca.ValueMember = "id";
                cbMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtNombre.Text = articulo.Nombre;
                    txtCodigoArticulo.Text = articulo.Codigo;
                    txtDescripcion.Text = articulo.Descripcion;
                    cbMarca.SelectedValue = articulo.IdMarca;
                    cbCategoria.SelectedValue= articulo.IdCategoria;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cbMarca.SelectedValue = articulo.IdMarca.Id;
                    cbCategoria.SelectedValue = articulo.IdCategoria.Id;
                    listImagen = articulo.Imagen.ToList();
                    cargarDGV();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btSavedImaged_Click(object sender, EventArgs e)
        {

            try {
                if (string.IsNullOrEmpty(txtUrlImagen.Text)) {
                    MessageBox.Show("Ingrese url de imagen para guardar");
                }
                else {

                    Imagen auxImage = new Imagen();
                    auxImage.ID = -1;
                    auxImage.ImagenUrl = txtUrlImagen.Text;
                    listImagen.Add(auxImage);
                    cargarDGV();
                    txtUrlImagen.Clear();

                }                    

            }catch (Exception ex){

                throw ex;

            }
        }

        private void cargarDGV()
        {
            try 
            {

                dgvArticulo.DataSource = null;
                dgvArticulo.DataSource = listImagen;

                ocultarColumnas();

                if (listImagen.Count == 0)
                {
                    dgvArticulo.ClearSelection();
                    pbArticulo.Image = null;
                    return;
                }

                if (dgvArticulo.Rows.Count == listImagen.Count)
                {
                    int ultimoIndiceList = listImagen.Count - 1;
                    int colImagenUrl = dgvArticulo.Columns["ImagenUrl"].Index;
                    dgvArticulo.CurrentCell = dgvArticulo[colImagenUrl, ultimoIndiceList];
                    dgvArticulo.FirstDisplayedScrollingRowIndex = ultimoIndiceList;                    
                    Imagen imagen = (Imagen)dgvArticulo.CurrentRow.DataBoundItem;
                    cargarImagen(imagen.ImagenUrl);
                    
                }

            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulo.Columns["ID"].Visible = false;
            dgvArticulo.Columns["IdArticulo"].Visible = false;

        }

        private void cargarImagen(String imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception)
            {                
                pbArticulo.Load("https://e1.pngegg.com/pngimages/50/931/png-clipart-through-the-ages-empty-street-thumbnail.png");
            }
        }

        private void btDeletedImage_Click(object sender, EventArgs e)
        {
            try {

                if (listImagen.Count > 0)
                {

                    Imagen imagen = (Imagen)dgvArticulo.CurrentRow.DataBoundItem;
                    if (imagen.ID > 0) listUrlImgBorrar.Add(imagen.ID); 
                    int auxInd = indiceImagenPorBorrar(imagen.ImagenUrl);
                    listImagen.RemoveAt(auxInd);
                    cargarDGV();

                }
                else {

                    MessageBox.Show("No hay imagen para borrar");

                }

            } catch (Exception ex) {

                MessageBox.Show("Error al borrar la imagen: " + ex.Message);

            }
        }

        private int indiceImagenPorBorrar(String imagenPorBorrar) 
        {
            int cantIndice = listImagen.Count;
            for (int i = 0; i < cantIndice; i++)
            {
                if (listImagen[i].ImagenUrl.Equals(imagenPorBorrar))
                {
                    return i;
                }
            }
            return -1;
        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvArticulo.Rows.Count == listImagen.Count) 
                {
                    if (dgvArticulo.CurrentRow != null) 
                    {
                        Imagen imagen = (Imagen)dgvArticulo.CurrentRow.DataBoundItem;
                        cargarImagen(imagen.ImagenUrl);
                    }
                }                

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
