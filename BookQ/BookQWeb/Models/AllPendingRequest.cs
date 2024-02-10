using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class AllPendingRequest
{
    public int RequestId { get; set; }

    public string? Type { get; set; }

    public string? Comment { get; set; }

    public string? Status { get; set; }

    public int? CreditHours { get; set; }

    public int? CourseId { get; set; }

    public int? StudentId { get; set; }

    public int? AdvisorId { get; set; }
}
