using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Observable.ObserverPattern
{
    abstract class BaseObservable<T> : IObservable<T>
    {
        private List<IObserver<T>> _observers = new List<IObserver<T>>();
        private T _result;

        public void Subcribe(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unsubcribe(IObserver<T> observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void NotifyChange()
        {
            foreach (IObserver<T> observer in _observers)
            {
                observer.Update(_result);
            }
        }
    }
}
