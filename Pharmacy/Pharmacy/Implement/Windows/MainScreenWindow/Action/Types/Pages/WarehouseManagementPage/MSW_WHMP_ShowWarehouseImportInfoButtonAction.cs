using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage
{
    internal class MSW_WHMP_ShowWarehouseImportInfoButtonAction : MSW_WHMP_ButtonAction
    {
        public MSW_WHMP_ShowWarehouseImportInfoButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        public override void ExecuteCommand()
        {
            base.ExecuteCommand();
            object[] param = DataTransfer[1] as object[];
            DataGrid ctrl = param[0] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedWarehouseImport = WHMPViewModel.LstWarehouseImport[ctrl.SelectedIndex];
            PageHost.UpdateCurrentPageSource(PageSource.SHOW_WAREHOUSE_IMPORT_INFO_PAGE);

            return;
        }
    }
}