using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage
{
    public class MSW_UMP_DeleteUserButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private UserManagementPageViewModel _viewModel;
        private DataGrid ctrl;
        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as UserManagementPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            if (!CanDeleteAccount())
            {
                return false;
            }

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa tài khoản này?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {

                SQLQueryCustodian sqlQueryObserver = new SQLQueryCustodian((queryResult) =>
                {
                    if (queryResult.MesResult == MessageQueryResult.Finished)
                    {
                        App.Current.ShowApplicationMessageBox("Xóa tài khoản thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        _viewModel.UserItemSource.RemoveAt(ctrl.SelectedIndex);

                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_USER_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    _viewModel.UserItemSource[ctrl.SelectedIndex].Username);
            }

            return true;
        }

        private bool CanDeleteAccount()
        {
            if (_viewModel.UserItemSource[ctrl.SelectedIndex].Username.
                Equals(App.Current.CurrentUser.Username))
            {
                App.Current.ShowApplicationMessageBox("Bạn không thể xóa tài khoản hiện tại đang đăng nhập!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Stop,
                        OwnerWindow.MainScreen,
                        "Cảnh báo!");
                return false;
            }

            if (_viewModel.UserItemSource[ctrl.SelectedIndex].IsAdmin)
            {
                App.Current.ShowApplicationMessageBox("Bạn không thể xóa tài khoản quản trị hệ thống!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Stop,
                        OwnerWindow.MainScreen,
                        "Cảnh báo!");
                return false;
            }

            return true;
        }
    }
}
