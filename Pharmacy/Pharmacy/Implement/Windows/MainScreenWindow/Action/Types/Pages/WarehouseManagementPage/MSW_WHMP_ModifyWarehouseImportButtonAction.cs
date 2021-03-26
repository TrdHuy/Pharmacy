using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage
{
    internal class MSW_WHMP_ModifyWarehouseImportButtonAction : MSW_WHMP_ButtonAction
    {

        public MSW_WHMP_ModifyWarehouseImportButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            DataGrid ctrl = DataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedWarehouseImport = WHMPViewModel.LstWarehouseImport[ctrl.SelectedIndex];
            PageHost.UpdateCurrentPageSource(PageSource.MODIFY_WAREHOUSE_IMPORT_PAGE);

            return;
        }
    }
}