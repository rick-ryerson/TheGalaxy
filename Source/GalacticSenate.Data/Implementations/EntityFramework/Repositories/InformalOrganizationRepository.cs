using GalacticSenate.Data.Interfaces;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Implementations.EntityFramework.Repositories {
    public class InformalOrganizationRepository : OrganizationRepository, IInformalOrganizationRepository {
        public InformalOrganizationRepository(IUnitOfWork<DataContext> unitOfWork) : base(unitOfWork) {
        }

        public Task<InformalOrganization> AddAsync(InformalOrganization model) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(InformalOrganization model) {
            throw new NotImplementedException();
        }

        IEnumerable<InformalOrganization> IRepository<InformalOrganization, Guid>.Get(int pageIndex, int pageSize) {
            throw new NotImplementedException();
        }

        Task<InformalOrganization> IRepository<InformalOrganization, Guid>.GetAsync(Guid id) {
            throw new NotImplementedException();
        }
    }
}
