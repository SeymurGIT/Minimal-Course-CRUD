using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinimalCourseCRUD.Data;
using MinimalCourseCRUD.Entity;
using MinimalCourseCRUD.Models;

namespace MinimalCourseCRUD.Controllers
{
    public class CourseController : Controller
    {
        private readonly Context _context;

        public CourseController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(c=>c.Instructor).ToListAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateInstructors();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course model, bool leaveEmptyCheckbox)
        {
            if ((!leaveEmptyCheckbox && (model.InstructorId == 0 || string.IsNullOrWhiteSpace(model.Title))) || (leaveEmptyCheckbox && string.IsNullOrWhiteSpace(model.Title)))
            {
                ModelState.AddModelError("", "Xahiş edirik, təlimçi seçin.");

                // Re-populate the ViewBag in case of validation failure
                await PopulateInstructors();

                return View(model);
            }

            if (leaveEmptyCheckbox)
            {
                model.InstructorId = null;  // Adjust based on your data type or logic
            }


            _context.Courses.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //Instructorlar populate etmek
        private async Task PopulateInstructors()
        {
            ViewBag.Instructors = new SelectList
              (
                  await _context.Instructors
                  .Select(i => new { i.InstructorId, i.FullName })
                  .ToListAsync(), "InstructorId", "FullName"
              );
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Course model)
        {
            var expectedCourse = await _context.Courses.FirstOrDefaultAsync(c=>c.CourseId == model.CourseId);

            if(expectedCourse == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(expectedCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.CourseRegs)
                .ThenInclude(c => c.Student)
                .FirstOrDefaultAsync(c => c.CourseId == id);

            if(course == null)
            {
                return NotFound();
            }

            var courseInfo = new UpdateCourseViewModel
            {
                CourseId = course.CourseId,
                Title = course.Title,
                CourseRegisters = course.CourseRegs,
                InstructorId = course.InstructorId
            };
            await PopulateInstructors();
            return View(courseInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateCourseViewModel model)
        {
            if (model.InstructorId == 0 || string.IsNullOrWhiteSpace(model.Title))
            {
                ModelState.AddModelError("", "Xahiş edirik təlimçi seçin");
                await PopulateInstructors();
                return View(model);
            }
            if (ModelState.IsValid)
            {
            try
            {
            _context.Courses.Update(new Course()
            {
                CourseId = model.CourseId,
                Title = model.Title,
                InstructorId = model.InstructorId
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Course");
            }
            catch(DbUpdateConcurrencyException ex)
            {
                if(!_context.Courses.Any(o=>o.CourseId == model.CourseId))
                {
                return StatusCode(500, "Daxili server xətası" + ex.Message);
                }
                else
                {
                    throw;
                }
            }
            }
            await PopulateInstructors();
            return View(model);

        }

    }
}
