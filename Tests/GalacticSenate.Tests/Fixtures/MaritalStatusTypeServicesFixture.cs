using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Services.MaritalStatusType.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Tests.Fixtures
{
    public abstract class MaritalStatusTypeServicesFixture : GalacticSenateFixture {
      protected readonly IMaritalStatusTypeRepository maritalStatusTypeRepository;
      protected readonly IMaritalStatusTypeEventsFactory maritalStatusTypeEventsFactory;

      protected MaritalStatusTypeServicesFixture(string databaseName) : base(databaseName) {
         this.maritalStatusTypeRepository = new MaritalStatusTypeRepository(unitOfWork);
         this.maritalStatusTypeEventsFactory = new MaritalStatusTypeEventsFactory();
      }
   }
}
