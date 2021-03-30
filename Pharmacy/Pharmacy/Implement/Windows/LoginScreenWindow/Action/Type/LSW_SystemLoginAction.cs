using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type
{
    internal class LSW_SystemLoginAction : LSW_ButtonAction
    {
        private SQLQueryCustodian _observer;

        public LSW_SystemLoginAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            TextBox userNameTextEdit = (TextBox)DataTransfer[0];
            PasswordBox userPasswordTextEdit = (PasswordBox)DataTransfer[1];

            string userName = userNameTextEdit.Text;
            string passWord = userPasswordTextEdit.Password;

            try
            {
                _observer = new SQLQueryCustodian(SQLQueryCallback);
                DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.CHECK_USER_AVAIL_CMD_KEY
                    , PharmacyDefinitions.LOGIN_BUTTON_PERFORM_DELAY_TIME
                    , _observer
                    , userName, passWord);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            return;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            List<tblUser> result = (List<tblUser>)queryResult.Result;
            try
            {
                int count = result.Count();
                if (count == 1)
                {
                    if (result[0].IsActive)
                    {
                        App.Current.SessionIDInstansiation(result[0]);
                        SaveUserName(result[0].Username);
                    }
                    else
                    {
                        App.Current.ShowApplicationMessageBox("Tài khoản này hiện tại đã bị xóa, vui lòng liên hệ admin để khôi phục!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                        OwnerWindow.LoginScreen);
                    }

                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Thông tin đăng nhập không chính xác!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                        OwnerWindow.LoginScreen);
                }
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message,
                       HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                       HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                       OwnerWindow.LoginScreen);
            }
            finally
            {
                if (LSWViewModel != null)
                {
                    LSWViewModel.IsLoginButtonRunning = false;
                }

                //In this asycn action, till the call back method was done execute
                //need to set the Compelete flag  
                SetCompleteFlagAfterExecuteCommand();
            }
        }

        protected override void SetCompleteFlagAfterExecuteCommand()
        {
            IsCompleted = !LSWViewModel.IsLoginButtonRunning;
        }

        private void SaveUserName(string userName)
        {
            if (LSWViewModel.IsUserRemember)
            {
                LSWViewModel.UserName = userName;
            }
        }
    }
}
