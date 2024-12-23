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
    internal class FamilyRepository : InformalOrganizationRepository, IFamilyRepository {
        public FamilyRepository(IUnitOfWork<DataContext> unitOfWork) : base(unitOfWork) { }

        async Task<Family> IRepository<Family, Guid>.AddAsync(Family model) {
            await unitOfWork
                .Context
                .Families
                .AddAsync(model);

            return model;
        }

        Task IRepository<Family, Guid>.DeleteAsync(Family model) {
            return ((IRepository<Family, Guid>)this).DeleteAsync(model.Id);
        }

        async Task IRepository<Family, Guid>.DeleteAsync(Guid id) {
            var entity = await ((IRepository<Family, Guid>)this).GetAsync(id);

            if (entity == null)
                throw new DeleteException($"Family with id {id} does not exist.");

            unitOfWork
                .Context
                .Families
                .Remove(entity);
        }
        IEnumerable<Family> IRepository<Family, Guid>.Get(int pageIndex, int pageSize) {
            return unitOfWork
               .Context
               .Families
               .Skip(pageSize * pageIndex)
               .Take(pageSize);
        }

        async Task<Family> IRepository<Family, Guid>.GetAsync(Guid id) {
            return await unitOfWork
                .Context
                .Families
                .FindAsync(id);
        }

        void IRepository<Family, Guid>.Update(Family model) {
            var entity = unitOfWork
               .Context
               .Families
               .Find(model.Id);

            if (entity == null)
                return;

            var entry = unitOfWork
               .Context
               .Entry(entity);

            entry.CurrentValues.SetValues(model);
        }
    }
}
