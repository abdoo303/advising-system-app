using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookQWeb.Models;

public partial class AdvisingSystemContext : DbContext
{
    public AdvisingSystemContext()
    {
    }


    [DbFunction(Schema = "dbo")]
    public virtual IQueryable<Request> RequestsFromAdvisor(int advisorId)
    {
        var advisorIdParameter = new SqlParameter("@advisor_id", advisorId);

        return Set<Request>().FromSqlRaw("SELECT * FROM dbo.FN_Advisors_Requests(@advisor_id)", advisorIdParameter);
    }



    public AdvisingSystemContext(DbContextOptions<AdvisingSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Advisor> Advisors { get; set; }

    public virtual DbSet<AdvisorsGraduationPlan> AdvisorsGraduationPlans { get; set; }

    public virtual DbSet<AllPendingRequest> AllPendingRequests { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<CoursesMakeupExam> CoursesMakeupExams { get; set; }

    public virtual DbSet<CoursesSlotsInstructor> CoursesSlotsInstructors { get; set; }

    public virtual DbSet<ExamStudent> ExamStudents { get; set; }

    public virtual DbSet<GraduationPlan> GraduationPlans { get; set; }

    public virtual DbSet<Installment> Installments { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }
    public virtual DbSet<AssignedStudent> AssignedStudents { get; set; }

    public virtual DbSet<InstructorsAssignedCourse> InstructorsAssignedCourses { get; set; }

    public virtual DbSet<MakeUpExam> MakeUpExams { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<SemsterOfferedCourse> SemsterOfferedCourses { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentInstructorCourseTake> StudentInstructorCourseTakes { get; set; }

    public virtual DbSet<StudentPayment> StudentPayments { get; set; }

    public virtual DbSet<StudentPhone> StudentPhones { get; set; }

    public virtual DbSet<StudentsCoursesTranscript> StudentsCoursesTranscripts { get; set; }

    public virtual DbSet<ViewCoursePrerequisite> ViewCoursePrerequisites { get; set; }

    public virtual DbSet<ViewStudent> ViewStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MYPC\\SQLEXPRESS; Database=Advising_System;Trusted_Connection=True;TrustServerCertificate=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advisor>(entity =>
        {
            entity.HasKey(e => e.AdvisorId).HasName("PK__Advisor__AC11E3FEFFD04240");

            entity.ToTable("Advisor");

            entity.Property(e => e.AdvisorId).HasColumnName("advisor_id");
            entity.Property(e => e.AdvisorName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("advisor_name");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Office)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("office");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<AdvisorsGraduationPlan>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Advisors_Graduation_Plan");

            entity.Property(e => e.AdvisorId).HasColumnName("advisor_id");
            entity.Property(e => e.AdvisorId1).HasColumnName("AdvisorID");
            entity.Property(e => e.AdvisorName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("advisor_name");
            entity.Property(e => e.ExpectedGradDate).HasColumnName("expected_grad_date");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.SemesterCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("semester_code");
            entity.Property(e => e.SemesterCreditHours).HasColumnName("semester_credit_hours");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
        });

        modelBuilder.Entity<AllPendingRequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("all_Pending_Requests");

            entity.Property(e => e.AdvisorId).HasColumnName("advisor_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CreditHours).HasColumnName("credit_hours");
            entity.Property(e => e.RequestId)
                .ValueGeneratedOnAdd()
                .HasColumnName("request_id");
            entity.Property(e => e.Status)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Type)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__8F1EF7AECD7D2A13");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CreditHours).HasColumnName("credit_hours");
            entity.Property(e => e.IsOffered).HasColumnName("is_offered");
            entity.Property(e => e.Major)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("major");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Semester).HasColumnName("semester");

            entity.HasMany(d => d.Courses).WithMany(p => p.PrerequisiteCourses)
                .UsingEntity<Dictionary<string, object>>(
                    "PreqCourseCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PreqCours__cours__46E78A0C"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("PrerequisiteCourseId")
                        .HasConstraintName("FK__PreqCours__prere__45F365D3"),
                    j =>
                    {
                        j.HasKey("PrerequisiteCourseId", "CourseId").HasName("PK__PreqCour__29975FB8A0403CB6");
                        j.ToTable("PreqCourse_course");
                        j.IndexerProperty<int>("PrerequisiteCourseId").HasColumnName("prerequisite_course_id");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                    });

            entity.HasMany(d => d.GraduationPlans).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "GradPlanCourse",
                    r => r.HasOne<GraduationPlan>().WithMany()
                        .HasForeignKey("PlanId", "SemesterCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Grad"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__GradPlan___cours__5AEE82B9"),
                    j =>
                    {
                        j.HasKey("CourseId", "PlanId", "SemesterCode").HasName("PK__GradPlan__CE1B721EA15DFE4B");
                        j.ToTable("GradPlan_Course");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                        j.IndexerProperty<int>("PlanId").HasColumnName("plan_id");
                        j.IndexerProperty<string>("SemesterCode")
                            .HasMaxLength(40)
                            .IsUnicode(false)
                            .HasColumnName("semester_code");
                    });

            entity.HasMany(d => d.Instructors).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "InstructorCourse",
                    r => r.HasOne<Instructor>().WithMany()
                        .HasForeignKey("InstructorId")
                        .HasConstraintName("FK__Instructo__instr__4CA06362"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__Instructo__cours__4BAC3F29"),
                    j =>
                    {
                        j.HasKey("CourseId", "InstructorId").HasName("PK__Instruct__150002C0B788AB27");
                        j.ToTable("Instructor_Course");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                        j.IndexerProperty<int>("InstructorId").HasColumnName("instructor_id");
                    });

            entity.HasMany(d => d.PrerequisiteCourses).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "PreqCourseCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("PrerequisiteCourseId")
                        .HasConstraintName("FK__PreqCours__prere__45F365D3"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PreqCours__cours__46E78A0C"),
                    j =>
                    {
                        j.HasKey("PrerequisiteCourseId", "CourseId").HasName("PK__PreqCour__29975FB8A0403CB6");
                        j.ToTable("PreqCourse_course");
                        j.IndexerProperty<int>("PrerequisiteCourseId").HasColumnName("prerequisite_course_id");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                    });

            entity.HasMany(d => d.SemesterCodes).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseSemester",
                    r => r.HasOne<Semester>().WithMany()
                        .HasForeignKey("SemesterCode")
                        .HasConstraintName("FK__Course_Se__semes__5812160E"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__Course_Se__cours__571DF1D5"),
                    j =>
                    {
                        j.HasKey("CourseId", "SemesterCode").HasName("PK__Course_S__21D923B6E2F66CDB");
                        j.ToTable("Course_Semester");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                        j.IndexerProperty<string>("SemesterCode")
                            .HasMaxLength(40)
                            .IsUnicode(false)
                            .HasColumnName("semester_code");
                    });
        });

        modelBuilder.Entity<CoursesMakeupExam>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Courses_MakeupExams");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Semester).HasColumnName("semester");
            entity.Property(e => e.Type)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<CoursesSlotsInstructor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Courses_Slots_Instructor");

            entity.Property(e => e.Course)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseId1).HasColumnName("course_id");
            entity.Property(e => e.Day)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("day");
            entity.Property(e => e.Instructor)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.InstructorId).HasColumnName("instructor_id");
            entity.Property(e => e.Location)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.Time)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("time");
        });

        modelBuilder.Entity<ExamStudent>(entity =>
        {
            entity.HasKey(e => new { e.ExamId, e.StudentId }).HasName("PK__Exam_Stu__2E2F4B80DAEFBA56");

            entity.ToTable("Exam_Student");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamStudents)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__Exam_Stud__exam___6A30C649");

            entity.HasOne(d => d.Student).WithMany(p => p.ExamStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exam_Stud__stude__6B24EA82");
        });

        modelBuilder.Entity<GraduationPlan>(entity =>
        {
            entity.HasKey(e => new { e.PlanId, e.SemesterCode }).HasName("PK__Graduati__10585B0526DDB8B9");

            entity.ToTable("Graduation_Plan");

            entity.Property(e => e.PlanId)
                .ValueGeneratedOnAdd()
                .HasColumnName("plan_id");
            entity.Property(e => e.SemesterCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("semester_code");
            entity.Property(e => e.AdvisorId).HasColumnName("advisor_id");
            entity.Property(e => e.ExpectedGradDate).HasColumnName("expected_grad_date");
            entity.Property(e => e.SemesterCreditHours).HasColumnName("semester_credit_hours");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Advisor).WithMany(p => p.GraduationPlans)
                .HasForeignKey(d => d.AdvisorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Graduatio__advis__403A8C7D");

            entity.HasOne(d => d.Student).WithMany(p => p.GraduationPlans)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Graduatio__stude__412EB0B6");
        });

        modelBuilder.Entity<Installment>(entity =>
        {
            entity.HasKey(e => new { e.PaymentId, e.Deadline }).HasName("PK__Installm__3F60B5DBC65CA78E");

            entity.ToTable("Installment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");
            entity.Property(e => e.Status)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasDefaultValue("notPaid")
                .HasColumnName("status");

            entity.HasOne(d => d.Payment).WithMany(p => p.Installments)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Installme__payme__72C60C4A");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__Instruct__A1EF56E878AFFCFC");

            entity.ToTable("Instructor");

            entity.Property(e => e.InstructorId)
                .ValueGeneratedNever()
                .HasColumnName("instructor_id");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Faculty)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("faculty");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Office)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("office");
        });

        modelBuilder.Entity<InstructorsAssignedCourse>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Instructors_AssignedCourses");

            entity.Property(e => e.Course)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Instructor)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.InstructorId).HasColumnName("instructor_id");
        });

        modelBuilder.Entity<MakeUpExam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__MakeUp_E__9C8C7BE99C6FD0FB");

            entity.ToTable("MakeUp_Exam");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Type)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.Course).WithMany(p => p.MakeUpExams)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MakeUp_Ex__cours__6754599E");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA8371251E");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.FundPercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fund_percentage");
            entity.Property(e => e.NInstallments).HasColumnName("n_installments");
            entity.Property(e => e.SemesterCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("semester_code");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");
            entity.Property(e => e.Status)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasDefaultValue("notPaid")
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.SemesterCodeNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.SemesterCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Payment__semeste__6FE99F9F");

            entity.HasOne(d => d.Student).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Payment__student__6EF57B66");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Request__18D3B90FDB881D21");

            entity.ToTable("Request");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.AdvisorId).HasColumnName("advisor_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CreditHours).HasColumnName("credit_hours");
            entity.Property(e => e.Status)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Type)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.Advisor).WithMany(p => p.Requests)
                .HasForeignKey(d => d.AdvisorId)
                .HasConstraintName("FK__Request__advisor__6477ECF3");

            entity.HasOne(d => d.Student).WithMany(p => p.Requests)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Request__student__6383C8BA");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.SemesterCode).HasName("PK__Semester__EC7D418BD704C78A");

            entity.ToTable("Semester");

            entity.Property(e => e.SemesterCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("semester_code");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<SemsterOfferedCourse>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Semster_offered_Courses");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SemesterCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("semester_code");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Slot__971A01BB0635E0D8");

            entity.ToTable("Slot");

            entity.Property(e => e.SlotId)
                .ValueGeneratedNever()
                .HasColumnName("slot_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Day)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("day");
            entity.Property(e => e.InstructorId).HasColumnName("instructor_id");
            entity.Property(e => e.Location)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Time)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("time");

            entity.HasOne(d => d.Course).WithMany(p => p.Slots)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Slot__course_id__5EBF139D");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Slots)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Slot__instructor__5FB337D6");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__2A33069AFD4CCD34");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.AcquiredHours).HasColumnName("acquired_hours");
            entity.Property(e => e.AdvisorId).HasColumnName("advisor_id");
            entity.Property(e => e.AssignedHours).HasColumnName("assigned_hours");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("f_name");
            entity.Property(e => e.Faculty)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("faculty");
            entity.Property(e => e.FinancialStatus).HasColumnName("financial_status");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gpa");
            entity.Property(e => e.LName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("l_name");
            entity.Property(e => e.Major)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("major");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Semester).HasColumnName("semester");

            entity.HasOne(d => d.Advisor).WithMany(p => p.Students)
                .HasForeignKey(d => d.AdvisorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Student__advisor__3A81B327");
        });

        modelBuilder.Entity<StudentInstructorCourseTake>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId, e.SemesterCode }).HasName("PK__Student___582E94A1C1FBE5D5");

            entity.ToTable("Student_Instructor_Course_take");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.SemesterCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("semester_code");
            entity.Property(e => e.ExamType)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasDefaultValue("Normal")
                .HasColumnName("exam_type");
            entity.Property(e => e.Grade)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("grade");
            entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentInstructorCourseTakes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Student_I__cours__5070F446");

            entity.HasOne(d => d.Instructor).WithMany(p => p.StudentInstructorCourseTakes)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Student_I__instr__5165187F");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentInstructorCourseTakes)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Student_I__stude__4F7CD00D");
        });

        modelBuilder.Entity<StudentPayment>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Student_Payment");

            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.FName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("f_name");
            entity.Property(e => e.FundPercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fund_percentage");
            entity.Property(e => e.LName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("l_name");
            entity.Property(e => e.NInstallments).HasColumnName("n_installments");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.SemesterCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("semester_code");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");
            entity.Property(e => e.Status)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("studentID");
            entity.Property(e => e.StudentId1).HasColumnName("student_id");
        });

        modelBuilder.Entity<StudentPhone>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.PhoneNumber }).HasName("PK__Student___902A303C56046C6D");

            entity.ToTable("Student_Phone");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentPhones)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student_P__stude__3D5E1FD2");
        });

        modelBuilder.Entity<StudentsCoursesTranscript>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Students_Courses_transcript");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.ExamType)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("exam_type");
            entity.Property(e => e.FName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("f_name");
            entity.Property(e => e.Grade)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("grade");
            entity.Property(e => e.LName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("l_name");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SemesterCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("semester_code");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
        });

        modelBuilder.Entity<ViewCoursePrerequisite>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_Course_prerequisites");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CreditHours).HasColumnName("credit_hours");
            entity.Property(e => e.IsOffered).HasColumnName("is_offered");
            entity.Property(e => e.Major)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("major");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PreRequsiteCourseId).HasColumnName("preRequsite_course_id");
            entity.Property(e => e.PreRequsiteCourseName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("preRequsite_course_name");
            entity.Property(e => e.Semester).HasColumnName("semester");
        });

        modelBuilder.Entity<ViewStudent>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_Students");

            entity.Property(e => e.AcquiredHours).HasColumnName("acquired_hours");
            entity.Property(e => e.AdvisorId).HasColumnName("advisor_id");
            entity.Property(e => e.AssignedHours).HasColumnName("assigned_hours");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("f_name");
            entity.Property(e => e.Faculty)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("faculty");
            entity.Property(e => e.FinancialStatus).HasColumnName("financial_status");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gpa");
            entity.Property(e => e.LName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("l_name");
            entity.Property(e => e.Major)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("major");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Semester).HasColumnName("semester");
            entity.Property(e => e.StudentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("student_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
