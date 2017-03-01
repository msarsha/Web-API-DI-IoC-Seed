using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
