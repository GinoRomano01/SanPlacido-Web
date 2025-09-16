using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SanPlacidoDATA;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SanPlacidoAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TipodeDniController : ControllerBase
    {
        // GET: api/<TipodeDniController>
        private readonly LocalidadDni _data;

        public TipodeDniController(IOptions<ConnectionStrings> opciones)
        {
            _data = new LocalidadDni(opciones);
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var lista = _data.ObtenerTiposDeDni();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener tipos de DNI", detalle = ex.Message });
            }
        }

        // GET api/<TipodeDniController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TipodeDniController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TipodeDniController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipodeDniController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
