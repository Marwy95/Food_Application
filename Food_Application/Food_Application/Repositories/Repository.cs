using Food_Application.Data;
using Food_Application.Models;
using System.Linq.Expressions;

namespace Food_Application.Repositories
{
    public class Repository<T> : IRepository<T> where T :BaseModel
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
             _context.Add(entity);
            return entity;
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().Where(e => !e.Deleted);
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
        public T GetByID(int id)
        {
            return GetAll().FirstOrDefault(t => t.ID == id);
        }
        public T First(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Any(predicate);
        }
        public void Update(T entity)
        {
           _context.Update(entity);
        }
        public void Delete(int id)
        {
            var entity = GetByID(id);
           Delete(entity);
        }
        public void Delete(T entity)
        {
            entity.Deleted = true;
            Update(entity);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public string Test()
        {
            return "hello from repository";
        }

     
    }
}
