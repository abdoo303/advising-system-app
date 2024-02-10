using System;
using System.Collections.Generic;

namespace BookQWeb.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string? Name { get; set; }

    public string? Major { get; set; }

    public bool? IsOffered { get; set; }

    public int? CreditHours { get; set; }

    public int? Semester { get; set; }

    public virtual ICollection<MakeUpExam> MakeUpExams { get; set; } = new List<MakeUpExam>();

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public virtual ICollection<StudentInstructorCourseTake> StudentInstructorCourseTakes { get; set; } = new List<StudentInstructorCourseTake>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<GraduationPlan> GraduationPlans { get; set; } = new List<GraduationPlan>();

    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    public virtual ICollection<Course> PrerequisiteCourses { get; set; } = new List<Course>();

    public virtual ICollection<Semester> SemesterCodes { get; set; } = new List<Semester>();
}
