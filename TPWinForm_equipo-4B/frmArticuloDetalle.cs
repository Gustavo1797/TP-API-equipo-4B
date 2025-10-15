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
    public partial class frmArticuloDetalle : Form
    {
        private Articulo articuloAux;
        public frmArticuloDetalle(Articulo articulo)
        {
            InitializeComponent();
            articuloAux = articulo;
        }

        private void frmArticuloDetalle_Load(object sender, EventArgs e)
        {
            lbCodigoD2.Text = articuloAux.Codigo;
            lbNombreD2.Text = articuloAux.Nombre;
            lbDescripcionD2.Text = articuloAux.Descripcion;
            lbMarcaD2.Text = articuloAux.IdMarca.Descripcion;
            lbCategoriaD2.Text = articuloAux.IdCategoria.Descripcion;
            lbPrecioD2.Text = articuloAux.Precio.ToString("C");

            try
            {
                pbImagenD.Load(articuloAux.Imagen[0].ImagenUrl);
            }
            catch
            {
                pbImagenD.Load("https://e1.pngegg.com/pngimages/50/931/png-clipart-through-the-ages-empty-street-thumbnail.png");
            }
        }
    }
}
