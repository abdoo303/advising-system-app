using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Semester
{
    public string SemesterCode { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
