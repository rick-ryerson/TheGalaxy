using GalacticSenate.Library.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.Party.Events
{
    public interface IPartyEventsFactory :
       ICreatedEventFactory<Model.Party>,
       IUpdatedEventFactory<Model.Party>,
       IDeletedEventFactory<Guid>
    { }
    public class PartyEventsFactory : IPartyEventsFactory
    {
        public Created<Model.Party> Created(Model.Party item)
        {
            return new Created<Model.Party>(item);
        }

        public Deleted<Guid> Deleted(Guid id)
        {
            return new Deleted<Guid>(id);
        }

        public Updated<Model.Party> Updated(Model.Party newValue, Model.Party oldValue)
        {
            return new Updated<Model.Party>(newValue, oldValue);
        }
    }

}
