using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_articulo.Controllers
{
    public class ImagenController : ApiController
    {

        // POST api/Imagen
        [HttpPost]
        [Route("api/Imagen")]
        public HttpResponseMessage Post([FromBody] ImagenDTO imagenDTO)
        {
            try
            {
                ArticuloNegocio negocioArt = new ArticuloNegocio();

                if (imagenDTO == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se ingresaron atributos a la solicitud.");

                if (!negocioArt.Listar().Any(x => x.Id == imagenDTO.IdArticulo))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El Id de articulo ingresado no existe: " + imagenDTO.IdArticulo + ", ingrese otro Id.");

                if (imagenDTO.ImagenUrl == null || imagenDTO.ImagenUrl.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se ingreso ningun atributo de Imagen, es obligatorio ingresar minimamente una url de imagen.");
                }

                if (imagenDTO.ImagenUrl.Count > 0) 
                {
                    foreach (string s in imagenDTO.ImagenUrl)
                    {
                        if (string.IsNullOrWhiteSpace(s))
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Se intento ingresar una URL de imagen vacía, por favor de ingresar una url.");
                        }
                    }                    
                }

                ImagenNegocio negocioIMG = new ImagenNegocio();
                negocioIMG.Agregar(imagenDTO.ImagenUrl, imagenDTO.IdArticulo);

                return Request.CreateResponse(HttpStatusCode.OK, "Articulo agregado correctamente");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado");
            }

        }
    }
}