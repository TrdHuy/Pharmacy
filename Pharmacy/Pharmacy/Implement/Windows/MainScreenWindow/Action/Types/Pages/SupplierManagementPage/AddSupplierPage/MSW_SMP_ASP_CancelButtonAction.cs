using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.AddSupplierPage
{
    internal class MSW_SMP_ASP_CancelButtonAction : MSW_SMP_ASP_ButtonAction
    {
        public MSW_SMP_ASP_CancelButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
      
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_MANAGEMENT_PAGE);

        }
    }
}