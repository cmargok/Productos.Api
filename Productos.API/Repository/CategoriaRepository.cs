using Microsoft.EntityFrameworkCore;
using Productos.API.Model;
using Productos.API.Model.Entities;
using Productos.API.Repository.Contratos;

namespace Productos.API.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDBContext _appDBContext;
        public CategoriaRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
                                      => await _appDBContext.Categorias.ToListAsync();
        
    }
}
