using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Core.MVVM.ViewModels
{
    class BaseLoginScreenWindowViewModel : AbstractViewModel
    {
        public BaseLoginScreenWindowViewModel()
        {
        }

        public BaseLoginScreenWindowViewModel(AbstractViewModel parentsModel) : base(parentsModel)
        {
        }

        public override void Update()
        {
        }

        protected override void InitPropertiesRegistry()
        {
        }

        
    }
}
