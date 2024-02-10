using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class StudentsCoursesTranscript
{
    public int StudentId { get; set; }

    public string? FName { get; set; }

    public string? LName { get; set; }

    public int CourseId { get; set; }

    public string? Name { get; set; }

    public string? ExamType { get; set; }

    public string? Grade { get; set; }

    public string SemesterCode { get; set; } = null!;
}
