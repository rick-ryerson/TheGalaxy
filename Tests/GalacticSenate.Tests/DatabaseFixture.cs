using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Tests
{
    public abstract class DatabaseFixture : IDisposable
    {

        private bool disposedValue;
        protected readonly DbContextOptions<DataContext> options;
        protected readonly DataContextFactory contextFactory;
        public DatabaseFixture(string databaseName)
        {
            options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName).Options;
            contextFactory = new DataContextFactory(options);

            var genders = new List<Gender>()
            {
                new Gender { Id = 1, Value = "Male" },
                new Gender { Id = 2, Value = "Female" }
            };

            using (var context = contextFactory.GetNewDataContext())
            {
                context.Set<Gender>().AddRange(genders);
            }
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
