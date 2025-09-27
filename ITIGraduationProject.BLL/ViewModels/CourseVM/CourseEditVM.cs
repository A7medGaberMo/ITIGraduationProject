using ITIGraduationProject.DAL.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITIGraduationProject.BLL.ViewModels.CourseVM
{
    public class CourseEditVM : CourseBaseVM
    {
        [Remote(action: "IsCourseNameUnique", controller: "Course", AdditionalFields = "Id")]
        public new string Name { get; set; } = string.Empty;
    }
}
