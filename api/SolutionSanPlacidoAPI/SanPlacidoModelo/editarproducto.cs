using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanPlacidoModelo
{
    public class editarproducto
    {
        public class ProductoDTO
        {
            public int Id { get; set; }
            public string NombredelProducto { get; set; }
            public string URLImagen { get; set; }
            public decimal PrecioUnitario { get; set; }
            public int IdCategoria { get; set; }

            // Detalles
            public int IdDetallesdeProducto { get; set; }
            public decimal Ancho { get; set; }
            public decimal Largo { get; set; }
            public decimal Alto { get; set; }
            public int IdTipodeProducto { get; set; }
            public int IdTipodeMadera { get; set; }
            public int IdTipodeAcabado { get; set; }
        }
    }
}
