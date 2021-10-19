using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Domain.Model
{
    public class Person
    {
        public Guid Id { get; set; }
        public Guid PartyId { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual Party Party { get; set; }
        public virtual List<PersonName> Names { get; set; }
        public virtual List<PersonMaritalStatus> MaritalStatuses { get; set; }
        public virtual List<PersonGender> Genders { get; set; }
    }
}
