using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.Generic;

namespace ITIGraduationProject.DAL.Repository
{
    public interface ICourseRepo : IGenericRepository<Course>
    {
        IEnumerable<Course> GetAllWithInstructor();
        Course? GetByIdWithInstructor(int id);
        Course? GetCourseWithInstructorById(int id);
        bool IsCourseNameUnique(string? name, int? excludeId = null);
    }
}
