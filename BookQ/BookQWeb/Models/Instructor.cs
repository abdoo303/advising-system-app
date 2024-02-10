using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Faculty { get; set; }

    public string? Office { get; set; }

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public virtual ICollection<StudentInstructorCourseTake> StudentInstructorCourseTakes { get; set; } = new List<StudentInstructorCourseTake>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
