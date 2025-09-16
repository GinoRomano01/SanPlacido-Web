using Microsoft.Extensions.Options;
using SanPlacidoModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SanPlacidoDATA;

namespace SanPlacidoDATA
{
    public class LocalidadDni
    {
        private readonly string _cadenaSQL;

        public LocalidadDni(IOptions<ConnectionStrings> opciones)
        {
            _cadenaSQL = opciones.Value.CadenaSQL;
        }

        public List<Localidad> ObtenerLocalidades()
        {
            var lista = new List<Localidad>();
            using SqlConnection connection = new SqlConnection(_cadenaSQL);
            using SqlCommand cmd = new SqlCommand("sp_getLocalidades", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Localidad
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString()
                });
            }
            return lista;
        }


        public List<TipodeDni> ObtenerTiposDeDni()
        {
            var lista = new List<TipodeDni>();
            using SqlConnection connection = new SqlConnection(_cadenaSQL);
            using SqlCommand cmd = new SqlCommand("sp_getTiposDeDni", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new TipodeDni
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString()
                });
            }
            return lista;
        }
    }
}
