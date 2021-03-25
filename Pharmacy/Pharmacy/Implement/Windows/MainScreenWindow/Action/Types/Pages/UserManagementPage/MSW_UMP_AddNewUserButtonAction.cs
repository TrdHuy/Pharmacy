using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage
{
    internal class MSW_UMP_AddNewUserButtonAction : MSW_UMP_ButtonAction
    {
        public MSW_UMP_AddNewUserButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.USER_INSTANTIATION_PAGE);
        }
    }
}