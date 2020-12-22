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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    public class UserManagementPageViewModel : AbstractViewModel
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public ObservableCollection<UserItemViewModel> UserItemSource { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }

        public UserManagementPageViewModel() : base()
        {
            UserItemSource = new ObservableCollection<UserItemViewModel>();

            InstantiateItems();
        }

        private void InstantiateItems()
        {
            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_USER_TABLE_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            List<tblUser> result = (List<tblUser>)queryResult.Result;
            foreach (tblUser user in result)
            {
                UserItemViewModel uIVM = new UserItemViewModel(user);
                UserItemSource.Add(uIVM);
            }
        }
    }

    public class UserItemViewModel : AbstractViewModel
    {
        private tblUser _userInfo;
        private ImageSource _userAvatarSource;
        public RunInputCommand EditButtonCommand { get; set; }
        public RunInputCommand DeleteButtonCommand { get; set; }

        public string FullName
        {
            get
            {
                return _userInfo.FullName;
            }
            set
            {
                _userInfo.FullName = value;
                InvalidateOwn();
            }
        }
        public string Username
        {
            get
            {
                return _userInfo.Username;
            }
            set
            {
                _userInfo.Username = value;
                InvalidateOwn();
            }
        }
        public string Phone
        {
            get
            {
                return _userInfo.Phone;
            }
            set
            {
                _userInfo.Phone = value;
                InvalidateOwn();
            }
        }
        public string Job
        {
            get
            {
                return _userInfo.Job;
            }
            set
            {
                _userInfo.Job = value;
                InvalidateOwn();
            }
        }
    
        protected override void InitPropertiesRegistry()
        {
        }

        public UserItemViewModel(tblUser userInfo) : base()
        {
            _userInfo = (tblUser)userInfo;
            EditButtonCommand = new RunInputCommand(EditButtonClickEvent);
        }

        private void EditButtonClickEvent(object obj)
        {
            DataGrid ctrl = obj as DataGrid;

            int a = 1;
        }
    }
}
