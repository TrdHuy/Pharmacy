using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserInstantiationPage
{
    internal class MSW_UMP_UIP_CancleButtonAction : MSW_UMP_UIP_ButtonAction
    {
        public MSW_UMP_UIP_CancleButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.USER_MANAGEMENT_PAGE);
        }
    }
}
