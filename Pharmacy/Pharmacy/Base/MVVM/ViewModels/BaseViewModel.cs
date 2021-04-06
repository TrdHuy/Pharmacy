using Pharmacy.Base.Observable;
using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Pharmacy.Base.MVVM.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, Observable.ObserverPattern.IObservable<object>, IDestroyable
    {
        private List<string> _propertiesRegistry = new List<string>();
        private List<Observable.ObserverPattern.IObserver<object>> _observers = new List<Observable.ObserverPattern.IObserver<object>>();

        public BaseViewModel ParentsModel { get; set; }

        #region Ctor
        public BaseViewModel()
        {
            Init(null);
        }

        public BaseViewModel(BaseViewModel parentsModel)
        {
            Init(parentsModel);
        }

        private void Init(BaseViewModel parentsModel)
        {
            ParentsModel = parentsModel;
        }
        #endregion

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(object viewModel, string propertyName)
        {
            VerifyPropertyName(viewModel, propertyName);
            PropertyChanged?.Invoke(viewModel, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(object viewModel, string propertyName)
        {
            if (TypeDescriptor.GetProperties(viewModel)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }
        #endregion

        #region Observable field
        public void Subcribe(Observable.ObserverPattern.IObserver<object> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unsubcribe(Observable.ObserverPattern.IObserver<object> observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void NotifyChange(object result)
        {
            foreach (Observable.ObserverPattern.IObserver<object> observer in _observers)
            {
                observer.Update(result);
            }
        }

        #endregion

        #region Public methods
        public void Invalidate(string property)
        {
            OnChanged(this, property);
        }

        public void Invalidate(BaseViewModel vm, string property)
        {
            OnChanged(vm, property);
        }

        public void InvalidateOwn([CallerMemberName()] string name = null)
        {
            OnChanged(this, name);
        }

        public void AddOnPropertyChange(string properties)
        {
            _propertiesRegistry.Add(properties);
        }

        public void InvalidateAll()
        {
            foreach (string property in _propertiesRegistry)
            {
                Invalidate(property);
            }
        }

        #endregion


        public virtual void OnDestroy()
        {
        }


        public virtual void RefreshViewModel()
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);

            foreach (PropertyDescriptor property in properties)
            {
                var attributes = property.Attributes;

                if (attributes[typeof(BindableAttribute)].Equals(BindableAttribute.Yes))
                {
                    Invalidate(property.Name);
                }
            }
        }
    }
}
