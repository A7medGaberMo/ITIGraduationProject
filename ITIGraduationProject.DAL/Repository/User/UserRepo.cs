using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.Generic;
using System.Collections.Generic;
using System.Linq;

namespace ITIGraduationProject.DAL.Repository
{
    public class UserRepo : GenericRepository<User>, IUserRepo
    {
        public UserRepo(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<User> GetAllInstructors()
        {
            return _context.Users.Where(u => u.Role == Models.Enums.UserRole.Instructor).ToList();
        }

        public IEnumerable<User> GetAllTrainees()
        {
            return _context.Users.Where(u => u.Role == Models.Enums.UserRole.Trainee).ToList();
        }

        public IEnumerable<User> GetUsersByAgeRange(int minAge, int maxAge)
        {
            return _context.Users.Where(u => u.Age >= minAge && u.Age <= maxAge).ToList();
        }

        public bool IsAgeUnique(int age, int? excludeId = null)
        {
            return !_context.Users.Any(u => u.Age == age && (!excludeId.HasValue || u.Id != excludeId.Value));
        }
    }
}
