using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Domain.Model {
   public class RoleType {
      public int Id { get; set; }
      public string Description { get; set; }
   }
   public class PartyRoleType : RoleType {
      public List<PartyRole> PartyRoles { get; set; }
   }

   public class PartyRole {
      public int Id { get; set; }
      public DateTime? From { get; set; }
      public DateTime? Thru { get; set; }

      public PartyRoleType PartyRoleType { get; set; }
      public List<Party> Parties { get; set; }
   }
   public class PersonRole : PartyRole { }
   public class Employee : PersonRole { }
   public class Contact : PersonRole { }

   public class Customer : PartyRole { }
   public class BillToCustomer : Customer { }
   public class ShipToCustomer : Customer { }

   public class Prospect : PartyRole { }

   public class OrganizationRole : PartyRole { }
   public class Partner : OrganizationRole { }
   public class Household : OrganizationRole { }
   public class Supplier : OrganizationRole { }
   public class Association : OrganizationRole { }

   public class OrganizationUnit : OrganizationRole { }
   public class ParentOrganization : OrganizationUnit { }
   public class Subsidiary : OrganizationUnit { }
   public class Division : OrganizationUnit { }
   public class Department : OrganizationUnit { }
   public class InternalOrganizaiton : OrganizationRole { }
}
