using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.BaseWindow.MVVM.ViewModel.OVs;
using System.Diagnostics;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs
{
    internal abstract class MSW_ButtonCommandOV : ButtonCommandOV
    {

        protected MSW_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel) { }

        protected override ICommandExecuter OnKey(string keyTag, object paramaters, bool isViewModelOnKey = true, string windowTag = WindowTag.WINDOW_TAG_MAIN_SCREEN)
        {
            return base.OnKey(keyTag, paramaters, isViewModelOnKey, windowTag);
        }

        protected override ICommandExecuter OnKey(string keyTag, object paramaters, BuilderLocker locker, bool isViewModelOnKey = true, string windowTag = WindowTag.WINDOW_TAG_MAIN_SCREEN)
        {
            return base.OnKey(keyTag, paramaters, locker, isViewModelOnKey, windowTag);
        }

    }
}
