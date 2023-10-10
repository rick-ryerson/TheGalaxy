using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
   public interface IOrganizationRepository {
      IEnumerable<Organization> Get(int pageIndex, int pageSize);
      Task<Organization> GetAsync(Guid organizationId);
      Task<Organization> AddAsync(Organization organization);
      void UpdateAsync(Organization organization);
      Task DeleteAsync(Guid organizationId);
   }
}
