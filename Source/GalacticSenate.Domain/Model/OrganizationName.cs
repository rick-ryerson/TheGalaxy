using Celestial.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Domain.Model
{
    public class OrganizationName
    {
        public Guid OrganizationId { get; set; }
        public int OrganizationNameValueId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual OrganizationNameValue OrganizationNameValue { get; set; }

    }
}
