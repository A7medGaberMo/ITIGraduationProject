using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository.Generic;
using System.Collections.Generic;

namespace ITIGraduationProject.DAL.Repository
{
    public interface IUserRepo : IGenericRepository<User>
    {
        IEnumerable<User> GetAllInstructors();
        IEnumerable<User> GetAllTrainees();
        IEnumerable<User> GetUsersByAgeRange(int minAge, int maxAge);
        bool IsAgeUnique(int age, int? excludeId = null);
    }
}
