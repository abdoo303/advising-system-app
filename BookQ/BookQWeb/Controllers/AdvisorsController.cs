using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookQWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using BookQWeb.Models.ViewModels;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Azure.Core;

namespace BookQWeb.Controllers
{
    public class AdvisorsController : Controller
    {
        private readonly AdvisingSystemContext _context;
        public AdvisorsController(AdvisingSystemContext context)
        {
            _context = context;
        }

        // GET: Advisors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advisors.ToListAsync());
        }

        public async Task<IActionResult> Home()
        {
            int? advisorId = HttpContext.Session.GetInt32("AdvisorId");
            var advisor = await _context.Advisors.FirstOrDefaultAsync(a => a.AdvisorId == advisorId);
            ViewBag.AdvisorId = advisorId;
            string? advisorName = HttpContext.Session.GetString("AdvisorName");
            ViewBag.AdvisorName = advisorName;
            if (advisorId == null)
            {
                
                return RedirectToAction("Index", "Advisors"); // Redirect to login page or handle as appropriate
            }
           
            return View(advisor);
        }

        // GET: Advisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors
                .FirstOrDefaultAsync(m => m.AdvisorId == id);
            if (advisor == null)
            {
                return NotFound();
            }

            return View(advisor);
        }

        // GET: Advisors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvisorId,AdvisorName,Email,Office,Password")] Advisor advisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advisor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(advisor);
        }

        //hi start/
        // GET: Advisors/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("AdvisorId,AdvisorName,Email,Office,Password")] Advisor advisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advisor);
                await _context.SaveChangesAsync();


                // save to session
                HttpContext.Session.SetInt32("AdvisorId", advisor.AdvisorId);
                HttpContext.Session.SetString("AdvisorName", advisor.AdvisorName??"");

                // Pass the advisorId to the view


                return RedirectToAction("Home", "Advisors");
            }

            return View(advisor);
        }

        // Inside AdvisorsController.cs
        public IActionResult Students()
        {
            // Retrieve the advisor ID from the session
            int? advisorId = HttpContext.Session.GetInt32("AdvisorId");
            System.Diagnostics.Debug.WriteLine("the id is " + advisorId);
            if (advisorId == null)
            {
                // Handle the case where advisor ID is not available
                return RedirectToAction("Login", "Advisors"); // Redirect to login page or handle as appropriate
            }

            // Retrieve the advisor by ID
            var advisor = _context.Advisors
                .Include(a => a.Students) 
                 .ThenInclude(s => s.GraduationPlans)
                .FirstOrDefault(a => a.AdvisorId == advisorId);

            if (advisor == null)
            {
                return NotFound();
            }

            // Pass the advisor and associated students to the view
            return View(advisor);
        }
        public IActionResult AllRequests()
        {
            // Retrieve the advisor ID from the session
            int? advisorId = HttpContext.Session.GetInt32("AdvisorId");
            System.Diagnostics.Debug.WriteLine("the id is " + advisorId);
            if (advisorId == null)
            {
                // Handle the case where advisor ID is not available
                return RedirectToAction("Login", "Advisors"); // Redirect to login page or handle as appropriate
            }

            // Retrieve the advisor by ID
            var advisor = _context.Advisors
                .Include(a => a.Requests)
                .FirstOrDefault(a => a.AdvisorId == advisorId);

            if (advisor == null)
            {
                return NotFound();
            }

            // Pass the advisor and associated students to the view
            return View(advisor);
        }
        public IActionResult PendingRequests()
        {
            // Retrieve the advisor ID from the session
            int? advisorId = HttpContext.Session.GetInt32("AdvisorId");
            System.Diagnostics.Debug.WriteLine("the id is " + advisorId);
            if (advisorId == null)
            {
                // Handle the case where advisor ID is not available
                return RedirectToAction("Login", "Advisors"); // Redirect to login page or handle as appropriate
            }

            // Retrieve the advisor by ID
            var advisor = _context.Advisors
                .Include(a => a.Requests)
                .FirstOrDefault(a => a.AdvisorId == advisorId);

            if (advisor == null)
            {
                return NotFound();
            }

            // Pass the advisor and associated students to the view
            return View(advisor);
        }
        public async Task<IActionResult> MiddleStage(int? id)
        {
            int? advisorId = HttpContext.Session.GetInt32("AdvisorId");
            System.Diagnostics.Debug.WriteLine("the id is " + advisorId);
            if (advisorId == null || id == null)
            {
                return RedirectToAction("Login", "Advisors");
            }
            var advisor = _context.Advisors
                .Include(a => a.Requests)
                .FirstOrDefault(a => a.AdvisorId == advisorId);

            if (advisor == null)
            {
                return NotFound();
            }
            var request = advisor.Requests.FirstOrDefault(a => a.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            Category model = new Category {
                Name = request.Type ?? ""
            };
       
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MiddleStage(int id, Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var sqlString = $"EXEC Procedures_AdvisorApproveRejectCourseRequest ";
                    if ((model.Name).Contains("credit", StringComparison.OrdinalIgnoreCase)) sqlString = $"EXEC Procedures_AdvisorApproveRejectCHRequest ";

                    sqlString += $"@requestID = {id}, " +
                                    $"@current_sem_code = '{model.Code}'";

                    var rowsAffected = await _context.Database.ExecuteSqlRawAsync(sqlString);

                    System.Diagnostics.Debug.WriteLine("Hello " + rowsAffected);

                    if (rowsAffected > 0)
                    {
                        // Rows were affected, graduation plan successfully created
                        TempData["SuccessMessage"] = "Success!";
                    }

                    else
                    {
                        // No rows were affected, graduation plan not created
                        TempData["ErrorMessage"] = "Failed. Please try again.";
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Log the exception details (you can replace Console.WriteLine with your preferred logging mechanism)
                    Console.WriteLine($"DbUpdateConcurrencyException: {ex.Message}\nStackTrace: {ex.StackTrace}");

                    // Check if the entity still exists in the database
                    if (!_context.Students.Any(s => s.StudentId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // There might be other reasons for concurrency exception, handle them accordingly
                        ModelState.AddModelError(string.Empty, "Concurrency error. The record you attempted to edit was modified by another user.");
                        return View(model);
                    }
                }

                return RedirectToAction("MiddleStage", "Advisors");
            }

            return View(model);
        }
       
        // Inside AdvisorsController
        // Inside AdvisorsController
        public async Task<IActionResult> CreateGraduationPlan(int? id)
        {
            System.Diagnostics.Debug.WriteLine("look that is the id "+ id);

            if (id == null)
            {

                return NotFound();
            }

            var advisor = await _context.Advisors.FirstOrDefaultAsync(a => a.AdvisorId == HttpContext.Session.GetInt32("AdvisorId"));

            if (advisor == null)
            {
                return NotFound();
            }
            System.Diagnostics.Debug.WriteLine("the id of the student is5 " + id+" "+id.Value);

            // Create a GraduationPlanViewModel to hold the data needed for creating the graduation plan
            var graduationPlanViewModel = new GraduationPlan
            {
                AdvisorId = advisor.AdvisorId, // Pass the AdvisorId to the view
                StudentId = id // Pass the studentId to the view
            };
            System.Diagnostics.Debug.WriteLine("look that is the id number 2 " + id);


            return View(graduationPlanViewModel);
        }

        // POST: Advisors/CreateGraduationPlan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGraduationPlan(int? id, [Bind("SemesterCode,ExpectedGradDate,SemesterCreditHours")] GraduationPlan model)
        {
            

    

            if (ModelState.IsValid)
            {
                try
                {

                    var sqlString = $"EXEC Procedures_AdvisorCreateGP " +
                             $"@Semester_code = {model.SemesterCode}, " +
                             $"@expected_graduation_date = '{model.ExpectedGradDate:yyyy-MM-dd}', " +
                             $"@sem_credit_hours = {model.SemesterCreditHours}, " +
                             $"@advisor_id = {HttpContext.Session.GetInt32("AdvisorId")}, " +
                             $"@student_id = {id}";

                    var rowsAffected = await _context.Database.ExecuteSqlRawAsync(sqlString);
                    System.Diagnostics.Debug.WriteLine("Hello "+ rowsAffected);

                    if (rowsAffected > 1)
                    {
                        // Rows were affected, graduation plan successfully created
                        TempData["SuccessMessage"] = "Graduation plan successfully created!";
                    }
                    else if (rowsAffected == 1)
                    {
                        TempData["SuccessMessage"] = "check if credit hours are greater than 157!";
                        
                    }
                    else
                    {
                        // No rows were affected, graduation plan not created
                        TempData["ErrorMessage"] = "Failed to create graduation plan. Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Hello 3leko");
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    TempData["ErrorMessage"] = "An error occurred. Please try again or contact support.";
                }
            }

            return RedirectToAction("CreateGraduationPlan","Advisors");
        }


        public async Task<IActionResult> EditGraduationPlan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                var student = await _context.Students.FirstOrDefaultAsync(a => a.StudentId == id);
                
                if (student == null)
                {

                    return RedirectToAction("Login", "Advisors"); // Redirect to login page or handle as appropriate
                }

                return View(student);
            
        }
        public async Task<IActionResult> EditDate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.GraduationPlans)
                .FirstOrDefaultAsync(a => a.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }
            var plan = student.GraduationPlans.FirstOrDefault();
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Advisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDate(int id,  GraduationPlan plan)
        {



            if (ModelState.IsValid)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Hello abdo is here2 "+plan.ExpectedGradDate);

                    var sqlString = $"EXEC Procedures_AdvisorUpdateGP " +
                             $"@expected_grad_date = '{plan.ExpectedGradDate:yyyy-MM-dd}', " +
                             $"@studentID = {id} " ;

                    var rowsAffected = await _context.Database.ExecuteSqlRawAsync(sqlString);
                    System.Diagnostics.Debug.WriteLine("Hello " + rowsAffected);

                    if (rowsAffected >0)
                    {
                        // Rows were affected, graduation plan successfully created
                        TempData["SuccessMessage"] = "Graduation plan date updated!";
                    }
                 
                    else
                    {
                        // No rows were affected, graduation plan not created
                        TempData["ErrorMessage"] = "Failed to update graduation plan date. Please try again.";
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Log the exception details (you can replace Console.WriteLine with your preferred logging mechanism)
                    Console.WriteLine($"DbUpdateConcurrencyException: {ex.Message}\nStackTrace: {ex.StackTrace}");

                    // Check if the entity still exists in the database
                    if (!_context.Students.Any(s => s.StudentId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // There might be other reasons for concurrency exception, handle them accordingly
                        ModelState.AddModelError(string.Empty, "Concurrency error. The record you attempted to edit was modified by another user.");
                        return View(plan);
                    }
                }

                return RedirectToAction("EditDate", "Advisors");
            }

            return View(plan);
        }


        public async Task<IActionResult> AddCourse(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var model = new Category { };
            return View(model);
        }

        // POST: Advisors/AddCourse/studentid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourse(int? id, Category model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var sqlString = $"EXEC Procedures_AdvisorAddCourseGP " +
                             $"@student_id = {id}, " +
                             $"@Semester_code = '{model.Code}', " +
                             $"@course_name = '{model.Name}'";
                         

                    var rowsAffected = await _context.Database.ExecuteSqlRawAsync(sqlString);
                    System.Diagnostics.Debug.WriteLine("Hello " + rowsAffected);

                    if (rowsAffected > 0)
                    {
                        // Rows were affected, graduation plan successfully created
                        TempData["SuccessMessage"] = "Course added Successfully!";
                    }
                 
                    else
                    {
                        // No rows were affected, graduation plan not created
                        TempData["ErrorMessage"] = "Failed to add the course. Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Hello 3leko");
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    TempData["ErrorMessage"] = "An error occurred. Please try again or contact support.";
                }
            }

            return RedirectToAction("AddCourse", "Advisors");
        }

        public async Task<IActionResult> ChooseMajor()
        {

            var advisor = await _context.Advisors.FirstOrDefaultAsync(a => a.AdvisorId == HttpContext.Session.GetInt32("AdvisorId"));

            if (advisor == null)
            {
                return NotFound();
            }
            var model = new Category { 
                Code ="code",
                Id = advisor.AdvisorId
            };
            return View(model);
        }

        // POST: Advisors/AddCourse/studentid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseMajor( Category model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {

                  
                    HttpContext.Session.SetString("major", model.Name);

                    return RedirectToAction("ViewStudentsInMajor", "Advisors");

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Hello 3leko");
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    TempData["ErrorMessage"] = "An error occurred. Please try again or contact support.";
                }
            }

            return RedirectToAction("ChooseMajor", "Advisors");
        }
         //[HttpGet]
        public async Task<IActionResult> ViewStudentsInMajor()
        {
            string? major = HttpContext.Session.GetString("major");
            int? id = HttpContext.Session.GetInt32("AdvisorId");
            if (major == null || id == null)
            {
                return NotFound();
            }

            System.Diagnostics.Debug.WriteLine("major " + major + " " + id);

            var students = new List<AssignedStudent>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("Procedures_AdvisorViewAssignedStudents", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AdvisorID", id);
                        command.Parameters.AddWithValue("@major", major);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var student = new AssignedStudent
                                {
                                    student_id = reader.GetInt32(0),
                                    Student_name = reader.GetString(1),
                                    major = reader.GetString(2),
                                    Course_name = reader.GetString(3)
                                };

                                students.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
                TempData["ErrorMessage"] = "An error occurred. Please try again or contact support.";
            }


            System.Diagnostics.Debug.WriteLine("Hello Rahim");

            return View(students);
        }


        public async Task<IActionResult> DeleteCourse(int? id)
        {
            var advisor = await _context.Advisors.FirstOrDefaultAsync(a => a.AdvisorId == HttpContext.Session.GetInt32("AdvisorId"));

            if (advisor == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var model = new Category2 { };
            return View(model);
        }

        // POST: Advisors/AddCourse/studentid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourse(int? id, Category2 model)
        {
            if (id == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {

                    System.Diagnostics.Debug.WriteLine("Hello id" + id);

                    var sqlString = $"EXEC Procedures_AdvisorDeleteFromGP " +
                             $"@studentID = {id}, " +
                             $"@sem_code = '{model.Code}', " +
                             $"@CourseID = {model.Id}";


                    var rowsAffected = await _context.Database.ExecuteSqlRawAsync(sqlString);
                    System.Diagnostics.Debug.WriteLine("Hello " + rowsAffected);

                    if (rowsAffected > 0)
                    {
                        // Rows were affected, graduation plan successfully created
                        TempData["SuccessMessage"] = "Course deleted Successfully!";
                    }

                    else
                    {
                        // No rows were affected, graduation plan not created
                        TempData["ErrorMessage"] = "Failed to delete the course. Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Hello 3leko");
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    TempData["ErrorMessage"] = "An error occurred. Please try again or contact support.";
                }
            }

            return RedirectToAction("DeleteCourse", "Advisors");
        }




        public IActionResult ViewRequests()
        {
            // Get the current advisor's id (you need to replace this with your actual logic)
            int currentAdvisorId = GetCurrentAdvisorId();

            // Use the SQL function to retrieve requests for the current advisor
            var requests = _context.RequestsFromAdvisor(currentAdvisorId).ToList();

            // You can now pass the 'requests' to your view or perform further actions
            return View(requests);
        }

        // Replace this with your actual logic to get the current advisor's id
        private int GetCurrentAdvisorId()
        {
            // Replace this with your logic to get the current advisor's id
            // For example, you might get it from the logged-in user's claims or session
            return 1; // Replace with the actual advisor id
        }


      
        // end

        // GET: Advisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor == null)
            {
                return NotFound();
            }
            return View(advisor);
        }

        // POST: Advisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvisorId,AdvisorName,Email,Office,Password")] Advisor advisor)
        {
            if (id != advisor.AdvisorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvisorExists(advisor.AdvisorId))
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
            return View(advisor);
        }
        // GET: Advisors/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Advisors/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var advisor = await _context.Advisors.FirstOrDefaultAsync(a => a.AdvisorId == model.id);

                if (advisor != null && advisor.Password == model.Password) // Note: This is a simple comparison. For security, consider hashing passwords.
                {
                    // Manual login logic: Create your own authentication mechanism, perhaps using cookies or session variables.
                    // For example, you might store the advisor's ID in a session variable.
                    HttpContext.Session.SetInt32("AdvisorId", advisor.AdvisorId);
                    HttpContext.Session.SetString("AdvisorName", advisor.AdvisorName??"");

                    return RedirectToAction("Home", "Advisors");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: Advisors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisors
                .FirstOrDefaultAsync(m => m.AdvisorId == id);
            if (advisor == null)
            {
                return NotFound();
            }

            return View(advisor);
        }

        // POST: Advisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advisor = await _context.Advisors.FindAsync(id);
            if (advisor != null)
            {
                _context.Advisors.Remove(advisor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvisorExists(int id)
        {
            return _context.Advisors.Any(e => e.AdvisorId == id);
        }
    }
}
