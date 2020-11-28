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

        public void onChanged(object viewModel, string propertyName)
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(viewModel, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }
    }
}
