using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ITIGraduationProject.DAL.Repository
{
    public class CourseRepo : GenericRepository<Course>, ICourseRepo
    {
        public CourseRepo(ApplicationDbContext context) : base(context) { }

        public bool IsCourseNameUnique(string? name, int? excludeId = null)
        {
            return !_context.Courses.Any(c =>
                c.Name == name && (!excludeId.HasValue || c.Id != excludeId.Value));
        }

        public Course? GetCourseWithInstructorById(int id)
        {
            return _context.Courses.Include(c => c.Instructor)
                                   .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Course> GetAllWithInstructor()
        {
            return _context.Courses.Include(c => c.Instructor).ToList();
        }

        public Course? GetByIdWithInstructor(int id)
        {
            return _context.Courses.Include(c => c.Instructor)
                                   .FirstOrDefault(c => c.Id == id);
        }
    }


}

