using ITIGraduationProject.BLL.ViewModels.GradeVM;
using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.UnitofWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITIGraduationProject.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Grade> GetAll()
        {
            return _unitOfWork.Grades.AsQueryable()
                                     .Include(g => g.Trainee)
                                     .Include(g => g.Session)
                                        .ThenInclude(s => s.Course)
                                     .ToList();
        }

     
    
        public void Create(GradeCreateVM vm)
        {
            if (vm.Value < 0 || vm.Value > 100)
                throw new Exception("Grade value must be between 0 and 100.");

            var grade = new Grade
            {
                TraineeId = vm.TraineeId,
                SessionId = vm.SessionId,
                Value = vm.Value
            };

            _unitOfWork.Grades.Add(grade);
            _unitOfWork.Complete();
        }

        public void Update(GradeEditVM vm)
        {
            var grade = _unitOfWork.Grades.GetById(vm.Id);
            if (grade == null) throw new Exception("Grade not found.");

            if (vm.Value < 0 || vm.Value > 100)
                throw new Exception("Grade value must be between 0 and 100.");

            grade.TraineeId = vm.TraineeId;
            grade.SessionId = vm.SessionId;
            grade.Value = vm.Value;

            _unitOfWork.Grades.Update(grade);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var grade = _unitOfWork.Grades.GetById(id);
            if (grade == null) throw new Exception("Grade not found.");

            _unitOfWork.Grades.Remove(grade);
            _unitOfWork.Complete();
        }

        public Grade? GetById(int id)
        {
            return _unitOfWork.Grades.AsQueryable()
                                     .Include(g => g.Trainee)
                                     .Include(g => g.Session)
                                        .ThenInclude(s => s.Course)
                                     .FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Grade> GetPagedGrades(int pageNumber, int pageSize, string? traineeName = null, int? courseId = null)
        {
            IQueryable<Grade> query = _unitOfWork.Grades.AsQueryable()
                                                        .Include(g => g.Trainee)
                                                        .Include(g => g.Session)
                                                            .ThenInclude(s => s.Course);

            if (!string.IsNullOrWhiteSpace(traineeName))
                query = query.Where(g => g.Trainee != null && g.Trainee.Name.Contains(traineeName));

            if (courseId.HasValue && courseId.Value > 0)
                query = query.Where(g => g.Session != null && g.Session.CourseId == courseId.Value);

            return query.OrderBy(g => g.Id)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        }

        public int GetGradesCount(string? traineeName = null, int? courseId = null)
        {
            IQueryable<Grade> query = _unitOfWork.Grades.AsQueryable();

            if (!string.IsNullOrWhiteSpace(traineeName))
                query = query.Where(g => g.Trainee != null && g.Trainee.Name.Contains(traineeName));

            if (courseId.HasValue && courseId.Value > 0)
                query = query.Where(g => g.Session != null && g.Session.CourseId == courseId.Value);

            return query.Count();
        }
    }
}
