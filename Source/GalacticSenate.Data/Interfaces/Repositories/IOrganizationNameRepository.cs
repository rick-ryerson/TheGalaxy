using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
    public interface IOrganizationNameRepository {
        IEnumerable<OrganizationName> Get(Guid organizationId, int pageIndex, int pageSize);
        IEnumerable<OrganizationName> Get(Guid organizationId, DateTime fromDate, int pageIndex, int pageSize);
        IEnumerable<OrganizationName> Get(OrganizationNameValue organizationNameValue, int pageIndex, int pageSize);
        Task<OrganizationName> GetAsync(Guid organizationId, int organizationNameValueId, DateTime fromDate);
        Task<OrganizationName> AddAsync(OrganizationName organizationName);
        void Update(OrganizationName organizationName);
        Task DeleteAsync(Guid organizationId, int organizationNameValueId, DateTime fromDate);
    }
}
