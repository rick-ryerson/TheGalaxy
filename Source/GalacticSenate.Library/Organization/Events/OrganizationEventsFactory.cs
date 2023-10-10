using GalacticSenate.Library.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Organization.Events {
   public interface IOrganizationEventsFactory :
      ICreatedEventFactory<Model.Organization>,
      IUpdatedEventFactory<Model.Organization>,
      IDeletedEventFactory<int> { }
   public class OrganizationEventsFactory : IOrganizationEventsFactory {
      public Created<Model.Organization> Created(Model.Organization item) {
         return new Created<Model.Organization>(item);
      }

      public Deleted<int> Deleted(int id) {
         return new Deleted<int>(id);
      }

      public Updated<Model.Organization> Updated(Model.Organization newValue, Model.Organization oldValue) {
         return new Updated<Model.Organization>(newValue, oldValue);
      }
   }

}
