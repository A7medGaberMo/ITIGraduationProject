using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.Generic;
using System.Collections.Generic;
using System.Linq;

namespace ITIGraduationProject.DAL.Repository
{
    public class SessionRepo : GenericRepository<Session>, ISessionRepo
    {
        public SessionRepo(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Session> GetSessionsByCourseId(int courseId)
        {
            return _context.Sessions.Where(s => s.CourseId == courseId).ToList();
        }

        public Session? GetSessionWithGrades(int id)
        {
            return _context.Sessions.FirstOrDefault(s => s.Id == id);
        }
    }
}
