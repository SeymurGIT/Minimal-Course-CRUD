using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MinimalCourseCRUD.Entity
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa ad yazın.")]
        public string? InstructorName { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa ad yazın.")]
        public string? InstructorSurname { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return this.InstructorName + " " + this.InstructorSurname;
            }
        }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MMM-dd}")]
        public DateOnly HireDate { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
