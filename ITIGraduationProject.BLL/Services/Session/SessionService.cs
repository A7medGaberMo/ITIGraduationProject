using ITIGraduationProject.BLL.ViewModels.SessionVM;
using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.UnitofWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITIGraduationProject.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SessionListVM> GetPagedSessions(int pageNumber, int pageSize, string? courseName = null, string? search = null)
        {
            IQueryable<Session> query = _unitOfWork.Sessions.AsQueryable()
                                                             .Include(s => s.Course);

            if (!string.IsNullOrWhiteSpace(courseName))
                query = query.Where(s => s.Course != null && s.Course.Name.Contains(courseName));

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(s => s.Course != null && s.Course.Name.Contains(search));

            return query
                .OrderBy(s => s.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SessionListVM
                {
                    Id = s.Id,
                    CourseName = s.Course != null ? s.Course.Name : "N/A",
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                })
                .ToList();
        }

        public int GetSessionsCount(string? courseName = null, string? search = null)
        {
            IQueryable<Session> query = _unitOfWork.Sessions.AsQueryable()
                                                             .Include(s => s.Course);

            if (!string.IsNullOrWhiteSpace(courseName))
                query = query.Where(s => s.Course != null && s.Course.Name.Contains(courseName));

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(s => s.Course != null && s.Course.Name.Contains(search));

            return query.Count();
        }

        public IEnumerable<SessionListVM> GetAll()
        {
            return _unitOfWork.Sessions.AsQueryable()
                                       .Include(s => s.Course)
                                       .Select(s => new SessionListVM
                                       {
                                           Id = s.Id,
                                           CourseName = s.Course != null ? s.Course.Name : "N/A",
                                           StartDate = s.StartDate,
                                           EndDate = s.EndDate
                                       })
                                       .ToList();
        }

        // ✅ New method: get all distinct courses for dropdown
        public IEnumerable<(int CourseId, string CourseName)> GetAllCourses()
        {
            return _unitOfWork.Sessions.AsQueryable()
                                       .Include(s => s.Course)
                                       .Where(s => s.Course != null)
                                       .Select(s => new { s.CourseId, s.Course.Name })
                                       .Distinct()
                                       .OrderBy(c => c.Name)
                                       .AsEnumerable()
                                       .Select(c => (c.CourseId, c.Name));
        }

        public SessionEditVM? GetById(int id)
        {
            var session = _unitOfWork.Sessions.AsQueryable()
                                               .Include(s => s.Course)
                                               .FirstOrDefault(s => s.Id == id);

            if (session == null) return null;

            return new SessionEditVM
            {
                Id = session.Id,
                CourseId = session.CourseId,
                StartDate = session.StartDate,
                EndDate = session.EndDate
            };
        }

        public void Create(SessionCreateVM vm)
        {
            if (vm.StartDate < DateTime.Today)
                throw new Exception("Start Date cannot be in the past.");
            if (vm.EndDate <= vm.StartDate)
                throw new Exception("End Date must be after Start Date.");

            var courseExists = _unitOfWork.Courses.GetById(vm.CourseId) != null;
            if (!courseExists)
                throw new Exception("Selected course does not exist.");

            var session = new Session
            {
                CourseId = vm.CourseId,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate
            };

            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Complete();
        }

        public void Update(SessionEditVM vm)
        {
            var session = _unitOfWork.Sessions.GetById(vm.Id);
            if (session == null)
                throw new Exception("Session not found.");

            if (vm.StartDate < DateTime.Today)
                throw new Exception("Start Date cannot be in the past.");
            if (vm.EndDate <= vm.StartDate)
                throw new Exception("End Date must be after Start Date.");

            var courseExists = _unitOfWork.Courses.GetById(vm.CourseId) != null;
            if (!courseExists)
                throw new Exception("Selected course does not exist.");

            session.CourseId = vm.CourseId;
            session.StartDate = vm.StartDate;
            session.EndDate = vm.EndDate;

            _unitOfWork.Sessions.Update(session);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var session = _unitOfWork.Sessions.GetById(id);
            if (session == null)
                throw new Exception("Session not found.");

            _unitOfWork.Sessions.Remove(session);
            _unitOfWork.Complete();
        }
    }
}
