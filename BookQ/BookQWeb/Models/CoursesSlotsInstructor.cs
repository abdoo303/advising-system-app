using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class CoursesSlotsInstructor
{
    public int CourseId { get; set; }

    public string? Course { get; set; }

    public int SlotId { get; set; }

    public string? Day { get; set; }

    public string? Time { get; set; }

    public string? Location { get; set; }

    public int? CourseId1 { get; set; }

    public int? InstructorId { get; set; }

    public string? Instructor { get; set; }
}
