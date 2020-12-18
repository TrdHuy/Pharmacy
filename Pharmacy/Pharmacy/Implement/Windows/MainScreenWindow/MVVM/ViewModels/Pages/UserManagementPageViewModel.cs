using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using Pharmacy.Implement.Utils.Extensions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Implement.Models;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    public class UserManagementPageViewModel : AbstractViewModel
    {
        public ObservableCollection<UserItemViewModel> UserItemSource { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }

        public UserManagementPageViewModel() : base()
        {
            UserItemViewModel uIVM = new UserItemViewModel(App.Current.CurrentUser);
        }
    }

    public class UserItemViewModel : AbstractViewModel
    {
        private UserModel _userInfo;

        protected override void InitPropertiesRegistry()
        {
        }

        public UserItemViewModel(UserModel userInfo) : base()
        {
            _userInfo = (UserModel) userInfo.Clone();
        }
    }
}
