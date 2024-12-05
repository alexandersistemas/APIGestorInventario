using System;
using System.Collections.Generic;

namespace GestorInventario.Model;

public partial class SalidasInventario
{
    public int IdSalida { get; set; }

    public int IdProducto { get; set; }

    public int IdEntrada { get; set; }

    public int Cantidad { get; set; }

    public string? FechaSalida { get; set; }

    public virtual EntradasInventario IdEntradaNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
