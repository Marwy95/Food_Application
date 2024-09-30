using System.Linq.Expressions;

namespace Food_Application.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T GetByID(int id);
        T First(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
        void SaveChanges();
        string Test();
       
     
    
      
    }
}
