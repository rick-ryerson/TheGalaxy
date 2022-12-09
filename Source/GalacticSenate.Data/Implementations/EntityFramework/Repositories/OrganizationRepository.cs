using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Implementations.EntityFramework.Repositories {
   internal class OrganizationRepository : IOrganizationRepository {
      private readonly IUnitOfWork<DataContext> unitOfWork;

      public OrganizationRepository(IUnitOfWork<DataContext> unitOfWork) {
         this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      }

      public async Task<Organization> AddAsync(Organization organization) {
         await unitOfWork.Context.Organizations.AddAsync(organization);

         return organization;
      }

      public async Task DeleteAsync(Guid organizationId) {
         var organization = await GetAsync(organizationId);

         if (organization == null)
            throw new DeleteException($"Organization with id {organizationId} does not exist.");

         unitOfWork
            .Context
            .Organizations
            .Remove(organization);
      }

      public IEnumerable<Organization> Get(int pageIndex, int pageSize) {
         return unitOfWork
            .Context
            .Organizations
            .Skip(pageSize * pageIndex)
            .Take(pageSize);
      }

      public async Task<Organization> GetAsync(Guid organizationId) {
         return await unitOfWork
            .Context
            .Organizations
            .FindAsync(organizationId);
      }

      public void UpdateAsync(Organization organization) {
         unitOfWork
            .Context
            .Organizations
            .Attach(organization);

         unitOfWork
            .Context
            .Entry(organization)
            .State = EntityState.Modified;
      }
   }
}
