using Productos.API.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace Productos.API.Model.domains
{
    public class CategoriaDto
    {        
        public int Categoria_ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
       
    }
}
