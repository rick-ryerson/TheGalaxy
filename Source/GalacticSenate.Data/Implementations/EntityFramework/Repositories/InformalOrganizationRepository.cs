using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Exceptions;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Implementations.EntityFramework.Repositories {
    internal class InformalOrganizationRepository : OrganizationRepository, IInformalOrganizationRepository {
        public InformalOrganizationRepository(IUnitOfWork<DataContext> unitOfWork) : base(unitOfWork) {
        }

        public async Task<InformalOrganization> AddAsync(InformalOrganization model) {
            var result = await unitOfWork
                .Context
                .InformalOrganizations
                .AddAsync(model);

            return result.Entity;
        }

        public Task DeleteAsync(InformalOrganization model) {
            return ((IRepository<InformalOrganization, Guid>)this).DeleteAsync(model.Id);
        }
        async Task IRepository<InformalOrganization, Guid>.DeleteAsync(Guid id) {
            var entity = await ((IRepository<InformalOrganization, Guid>)this).GetAsync(id);

            if (entity == null)
                throw new DeleteException($"Informal Organization with id {id} does not exist.");

            unitOfWork
                .Context
                .InformalOrganizations
                .Remove(entity);
        }
        public void Update(InformalOrganization model) {
            var entity = unitOfWork
                .Context
                .InformalOrganizations
                .Find(model.Id);

            if (entity == null)
                return;

            var entry = unitOfWork
                .Context
                .Entry(entity);

            entry.CurrentValues.SetValues(model);
        }

        IEnumerable<InformalOrganization> IRepository<InformalOrganization, Guid>.Get(int pageIndex, int pageSize) {
            return unitOfWork
                .Context
                .InformalOrganizations
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
        }

        async Task<InformalOrganization> IRepository<InformalOrganization, Guid>.GetAsync(Guid id) {
            return await unitOfWork
                .Context
                .InformalOrganizations
                .FindAsync(id);
        }
    }
}
