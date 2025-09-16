using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SanPlacidoDATA;
using SanPlacidoModelo; 
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SanPlacidoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteUsuarioController : ControllerBase
    {

        private readonly ClienteUsuarioData _data;

        public ClienteUsuarioController(IOptions<ConnectionStrings> opciones)
        {
            _data = new ClienteUsuarioData(opciones);
        }




        // GET: api/<ClienteUsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClienteUsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClienteUsuarioController>
        [HttpPost]
        public IActionResult CrearClienteUsuario([FromBody] ClienteUsuario dto)
        {
            if (dto == null)
                return BadRequest("Los datos no pueden ser nulos.");

            try
            {
                int idCliente = _data.CrearClienteYUsuario(dto);
                return Ok(new
                {
                    Mensaje = "Cliente y usuario creados correctamente.",
                    IdCliente = idCliente
                });
            }
            catch (Exception ex)
            {
                // Mostramos el mensaje exacto
                return StatusCode(500, new
                {
                    Mensaje = "Ocurrió un error al crear el cliente y el usuario.",
                    Detalle = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        // PUT api/<ClienteUsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClienteUsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
