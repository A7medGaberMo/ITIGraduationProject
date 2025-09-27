using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BLL.Custom_Validation
{
    public class EndDateAfterStartDateAttribute : ValidationAttribute
    {
        private readonly string _startDateProperty;

        public EndDateAfterStartDateAttribute(string startDateProperty)
        {
            _startDateProperty = startDateProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDateProperty);
            if (startDateProperty == null)
                return new ValidationResult($"Unknown property: {_startDateProperty}");

            var startDateValue = startDateProperty.GetValue(validationContext.ObjectInstance);

            if (value is DateTime endDate && startDateValue is DateTime startDate)
            {
                if (endDate <= startDate)
                    return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
