using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Services.OrganizationNameValue.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Tests.Fixtures
{
    public abstract class OrganizationNameValueServicesFixture : GalacticSenateFixture {
      protected readonly IOrganizationNameValueRepository organizationNameValueRepository;
      protected readonly IOrganizationNameValueEventsFactory organizationNameValueEventsFactory;

      protected OrganizationNameValueServicesFixture(string databaseName) : base(databaseName) {
         this.organizationNameValueRepository = new OrganizationNameValueRepository(unitOfWork);
         this.organizationNameValueEventsFactory = new OrganizationNameValueEventsFactory();
      }
   }
}
