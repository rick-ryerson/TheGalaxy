﻿using EventBus.Abstractions;
using GalacticSenate.Data.Implementations.EntityFramework;
using GalacticSenate.Data.Seeding;
using GalacticSenate.Domain.Model;
using GalacticSenate.Library.Events;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Tests {
   public abstract class GalacticSenateFixture : DatabaseFixture {
      protected readonly UnitOfWork unitOfWork;
      protected readonly Mock<IEventBus> eventBusMock;
      protected readonly IEventFactory eventFactory;

      protected GalacticSenateFixture(string databaseName) : base(databaseName) {
         unitOfWork = new UnitOfWork(dataContext);
         eventBusMock = new Mock<IEventBus>();
         eventFactory = new EventFactory();
      }
   }
   public abstract class DatabaseFixture : IDisposable {

      private bool disposedValue;
      protected readonly DbContextOptions<DataContext> options;
      protected readonly DataContextFactory contextFactory;
      protected readonly DataContext dataContext;

      public DatabaseFixture(string databaseName) {
         options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName).Options;
         contextFactory = new DataContextFactory(options);

         dataContext = contextFactory.GetNewDataContext();

         GenderSeeder.Seed(dataContext);

         dataContext.SaveChanges();
      }

      protected virtual void Dispose(bool disposing) {
         if (!disposedValue) {
            if (disposing) {
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

      public void Dispose() {
         // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
         Dispose(disposing: true);
         GC.SuppressFinalize(this);
      }
   }
}
