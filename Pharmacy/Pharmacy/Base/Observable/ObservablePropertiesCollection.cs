using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Observable
{

    public class ObservablePropertiesCollection<T> : ObservableCollection<T>
        where T : INotifyPropertyChanged
    {
        public PropertyChangedEventHandler ItemPropertiesChanged;

        public ObservablePropertiesCollection() : base()
        {
            CollectionChanged += ObservablePropertiesCollectionChangedCallback;
        }

        private void ObservablePropertiesCollectionChangedCallback(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (object item in e.NewItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += ItemPropertyChangedCallback;
                }
            }

            if (e.OldItems != null)
            {
                foreach (object item in e.OldItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged -= ItemPropertyChangedCallback;
                }
            }
        }

        private void ItemPropertyChangedCallback(object sender, PropertyChangedEventArgs e)
        {
            ItemPropertiesChanged?.Invoke(sender, e);
        }

    }
}
