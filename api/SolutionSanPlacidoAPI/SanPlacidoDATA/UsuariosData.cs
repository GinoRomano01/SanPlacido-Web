using Microsoft.Extensions.Options;
using SanPlacidoModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanPlacidoDATA
{
    public class UsuariosData
    {
        private readonly string _cadenaSQL;

        public UsuariosData(IOptions<ConnectionStrings> opciones)
        {
            _cadenaSQL = opciones.Value.CadenaSQL;
        }
        public void EditarUsuario(EditarUsuario usuario)
        {
            using SqlConnection connection = new SqlConnection(_cadenaSQL);
            connection.Open();

            try
            {
                using (SqlCommand cmdUsuario = new SqlCommand("sp_editarUsuarios", connection))
                {
                    cmdUsuario.CommandType = CommandType.StoredProcedure;
                    cmdUsuario.Parameters.AddWithValue("@Id", usuario.Id);
                    cmdUsuario.Parameters.AddWithValue("@NombredeUsuario", usuario.NombredeUsuario);
                    cmdUsuario.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    cmdUsuario.Parameters.AddWithValue("@CorreoElectronico", usuario.CorreoElectronico);
                    cmdUsuario.Parameters.AddWithValue("@IdTipodeUsuario", usuario.IdTipoUsuario);
                    cmdUsuario.Parameters.AddWithValue("@IdTipodeRol", usuario.IdTipodeRol);
                    

                    cmdUsuario.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error general: {ex.Message}");
            }
        }

        
    }
}
