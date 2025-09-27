using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Repository;
using ITIGraduationProject.DAL.Repository.Generic;

namespace ITIGraduationProject.DAL.Repository.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICourseRepo Courses { get; }        
        public IGenericRepository<Session> Sessions { get; }
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Grade> Grades { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Courses = new CourseRepo(_context);    
            Sessions = new GenericRepository<Session>(_context);
            Users = new GenericRepository<User>(_context);
            Grades = new GenericRepository<Grade>(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
