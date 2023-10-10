using GalacticSenate.Library.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.PersonNameValue.Events {
   public interface IPersonNameValueEventsFactory :
      ICreatedEventFactory<Model.PersonNameValue>,
      IUpdatedEventFactory<Model.PersonNameValue>,
      IDeletedEventFactory<int> { }
   public class PersonNameValueEventsFactory : IPersonNameValueEventsFactory {
      public Created<Model.PersonNameValue> Created(Model.PersonNameValue item) {
         return new Created<Model.PersonNameValue>(item);
      }

      public Deleted<int> Deleted(int id) {
         return new Deleted<int>(id);
      }

      public Updated<Model.PersonNameValue> Updated(Model.PersonNameValue newValue, Model.PersonNameValue oldValue) {
         return new Updated<Model.PersonNameValue>(newValue, oldValue);
      }
   }

}
