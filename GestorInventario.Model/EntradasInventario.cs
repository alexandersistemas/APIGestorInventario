using System;
using System.Collections.Generic;

namespace GestorInventario.Model;

public partial class EntradasInventario
{
    public int IdEntrada { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public DateOnly FechaCaducidad { get; set; }

    public DateTime? FechaEntrada { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual ICollection<SalidasInventario> SalidasInventarios { get; set; } = new List<SalidasInventario>();
}
