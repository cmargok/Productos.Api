using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Productos.API.Model.Entities
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int Categoria_ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public List<Producto> Productos { get; set; }
    }
}
