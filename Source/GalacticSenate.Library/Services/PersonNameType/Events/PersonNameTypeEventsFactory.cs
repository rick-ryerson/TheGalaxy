using GalacticSenate.Library.Events;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.PersonNameType.Events {
   public interface IPersonNameTypeEventsFactory :
       ICreatedEventFactory<Model.PersonNameType>,
       IUpdatedEventFactory<Model.PersonNameType>,
       IDeletedEventFactory<int>
    { }
    public class PersonNameTypeEventsFactory : IPersonNameTypeEventsFactory
    {
        public Created<Model.PersonNameType> Created(Model.PersonNameType item)
        {
            return new Created<Model.PersonNameType>(item);
        }

        public Deleted<int> Deleted(int id)
        {
            return new Deleted<int>(id);
        }

        public Updated<Model.PersonNameType> Updated(Model.PersonNameType newValue, Model.PersonNameType oldValue)
        {
            return new Updated<Model.PersonNameType>(newValue, oldValue);
        }
    }

}
