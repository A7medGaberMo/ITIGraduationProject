using System.Linq;

namespace ITIGraduationProject.DAL.Repository.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void SaveChanges();

       
        IQueryable<T> AsQueryable();
    }
}
