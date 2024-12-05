using System;
using System.Collections.Generic;

namespace GestorInventario.Model;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<EntradasInventario> EntradasInventarios { get; set; } = new List<EntradasInventario>();

    public virtual ICollection<SalidasInventario> SalidasInventarios { get; set; } = new List<SalidasInventario>();
}
