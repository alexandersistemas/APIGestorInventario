using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorInventario.DTO
{
    public class EstadoInventarioDTO
    {

        public int IdProducto { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? FechaCaducidad { get; set; }

        public int? CantidadDisponible { get; set; }

        public string Estado { get; set; } = null!;

    }
}
