using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
   public interface IOrganizationNameValueRepository {
      IEnumerable<OrganizationNameValue> Get(int pageIndex, int pageSize);
      Task<OrganizationNameValue> GetAsync(int id);
      Task<OrganizationNameValue> GetExactAsync(string value);
      IEnumerable<OrganizationNameValue> GetContains(string value);
      Task<OrganizationNameValue> AddAsync(OrganizationNameValue organizationNameValue);
      void Update(OrganizationNameValue organizationNameValue);
      Task DeleteAsync(int id);
   }
}
