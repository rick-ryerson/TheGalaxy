using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Tests.Fixtures {
    public abstract class OrganizationNameValueServicesFixture : GalacticSenateFixture {
        protected readonly IOrganizationNameValueRepository organizationNameValueRepository;
        protected readonly IEventsFactory eventsFactory;

        protected OrganizationNameValueServicesFixture(string databaseName) : base(databaseName) {
            this.organizationNameValueRepository = new OrganizationNameValueRepository(unitOfWork);
            this.eventsFactory = new EventsFactory();
        }
    }
}
