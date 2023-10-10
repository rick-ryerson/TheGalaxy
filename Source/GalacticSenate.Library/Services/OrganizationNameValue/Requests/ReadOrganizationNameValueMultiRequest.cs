using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Services.OrganizationNameValue.Requests
{
    public class ReadOrganizationNameValueMultiRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReadOrganizationNameValueRequest
    {
        public int Id { get; set; }
    }

    public class ReadOrganizationNameValueValueRequest
    {
        public string Value { get; set; }
        public bool Exact { get; set; }
    }
}
