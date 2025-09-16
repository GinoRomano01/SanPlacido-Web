using Microsoft.AspNetCore.Mvc;
using SanPlacidoDATA;
using SanPlacidoModelo;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SanPlacidoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosData _usuariosData;

        public UsuariosController(UsuariosData usuariosData)
        {
            _usuariosData = usuariosData;
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("usuario/{id}")]
        public IActionResult EditarUsuario(int id, [FromBody] EditarUsuario usuario)
        {
            if (usuario == null)
                return BadRequest("Datos del usuario no proporcionados.");

            usuario.Id = id;

            try
            {
                _usuariosData.EditarUsuario(usuario);
                return Ok(new { mensaje = "Usuario actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }


        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
