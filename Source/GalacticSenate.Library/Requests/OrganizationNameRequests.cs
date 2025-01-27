using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Library.Requests {
    public class AddOrganizationNameRequest {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
    }

    public class GetOrganizationNamesForOrganizationRequest {
        public Guid OrganizationId { get; set; }
        public DateTime? ForDate { get; set; }
    }
}
