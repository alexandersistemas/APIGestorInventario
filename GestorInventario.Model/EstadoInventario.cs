using System;
using System.Collections.Generic;

namespace GestorInventario.Model;

public partial class EstadoInventario
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly? FechaCaducidad { get; set; }

    public int? CantidadDisponible { get; set; }

    public string Estado { get; set; } = null!;
}
