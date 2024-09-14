using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalCourseCRUD.Data;
using MinimalCourseCRUD.Entity;
using MinimalCourseCRUD.Models;

namespace MinimalCourseCRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly Context _context;

        public StudentController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student model)
        {
            _context.Students.Add(model);
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
            var student = await _context.Students
                .Include(o => o.CourseRegs)
                .ThenInclude(o => o.Course)
                .FirstOrDefaultAsync(o => o.StudentId == id);

            if(student == null)
            {
                return NotFound();
            }
            var model = new UpdateStudentViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                StudentSurname = student.StudentSurname,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                CourseRegisters = student.CourseRegs
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateStudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(id!= model.StudentId)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.StudentName = model.StudentName;
            student.StudentSurname = model.StudentSurname;
            student.Email = model.Email;
            student.PhoneNumber = model.PhoneNumber;

            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if(!_context.Students.Any(s=>s.StudentId == model.StudentId))
                {
                    return StatusCode(500, "Internal server error: " + ex.Message);
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if(student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");   
        }


    }
}
