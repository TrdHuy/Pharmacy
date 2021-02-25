using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InvoiceManagementPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.InvoiceManagementPage
{
    public class MSW_IMP_DeleteButtonAction : Base.UIEventHandler.Action.IAction
    {
        private InvoiceManagementPageViewModel _viewModel;
        private SQLQueryCustodian _sqlQueryObserver;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as InvoiceManagementPageViewModel;

            if(_viewModel.CurrentSelectedOrderOV != null)
            {
                var x = App.Current.ShowApplicationMessageBox("Bạn có muốn xóa hóa đơn này?",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo");
                if(x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
                {
                    _sqlQueryObserver = new SQLQueryCustodian(SetDeactiveCustomerOrderQueryCallback);
                    DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_CUSTOMER_ORDER_DEACTIVE_CMD_KEY,
                        _sqlQueryObserver,
                        _viewModel.CurrentSelectedOrderOV.Order
                        );
                }
            }
            return true;
        }

        private void SetDeactiveCustomerOrderQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Xóa hóa đơn thành công!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                OwnerWindow.MainScreen,
                "Thông báo!!");

                _viewModel.CustomerOrdersItemSource.Remove(_viewModel.CurrentSelectedOrderOV);
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi xóa hóa đơn!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                OwnerWindow.MainScreen,
                "Thông báo!!");
            }
        }
    }
}
