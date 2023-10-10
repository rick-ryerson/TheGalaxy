using GalacticSenate.Library.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Services.OrganizationNameValue.Events
{

    public interface IOrganizationNameValueEventsFactory :
    ICreatedEventFactory<Model.OrganizationNameValue>,
    IUpdatedEventFactory<Model.OrganizationNameValue>,
    IDeletedEventFactory<int>
    { }
    public class OrganizationNameValueEventsFactory : IOrganizationNameValueEventsFactory
    {
        public Created<Model.OrganizationNameValue> Created(Model.OrganizationNameValue item)
        {
            return new Created<Model.OrganizationNameValue>(item);
        }

        public Deleted<int> Deleted(int id)
        {
            return new Deleted<int>(id);
        }

        public Updated<Model.OrganizationNameValue> Updated(Model.OrganizationNameValue newValue, Model.OrganizationNameValue oldValue)
        {
            return new Updated<Model.OrganizationNameValue>(newValue, oldValue);
        }
    }

}
