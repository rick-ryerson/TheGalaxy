using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Library.Events {
   internal class Created<T> : Event {
      public Created(T value) {
         Value = value;
      }

      public T Value { get; }
   }
   internal class Updated<T> : Event {
      public Updated(T newValue, T oldValue) {
         NewValue = newValue;
         OldValue = oldValue;
      }

      public T NewValue { get; }
      public T OldValue { get; }
   }
   internal class Deleted<T> : Event {
      public Deleted(T value) {
         Value = value;
      }

      public T Value { get; }
   }
}
