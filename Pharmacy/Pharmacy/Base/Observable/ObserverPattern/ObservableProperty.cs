using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Base.Observable.ObserverPattern
{
    public class ObservableProperty : BaseObservable<ObservableProperty>
    {
        private object _value;
        private OnPropertyChangedEventHandler OnPropertyChangedEvent;
        public object Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
                OnPropertyChangedEvent?.Invoke(this, new OnPropertyChangedEventArgs(_value));
                NotifyChange(this);
            }
        }

        public event OnPropertyChangedEventHandler OnPropertyChanged
        {
            add { OnPropertyChangedEvent += value; }
            remove { OnPropertyChangedEvent -= value; }
        }

    }

    public delegate void OnPropertyChangedEventHandler(object sender, OnPropertyChangedEventArgs e);

    public class OnPropertyChangedEventArgs : EventArgs
    {
        private object _value;

        public OnPropertyChangedEventArgs(object val)
        {
            _value = val;
        }

        public object Value
        {
            get { return _value; }
        }
    }
}
