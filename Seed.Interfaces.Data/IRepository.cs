using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Interfaces.Data
{
    public interface IRepository<T> where T : class
    {
        T FindById(int id);
        IEnumerable<T> FindAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Remove(int id);
        void Create(T entity);
    }
}
