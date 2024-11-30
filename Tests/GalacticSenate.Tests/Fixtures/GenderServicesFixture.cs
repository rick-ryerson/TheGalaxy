using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Tests.Fixtures {
    public abstract class GenderServicesFixture : GalacticSenateFixture {
        protected readonly IGenderRepository genderRepository;
        protected readonly IEventsFactory<Model.Gender, int> genderEventsFactory;

        protected GenderServicesFixture(string databaseName) : base(databaseName) {
            this.genderRepository = new GenderRepository(unitOfWork);
            this.genderEventsFactory = new EventsFactory<Model.Gender, int>();
        }
    }
}
