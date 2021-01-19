using Pharmacy.Base.Observable.ObserverPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Observable
{
    public abstract class OnPropertyChanged : BaseObservable<object>, INotifyPropertyChanged
    {
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
    }
}
