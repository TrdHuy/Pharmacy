using Pharmacy.Implement.Windows.BaseWindow.MVVM.Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.BaseWindow.Utils.PageController
{
    public class PageSourceWatcher : Base.Observable.ObserverPattern.IObserver<PageVO>
    {
        private Action<PageVO> OnPageSourceChange;

        internal PageSourceWatcher(Action<PageVO> onSourceChange)
        {
            OnPageSourceChange = onSourceChange;
        }
        public void Update(PageVO value)
        {
            OnPageSourceChange?.Invoke(value);
        }
    }
}
