using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalCourseCRUD.Entity
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa ad yazın.")]
        public string? StudentName { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa soyad yazın.")]
        public string? StudentSurname { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return this.StudentName + " " + this.StudentSurname;
            }
        }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<CourseRegistration> CourseRegs { get; set; } = new List<CourseRegistration>();


    }
}
