using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage
{
    internal class MSW_WHMP_DeleteWarehouseImportButtonAction : MSW_WHMP_ButtonAction
    {
        private DataGrid warehouseDataGrid;
        public MSW_WHMP_DeleteWarehouseImportButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);

            warehouseDataGrid = DataTransfer[1] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa thông tin nhập kho này?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {

                SQLQueryCustodian sqlQueryObserver = new SQLQueryCustodian((queryResult) =>
                {
                    if (queryResult.MesResult == MessageQueryResult.Done)
                    {
                        App.Current.ShowApplicationMessageBox("Xóa thông tin nhập kho thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        WHMPViewModel.WarehouseImportItemSource.RemoveAt(warehouseDataGrid.SelectedIndex);
                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_WAREHOUSE_IMPORT_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    WHMPViewModel.WarehouseImportItemSource[warehouseDataGrid.SelectedIndex].ImportID);

                return;
            }

            return;
        }
    }
}
