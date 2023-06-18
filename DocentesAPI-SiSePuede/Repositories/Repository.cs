using DocentesAPI_SiSePuede.Models;
using Microsoft.EntityFrameworkCore;

namespace DocentesAPI_SiSePuede.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly Sistem21PrimariaContext Context;

        public Repository(Sistem21PrimariaContext cx)
        {
            Context=cx;
        }
        public DbSet<T> GetAll()
        {
            return Context.Set<T>();
        }
        public T? GetById(object id)
        {
            return Context.Find<T>(id);
        }
        public void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }
        public void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }
        public void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
    }
}
