using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.AddSupplierPage
{
    internal class MSW_SMP_ASP_CancelButtonAction : MSW_SMP_ASP_ButtonAction
    {
        public MSW_SMP_ASP_CancelButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
      
        protected override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_MANAGEMENT_PAGE);

        }
    }
}