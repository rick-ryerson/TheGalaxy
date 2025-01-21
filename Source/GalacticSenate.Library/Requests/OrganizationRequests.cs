using System;
using System.Collections.Generic;
using System.Text;

using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Requests {

    public class AddOrganizationRequest : AddPartyRequest {
        public string Name { get; set; }
    }
    public class ReadOrganizationMultiRequest : ReadPartyMultiRequest { }
    public class ReadOrganizationRequest : ReadPartyRequest { }

}
