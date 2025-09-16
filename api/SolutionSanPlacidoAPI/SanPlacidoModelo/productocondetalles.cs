using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanPlacidoModelo
{
    public class productocondetalles
    {
        public int IdProducto { get; set; }
        public string NombredelProducto { get; set; }
        public string URLImagen { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Categoria { get; set; }
        public int IdDetallesdeProducto { get; set; }
        public decimal Largo { get; set; }
        public decimal Ancho { get; set; }
        public decimal Alto { get; set; }
        public string TipoProducto { get; set; }
        public string TipoMadera { get; set; }
        public string TipoAcabado { get; set; }
    }
}
