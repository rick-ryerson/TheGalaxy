using Celestial.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Domain.Model
{
    public class OrganizationName : HistoricRelation<Organization, OrganizationNameValue>
    {
        public int Id { get; set; }
        public int Ordinal { get; set; }
    }
}
