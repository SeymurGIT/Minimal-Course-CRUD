using System.ComponentModel.DataAnnotations;

namespace MinimalCourseCRUD.Entity
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Yaradılacaq kurs adını yazın")]
        public string? Title { get; set; }
        public int? InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;
        public ICollection<CourseRegistration> CourseRegs { get; set; } = new List<CourseRegistration>();

    }
}
