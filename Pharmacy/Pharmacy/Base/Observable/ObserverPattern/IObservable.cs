using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Observable.ObserverPattern
{
    interface IObservable
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void NotifyChange();
    }
}
