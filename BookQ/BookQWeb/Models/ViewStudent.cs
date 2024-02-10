using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class ViewStudent
{
    public int StudentId { get; set; }

    public string? FName { get; set; }

    public string? LName { get; set; }

    public string? Password { get; set; }

    public decimal? Gpa { get; set; }

    public string? Faculty { get; set; }

    public string? Email { get; set; }

    public string? Major { get; set; }

    public bool? FinancialStatus { get; set; }

    public int? Semester { get; set; }

    public int? AcquiredHours { get; set; }

    public int? AssignedHours { get; set; }

    public int? AdvisorId { get; set; }
}
