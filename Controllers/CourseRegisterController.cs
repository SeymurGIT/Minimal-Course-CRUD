using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinimalCourseCRUD.Data;
using MinimalCourseCRUD.Entity;

namespace MinimalCourseCRUD.Controllers
{
    public class CourseRegisterController : Controller
    {
        private readonly Context _context;

        public CourseRegisterController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courseRegisters = await _context.CourseRegistrations
                .Include(s => s.Student)
                .Include(c => c.Course)
                .ToListAsync();

            return View(courseRegisters);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<SelectListItem> courses = await _context.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.CourseId.ToString(),
                    Text = c.Title
                }).ToListAsync();

            courses.Insert(0, new SelectListItem { Value = "", Text = "Kurs seçin..." });
            ViewBag.Courses = courses;

            ViewBag.Students = new SelectList(await _context.Students
                .Select(s => new { s.StudentId, s.FullName })
                .ToListAsync(), "StudentId","FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseRegistration model)
        {
            await Create();

            if (model.StudentId == 0 || model.CourseId == 0)
            {
                ModelState.AddModelError("", "Xahiş edirik şagirdi qeydiyyat edəcəyiniz kursu seçin");
                return View(model);
            }

            model.RegistrationDate = DateTime.Parse(DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            _context.CourseRegistrations.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CourseRegistration model)
        {
            var expectedCourseRegister = await _context.CourseRegistrations.FirstOrDefaultAsync(c => c.RegId == model.RegId);

            if (expectedCourseRegister == null)
            {
                return NotFound();
            }

            _context.CourseRegistrations.Remove(expectedCourseRegister);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
