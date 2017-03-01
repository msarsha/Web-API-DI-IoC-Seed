using Seed.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Seed.Data.Repositories
{
    public abstract class BaseEFRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        private DbContext _context;

        //protected MyDbContext Context
        //{
        //    get
        //    {
        //        return this._context as NesherContext;
        //    }
        //}

        protected BaseEFRepository(DbContext context)
        {
            this._context = context;
            this.DbSet = context.Set<T>();
        }


        public void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IEnumerable<T> FindAll()
        {
            return DbSet;
        }

        public T FindById(int id)
        {
            return DbSet.Find(id);
        }

        public void Remove(int id)
        {
            T entity = FindById(id);
            DbSet.Remove(entity);
        }
    }
}
