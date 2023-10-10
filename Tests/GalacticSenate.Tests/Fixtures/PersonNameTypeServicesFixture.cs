using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Services.PersonNameType.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Tests.Fixtures
{
    public abstract class PersonNameTypeServicesFixture : GalacticSenateFixture {
      protected readonly IPersonNameTypeRepository personNameTypeRepository;
      protected readonly IPersonNameTypeEventsFactory personNameTypeEventsFactory;

      protected PersonNameTypeServicesFixture(string databaseName) : base(databaseName) {
         this.personNameTypeRepository = new PersonNameTypeRepository(unitOfWork);
         this.personNameTypeEventsFactory = new PersonNameTypeEventsFactory();
      }
   }
}
