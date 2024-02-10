using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class StudentPayment
{
    public int StudentId { get; set; }

    public string? FName { get; set; }

    public string? LName { get; set; }

    public int PaymentId { get; set; }

    public int? Amount { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Deadline { get; set; }

    public int? NInstallments { get; set; }

    public decimal? FundPercentage { get; set; }

    public string? Status { get; set; }

    public int? StudentId1 { get; set; }

    public string? SemesterCode { get; set; }
}
