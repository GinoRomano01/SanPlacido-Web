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
    public class ClienteUsuarioData
    {
        private readonly string _cadenaSQL;

        public ClienteUsuarioData(IOptions<ConnectionStrings> opciones)
        {
            _cadenaSQL = opciones.Value.CadenaSQL;
        }

        public int CrearClienteYUsuario(ClienteUsuario dto)
        {
            using SqlConnection connection = new SqlConnection(_cadenaSQL);
            connection.Open();

            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                int idCliente;

                // Insertar Cliente
                using (SqlCommand cmdCliente = new SqlCommand("sp_crearClienteUsuario", connection, transaction))
                {
                    cmdCliente.CommandType = CommandType.StoredProcedure;
                    cmdCliente.Parameters.AddWithValue("@DNI", dto.Cliente.Dni);
                    cmdCliente.Parameters.AddWithValue("@Nombre", dto.Cliente.Nombre);
                    cmdCliente.Parameters.AddWithValue("@Apellido", dto.Cliente.Apellido);
                    cmdCliente.Parameters.AddWithValue("@Telefono", dto.Cliente.Telefono);
                    cmdCliente.Parameters.AddWithValue("@Calle", dto.Cliente.Calle);
                    cmdCliente.Parameters.AddWithValue("@Numero", dto.Cliente.Numero);
                    cmdCliente.Parameters.AddWithValue("@IdLocalidad", dto.Cliente.IdLocalidad);
                    cmdCliente.Parameters.AddWithValue("@IdTipodeDni", dto.Cliente.IdTipoDni);


                    var paramIdCliente = new SqlParameter("@IdCliente", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmdCliente.Parameters.Add(paramIdCliente);

                    cmdCliente.ExecuteNonQuery();
                    idCliente = (int)paramIdCliente.Value;
                }

                // Insertar Usuario
                using (SqlCommand cmdUsuario = new SqlCommand("sp_crearUsuarios", connection, transaction))
                {
                    cmdUsuario.CommandType = CommandType.StoredProcedure;
                    cmdUsuario.Parameters.AddWithValue("@NombredeUsuario", dto.Usuario.NombredeUsuario);
                    cmdUsuario.Parameters.AddWithValue("@Contraseña", dto.Usuario.Contraseña);
                    cmdUsuario.Parameters.AddWithValue("@CorreoElectronico", dto.Usuario.CorreoElectronico);
                    cmdUsuario.Parameters.AddWithValue("@IdTipodeUsuario", dto.Usuario.IdTipoUsuario);
                    cmdUsuario.Parameters.AddWithValue("@IdTipodeRol", dto.Usuario.IdTipodeRol);
                    cmdUsuario.Parameters.AddWithValue("@IdCliente", idCliente);

                    cmdUsuario.ExecuteNonQuery();
                }

                transaction.Commit();
                return idCliente;
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                throw new Exception($"Error de SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Error general: {ex.Message}");
            }
        }


        public bool ValidarUsuario(string usuario, string contrasena)
        {
            using (SqlConnection connection = new SqlConnection(_cadenaSQL))
            {
                connection.Open();


                SqlCommand cmd = new SqlCommand("_ValidarUsuario", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombredeUsuario", usuario);
                cmd.Parameters.AddWithValue("@Contraseña", contrasena);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // true si encontró usuario
                }

            }
        }

    }
}
