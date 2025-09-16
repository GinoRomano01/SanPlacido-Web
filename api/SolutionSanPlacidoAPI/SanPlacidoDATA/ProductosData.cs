using Microsoft.Extensions.Options;
using SanPlacidoModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SanPlacidoModelo.editarproducto;
namespace SanPlacidoDATA
{
    public class ProductosData
    {
        private readonly string _cadenaSQL;

        public ProductosData(IOptions<ConnectionStrings> opciones)
        {
            _cadenaSQL = opciones.Value.CadenaSQL;
        }


        public List<productocondetalles> MostrarProductos()
        {
            List<productocondetalles> lista = new List<productocondetalles>();

            using (SqlConnection con = new SqlConnection(_cadenaSQL))
            {
                using (SqlCommand cmd = new SqlCommand("sp_verProductos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            productocondetalles p = new productocondetalles
                            {
                                IdProducto = dr.GetInt32(dr.GetOrdinal("IdProducto")),
                                NombredelProducto = dr.GetString(dr.GetOrdinal("NombredelProducto")),
                                URLImagen = dr.GetString(dr.GetOrdinal("URLImagen")),
                                PrecioUnitario = dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")),
                                Categoria = dr.GetString(dr.GetOrdinal("Categoria")),
                                Largo = dr.GetDecimal(dr.GetOrdinal("Largo")),
                                Ancho = dr.GetDecimal(dr.GetOrdinal("Ancho")),
                                Alto = dr.GetDecimal(dr.GetOrdinal("Alto")),
                                TipoProducto = dr.GetString(dr.GetOrdinal("TipoProducto")),
                                TipoMadera = dr.GetString(dr.GetOrdinal("TipoMadera")),
                                TipoAcabado = dr.GetString(dr.GetOrdinal("TipoAcabado"))
                            };
                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }

        public productocondetalles MostrarProductoPorId(int id)
        {
            productocondetalles producto = null;

            using (SqlConnection con = new SqlConnection(_cadenaSQL))
            {
                using (SqlCommand cmd = new SqlCommand("sp_verProductoPorId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", id);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            producto = new productocondetalles
                            {
                                IdProducto = dr.GetInt32(dr.GetOrdinal("IdProducto")),
                                NombredelProducto = dr.GetString(dr.GetOrdinal("NombredelProducto")),
                                URLImagen = dr.GetString(dr.GetOrdinal("URLImagen")),
                                PrecioUnitario = dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")),
                                Categoria = dr.GetString(dr.GetOrdinal("Categoria")),
                                IdDetallesdeProducto = dr.GetInt32(dr.GetOrdinal("IdDetallesdeProducto")),
                                Largo = dr.GetDecimal(dr.GetOrdinal("Largo")),
                                Ancho = dr.GetDecimal(dr.GetOrdinal("Ancho")),
                                Alto = dr.GetDecimal(dr.GetOrdinal("Alto")),
                                TipoProducto = dr.GetString(dr.GetOrdinal("TipoProducto")),
                                TipoMadera = dr.GetString(dr.GetOrdinal("TipoMadera")),
                                TipoAcabado = dr.GetString(dr.GetOrdinal("TipoAcabado"))


                            };
                        }
                        Console.WriteLine("IdDetallesdeProducto desde DB: " + dr["IdDetallesdeProducto"]);
                    }
                }
            }

            return producto;
        }
        public void InsertarProducto(CargarProducto dto)
        {
            using (SqlConnection conn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("Producto_Insertar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombredelProducto", dto.NombredelProducto);
                cmd.Parameters.AddWithValue("@URLImagen", dto.URLImagen);
                cmd.Parameters.AddWithValue("@PrecioUnitario", dto.PrecioUnitario);
                cmd.Parameters.AddWithValue("@IdCategoria", dto.IdCategoria);
                cmd.Parameters.AddWithValue("@Ancho", dto.Ancho);
                cmd.Parameters.AddWithValue("@Largo", dto.Largo);
                cmd.Parameters.AddWithValue("@Alto", dto.Alto);
                cmd.Parameters.AddWithValue("@IdTipodeProducto", dto.IdTipodeProducto);
                cmd.Parameters.AddWithValue("@IdTipodeMadera", dto.IdTipodeMadera);
                cmd.Parameters.AddWithValue("@IdTipodeAcabado", dto.IdTipodeAcabado);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public void ActualizarDetalles(ProductoDTO dto)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            using (SqlCommand cmd = new SqlCommand("DetallesdeProductoU", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", dto.IdDetallesdeProducto);
                cmd.Parameters.AddWithValue("@Ancho", dto.Ancho);
                cmd.Parameters.AddWithValue("@Largo", dto.Largo);
                cmd.Parameters.AddWithValue("@Alto", dto.Alto);
                cmd.Parameters.AddWithValue("@IdTipodeProducto", dto.IdTipodeProducto);
                cmd.Parameters.AddWithValue("@IdTipodeMadera", dto.IdTipodeMadera);
                cmd.Parameters.AddWithValue("@IdTipodeAcabado", dto.IdTipodeAcabado);
               
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar producto
        public void ActualizarProducto(ProductoDTO dto)
        {
            using (SqlConnection con = new SqlConnection(_cadenaSQL))
            {
                using (SqlCommand cmd = new SqlCommand("ProductoU", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 🔹 Parámetros obligatorios
                    cmd.Parameters.AddWithValue("@IdProducto", dto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", dto.NombredelProducto);
                    cmd.Parameters.AddWithValue("@Precio", dto.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@Url", dto.URLImagen);
                    cmd.Parameters.AddWithValue("@IdCategoria", dto.IdCategoria);

                    // 🔹 Parámetros de detalles
                    
                    cmd.Parameters.AddWithValue("@Ancho", dto.Ancho);
                    cmd.Parameters.AddWithValue("@Largo", dto.Largo);
                    cmd.Parameters.AddWithValue("@Alto", dto.Alto);
                    cmd.Parameters.AddWithValue("@IdTipodeProducto", dto.IdTipodeProducto);
                    cmd.Parameters.AddWithValue("@IdTipodeMadera", dto.IdTipodeMadera);
                    cmd.Parameters.AddWithValue("@IdTipodeAcabado", dto.IdTipodeAcabado);

                    // 🔹 Logging de depuración
                    Console.WriteLine("===== DTO que llega a ActualizarProducto =====");
                    Console.WriteLine($"IdProducto: {dto.Id}");
                    Console.WriteLine($"Nombre: {dto.NombredelProducto}");
                    Console.WriteLine($"Precio: {dto.PrecioUnitario}");
                    Console.WriteLine($"URLImagen: {dto.URLImagen}");
                    Console.WriteLine($"IdCategoria: {dto.IdCategoria}");
                    Console.WriteLine($"IdDetallesdeProducto: {dto.IdDetallesdeProducto}");
                    Console.WriteLine($"Ancho: {dto.Ancho}, Largo: {dto.Largo}, Alto: {dto.Alto}");
                    Console.WriteLine($"IdTipodeProducto: {dto.IdTipodeProducto}");
                    Console.WriteLine($"IdTipodeMadera: {dto.IdTipodeMadera}");
                    Console.WriteLine($"IdTipodeAcabado: {dto.IdTipodeAcabado}");
                    Console.WriteLine("==============================================");

                    // 🔹 Ejecutar SP y verificar filas afectadas
                    con.Open();
                    int filas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Filas afectadas: {filas}");

                    if (filas == 0)
                        throw new Exception("No se encontraron registros para actualizar.");
                }
            }
        }
        // Soft delete
        public void EliminarProducto(int id)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            using (SqlCommand cmd = new SqlCommand("ProductoD", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
