using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SanPlacidoModelo;

namespace SanPlacidoModelo
{
    public class EditarUsuario
    {
        public int Id { get; set; }
        public string NombredeUsuario { get; set; }
        public string Contraseña { get; set; }
        public string CorreoElectronico { get; set; }

        public int IdTipoUsuario { get; set; }
        public int IdTipodeRol { get; set; }
    }
}
