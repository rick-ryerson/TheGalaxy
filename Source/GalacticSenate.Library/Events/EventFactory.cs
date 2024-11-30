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

    public interface IEventsFactory<T, K> :
    ICreatedEventFactory<T>,
    IUpdatedEventFactory<T>,
    IDeletedEventFactory<K> {

    }

    public class EventsFactory<T, K> : IEventsFactory<T, K> {
        public Created<T> Created(T item) {
            return new Created<T>(item);
        }

        public Deleted<K> Deleted(K id) {
            return new Deleted<K>(id);
        }

        public Updated<T> Updated(T newValue, T oldValue) {
            return new Updated<T>(newValue, oldValue);
        }
    }
}
