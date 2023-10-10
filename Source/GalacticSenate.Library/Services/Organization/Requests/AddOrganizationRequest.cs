using System;
using System.Collections.Generic;
using System.Text;

using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.Organization.Requests
{
    public class AddOrganizationRequest
    {
        public Guid PartyId { get; set; }

        public Model.OrganizationNameValue Name { get; set; }
    }
}
