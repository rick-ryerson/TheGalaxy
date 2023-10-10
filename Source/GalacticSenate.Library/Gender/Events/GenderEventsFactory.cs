using GalacticSenate.Library.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Model = GalacticSenate.Domain.Model;

namespace GalacticSenate.Library.Gender.Events {
   public interface IGenderEventsFactory :
      ICreatedEventFactory<Model.Gender>,
      IUpdatedEventFactory<Model.Gender>,
      IDeletedEventFactory<int> { }
   public class GenderEventsFactory : IGenderEventsFactory {
      public Created<Model.Gender> Created(Model.Gender item) {
         return new Created<Model.Gender>(item);
      }

      public Deleted<int> Deleted(int id) {
         return new Deleted<int>(id);
      }

      public Updated<Model.Gender> Updated(Model.Gender newValue, Model.Gender oldValue) {
         return new Updated<Model.Gender>(newValue, oldValue);
      }
   }

}
