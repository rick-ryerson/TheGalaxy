using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Data.Interfaces.Repositories {
   public interface IRepositoryFactory {
      IGenderRepository GetGenderRepository();
      IMaritalStatusTypeRepository GetMaritalStatusTypeRepository();
      IPersonNameTypeRepository GetPersonNameTypeRepository();
      IOrganizationNameValueRepository GetOrganizationNameValueRepository();
      IPersonNameValueRepository GetPersonNameValueRepository();
      IPartyRepository GetPartyRepository();
   }
}
