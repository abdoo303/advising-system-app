using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class SemsterOfferedCourse
{
    public int CourseId { get; set; }

    public string? Name { get; set; }

    public string SemesterCode { get; set; } = null!;
}
