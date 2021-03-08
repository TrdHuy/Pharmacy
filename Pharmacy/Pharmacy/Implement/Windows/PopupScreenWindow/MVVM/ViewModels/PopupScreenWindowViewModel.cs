using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels
{
    public class PopupScreenWindowViewModel: BaseViewModel
    {
        public Uri CurrentPageSource { get; set; }
        public long PageLoadingDelaytime { get; set; }
    }
}
