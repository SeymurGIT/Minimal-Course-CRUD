using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalCourseCRUD.Entity
{
    public class CourseRegistration
    {
        [Key]
        public int RegId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!; 
        public DateTime RegistrationDate { get; set; }
    }
}
