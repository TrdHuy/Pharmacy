using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerInstantiationPage
{
    internal class MSW_CMP_CIP_CancleButtonAction : MSW_CMP_CIP_ButtonAction
    {
        public MSW_CMP_CIP_CancleButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_MANAGEMENT_PAGE);
        }
    }
}
