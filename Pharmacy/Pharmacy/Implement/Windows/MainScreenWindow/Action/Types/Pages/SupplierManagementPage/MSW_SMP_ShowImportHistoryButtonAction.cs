using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage
{
    internal class MSW_SMP_ShowImportHistoryButtonAction : MSW_SMP_ButtonAction
    {
        public MSW_SMP_ShowImportHistoryButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            DataGrid ctrl = DataTransfer[0] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedSupplier = SMPViewModel.SupplierItemSource[ctrl.SelectedIndex];
            PageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_IMPORT_HISTORY_PAGE);

        }
    }
}