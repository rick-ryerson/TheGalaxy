using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Events {
   public interface ICreatedEventFactory<M> {
      Created<M> Created(M item);
   }
   public interface IUpdatedEventFactory<M> {
      Updated<M> Updated(M newValue, M oldValue);
   }
   public interface IDeletedEventFactory<M> {
      Deleted<M> Deleted(M id);
   }


   //public interface IEventFactory3 {
   //   Created<T> CreateCreated<T>(T item);
   //   Deleted<T> CreateDeleted<T>(T item);
   //   Updated<T> CreateUpdated<T>(T newValue, T oldValue);
   //}
   //public class EventFactory3 : IEventFactory3 {
   //   public Created<T> CreateCreated<T>(T item) {
   //      return new Created<T>(item);
   //   }

   //   public Updated<T> CreateUpdated<T>(T newValue, T oldValue) {
   //      return new Updated<T>(newValue, oldValue);
   //   }

   //   public Deleted<T> CreateDeleted<T>(T item) {
   //      return new Deleted<T>(item);
   //   }
   //}
}
