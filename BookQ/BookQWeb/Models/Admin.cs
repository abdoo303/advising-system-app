using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string AdminName { get; set; } = null!;

    public string AdminEmail { get; set; } = null!;
    public string AdminPassword { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

}
