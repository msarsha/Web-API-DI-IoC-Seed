using Seed.Data.Entities;
using Seed.Infrastructure.Models;
using Seed.Interfaces.BL;
using Seed.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Infrastructure.BL
{
    public class SeedManager : ISeedManager
    {
        private ISeedRepository repo;
        private IUnitOfWork uow;

        public SeedManager(ISeedRepository repo, IUnitOfWork uow)
        {
            this.repo = repo;
            this.uow = uow;
        }

        public IEnumerable<string> FindAll()
        {
            return new string[] { "Hello", "Seed" };
        }

        public string FindById(int id)
        {
            return "Hello " + id.ToString();
        }

        public void CreateSeed(SeedDto seed)
        {
            SeedEntity entity = new SeedEntity() { };
            repo.Create(entity);
            uow.SaveChanges();
        }

    }
}
