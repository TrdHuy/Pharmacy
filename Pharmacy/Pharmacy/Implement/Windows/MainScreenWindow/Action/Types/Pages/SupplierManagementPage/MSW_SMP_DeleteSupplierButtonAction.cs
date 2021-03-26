using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage
{
    internal class MSW_SMP_DeleteSupplierButtonAction : MSW_SMP_ButtonAction
    {
        private DataGrid supplierDataGrid;
        public MSW_SMP_DeleteSupplierButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            supplierDataGrid = DataTransfer[1] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa nhà cung cấp này?",
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
                        App.Current.ShowApplicationMessageBox("Xóa nhà cung cấp thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        SMPViewModel.SupplierItemSource.RemoveAt(supplierDataGrid.SelectedIndex);
                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_SUPPLIER_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    SMPViewModel.SupplierItemSource[supplierDataGrid.SelectedIndex].SupplierID);

                return;
            }

            return;
        }
    }
}
