using Productos.API.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Productos.API.Model.domains
{
    public class ProductoConCategoriaDto
    {
        public int Producto_ID { get; set; }

        public string Producto { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string ImagenUrl { get; set; } = string.Empty;

        public int Cantidad { get; set; }
        public int Precio { get; set; }

        public int Categoria_ID { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
