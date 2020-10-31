using Pharmacy.Base.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.Model
{
    class UserModel : AbstractBaseModel
    {
        public const string KEY_USER_LOGIN_NAME = "user_login_name";
        public const string KEY_USER_LOGIN_PASSWORD = "user_login_password";

        public const string KEY_USER_REAL_NAME = "user_real_name";
        public const string KEY_USER_DISPLAYED_NAME = "user_displayed_name";
        public const string KEY_USER_DOB = "user_dob";
        public const string KEY_USER_EDUCATION_LEVEL = "user_education_level";
        public const string KEY_USER_ADDRESS = "user_address";
        public const string KEY_USER_PHONE = "user_phone";
        public const string KEY_USER_EMAIL = "user_email";
        
    }
}
