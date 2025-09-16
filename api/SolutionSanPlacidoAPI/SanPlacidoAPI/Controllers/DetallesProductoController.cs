using Microsoft.AspNetCore.Mvc;
using SanPlacidoDATA;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SanPlacidoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesProductoController : ControllerBase
    {
        private readonly DetallesdeProductoData _dato;

        public DetallesProductoController(IConfiguration config)
        {
            _dato = new DetallesdeProductoData(config.GetConnectionString("CadenaSQL"));
        }

        [HttpGet("categorias")]
        public IActionResult GetCategorias()
        {
            return Ok(_dato.ObtenerCategorias());
        }

        [HttpGet("tiposproducto")]
        public IActionResult GetTiposProducto()
        {
            return Ok(_dato.ObtenerTiposProducto());
        }

        [HttpGet("tiposmadera")]
        public IActionResult GetTiposMadera()
        {
            return Ok(_dato.ObtenerTiposMadera());
        }

        [HttpGet("tiposacabado")]
        public IActionResult GetTiposAcabado()
        {
            return Ok(_dato.ObtenerTiposAcabado());
        }


        // GET api/<DetallesProductoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DetallesProductoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DetallesProductoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DetallesProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
