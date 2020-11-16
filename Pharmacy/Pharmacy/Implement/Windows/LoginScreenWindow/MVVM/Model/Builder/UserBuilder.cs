using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.Model.Builder
{
    class UserBuilder
    {
        private UserModel _userModel;

        public UserBuilder()
        {
            _userModel = new UserModel();
        }

        public UserBuilder BuildUserLoginName(object val)
        {
            _userModel[UserModel.KEY_USER_LOGIN_NAME] = val;
            return this;
        }

        public UserBuilder BuildUserLoginPassword(object val)
        {
            _userModel[UserModel.KEY_USER_LOGIN_PASSWORD] = val;
            return this;
        }

        public UserBuilder BuildUserRealName(object val)
        {
            _userModel[UserModel.KEY_USER_REAL_NAME] = val;
            return this;
        }

        public UserBuilder BuildUserDisplayedName(object val)
        {
            _userModel[UserModel.KEY_USER_DISPLAYED_NAME] = val;
            return this;
        }

        public UserBuilder BuildUserDOB(object val)
        {
            _userModel[UserModel.KEY_USER_DOB] = val;
            return this;
        }

        public UserBuilder BuildUserEducationLevel(object val)
        {
            _userModel[UserModel.KEY_USER_EDUCATION_LEVEL] = val;
            return this;
        }

        public UserBuilder BuildUserAddress(object val)
        {
            _userModel[UserModel.KEY_USER_ADDRESS] = val;
            return this;
        }

        public UserBuilder BuildUserPhone(object val)
        {
            _userModel[UserModel.KEY_USER_PHONE] = val;
            return this;
        }

        public UserBuilder BuildUserEmail(object val)
        {
            _userModel[UserModel.KEY_USER_EMAIL] = val;
            return this;
        }

        public UserBuilder BuildUserAccessLevel(object val)
        {
            _userModel[UserModel.KEY_USER_ACCESS_LEVEL] = val;
            return this;
        }

        public UserModel Build()
        {
            return _userModel;
        }
    }
}
