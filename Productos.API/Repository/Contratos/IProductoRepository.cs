using Productos.API.Model.domains.Search;
using Productos.API.Model.domains;
using Productos.API.Model.Entities;
using static Productos.API.Utils.PagingExtention;

namespace Productos.API.Repository.Contratos
{
    public interface IProductoRepository
    {
        public Task<int> CreateProductoAsync(Producto producto);
        public Task<Producto> GetProductoAsync(int id);
        public Task<bool> UpdateProductoAsync(int id, Producto producto);
        public Task<bool> DeleteProductoAsync(int id);
        public Task<DataCollection<ProductoConCategoriaDto>> GetAllListed(SearchDto parameters);
    }
}
