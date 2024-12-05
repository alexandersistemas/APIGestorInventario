using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorInventario.DTO
{
    public class ReporteDTO
    {
        public string? NombreProducto { get; set; }
        public string? Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public string? FechaCaducidad { get; set; }
        public string? FechaEntrada { get; set; }


    }
}
