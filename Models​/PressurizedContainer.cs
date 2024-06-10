using System;
using System.Collections.Generic;

namespace KolokwiumCF.Models​;

public partial class PressurizedContainer
{
    public int ContainerId { get; set; }

    public int Pressure { get; set; }

    public virtual Container Container { get; set; } = null!;
}
