using ITIGraduationProject.BLL.ViewModels.SessionVM;
using System.Collections.Generic;

namespace ITIGraduationProject.BLL.Services
{
    public interface ISessionService
    {
        // Basic CRUD
        IEnumerable<SessionListVM> GetAll();       // Return ListVM for display
        SessionEditVM? GetById(int id);            // Return EditVM for editing
        void Create(SessionCreateVM vm);
        void Update(SessionEditVM vm);
        void Delete(int id);

        // Search + Pagination
        IEnumerable<SessionListVM> GetPagedSessions(
            int pageNumber,
            int pageSize,
            string? courseName = null,
            string? search = null);

        int GetSessionsCount(string? courseName = null, string? search = null);

        // ✅ New method for course filter dropdown
        IEnumerable<(int CourseId, string CourseName)> GetAllCourses();
    }
}
