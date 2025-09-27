using System.ComponentModel.DataAnnotations;
using ITIGraduationProject.DAL.Models.Enums;

namespace ITIGraduationProject.BLL.ViewModels.CourseVM
{
    public class CourseBaseVM
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        public int? InstructorId { get; set; }

        public string InstructorName { get; set; } = string.Empty; // display only
    }
}
