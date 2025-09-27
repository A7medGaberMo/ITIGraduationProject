using ITIGraduationProject.DAL.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITIGraduationProject.DAL.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

       [ Range(16,30)]
        public int Age { get; set; } = 16;
        [Required]
        public UserRole Role { get; set; }

        // Navigation
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}
