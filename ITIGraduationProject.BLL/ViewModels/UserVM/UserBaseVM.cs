using ITIGraduationProject.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BLL.ViewModels.UserVM
{
    public class UserBaseVM
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Range(15, 40)]
        public int Age { get; set; } = 16;

        [Required]
        public UserRole Role { get; set; }
    }
}
