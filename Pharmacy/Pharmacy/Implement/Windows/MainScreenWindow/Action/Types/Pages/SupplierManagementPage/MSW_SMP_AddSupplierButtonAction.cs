using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage
{
    internal class MSW_SMP_AddSupplierButtonAction : MSW_SMP_ButtonAction
    {

        public MSW_SMP_AddSupplierButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.ADD_SUPPLIER_PAGE);
        }
    }
}