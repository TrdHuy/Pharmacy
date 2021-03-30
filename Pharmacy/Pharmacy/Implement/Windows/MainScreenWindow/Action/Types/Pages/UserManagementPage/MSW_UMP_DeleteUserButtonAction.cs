using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage
{
    internal class MSW_UMP_DeleteUserButtonAction : MSW_UMP_ButtonAction
    {
        private DataGrid userDataGrid;
        public MSW_UMP_DeleteUserButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            userDataGrid = DataTransfer[0] as DataGrid;

            if (!CanDeleteAccount())
            {
                return;
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
                    if (queryResult.MesResult == MessageQueryResult.Done)
                    {
                        App.Current.ShowApplicationMessageBox("Xóa tài khoản thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        UMPViewModel.UserItemSource.RemoveAt(userDataGrid.SelectedIndex);

                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_USER_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    UMPViewModel.UserItemSource[userDataGrid.SelectedIndex].Username);
            }

        }

        private bool CanDeleteAccount()
        {
            if (UMPViewModel.UserItemSource[userDataGrid.SelectedIndex].Username.
                Equals(App.Current.CurrentUser.Username))
            {
                App.Current.ShowApplicationMessageBox("Bạn không thể xóa tài khoản hiện tại đang đăng nhập!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Stop,
                        OwnerWindow.MainScreen,
                        "Cảnh báo!");
                return false;
            }

            if (UMPViewModel.UserItemSource[userDataGrid.SelectedIndex].IsAdmin)
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
