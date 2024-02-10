using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class InstructorsAssignedCourse
{
    public int InstructorId { get; set; }

    public string? Instructor { get; set; }

    public int CourseId { get; set; }

    public string? Course { get; set; }
}
