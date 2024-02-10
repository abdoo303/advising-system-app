using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class ViewCoursePrerequisite
{
    public int CourseId { get; set; }

    public string? Name { get; set; }

    public string? Major { get; set; }

    public bool? IsOffered { get; set; }

    public int? CreditHours { get; set; }

    public int? Semester { get; set; }

    public int PreRequsiteCourseId { get; set; }

    public string? PreRequsiteCourseName { get; set; }
}
