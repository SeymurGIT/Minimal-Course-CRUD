using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MinimalCourseCRUD.Data;
using MinimalCourseCRUD.Entity;
using MinimalCourseCRUD.Models;
using System.Globalization;

namespace MinimalCourseCRUD.Controllers
{
    public class InstructorController : Controller
    {
        private readonly Context _context;

        public InstructorController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructors.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {

                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = _context.Instructors.Find(id);

            if (instructor == null)
            {
                return NotFound();
            }
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            var instructorInfo = new UpdateInstructorViewModel
            {
                InstructorName = instructor.InstructorName,
                InstructorSurname = instructor.InstructorSurname,
                Email = instructor.Email,
                PhoneNumber = instructor.PhoneNumber,
                HireDate = instructor.HireDate
            };

            return View(instructorInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateInstructorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (id != model.InstructorId)
            {
                return NotFound();
            }

            var instructor = _context.Instructors.Find(id);

            if (instructor == null)
            {
                return NotFound();
            }
            instructor.InstructorName = model.InstructorName;
            instructor.InstructorSurname = model.InstructorSurname;
            instructor.Email = model.Email;
            instructor.PhoneNumber = model.PhoneNumber;
            instructor.HireDate = model.HireDate;

            try
            {
                _context.Instructors.Update(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                if(!_context.Instructors.Any(i=>i.InstructorId == id))
                {
                    return StatusCode(500, "Daxili server xətası" + ex.Message);
                }

                else
                {
                    throw;
                }
            }
        }
    }
}
