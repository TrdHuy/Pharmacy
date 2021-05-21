using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;
using System.Linq;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage
{
    internal class MSW_WHMP_ShowWarehouseImportInfoButtonAction : MSW_WHMP_ButtonAction
    {
        public MSW_WHMP_ShowWarehouseImportInfoButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            object[] param = DataTransfer[0] as object[];
            DataGrid ctrl = param[0] as DataGrid;

            var selectedItem = ctrl.SelectedItem as MSW_WHMP_WarehouseImportOV;

            MSW_DataFlowHost.Current.CurrentModifiedWarehouseImport = WHMPViewModel.LstWarehouseImport.Where(o => o.ImportID == selectedItem.ImportID).FirstOrDefault();
            PageHost.UpdateCurrentPageSource(PageSource.SHOW_WAREHOUSE_IMPORT_INFO_PAGE);

            return;
        }
    }
}