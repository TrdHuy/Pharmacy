using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage
{
    public class MSW_OPMP_DeleteOtherPaymentButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private OtherPaymentsManagementPageViewModel _viewModel;
        private DataGrid ctrl;
        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as OtherPaymentsManagementPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa thanh toán này?",
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
                        App.Current.ShowApplicationMessageBox("Xóa thông tin thanh toán thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        _viewModel.OtherPaymentItemSource.RemoveAt(ctrl.SelectedIndex);
                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_OTHER_PAYMENT_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    _viewModel.OtherPaymentItemSource[ctrl.SelectedIndex].PaymentID);

                return true;
            }

            return false;
        }
    }
}
