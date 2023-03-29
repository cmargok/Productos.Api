using Productos.API.Model.domains;
using Productos.API.Model.domains.Search;
using Productos.API.Model.Entities;
using Productos.API.Repository.Contratos;
using static Productos.API.Utils.PagingExtention;

namespace Productos.API.Service
{
    public interface IProductoService
    {
        public Task<ProductoDto> GetProductoByIdAsync(int ProductoId);
        public Task<bool> ModifyProductoAsync(int ProductoId, ProductoDto productoModified);
        public Task<bool> DeleteProductoAsync(int ProductoId);
        public Task<int> CreateProducto(ProductoDto productoNuevo);
        public Task<DataCollection<ProductoConCategoriaDto>> GetListedProductoAsync(SearchDto Busqueda);
    }

    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _ProductoRepository;
        public ProductoService(IProductoRepository productoRepository)
        {
            _ProductoRepository = productoRepository;
        }

        public async Task<ProductoDto> GetProductoByIdAsync(int ProductoId)
        {
            var producto = await _ProductoRepository.GetProductoAsync(ProductoId);

            return new ProductoDto
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                ImagenUrl = producto.ImagenUrl,
                Categoria_ID = producto.Categoria_ID,
                Cantidad = producto.Cantidad
            };
        
        }
        public async Task<bool> ModifyProductoAsync(int ProductoId, ProductoDto productoModified)
        {          
            
            var productoToDB = new Producto
            {
                Nombre = productoModified.Nombre,
                Descripcion = productoModified.Descripcion,
                ImagenUrl = productoModified.ImagenUrl,
                Categoria_ID = productoModified.Categoria_ID,
                Cantidad = productoModified.Cantidad

            };

            var producto = await _ProductoRepository.UpdateProductoAsync(ProductoId, productoToDB);

            if (producto) return true;
            return false;
        }

        
        public async Task<DataCollection<ProductoConCategoriaDto>> GetListedProductoAsync(SearchDto Busqueda)
        {
            var data = await _ProductoRepository.GetAllListed(Busqueda);

            return data;
        }


        public async Task<bool> DeleteProductoAsync(int ProductoId)
        {
            var producto = await _ProductoRepository.DeleteProductoAsync(ProductoId);

            return producto;

        }

        public async Task<int> CreateProducto(ProductoDto productoNuevo)
        {
            var productoToDB = new Producto
            {
                Nombre = productoNuevo.Nombre,
                Descripcion = productoNuevo.Descripcion,
                ImagenUrl = productoNuevo.ImagenUrl,
                Categoria_ID = productoNuevo.Categoria_ID,
                Cantidad = productoNuevo.Cantidad
                
            };

            var productoId = await _ProductoRepository.CreateProductoAsync(productoToDB);

            return productoId;
        }

    }
}
