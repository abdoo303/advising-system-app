using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public string? Type { get; set; }

    public string? Comment { get; set; }

    public string? Status { get; set; }

    public int? CreditHours { get; set; }

    public int? CourseId { get; set; }

    public int? StudentId { get; set; }

    public int? AdvisorId { get; set; }

    public virtual Advisor? Advisor { get; set; }

    public virtual Student? Student { get; set; }
}
