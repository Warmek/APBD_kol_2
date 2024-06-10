using System;
using System.Collections.Generic;

namespace KolokwiumCF.Models​;

public partial class Container
{
    public int Id { get; set; }

    public int ShipId { get; set; }

    public string Content { get; set; } = null!;

    public virtual PressurizedContainer? PressurizedContainer { get; set; }

    public virtual Ship Ship { get; set; } = null!;
}
