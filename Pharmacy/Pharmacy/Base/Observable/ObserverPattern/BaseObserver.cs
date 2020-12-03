using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Observable.ObserverPattern
{
    public class BaseObserver<T> : IObserver<T>
    {
        protected Action<T> OnUpdate;
        protected Action OnCompleted;

        public BaseObserver(Action<T> onUpdate = null, Action onCompleted = null)
        {
            OnUpdate = onUpdate;
            OnCompleted = onCompleted;
        }

        public void Update(T value)
        {
            OnUpdate?.Invoke(value);
            OnCompleted?.Invoke();
        }

    }
}
