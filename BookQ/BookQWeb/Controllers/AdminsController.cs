using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookQWeb.Models;
using Microsoft.Data.SqlClient;

namespace BookQWeb.Controllers
{

    public class AdminsController : Controller
    {
        private readonly AdvisingSystemContext _context;

        public AdminsController(AdvisingSystemContext context)
        {
            _context = context;
        }
        public IActionResult Mahmoud()
        {
            return View();
        }
        public async Task<IActionResult> GetSlots()
        {
            //int? id = HttpContext.Session.GetInt32("AdminId");
            //if (id == null)
            //{
            //    return RedirectToAction("Login", "Admins");
            //}
            var admins = _context.Slots.ToList(); // Retrieve all Admin records from the database

            return View(admins);
        }


        // GET: Admins
        public async Task<IActionResult> Index()
        {
              return _context.Admins != null ? 
                          View(await _context.Admins.ToListAsync()) :
                          Problem("Entity set 'AdvisingSystemContext.Admins'  is null.");
        }
        public async Task<IActionResult> Payments()
        {
            var paymentsWithStudents = await _context.Payments
                                                .Include(p => p.Student)
                                                .ToListAsync();

            return View(paymentsWithStudents);
        }

        [HttpPost]
        public IActionResult DeleteCourse(int? courseId)
        {
            try
            {
                if (courseId == null || courseId < 1)
                {
                    ViewBag.ErrorMessage = "Invalid Course ID.";
                    return RedirectToAction("DeleteCourseFailure"); // Redirect to failure view
                }

                var courseExists = _context.Courses.Any(c => c.CourseId == courseId);

                if (!courseExists)
                {
                    ViewBag.ErrorMessage = "Course ID not found in the database.";
                    return RedirectToAction("DeleteCourseFailure"); // Redirect to failure view
                }

                _context.Database.ExecuteSqlInterpolated($"EXEC Procedures_AdminDeleteCourse {courseId}");
                return RedirectToAction("DeleteCourseSuccess"); // Redirect to success view
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("DeleteCourseFailure"); // Redirect to failure view on error
            }
        }

        public IActionResult DeleteSucc()
        {
            return View();
        }

        /*public async Task<IActionResult> AddMakeupExam(string type, DateTime date, int courseId)
        {
            try
            {
                if (string.IsNullOrEmpty(type) || date == default || courseId == 0)
                {
                    ViewBag.ErrorMessage = "One of the inputs is null.";
                    return View("MakeupExamAddFailure"); // Return a failure view for null inputs
                }

                // Execute stored procedure
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC Procedures_AdminAddExam 
                @Type = {type}, 
                @date = {date}, 
                @courseID = {courseId}"
                );

                return RedirectToAction("MakeupExamAddSuccess"); // Redirect to success view
            }
            catch (Exception ex)
            {
                // Handle exception if needed
                ViewBag.ErrorMessage = ex.Message;
                return View("MakeupExamAddFailure"); // Return a failure view
            }
        }
*/
        public IActionResult AddedSucc()
        {
            return View();
        }
        public IActionResult AddMakeupExam()
        {
            return View();
        }
        public IActionResult MakeupExamAddSuccess()
        {
            return View();
        }
        public IActionResult MakeupExamAddFailure()
        {
            return View();
        }
        public IActionResult GraduationPlansWithAdvisorsFailure()
        {
            return View();
        }
        public IActionResult StudentsTranscriptFailure()
        {
            return View();
        }
        public IActionResult SemestersWithCoursesFailure()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UpdateStudentStatus()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteSlots()
        {
            return View();
        }
        public IActionResult DeleteSlotsFailure()
        {
            return View();
        }
        public IActionResult DeleteSlotsSuccess()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteCourse()
        {
            return View();
        }        
        public IActionResult DeleteCourseFailure()
        {
            return View();
        }        
        public IActionResult DeleteCourseSuccess()
        {
            return View();
        }        
        public IActionResult AdminPart2()
        {
            return View();
        }
        public async Task<IActionResult> ExecuteMakeupExam(string type, DateTime date, int courseId)
        {
            try
            {
                if (string.IsNullOrEmpty(type) || date == default || courseId == 0)
                {
                    return RedirectToAction("MakeupExamAddFailure"); // Return a failure view for null inputs
                }

                // Execute stored procedure
                // if exam in db, skip
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC Procedures_AdminAddExam 
                @Type = {type}, 
                @date = {date}, 
                @courseID = {courseId}"
                );

                return RedirectToAction("MakeupExamAddSuccess"); // Redirect to success view
            }
            catch (Exception ex)
            {
                // Handle exception if needed
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("MakeupExamAddFailure"); // Redirect to failure view
            }
        }
        [HttpPost]
        public IActionResult DeleteSlots(string currentSemester)
        {
            try
            {
                var semesterExists = _context.Semesters.Any(s => s.SemesterCode == currentSemester);

                if (!semesterExists)
                {
                    ViewBag.ErrorMessage = "Semester not found in the database.";
                    return RedirectToAction("DeleteSlotsFailure"); // Redirect to failure view
                }

                _context.Database.ExecuteSqlInterpolated($"EXEC Procedures_AdminDeleteSlots {currentSemester}");
                return RedirectToAction("DeleteSlotsSuccess"); // Redirect to success view
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("DeleteSlotsFailure"); // Redirect to failure view on error
            }
        }


