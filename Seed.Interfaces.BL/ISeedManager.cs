using Seed.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Interfaces.BL
{
    public interface ISeedManager
    {
        IEnumerable<string> FindAll();
        string FindById(int id);
        void CreateSeed(SeedDto seed);
    }
}
