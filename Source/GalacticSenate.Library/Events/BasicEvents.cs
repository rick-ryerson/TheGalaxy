using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Events {
   public class Created<T> : Event {
      public Created(T value) {
         Value = value;
      }

      public T Value { get; }
   }
   public class Updated<T> : Event {
      public Updated(T newValue, T oldValue) {
         NewValue = newValue;
         OldValue = oldValue;
      }

      public T NewValue { get; }
      public T OldValue { get; }
   }
   public class Deleted<T> : Event {
      public Deleted(T value) {
         Value = value;
      }

      public T Value { get; }
   }
}
