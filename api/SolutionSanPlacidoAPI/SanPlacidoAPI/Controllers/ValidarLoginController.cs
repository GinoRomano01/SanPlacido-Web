using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SanPlacidoDATA;
using SanPlacidoModelo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SanPlacidoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidarLoginController : ControllerBase
    {

        private readonly ClienteUsuarioData _data;

        public ValidarLoginController(IOptions<ConnectionStrings> opciones)
        {
            _data = new ClienteUsuarioData(opciones);
        }


        // GET: api/<ValidarLoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValidarLoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValidarLoginController>
        [HttpPost]
        public IActionResult Post([FromBody] LoginDTO login)
        {
            if (login == null || string.IsNullOrEmpty(login.NombredeUsuario) || string.IsNullOrEmpty(login.Contrasena))
                return BadRequest("Debe enviar usuario y contraseña");

            bool esValido = _data.ValidarUsuario(login.NombredeUsuario, login.Contrasena);

            if (esValido)
                return Ok(new { success = true, message = "Usuario válido" });
            else
                return Unauthorized(new { success = false, message = "Usuario o contraseña incorrectos" });
        }

        // PUT api/<ValidarLoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValidarLoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
