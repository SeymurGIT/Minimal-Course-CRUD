using MinimalCourseCRUD.Entity;
using System.ComponentModel.DataAnnotations;

namespace MinimalCourseCRUD.Models
{
    public class UpdateStudentViewModel
    {
        public int StudentId { get; set; }
        [Required]
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }

        public string FullName
        {
            get
            {
                return this.StudentName + " " + this.StudentSurname;
            }
        }

        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<CourseRegistration> CourseRegisters { get; set; } = new List<CourseRegistration>();
    }
}
