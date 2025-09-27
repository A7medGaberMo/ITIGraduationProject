using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository;
using ITIGraduationProject.DAL.Repository.Generic;

namespace ITIGraduationProject.DAL.Repository.UnitofWork
{
    public interface IUnitOfWork
    {
        ICourseRepo Courses { get; }       // <-- Changed from IGenericRepository<Course>
        IGenericRepository<Session> Sessions { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<Grade> Grades { get; }

        void Complete();
    }
}
