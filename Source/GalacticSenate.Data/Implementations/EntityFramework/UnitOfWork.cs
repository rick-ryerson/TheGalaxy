using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using GalacticSenate.Data.Interfaces;
using GalacticSenate.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Data.Implementations.EntityFramework.Repositories;

namespace GalacticSenate.Data.Implementations.EntityFramework {
   internal class UnitOfWork : IUnitOfWork<DataContext>, IDisposable {
      private bool disposedValue;
      private IDbContextTransaction transaction;
      private readonly DataContext context;

      public UnitOfWork(DataContext context) {
         this.context = context ?? throw new ArgumentNullException(nameof(context));
      }

      public DataContext Context {
         get {
            return context;
         }
      }

      public void Begin() {
         transaction = context.Database.BeginTransaction();
      }

      public void Commit() {
         transaction.Commit();
      }

      public void Rollback() {
         try {
            transaction.Rollback();
         }
         catch (Exception ex) {
            throw new RollbackException(new List<string> { "An error occurred while attempting to rollback.", ex.Message }, ex);
         }

      }

      public void Save() {
         try {
            context.SaveChanges();
         }
         catch (DbUpdateException dbue) {
            throw new SaveException(new List<string> { "An error occurred while attempting to save.", dbue.Message }, dbue.Entries.Select(e => e.Entity.ToString()).ToList(), dbue);
         }
      }

      public IGenderRepository GetGenderRepository() {
         return new GenderRepository(this);
      }
      public IMaritalStatusTypeRepository GetMaritalStatusTypeRepository() {
         return new MaritalStatusTypeRepository(this);
      }
      public IPersonNameTypeRepository GetPersonNameTypeRepository() {
         return new PersonNameTypeRepository(this);
      }
      public IOrganizationNameValueRepository GetOrganizationNameValueRepository() {
         return new OrganizationNameValueRepository(this);
      }
      public IPersonNameValueRepository GetPersonNameValueRepository() {
         return new PersonNameValueRepository(this);
      }
      public IPartyRepository GetPartyRepository() {
         return new PartyRepository(this);
      }
      public IOrganizationNameRepository GetOrganizationNameRepository() {
         return new OrganizationNameRepository(this);
      }
      public IOrganizationRepository GetOrganizationRepository() {
         return new OrganizationRepository(this);
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
      // ~UnitOfWork()
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