        /*        public async Task<IActionResult> IssueInstallments(int paymentId)
                {
                    try
                    {
                        if (paymentId <= 0)
                        {
                            ViewBag.ErrorMessage = "Invalid payment ID.";
                            return View("IssueInstallmentsFailure"); // Return a failure view for invalid payment ID
                        }

                        // Execute stored procedure to issue installments with the provided paymentId
                        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC Procedures_AdminIssueInstallment {paymentId}");

                        return View("IssueInstallmentsSuccess"); // Redirect to success view
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return View("IssueInstallmentsFailure"); // Redirect to failure view
                    }
                }*/
        [HttpGet]
        public IActionResult IssueInstallments()
        {
            return View();
        }
        public IActionResult IssueInstallmentsSuccess()
        {
            return View();
        }
        public IActionResult IssueInstallmentsFailure()
        {
            return View();
        }


        [HttpPost]
        public IActionResult IssueInstallments(int paymentId)
        {
            try
            {
                var payment = _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);

                if (payment == null)
                {
                    ViewBag.ErrorMessage = "Payment ID not found in the database.";
                    return RedirectToAction("IssueInstallmentsFailure"); // Redirect to failure view
                }

                _context.Database.ExecuteSqlInterpolated($"EXEC Procedures_AdminIssueInstallment {paymentId}");
                return RedirectToAction("IssueInstallmentsSuccess"); // Redirect to success view
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("IssueInstallmentsFailure"); // Redirect to failure view on error
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentStatus(int studentId)
        {
            try
            {
                if (studentId <= 0)
                {
                    return View("UpdateStudentStatusFailure"); // Return a failure view for invalid student ID
                }

                // Execute stored procedure to update student status
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC Procedure_AdminUpdateStudentStatus {studentId}");

                return View("UpdateStudentStatusSuccess"); // Redirect to success view
            }
            catch (Exception ex)
            {
                return View("UpdateStudentStatusFailure"); // Redirect to failure view
            }
        }
        public async Task<IActionResult> ActiveStudents()
        {
            try
            {
                var activeStudents = await _context.Students.ToListAsync(); //switch to view_students

                return View(activeStudents);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ActiveStudentsFailure"); // Redirect to failure view on error
            }
        }

        public async Task<IActionResult> GraduationPlansWithAdvisors()
        {
            try
            {
                var graduationPlansWithAdvisors = await _context.GraduationPlans.ToListAsync();

                return View(graduationPlansWithAdvisors);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("GraduationPlansWithAdvisorsFailure"); // Redirect to failure view on error
            }
        }
        public async Task<IActionResult> StudentsTranscript()
        {
            try
            {
                var studentsTranscript = await _context.StudentsCoursesTranscripts.ToListAsync();

                return View(studentsTranscript);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("StudentsTranscriptFailure"); // Redirect to failure view on error
            }
        }


        public async Task<IActionResult> SemestersWithOfferedCourses()
        {
            try
            {
                var semestersWithCourses = await _context.SemsterOfferedCourses.ToListAsync();

                return View(semestersWithCourses);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("SemestersWithCoursesFailure"); // Redirect to failure view on error
            }
        }




















        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,AdminName,AdminEmail")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,AdminName,AdminEmail")] Admin admin)
        {
            if (id != admin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AdminId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Admins == null)
            {
                return Problem("Entity set 'AdvisingSystemContext.Admins'  is null.");
            }
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
          return (_context.Admins?.Any(e => e.AdminId == id)).GetValueOrDefault();
        }
    }
}
