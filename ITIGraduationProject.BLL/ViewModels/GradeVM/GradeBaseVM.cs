using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BLL.ViewModels.GradeVM
{
    public class GradeBaseVM
    {
        [Required]
        public int SessionId { get; set; }

        [Required]
        public int TraineeId { get; set; }

        [Required, Range(0, 100)]
        public int Value { get; set; }
    }
}
