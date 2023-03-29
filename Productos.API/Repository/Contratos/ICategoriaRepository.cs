using Productos.API.Model.Entities;

namespace Productos.API.Repository.Contratos
{
    public interface ICategoriaRepository
    {
        public Task<IEnumerable<Categoria>> GetCategorias();
    }
}
