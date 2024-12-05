using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorInventario.DTO
{
    public class SalidasInventarioDTO
    {
        public int IdSalida { get; set; }
        public int IdProducto { get; set; }
        public int IdEntrada { get; set; }
        public int Cantidad { get; set; }
        public string FechaSalida { get; set; }

    }
}
