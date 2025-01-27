using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Library.Requests {
    public class AddOrganizationNameValueRequest {
        public string Value { get; set; }
    }

    public class DeleteOrganizationNameValueRequest {
        public int Id { get; set; }
    }

    public class ReadOrganizationNameValueMultiRequest {
        public Guid OrganizationId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadOrganizationNameValueRequest {
        public int Id { get; set; }
    }

    public class ReadOrganizationNameValueValueRequest {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }
    public class UpdateOrganizationNameValueRequest {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
