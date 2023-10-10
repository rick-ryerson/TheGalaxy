using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace GalacticSenate.Tests.Fixtures {
   public abstract class GalacticSenateFixture : DatabaseFixture
    {
        internal readonly IUnitOfWork<DataContext> unitOfWork;
        protected readonly Mock<IEventBus> eventBusMock;

        protected GalacticSenateFixture(string databaseName) : base(databaseName)
        {
            unitOfWork = new UnitOfWork(dataContext);
            eventBusMock = new Mock<IEventBus>();
        }
    }
    public abstract class DatabaseFixture : IDisposable
    {

        private bool disposedValue;
        protected readonly DbContextOptions<DataContext> options;
        protected readonly DataContextFactory contextFactory;
        protected readonly DataContext dataContext;

        public DatabaseFixture(string databaseName)
        {
            // TODO: implement DbContextOptionsBuilder.UseLoggerFactory
            options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName).Options;
            contextFactory = new DataContextFactory(options);

            dataContext = contextFactory.GetNewDataContext();

            GenderSeeder.Seed(dataContext);

            dataContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DatabaseFixture()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
