using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Productos.API.Model.Entities
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int Producto_ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Descripcion { get; set; } = string.Empty;

        [MaxLength]
        public string ImagenUrl { get; set; } = string.Empty;

        public int Cantidad { get; set; }
        public int Precio { get; set; }

        [ForeignKey("Categoria")]
        public int Categoria_ID { get; set; }
        public Categoria Categoria { get; set; }
    }
}
