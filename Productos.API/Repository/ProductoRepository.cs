using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Productos.API.Model;
using Productos.API.Model.domains;
using Productos.API.Model.domains.Search;
using Productos.API.Model.Entities;
using Productos.API.Repository.Contratos;
using Productos.API.Utils;
using static Productos.API.Utils.PagingExtention;

namespace Productos.API.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDBContext _appDBContext;
        public ProductoRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;   
        }

        public async Task<int> CreateProductoAsync(Producto producto)
        {            
            _appDBContext.Productos.Add(producto);  

            await _appDBContext.SaveChangesAsync();

            return producto.Producto_ID;
        }
        public async Task<Producto> GetProductoAsync(int id)
        {
             var productoGuardado = await _appDBContext.Productos.Where(pr => pr.Producto_ID == id).FirstOrDefaultAsync();

            if (productoGuardado != null) return productoGuardado;

            return null!;
           
        }
        public async Task<bool> UpdateProductoAsync(int id,Producto producto)
        {
            var productoGuardado = await _appDBContext.Productos.Where(pr => pr.Producto_ID == id).FirstOrDefaultAsync();

            if(productoGuardado != null)
            {
                productoGuardado.Nombre = producto.Nombre;

                productoGuardado.Descripcion = producto.Descripcion;

                if (!producto.ImagenUrl.Equals("null"))
                {
                    productoGuardado.ImagenUrl = producto.ImagenUrl;
                }

                await _appDBContext.SaveChangesAsync();
                return true;
            }            

            return false;
        }
        public async Task<bool> DeleteProductoAsync(int id)
        {
            var productoGuardado = await _appDBContext.Productos.Where(pr => pr.Producto_ID == id).FirstOrDefaultAsync();
            if (productoGuardado != null)
            {
                _appDBContext.Productos.Remove(productoGuardado);

                await _appDBContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
         

        public async Task<DataCollection<ProductoConCategoriaDto>>GetAllListed(SearchDto parameters)
        {
            var query = _appDBContext.Productos
                .Include(ct => ct.Categoria)
                .Select(x => new ProductoConCategoriaDto
                {
                    Categoria_ID = x.Categoria_ID,
                    Categoria = x.Categoria.Nombre,
                    Producto_ID = x.Producto_ID,
                    Producto = x.Nombre,
                    Descripcion = x.Descripcion,
                    ImagenUrl = x.ImagenUrl,
                    Precio = x.Precio
                });

            if(!String.IsNullOrWhiteSpace(parameters.Producto))
            {
                query = query.Where(x => x.Producto.Contains(parameters.Producto));
            }
            if (!String.IsNullOrWhiteSpace(parameters.Descripcion))
            {
                query = query.Where(x => x.Descripcion.Contains(parameters.Descripcion));
            }
            if (!String.IsNullOrWhiteSpace(parameters.Categoria))
            {
                query = query.Where(x => x.Categoria.Contains(parameters.Categoria));
            }          
            if (!String.IsNullOrWhiteSpace(parameters.Columna))
            {
                query = query.ApplyOrderDirection(parameters.Columna.Trim(), parameters.AscendingOrder);
            }


            var nose = await query.GetPagedAsync(Convert.ToInt32(parameters.Page), Convert.ToInt32(parameters.Take));    
            
            return nose;

        }

    }

    
}
