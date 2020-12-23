using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using Pharmacy.Implement.Utils.Extensions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using System.Windows.Controls;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage
{
    public class UserManagementPageViewModel : AbstractViewModel
    {
        private IActionListener _keyActionListener = KeyActionListener.Instance;
        private SQLQueryCustodian _sqlCmdObserver;

        public ObservableCollection<tblUser> UserItemSource { get; set; }
        public RunInputCommand EditButtonCommand { get; set; }
        public RunInputCommand DeleteUserButtonCommand { get; set; }
        public RunInputCommand AddNewUserButtonCommand { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }

        public UserManagementPageViewModel()
        {
            UserItemSource = new ObservableCollection<tblUser>();
            EditButtonCommand = new RunInputCommand(EditButtonClickEvent);
            DeleteUserButtonCommand = new RunInputCommand(DeleteUserButtonClickEvent);
            AddNewUserButtonCommand = new RunInputCommand(AddNewUserButtonClickEvent);
            InstantiateItems();
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

        private void EditButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_UMP_EDIT_BUTTON
                , dataTransfer);
        }

        private void DeleteUserButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_UMP_DELETE_BUTTON
                , dataTransfer);
        }

        private void AddNewUserButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_UMP_ADD_BUTTON
                , dataTransfer);
        }
    }

}
