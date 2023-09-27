using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Events {
   public interface IEventFactory {
      Created<T> CreateCreated<T>(T item);
      Deleted<T> CreateDeleted<T>(T item);
      Updated<T> CreateUpdated<T>(T newValue, T oldValue);
   }
   public class EventFactory : IEventFactory {
      public Created<T> CreateCreated<T>(T item) {
         return new Created<T>(item);
      }

      public Updated<T> CreateUpdated<T>(T newValue, T oldValue) {
         return new Updated<T>(newValue, oldValue);
      }

      public Deleted<T> CreateDeleted<T>(T item) {
         return new Deleted<T>(item);
      }

   }
}
