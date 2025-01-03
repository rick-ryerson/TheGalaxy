﻿using GalacticSenate.Data.Implementations.EntityFramework.Repositories;
using GalacticSenate.Data.Interfaces.Repositories;
using GalacticSenate.Library.Events;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Tests.Fixtures {
    public abstract class PersonNameTypeServicesFixture : GalacticSenateFixture {
        protected readonly IPersonNameTypeRepository personNameTypeRepository;
        protected readonly IEventsFactory eventsFactory;

        protected PersonNameTypeServicesFixture(string databaseName) : base(databaseName) {
            this.personNameTypeRepository = new PersonNameTypeRepository(unitOfWork);
            this.eventsFactory = new EventsFactory();
        }
    }
}
