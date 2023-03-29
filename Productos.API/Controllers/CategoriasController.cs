using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productos.API.Model.domains;
using Productos.API.Model.domains.Search;
using Productos.API.Repository.Contratos;

namespace Productos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetListOf()
        {
            var data = await _categoriaRepository.GetCategorias();

            IEnumerable<CategoriaDto> categorias = data.Select(x => new CategoriaDto
            {
                Categoria_ID = x.Categoria_ID,
                Nombre = x.Nombre,
            }).ToList();

            var response = GetDefaultResponse(data);
            return Ok(response);
        }

        public ResponseDTO<T> GetDefaultResponse<T>(T dato)
               => new() { Status = 200, Data = dato };
    }
}
