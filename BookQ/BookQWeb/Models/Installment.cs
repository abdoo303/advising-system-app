using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Installment
{
    public int PaymentId { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime Deadline { get; set; }

    public int? Amount { get; set; }

    public string? Status { get; set; }

    public virtual Payment Payment { get; set; } = null!;
}
