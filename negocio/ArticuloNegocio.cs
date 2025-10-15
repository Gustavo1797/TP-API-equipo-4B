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
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            Articulo artAct = null;
            int idArtAct = -1, idNuenoArt = -1;

            try
            {
                datos.setearConsulta("SELECT A.Id, Codigo, Nombre, A.Descripcion, IdMarca, IdCategoria, Precio, E.ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, E.Id IdImagenes FROM ARTICULOS A, IMAGENES E, MARCAS M, CATEGORIAS C WHERE E.IdArticulo = A.Id AND A.IdMarca = M.Id AND A.IdCategoria = C.Id order by A.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    idNuenoArt = datos.Lector.GetInt32(0);
                    if (idArtAct != idNuenoArt)
                    {
                        if (idArtAct != -1) lista.Add(artAct);
                        artAct = new Articulo();
                        idArtAct = idNuenoArt;
                        artAct.Id = idNuenoArt;
                        artAct.Codigo = datos.Lector.GetString(1);
                        artAct.Nombre = (string)datos.Lector["Nombre"];
                        artAct.Descripcion = (string)datos.Lector["Descripcion"];
                        artAct.IdMarca = new Marca();
                        artAct.IdMarca.Id = (int)datos.Lector["IdMarca"];
                        artAct.IdMarca.Descripcion = (string)datos.Lector["Marca"];
                        artAct.IdCategoria = new Categoria();
                        artAct.IdCategoria.Id = (int)datos.Lector["IdCategoria"];
                        artAct.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];
                        artAct.Precio = (decimal)datos.Lector["Precio"];
                    }
                    Imagen auxImagen = new Imagen();
                    auxImagen.ID = (int)datos.Lector["IdImagenes"];
                    auxImagen.IdArticulo = (int)datos.Lector["Id"];
                    auxImagen.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    artAct.Imagen.Add(auxImagen);
                }   

                if (idArtAct != -1) lista.Add(artAct);

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

        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) " + 
                    "values('" + nuevo.Codigo + "'" + ", '" + nuevo.Nombre + "'" + ", '" + nuevo.Descripcion + "'" + ", " + nuevo.Precio + "" + ", " + nuevo.IdMarca.Id + "" + ", " + nuevo.IdCategoria.Id + ")" );
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

        public void Modificar(Articulo articulo) {
            
            AccesoDatos accesoDatos = new AccesoDatos();

            try{

                accesoDatos.setearConsulta("update ARTICULOS set Codigo = @cod, Nombre = @nom, Descripcion = @desc, IdMarca = @idm, IdCategoria = @idc, Precio = @precio Where Id = @id");
                accesoDatos.setearParametro("@cod", articulo.Codigo);
                accesoDatos.setearParametro("@nom", articulo.Nombre);
                accesoDatos.setearParametro("@desc", articulo.Descripcion);     
                accesoDatos.setearParametro("@idm", articulo.IdMarca.Id);
                accesoDatos.setearParametro("@idc", articulo.IdCategoria.Id);
                accesoDatos.setearParametro("@precio", articulo.Precio);
                accesoDatos.setearParametro("@id", articulo.Id);

                accesoDatos.ejecutarAccion();

            }
            catch (Exception ex){

                throw ex;

            }finally{ 

                accesoDatos.cerrarConexion();

            }
        
        }

        public void Eliminar(int Id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(" delete from ARTICULOS where id= @id ");
                datos.setearParametro("@id ", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ObtenerIdArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            int idArticulo = 0;

            try
            {
                //select id from ARTICULOS where Codigo = 'S012'
                datos.setearConsulta("select Id from ARTICULOS where Codigo = @cod");
                datos.setearParametro("@cod", articulo.Codigo);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) {
                    return (int)datos.Lector["Id"];
                } else {
                    throw new Exception("No se encontró el id de artículo con ese código.");
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                datos.cerrarConexion();
            }
        }


        public List<Articulo> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.Id, Codigo, Nombre, A.Descripcion, IdMarca, IdCategoria, Precio, E.ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria " +
                                  "FROM ARTICULOS A, IMAGENES E, MARCAS M, CATEGORIAS C " +
                                  "WHERE E.IdArticulo = A.Id AND A.IdMarca = M.Id AND A.IdCategoria = C.Id AND ";

                if (campo == "Código")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Codigo > @filtro";
                            break;
                        case "Menor a":
                            consulta += "Codigo < @filtro";
                            break;
                        default:
                            consulta += "Codigo = @filtro";
                            break;
                    }
                }
                else if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > @filtro";
                            break;
                        case "Menor a":
                            consulta += "Precio < @filtro";
                            break;
                        default:
                            consulta += "Precio = @filtro";
                            break;
                    }
                }
                else // Nombre
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre LIKE @filtro";
                            filtro = filtro + "%";
                            break;
                        case "Termina con":
                            consulta += "Nombre LIKE @filtro";
                            filtro = "%" + filtro;
                            break;
                        default: // Contiene
                            consulta += "Nombre LIKE @filtro";
                            filtro = "%" + filtro + "%";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@filtro", filtro);
                datos.ejecutarLectura();

                Articulo artAct = null;
                int idArtAct = -1, idNuevoArt = -1;

                while (datos.Lector.Read())
                {
                    idNuevoArt = (int)datos.Lector["Id"];
                    if (idArtAct != idNuevoArt)
                    {
                        if (idArtAct != -1) lista.Add(artAct);

                        artAct = new Articulo();
                        artAct.Imagen = new List<Imagen>();
                        idArtAct = idNuevoArt;

                        artAct.Id = idNuevoArt;
                        artAct.Codigo = datos.Lector.GetString(1);
                        artAct.Nombre = (string)datos.Lector["Nombre"];
                        artAct.Descripcion = (string)datos.Lector["Descripcion"];
                        artAct.IdMarca = new Marca { Id = (int)datos.Lector["IdMarca"], Descripcion = (string)datos.Lector["Marca"] };
                        artAct.IdCategoria = new Categoria { Id = (int)datos.Lector["IdCategoria"], Descripcion = (string)datos.Lector["Categoria"] };
                        artAct.Precio = (decimal)datos.Lector["Precio"];
                    }

                    // Agregar imagen
                    Imagen auxImagen = new Imagen
                    {
                        IdArticulo = (int)datos.Lector["Id"],
                        ImagenUrl = (string)datos.Lector["ImagenUrl"]
                    };
                    artAct.Imagen.Add(auxImagen);
                }

                if (idArtAct != -1) lista.Add(artAct);

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

    }
}


