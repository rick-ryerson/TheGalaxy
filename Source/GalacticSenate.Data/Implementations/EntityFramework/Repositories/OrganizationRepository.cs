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
    public class OrganizationRepository : PartyRepository, IOrganizationRepository {
        public OrganizationRepository(IUnitOfWork<DataContext> unitOfWork) : base(unitOfWork) {
        }

        async Task<Organization> IRepository<Organization, Guid>.AddAsync(Organization organization) {
            await unitOfWork.Context.Organizations.AddAsync(organization);

            return organization;
        }

        async Task IRepository<Organization, Guid>.DeleteAsync(Organization model) {
            var entity = await GetAsync(model.Id);

            if (entity == null)
                throw new DeleteException($"Organization with id {model.Id} does not exist.");

            unitOfWork
               .Context
               .Organizations
               .Remove(model);
        }

        IEnumerable<Organization> IRepository<Organization, Guid>.Get(int pageIndex, int pageSize) {
            return unitOfWork
               .Context
               .Organizations
               .Skip(pageSize * pageIndex)
               .Take(pageSize);
        }

        async Task<Organization> IRepository<Organization, Guid>.GetAsync(Guid id) {
            return await unitOfWork
               .Context
               .Organizations
               .FindAsync(id);
        }
    }
}
