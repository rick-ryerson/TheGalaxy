using System;

namespace GalacticSenate.Library.Services.OrganizationName.Requests {
   public class AddOrganizationNameRequest
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
    }
}