using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITIGraduationProject.DAL.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        // Instructor (optional)
        public int? InstructorId { get; set; }
        public User? Instructor { get; set; }

        // Navigation
        public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    }
}
