namespace GalacticSenate.Library.Events {
   public interface ICreatedEventFactory {
      Created<M> Created<M>(M item);
   }
   public interface IUpdatedEventFactory {
      Updated<M> Updated<M>(M newValue, M oldValue);
   }
   public interface IDeletedEventFactory {
      Deleted<K> Deleted<K>(K id);
   }

    public interface IEventsFactory :
    ICreatedEventFactory,
    IUpdatedEventFactory,
    IDeletedEventFactory {

    }

    public class EventsFactory : IEventsFactory {
        public Created<T> Created<T>(T item) {
            return new Created<T>(item);
        }

        public Deleted<K> Deleted<K>(K id) {
            return new Deleted<K>(id);
        }

        public Updated<T> Updated<T>(T newValue, T oldValue) {
            return new Updated<T>(newValue, oldValue);
        }
    }
}
