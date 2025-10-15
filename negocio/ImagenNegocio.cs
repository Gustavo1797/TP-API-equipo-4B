using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Remoting.Lifetime;

namespace negocio
{
    public class ImagenNegocio
    {
        public void Agregar(List<string> listUrlImg,int idArticulo )
        {            

            foreach (String urlImg in listUrlImg) { 
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("Insert into IMAGENES (IdArticulo , ImagenUrl) values(@IdArt , @ImgUrl)");
                    datos.setearParametro("@IdArt", idArticulo);
                    datos.setearParametro("@ImgUrl", urlImg);
                    datos.ejecutarAccion();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally {
                    datos.cerrarConexion();

                }
            }
        }

        public void Eliminar(List<int> listUrlImgBorrar)
        {

            foreach (int id in listUrlImgBorrar)
            {
                AccesoDatos datos = new AccesoDatos();
                try 
                {
                    datos.setearConsulta("Delete from IMAGENES where Id = @ID");
                    datos.setearParametro("@ID", id);
                    datos.ejecutarAccion();

                } 
                catch (Exception ex) 
                { 
                    throw ex;
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }

        }
    }
}
