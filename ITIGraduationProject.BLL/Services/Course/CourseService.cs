using ITIGraduationProject.BLL.ViewModels.CourseVM;
using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace ITIGraduationProject.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Course> GetAll()
        {
            return _unitOfWork.Courses.GetAllWithInstructor();
        }

        public Course? GetById(int id)
        {
            return _unitOfWork.Courses.GetByIdWithInstructor(id);
        }

        public Course? GetCourseWithInstructor(int id)
        {
            return _unitOfWork.Courses.GetByIdWithInstructor(id);
        }

        public bool IsCourseNameUnique(string name, int? excludeId = null)
        {
            return _unitOfWork.Courses.IsCourseNameUnique(name, excludeId);
        }

        public void Create(CourseCreateVM vm)
        {
            var course = new Course
            {
                Name = vm.Name,
                Category = vm.Category,
                InstructorId = vm.InstructorId
            };
            _unitOfWork.Courses.Add(course);
            _unitOfWork.Complete();
        }

        public void Update(CourseEditVM vm)
        {
            var course = _unitOfWork.Courses.GetById(vm.Id);
            if (course == null) return;

            course.Name = vm.Name;
            course.Category = vm.Category;
            course.InstructorId = vm.InstructorId;

            _unitOfWork.Courses.Update(course);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            if (course == null) return;

            _unitOfWork.Courses.Remove(course);
            _unitOfWork.Complete();
        }

        public IEnumerable<Course> GetPagedCourses(int pageNumber, int pageSize, string? searchTerm = null, string? category = null, string? instructorName = null)
        {
            var query = _unitOfWork.Courses.GetAllWithInstructor().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(c => c.Name != null && c.Name.ToLower().Contains(searchTerm.ToLower()));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(c => c.Category != null && c.Category.ToLower() == category.ToLower());

            if (!string.IsNullOrEmpty(instructorName))
                query = query.Where(c => c.Instructor != null && c.Instructor.Name.ToLower().Contains(instructorName.ToLower()));

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }


        public int GetCoursesCount(string? searchTerm = null, string? category = null, string? instructorName = null)
        {
            var query = _unitOfWork.Courses.GetAllWithInstructor().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(c => c.Name.Contains(searchTerm));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(c => c.Category == category);

            if (!string.IsNullOrEmpty(instructorName))
                query = query.Where(c => c.Instructor != null && c.Instructor.Name.Contains(instructorName));

            return query.Count();
        }
    }
}
