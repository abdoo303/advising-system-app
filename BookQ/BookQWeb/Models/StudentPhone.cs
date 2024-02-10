using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class StudentPhone
{
    public int StudentId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
