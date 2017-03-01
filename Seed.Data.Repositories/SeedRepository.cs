using Seed.Data.Entities;
using Seed.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Seed.Data.Repositories
{
    public class SeedRepository : BaseEFRepository<SeedEntity>, ISeedRepository
    {
        public SeedRepository(DbContext context) : base(context)
        {
        }

        public SeedEntity GetSpcecialSeed()
        {
            throw new NotImplementedException();
        }
    }
}
