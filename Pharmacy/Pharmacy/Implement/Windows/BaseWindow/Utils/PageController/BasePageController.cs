using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Windows.BaseWindow.MVVM.Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.BaseWindow.Utils.PageController
{
    public class BasePageController : BaseObservable<PageVO>
    {
        public PageVO CurrentPageOV { get; set; }
        public PageSource CurrentPageSource { get; protected set; } = PageSource.NONE;
        public PageSource PreviousePageSource { get; protected set; } = PageSource.NONE;

    }


}
