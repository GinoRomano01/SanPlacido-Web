using System;


namespace SanPlacidoModelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombredeUsuario { get; set; }
        public string Contraseña { get; set; }
        public string CorreoElectronico { get; set; }

        public int IdTipoUsuario { get; set; }
        public int IdTipodeRol { get; set; }
        public int IdCliente { get; set; } // Relación con Cliente, si aplica
    }
}
