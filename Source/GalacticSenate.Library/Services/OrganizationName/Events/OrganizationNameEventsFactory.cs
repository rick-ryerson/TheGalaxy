using GalacticSenate.Library.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.OrganizationName.Events
{
    public interface IOrganizationNameEventsFactory :
       ICreatedEventFactory<Model.OrganizationName>,
       IUpdatedEventFactory<Model.OrganizationName>,
       IDeletedEventFactory<int>
    { }
    public class OrganizationNameEventsFactory : IOrganizationNameEventsFactory
    {
        public Created<Model.OrganizationName> Created(Model.OrganizationName item)
        {
            return new Created<Model.OrganizationName>(item);
        }

        public Deleted<int> Deleted(int id)
        {
            return new Deleted<int>(id);
        }

        public Updated<Model.OrganizationName> Updated(Model.OrganizationName newValue, Model.OrganizationName oldValue)
        {
            return new Updated<Model.OrganizationName>(newValue, oldValue);
        }
    }

}
