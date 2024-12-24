using System;
using System.Collections.Generic;
using System.Text;

using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Requests {
    public class AddPartyRequest {
        public Guid Id { get; set; }
    }
    public class AddOrganizationRequest : AddPartyRequest {
        public Model.OrganizationNameValue Name { get; set; }
    }
    public class AddInformalOrganizationRequest : AddOrganizationRequest { }

    public class AddFamilyRequest : AddInformalOrganizationRequest { }
}
