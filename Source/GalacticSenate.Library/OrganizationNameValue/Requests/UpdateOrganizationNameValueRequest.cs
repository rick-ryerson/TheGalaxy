using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.OrganizationNameValue.Requests
{
    public class UpdateOrganizationNameValueRequest
    {
        public int Id { get; set; }
        public string NewValue { get; set; }
    }
}
