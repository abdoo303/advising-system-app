using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class AdvisorsGraduationPlan
{
    public int PlanId { get; set; }

    public string SemesterCode { get; set; } = null!;

    public int? SemesterCreditHours { get; set; }

    public DateOnly? ExpectedGradDate { get; set; }

    public int? AdvisorId { get; set; }

    public int? StudentId { get; set; }

    public int AdvisorId1 { get; set; }

    public string? AdvisorName { get; set; }
}
