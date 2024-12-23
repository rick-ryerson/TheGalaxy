using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Implementations.EntityFramework.Repositories {
    internal class FamilyRepository : InformalOrganizationRepository, IFamilyRepository {
        public FamilyRepository(IUnitOfWork<DataContext> unitOfWork) : base(unitOfWork) { }

        Task<Family> IRepository<Family, Guid>.AddAsync(Family model) {
            throw new NotImplementedException();
        }

        Task IRepository<Family, Guid>.DeleteAsync(Family model) {
            throw new NotImplementedException();
        }

        Task IRepository<Family, Guid>.DeleteAsync(Guid id) {
            throw new NotImplementedException();
        }

        IEnumerable<Family> IRepository<Family, Guid>.Get(int pageIndex, int pageSize) {
            throw new NotImplementedException();
        }

        Task<Family> IRepository<Family, Guid>.GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        void IRepository<Family, Guid>.Update(Family model) {
            throw new NotImplementedException();
        }
    }
}
