using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Observable.ObserverPattern
{
    interface IObservable<out T>
    {
        void Subcribe(IObserver<T> observer);

        void Unsubcribe(IObserver<T> observer);

        void NotifyChange();
    }
}
