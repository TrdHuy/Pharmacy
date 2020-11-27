using Pharmacy.Base.Observable;
using Pharmacy.Base.Observable.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.MVVM.ViewModels
{
    public abstract class AbstractViewModel : OnPropertyChanged
    {
        private List<string> _propertiesRegistry = new List<string>();
        public AbstractViewModel ParentsModel { get; set; }

        public AbstractViewModel()
        {
            ParentsModel = null;
            InitPropertiesRegistry();
        }

        public AbstractViewModel(AbstractViewModel parentsModel)
        {
            ParentsModel = parentsModel;
            InitPropertiesRegistry();
        }

        public void Invalidate(string property)
        {
            onChanged(this, property);
        }

        public void InvalidateOwn([CallerMemberName()] string name = null)
        {
            onChanged(this, name);
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

        protected abstract void InitPropertiesRegistry();

    }
}
