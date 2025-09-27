using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BLL.ViewModels.UserVM
{
    public class UserCreateVM : UserBaseVM
    {
       [Range(15,40)]
        public new int Age { get; set; }
    }
}
