using System;
using System.Collections.Generic;

namespace GalacticSenate.Domain.Model {
    public class Organization {
        public Guid Id { get; set; }
        public Guid PartyId { get; set; }

        public virtual Party Party { get; set; }

        public virtual List<OrganizationName> Names { get; set; }
    }

    public class InformalOrganization {
        public Guid Id { get; set; }

        public virtual Organization Organization { get; set; }
    }

    public class Family {
        public Guid Id { get; set; }

        public virtual InformalOrganization InformalOrganization { get; set; }
    }

    public class LegalOrganization {
        public Guid Id { get; set; }
        public virtual Organization Organization { get; set; }
    }

    public class Corporation {
        public Guid Id { get; set; }

        public virtual LegalOrganization LegalOrganization { get; set; }
    }
}