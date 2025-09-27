using ITIGraduationProject.BLL.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BLL.ViewModels.SessionVM
{
    public class SessionBaseVM
    {
        [Required]
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        [Required, DataType(DataType.Date)]
        [DateGreaterThanToday(ErrorMessage = "Start date must be today or later")]
        public DateTime StartDate { get; set; }

        [Required, DataType(DataType.Date)]
        [EndDateAfterStartDate("StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime EndDate { get; set; }
    }
}
