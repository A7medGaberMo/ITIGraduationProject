using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.Generic;
using System.Collections.Generic;

namespace ITIGraduationProject.DAL.Repository
{
    public interface IGradeRepo : IGenericRepository<Grade>
    {
        IEnumerable<Grade> GetGradesByTraineeId(int traineeId);
        IEnumerable<Grade> GetGradesBySessionId(int sessionId);
    }
}
