using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage
{
    internal class MSW_CMP_DeleteButtonAction : MSW_CMP_ButtonAction
    {
        private DataGrid _customerDataGrid;

        public MSW_CMP_DeleteButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);

            _customerDataGrid = DataTransfer[1] as DataGrid;
            
            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa khách hàng này?",
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
                        App.Current.ShowApplicationMessageBox("Xóa khách hàng thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        CMPViewModel.CustomerItemSource.RemoveAt(_customerDataGrid.SelectedIndex);

                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_CUSTOMER_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    CMPViewModel.CustomerItemSource[_customerDataGrid.SelectedIndex].CustomerID);
            }
        }

    }
}
