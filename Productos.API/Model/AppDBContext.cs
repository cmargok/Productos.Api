using Microsoft.EntityFrameworkCore;
using Productos.API.Model.Entities;
using Productos.API.Model.Entities.Security;

namespace Productos.API.Model
{
    public class AppDBContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Rol> Roles { get; set; }
        //public DbSet<Carrito> Carrito { get; set; }
        //public DbSet<Compra> Compras { get; set; }
        //public DbSet<Orden> Ordenes { get; set; }
        public AppDBContext()
        {

        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
    }
}
