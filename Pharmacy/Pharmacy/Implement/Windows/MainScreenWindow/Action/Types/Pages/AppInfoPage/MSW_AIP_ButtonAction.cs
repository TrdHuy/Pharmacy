using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.AppInfoPage;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.AppInfoPage
{
    internal class MSW_AIP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected AppInfoPageViewModel AIPViewmodel
        {
            get
            {
                return ViewModel as AppInfoPageViewModel;
            }
        }

        public MSW_AIP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        protected override void ExecuteVM(params object[] dataTransfer)
        {

        }
    }
}
