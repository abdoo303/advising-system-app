using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class CoursesMakeupExam
{
    public int ExamId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Type { get; set; }

    public int? CourseId { get; set; }

    public string? Name { get; set; }

    public int? Semester { get; set; }
}
