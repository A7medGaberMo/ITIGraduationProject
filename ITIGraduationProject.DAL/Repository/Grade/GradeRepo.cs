using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.Generic;
using System.Collections.Generic;
using System.Linq;

namespace ITIGraduationProject.DAL.Repository
{
    public class GradeRepo : GenericRepository<Grade>, IGradeRepo
    {
        public GradeRepo(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Grade> GetGradesByTraineeId(int traineeId)
        {
            return _context.Grades.Where(g => g.TraineeId == traineeId).ToList();
        }

        public IEnumerable<Grade> GetGradesBySessionId(int sessionId)
        {
            return _context.Grades.Where(g => g.SessionId == sessionId).ToList();
        }
    }
}
