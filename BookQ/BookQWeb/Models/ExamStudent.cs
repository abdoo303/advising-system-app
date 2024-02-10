using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class ExamStudent
{
    public int ExamId { get; set; }

    public int StudentId { get; set; }

    public int? CourseId { get; set; }

    public virtual MakeUpExam Exam { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
