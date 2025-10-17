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
        [HttpPost]
        [Route("api/Articulo")]
        public HttpResponseMessage Post([FromBody] ArticuloDTO articuloDTO)
        {
            try
            {
                if (articuloDTO == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se ingresaron atributos a la solicitud.");

                ArticuloNegocio negocioArt = new ArticuloNegocio();

                if (string.IsNullOrWhiteSpace(articuloDTO.Codigo))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se ingreso el código del artículo, el mismo es obligatorio.");

                if (!negocioArt.Listar().Any(x => x.Codigo == articuloDTO.Codigo))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El codigo ingresado ya existe "+ articuloDTO.Codigo +", ingrese otro codigo.");

                if (string.IsNullOrWhiteSpace(articuloDTO.Nombre))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se ingreso el nombre del artículo, el mismo es obligatorio.");

                if (string.IsNullOrWhiteSpace(articuloDTO.Descripcion))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se ingreso la descripcion del artículo, el mismo es obligatorio.");

                if (articuloDTO.Precio == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El campo precio es obligario, debe ingresar un valor mayor a 0.");

                if (articuloDTO.Precio < 0)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El articulo no puede tener un precio negativo, se debe ingresar un precio mayor a 0.");

                
                MarcaNegocio negocioMar = new MarcaNegocio();
                if (!negocioMar.listar().Any(x => x.Id == articuloDTO.IdMarca))                    
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No existe una marca con ID: "+ articuloDTO.IdMarca);

                CategoriaNegocio categoriaNeg = new CategoriaNegocio();
                if (!categoriaNeg.listar().Any(x => x.Id == articuloDTO.IdCategoria))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No existe una categoría con ID: "+ articuloDTO.IdCategoria);

                Articulo nuevoArticulo = new Articulo();
                nuevoArticulo.Codigo = articuloDTO.Codigo;
                nuevoArticulo.Nombre = articuloDTO.Nombre;
                nuevoArticulo.Descripcion = articuloDTO.Descripcion;
                nuevoArticulo.IdMarca = new Marca { Id = articuloDTO.IdMarca };
                nuevoArticulo.IdCategoria = new Categoria { Id = articuloDTO.IdCategoria };
                nuevoArticulo.Precio = articuloDTO.Precio;                
                
                negocioArt.Agregar(nuevoArticulo);
                return Request.CreateResponse(HttpStatusCode.OK, "Articulo agregado correctamente");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,"Ocurrió un error inesperado");
            }            
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
