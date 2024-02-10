using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Slot
{
    public int SlotId { get; set; }

    public string? Day { get; set; }

    public string? Time { get; set; }

    public string? Location { get; set; }

    public int? CourseId { get; set; }

    public int? InstructorId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Instructor? Instructor { get; set; }
}
