using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Student
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

    public virtual Advisor? Advisor { get; set; }

    public virtual ICollection<ExamStudent> ExamStudents { get; set; } = new List<ExamStudent>();

    public virtual ICollection<GraduationPlan> GraduationPlans { get; set; } = new List<GraduationPlan>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<StudentInstructorCourseTake> StudentInstructorCourseTakes { get; set; } = new List<StudentInstructorCourseTake>();

    public virtual ICollection<StudentPhone> StudentPhones { get; set; } = new List<StudentPhone>();
}
