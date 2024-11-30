using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Tests.Fixtures {
    public abstract class PersonNameValueServicesFixture : GalacticSenateFixture {
        protected readonly IPersonNameValueRepository personNameValueRepository;
        protected readonly IEventsFactory<Model.PersonNameValue, int> personNameValueEventsFactory;

        protected PersonNameValueServicesFixture(string databaseName) : base(databaseName) {
            this.personNameValueRepository = new PersonNameValueRepository(unitOfWork);
            this.personNameValueEventsFactory = new EventsFactory<Model.PersonNameValue, int>();
        }
    }
}
