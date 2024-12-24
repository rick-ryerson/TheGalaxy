using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Library.Events;
using GalacticSenate.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticSenate.Tests.Properties {
    public abstract class PartyServiceFixture : GalacticSenateFixture {
        protected readonly PartyRepository partyRepository;
        protected readonly EventsFactory eventsFactory;

        protected PartyServiceFixture(string databaseName) : base(databaseName) {
            this.partyRepository = new PartyRepository(unitOfWork);
            this.eventsFactory = new EventsFactory();
        }
    }
}
