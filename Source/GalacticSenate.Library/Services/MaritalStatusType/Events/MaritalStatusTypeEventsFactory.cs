using GalacticSenate.Library.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.MaritalStatusType.Events
{

    public interface IMaritalStatusTypeEventsFactory :
    ICreatedEventFactory<Model.MaritalStatusType>,
    IUpdatedEventFactory<Model.MaritalStatusType>,
    IDeletedEventFactory<int>
    { }
    public class MaritalStatusTypeEventsFactory : IMaritalStatusTypeEventsFactory
    {
        public Created<Model.MaritalStatusType> Created(Model.MaritalStatusType item)
        {
            return new Created<Model.MaritalStatusType>(item);
        }

        public Deleted<int> Deleted(int id)
        {
            return new Deleted<int>(id);
        }

        public Updated<Model.MaritalStatusType> Updated(Model.MaritalStatusType newValue, Model.MaritalStatusType oldValue)
        {
            return new Updated<Model.MaritalStatusType>(newValue, oldValue);
        }
    }
}
