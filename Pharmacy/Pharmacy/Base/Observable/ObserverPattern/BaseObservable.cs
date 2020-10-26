using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Observable.ObserverPattern
{
    abstract class BaseObservable : IObservable
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyChange()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
