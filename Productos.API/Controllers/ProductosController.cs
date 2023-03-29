using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productos.API.Model.domains;
using Productos.API.Model.domains.Search;
using Productos.API.Repository;
using Productos.API.Service;

namespace Productos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _ProductoService;
        public ProductosController(IProductoService ProductoService)
        {
            _ProductoService = ProductoService;
        }

        [HttpGet("Listed")]
        public async Task<IActionResult> GetListOf([FromQuery] SearchDto Busqueda)
        {
            var data = await _ProductoService.GetListedProductoAsync(Busqueda);
            var response = GetDefaultResponse(data);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductoDto productoNuevo)
        {
            var data = await _ProductoService.CreateProducto(productoNuevo);

            var response = GetDefaultResponse(data);

            return Ok(response);
        }

        [HttpGet("{ProductoId}")]
        public async Task<IActionResult> GetListOf(int ProductoId)
        {
            var data = await _ProductoService.GetProductoByIdAsync(ProductoId);
            var response = GetDefaultResponse(data);
            return Ok(response);
        }

        [HttpPut("{ProductoId}")]
        public async Task<IActionResult> Modify( int ProductoId, ProductoDto productoModified)
        {
            var data = await _ProductoService.ModifyProductoAsync(ProductoId, productoModified);

            if (!data)  return NotFound("No se encontro el producto solicitado");

            var response = GetDefaultResponse(data);
            return Ok(response);
        }

        [HttpDelete("{ProductoId}")]
        public async Task<IActionResult> Delete(int ProductoId)
        {
            var data = await _ProductoService.DeleteProductoAsync(ProductoId);

            if (!data) return NotFound("No se encontro el producto solicitado");

            var response = GetDefaultResponse(data);

            return Ok(response);
        }
        public ResponseDTO<T> GetDefaultResponse<T>(T dato)
               => new() { Status = 200, Data = dato };

      
}
}
