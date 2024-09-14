using MinimalCourseCRUD.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MinimalCourseCRUD.Models
{
    public class UpdateInstructorViewModel
    {
        public int InstructorId { get; set; }
        public string? InstructorName { get; set; }
        public string? InstructorSurname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MMM-dd}")]
        public DateOnly HireDate { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

