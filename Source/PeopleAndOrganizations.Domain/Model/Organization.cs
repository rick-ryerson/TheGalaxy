using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Domain.Model
{
    public class Organization
    {
        public Party Party { get; set; }

        public virtual List<OrganizationName> Names { get; set; }
    }
}
