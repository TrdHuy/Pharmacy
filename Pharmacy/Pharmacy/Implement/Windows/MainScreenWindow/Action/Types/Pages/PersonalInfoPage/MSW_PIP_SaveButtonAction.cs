using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions.Entities;
using System;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Windows;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage
{
    internal class MSW_PIP_SaveButtonAction : MSW_PIP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private tblUser modifiedInfo;
        public MSW_PIP_SaveButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            if (!PIPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                PIPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
                return;
            }

            modifiedInfo = new tblUser();
            modifiedInfo.Username = PIPViewModel.CurrentUser.Username;
            modifiedInfo.FullName = PIPViewModel.FullNameText;
            modifiedInfo.Address = PIPViewModel.AdressText;
            modifiedInfo.Phone = PIPViewModel.PhoneText;
            modifiedInfo.Email = PIPViewModel.EmailText;
            modifiedInfo.Link = PIPViewModel.LinkText;
            modifiedInfo.Job = PIPViewModel.CurrentUser.Job;
            modifiedInfo.UserDataJSON = PIPViewModel.CurrentUser.GetUserDataJSON();

            modifiedInfo.Password = String.IsNullOrEmpty(PIPViewModel.NewPassword) ?
                App.Current.CurrentUser.Password : PIPViewModel.NewPassword;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.UPDATE_USER_INFO_CMD_KEY
                    , PharmacyDefinitions.SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME
                    , _sqlCmdObserver
                    , modifiedInfo
                    , App.Current.CurrentUser.Username
                    , PIPViewModel.UserImageFileName);
            return;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Cập nhật thông tin thành công!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                PageHost.UpdateCurrentPageSource(PageSource.HOME_PAGE);
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi cập nhật thông tin!");
            }
            PIPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
        }
    }
}
