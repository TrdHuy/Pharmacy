using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Windows.BaseWindow.MVVM.ViewModel.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.PSW_BasePageVM.OVs
{
    internal abstract class PSW_ButtonCommandOV : ButtonCommandOV
    {

        protected PSW_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel) { }

        protected override ICommandExecuter OnKey(string keyTag, object paramaters, bool isViewModelOnKey = true, string windowTag = WindowTag.WINDOW_TAG_POPUP_SCREEN)
        {
            return base.OnKey(keyTag, paramaters, isViewModelOnKey, windowTag);
        }

        protected override ICommandExecuter OnKey(string keyTag, object paramaters, BuilderLocker locker, bool isViewModelOnKey = true, string windowTag = WindowTag.WINDOW_TAG_POPUP_SCREEN)
        {
            return base.OnKey(keyTag, paramaters, locker, isViewModelOnKey, windowTag);
        }

    }
}