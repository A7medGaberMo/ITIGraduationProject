using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.Generic;
using System.Collections.Generic;

namespace ITIGraduationProject.DAL.Repository
{
    public interface ISessionRepo : IGenericRepository<Session>
    {
        IEnumerable<Session> GetSessionsByCourseId(int courseId);
        Session? GetSessionWithGrades(int id);
    }
}
