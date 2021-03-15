using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs
{
    public abstract class MSW_ButtonCommandOV : BaseViewModel
    {
        protected abstract Logger logger { get; }

        protected KeyActionListener _keyActionListener = KeyActionListener.Instance;

        protected MSW_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {

        }

        protected void OnKey(string keyTag, object paramaters, string windowTag = WindowTag.WINDOW_TAG_MAIN_SCREEN)
        {
            logger.I("OnKey: keyTag = " + keyTag + " windowTag = " + windowTag);

#if DEBUG
            Stopwatch onKeyWatcher = Stopwatch.StartNew();
#endif
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this.ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(windowTag
                , keyTag
                , dataTransfer);

#if DEBUG
            onKeyWatcher.Stop();
            var timeExecuted = onKeyWatcher.ElapsedMilliseconds;
            logger.D("Time executed on key = " + timeExecuted + "(ms)");
#endif
            logger.I("Done: keyTag = " + keyTag + " windowTag = " + windowTag);
        }

        protected void OnKey(string keyTag, object paramaters, FactoryLocker locker, string windowTag = WindowTag.WINDOW_TAG_MAIN_SCREEN)
        {
            logger.I("OnKey: keyTag = " + keyTag + " windowTag = " + windowTag);

#if DEBUG
            Stopwatch onKeyWatcher = Stopwatch.StartNew();
#endif
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this.ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(windowTag
                , keyTag
                , dataTransfer
                , locker);

#if DEBUG
            onKeyWatcher.Stop();
            var timeExecuted = onKeyWatcher.ElapsedMilliseconds;
            logger.D("Time executed on key = " + timeExecuted + "(ms)");
#endif
            logger.I("Done: keyTag = " + keyTag + " windowTag = " + windowTag);
        }
    }
}
