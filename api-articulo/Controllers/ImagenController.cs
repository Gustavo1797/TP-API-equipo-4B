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
        public void Post([FromBody] ImagenDTO imagenDTO)
        {
            ImagenNegocio negocio = new ImagenNegocio();
            negocio.Agregar(imagenDTO.ImagenUrl , imagenDTO.IdArticulo);

        }
    }
}