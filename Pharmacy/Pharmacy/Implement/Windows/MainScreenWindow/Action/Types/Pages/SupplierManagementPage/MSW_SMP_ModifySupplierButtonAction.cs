using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage
{
    internal class MSW_SMP_ModifySupplierButtonAction : MSW_SMP_ButtonAction
    {
        public MSW_SMP_ModifySupplierButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        public override void ExecuteCommand()
        {
            base.ExecuteCommand();

            DataGrid ctrl = DataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedSupplier = SMPViewModel.SupplierItemSource[ctrl.SelectedIndex];
            PageHost.UpdateCurrentPageSource(PageSource.MODIFY_SUPPLIER_PAGE);
        }
    }
}