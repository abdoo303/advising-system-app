using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class MakeUpExam
{
    public int ExamId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Type { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<ExamStudent> ExamStudents { get; set; } = new List<ExamStudent>();
}
