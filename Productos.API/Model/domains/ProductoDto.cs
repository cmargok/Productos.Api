using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Productos.API.Model.domains
{
    public class ProductoDto
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Descripcion { get; set; } = string.Empty;

        [MaxLength]
        public string ImagenUrl { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public int Categoria_ID { get; set; }
    }
}
