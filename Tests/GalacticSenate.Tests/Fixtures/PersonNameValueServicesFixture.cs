using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using GalacticSenate.Library.Gender.Events;
using GalacticSenate.Library.PersonNameType.Events;
using GalacticSenate.Library.PersonNameValue.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Tests.Fixtures {
   public abstract class PersonNameValueServicesFixture : GalacticSenateFixture {
      protected readonly IPersonNameValueRepository personNameValueRepository;
      protected readonly IPersonNameValueEventsFactory personNameValueEventsFactory;

      protected PersonNameValueServicesFixture(string databaseName) : base(databaseName) {
         this.personNameValueRepository = new PersonNameValueRepository(unitOfWork);
         this.personNameValueEventsFactory = new PersonNameValueEventsFactory();
      }
   }
}
