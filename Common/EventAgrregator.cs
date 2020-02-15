using System;
using System.Collections.Generic;

namespace Tasker.Common
{
    public class EventAgrregator : IObservable<Message>
    {
        private List<IObserver<Message>> observers;
        public EventAgrregator()
        {
            observers = new List<IObserver<Message>>();
        }
        public IDisposable Subscribe(IObserver<Message> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Message>> _observers;
            private IObserver<Message> _observer;

            public Unsubscriber(List<IObserver<Message>> observers, IObserver<Message> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void Notify(Message message)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(message);
            }
        }


    }
}
