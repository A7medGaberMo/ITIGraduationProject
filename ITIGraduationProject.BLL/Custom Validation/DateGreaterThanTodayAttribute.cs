using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BLL.Custom_Validation
{
    public class DateGreaterThanTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date.Date >= DateTime.Today;
            }
            return false;
        }
    }
}
