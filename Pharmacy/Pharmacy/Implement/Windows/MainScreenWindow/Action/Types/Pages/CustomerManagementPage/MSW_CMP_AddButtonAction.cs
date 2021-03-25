using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage
{
    internal class MSW_CMP_AddButtonAction : MSW_CMP_ButtonAction
    {
        public MSW_CMP_AddButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }


        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_INSTANTIATION_PAGE);
        }
    }
}