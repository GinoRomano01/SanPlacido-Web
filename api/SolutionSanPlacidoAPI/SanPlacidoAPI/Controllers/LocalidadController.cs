using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SanPlacidoDATA;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SanPlacidoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {

        private readonly LocalidadDni _data;

        public LocalidadController(IOptions<ConnectionStrings> opciones)
        {
            _data = new LocalidadDni(opciones);
        }
        // GET: api/<LocalidadController>


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var lista = _data.ObtenerLocalidades();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener localidades", detalle = ex.Message });
            }
        }


        // GET api/<LocalidadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LocalidadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LocalidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LocalidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
