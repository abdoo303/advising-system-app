using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class StudentInstructorCourseTake
{
    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public int? InstructorId { get; set; }

    public string SemesterCode { get; set; } = null!;

    public string? ExamType { get; set; }

    public string? Grade { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Instructor? Instructor { get; set; }

    public virtual Student Student { get; set; } = null!;
}
