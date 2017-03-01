using Seed.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Interfaces.Data
{
    public interface ISeedRepository : IRepository<SeedEntity>
    {
        SeedEntity GetSpcecialSeed();
    }
}
