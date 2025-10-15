using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public Articulo() 
        {
            Imagen = new List<Imagen>();
        }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion  { get; set; }
        public Marca IdMarca { get; set; }
        public Categoria IdCategoria { get; set; }
        public decimal Precio { get; set; }

        public List<Imagen> Imagen { get; set; }

        public static object filtrar(string campo, string criterio, string filtro)
        {
            throw new NotImplementedException();
        }
    }
}
