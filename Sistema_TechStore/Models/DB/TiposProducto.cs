using System;
using System.Collections.Generic;

namespace Sistema_TechStore.Models.DB;

public partial class TiposProducto
{
    public int IdTipoProducto { get; set; }

    public string TipoProducto { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
