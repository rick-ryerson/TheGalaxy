using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalacticSenate.Data.Interfaces.Repositories {
    public interface IOrganizationRepository : IPartyRepository, IRepository<Organization, Guid> {

    }
}
