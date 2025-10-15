using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Descripcion From MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca mar = new Marca();
                    mar.Id = (int)datos.Lector["Id"];
                    mar.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(mar);
                }
                return lista;
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


        public void Agregar(string marca)
        {
            
            AccesoDatos datos = new AccesoDatos();
            try
            {
             
                datos.setearConsulta("insert into marcas (Descripcion) values (@Descripcion)");
                datos.setearParametro("@Descripcion", marca);
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

        public void Eliminar(int id)
        {
            
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("delete from marcas where Id = @ID");
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
