using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Advisor
{
    public int AdvisorId { get; set; }

    public string? AdvisorName { get; set; }

    public string? Email { get; set; }

    public string? Office { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<GraduationPlan> GraduationPlans { get; set; } = new List<GraduationPlan>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
