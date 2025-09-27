using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.BLL.ViewModels.CourseVM;
using System.Collections.Generic;

namespace ITIGraduationProject.BLL.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll();
        Course? GetById(int id);
        Course? GetCourseWithInstructor(int id);
        bool IsCourseNameUnique(string name, int? excludeId = null);
        void Create(CourseCreateVM vm);
        void Update(CourseEditVM vm);
        void Delete(int id);

        // Search + Pagination + Filter
        IEnumerable<Course> GetPagedCourses(int pageNumber, int pageSize, string? searchTerm = null, string? category = null, string? instructorName = null);
        int GetCoursesCount(string? searchTerm = null, string? category = null, string? instructorName = null);
    }
}
