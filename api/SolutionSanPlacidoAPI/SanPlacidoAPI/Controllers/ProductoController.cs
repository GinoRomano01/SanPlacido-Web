using Microsoft.AspNetCore.Mvc;
using SanPlacidoDATA;
using SanPlacidoModelo;
using System.Data.SqlClient;
using static SanPlacidoModelo.editarproducto;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SanPlacidoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductosData _data;

        // 👇 Ahora recibe directamente ProductosData
        public ProductoController(ProductosData data)
        {
            _data = data;
        }
        
        // GET: api/<ProductoController>

        [HttpGet]
        public IActionResult Get()
        {
            var productos = _data.MostrarProductos();
            return Ok(productos); // ya devuelve lista como JSON
        }


        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var producto = _data.MostrarProductoPorId(id);
            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });
            return Ok(producto);
        }

        // POST api/<ProductoController>
        [HttpPost]
        public IActionResult Post([FromBody] CargarProducto producto)
        {
            if (producto == null)
                return BadRequest("El producto no puede ser nulo.");

            _data.InsertarProducto(producto);

            return Ok(new { success = true, message = "Producto cargado correctamente" });
        }

        [HttpPut("editar")]
        public IActionResult EditarProducto([FromBody] ProductoDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest(new { mensaje = "Datos inválidos" });

            try
            {
                _data.ActualizarProducto(dto);
                return Ok(new { mensaje = "Producto actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar producto", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _data.EliminarProducto(id);
            return Ok(new { message = "Producto eliminado" });
        }
    }
}
