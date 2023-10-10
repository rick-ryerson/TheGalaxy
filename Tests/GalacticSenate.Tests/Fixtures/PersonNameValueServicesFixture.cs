using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Services.PersonNameValue.Events;

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
