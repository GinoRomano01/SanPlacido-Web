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
    public class DetallesdeProductoData
    {
        private readonly string _cadenaSQL;

        public DetallesdeProductoData(string cadenaSQL)
        {
            _cadenaSQL = cadenaSQL;
        }

        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            using (SqlConnection conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("CategoriaS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Categoria
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }
            return lista;
        }

        public List<TipodeProducto> ObtenerTiposProducto()
        {
            List<TipodeProducto> lista = new List<TipodeProducto>();
            using (SqlConnection conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("TipoProductoS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new TipodeProducto
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }
            return lista;
        }

        public List<TipodeMadera> ObtenerTiposMadera()
        {
            List<TipodeMadera> lista = new List<TipodeMadera>();
            using (SqlConnection conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("TipoMaderaS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new TipodeMadera
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }
            return lista;
        }

        public List<TipodeAcabado> ObtenerTiposAcabado()
        {
            List<TipodeAcabado> lista = new List<TipodeAcabado>();
            using (SqlConnection conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("TipoAcabadoS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new TipodeAcabado
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }
            return lista;
        }
    }
}
