using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Implementations.EntityFramework.Repositories {
   public class OrganizationNameRepository : IOrganizationNameRepository {
      private readonly IUnitOfWork<DataContext> unitOfWork;

      public OrganizationNameRepository(IUnitOfWork<DataContext> unitOfWork) {
         this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      }

      public async Task<OrganizationName> AddAsync(OrganizationName organizationName) {
         await unitOfWork.Context.OrganizationNames.AddAsync(organizationName);

         return organizationName;
      }

      public async Task DeleteAsync(Guid organizationId, int organizationNameValueId, DateTime fromDate) {
         var organizationName = await GetAsync(organizationId, organizationNameValueId, fromDate);

         if (organizationName == null)
            throw new DeleteException($"OrganizationName with key {organizationId}, {organizationNameValueId}, {fromDate} does not exist.");

         unitOfWork.Context.OrganizationNames.Remove(organizationName);
      }

      public IEnumerable<OrganizationName> Get(Organization organization, int pageIndex, int pageSize) {
         return unitOfWork
            .Context
            .OrganizationNames
            .Where(o => o.Organization.Id == organization.Id)
            .Skip(pageSize * pageIndex)
            .Take(pageSize);
      }

      public IEnumerable<OrganizationName> Get(OrganizationNameValue organizationNameValue, int pageIndex, int pageSize) {
         return unitOfWork
            .Context
            .OrganizationNames
            .Where(o => o.OrganizationNameValueId == organizationNameValue.Id)
            .Skip(pageSize * pageIndex)
            .Take(pageSize);
      }

      public async Task<OrganizationName> GetAsync(Guid organizationId, int organizationNameValueId, DateTime fromDate) {
         return await unitOfWork
            .Context
            .OrganizationNames
            .FindAsync(organizationId, organizationNameValueId, fromDate);
      }

      public void Update(OrganizationName organizationName) {
         unitOfWork
            .Context
            .Attach(organizationName);

         unitOfWork
            .Context
            .Entry(organizationName)
            .State = EntityState.Modified;
      }
   }
}
