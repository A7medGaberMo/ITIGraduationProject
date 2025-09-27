using Microsoft.AspNetCore.Mvc;

namespace ITIGraduationProject.BLL.ViewModels.CourseVM
{
    public class CourseCreateVM : CourseBaseVM
    {
        [Remote(action: "IsCourseNameUnique", controller: "Course", AdditionalFields = "Id")]
        public new string Name { get; set; } = string.Empty;
    }
}
