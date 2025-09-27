using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITIGraduationProject.DAL.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}
