using System;
using System.Collections.Generic;

namespace KolokwiumCF.Models​;

public partial class Ship
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Container> Containers { get; set; } = new List<Container>();
}
