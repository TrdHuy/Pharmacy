using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage
{
    internal class UserManagementPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("UserManagementPageViewModel");

        private SQLQueryCustodian _sqlCmdObserver;

        public ObservableCollection<tblUser> UserItemSource { get; set; }
        public MSW_UMP_ButtomCommandOV ButtomCommandOV { get; set; }

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            UserItemSource = new ObservableCollection<tblUser>();
            ButtomCommandOV = new MSW_UMP_ButtomCommandOV(this);
            InstantiateItems();
        }

        protected override void OnInitialized()
        {
        }

        private void InstantiateItems()
        {
            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_USER_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            List<tblUser> result = (List<tblUser>)queryResult.Result;
            foreach (tblUser user in result)
            {
                UserItemSource.Add(user);
            }
        }

    }

}
