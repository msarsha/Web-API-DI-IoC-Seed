using Seed.Data.Entities;
using Seed.Data.Repositories;
using Seed.Infrastructure.BL;
using Seed.Interfaces.BL;
using Seed.Interfaces.Data;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Infrastructure.Resolver
{
    public class SimpleInjectorResolver
    {
        public static void SetupDependencies(Container container)
        {
            RegisterRepositories(container);
            RegisterServices(container);
        }

        private static void RegisterServices(Container container)
        {
            container.Register<ISeedManager, SeedManager>(Lifestyle.Scoped);
        }

        private static void RegisterRepositories(Container container)
        {
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<DbContext, FakeDbContext>(Lifestyle.Scoped);

            container.Register<ISeedRepository, SeedRepository>(Lifestyle.Scoped);
        }
    }
}
