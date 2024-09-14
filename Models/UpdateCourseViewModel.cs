using MinimalCourseCRUD.Entity;
using System.ComponentModel.DataAnnotations;

namespace MinimalCourseCRUD.Models
{
    public class UpdateCourseViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Yaradılacaq kurs adını yazın")]
        public string? Title { get; set; }
        public int? InstructorId { get; set; }
        public ICollection<CourseRegistration> CourseRegisters { get; set; } = new List<CourseRegistration>();
    }
}
