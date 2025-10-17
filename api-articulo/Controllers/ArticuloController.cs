using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dominio;
using negocio;


namespace api_articulo.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        [HttpGet]
        [Route("api/Articulo")]
        public IEnumerable<Articulo> Get()

        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            return articulo.Listar();
        }

        // GET: api/Articulo/5
        [HttpGet]
        [Route("api/Articulo/{id}")]
        public HttpResponseMessage Get(int id)
        {

            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El ID debe ser mayor que 0.");
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.Listar();
                Articulo articulo = lista.Find(x => x.Id == id);

                if (articulo == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No se encontro un artículo con ID {id}.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, articulo);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al obtener el articulo " + ex.Message);
            }
        }

        // POST: api/Articulo
        public void Post([FromBody] ArticuloDTO articuloDTO)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevoArticulo = new Articulo();
            nuevoArticulo.Codigo = articuloDTO.Codigo;
            nuevoArticulo.Nombre = articuloDTO.Nombre;
            nuevoArticulo.Descripcion = articuloDTO.Descripcion;
            nuevoArticulo.IdMarca = new Marca { Id = articuloDTO.IdMarca };
            nuevoArticulo.IdCategoria = new Categoria { Id = articuloDTO.IdCategoria };
            nuevoArticulo.Precio = articuloDTO.Precio;
            negocio.Agregar(nuevoArticulo);
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody] ArticuloDTO articuloDTO)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevoArticulo = new Articulo();
            nuevoArticulo.Codigo = articuloDTO.Codigo;
            nuevoArticulo.Nombre = articuloDTO.Nombre;
            nuevoArticulo.Descripcion = articuloDTO.Descripcion;
            nuevoArticulo.IdMarca = new Marca { Id = articuloDTO.IdMarca };
            nuevoArticulo.IdCategoria = new Categoria { Id = articuloDTO.IdCategoria };
            nuevoArticulo.Precio = articuloDTO.Precio;
            nuevoArticulo.Id = id;

            negocio.Modificar(nuevoArticulo);
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.Eliminar(id);
        }
    }
}
