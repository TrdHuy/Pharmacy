using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.UserManagement
{
    public class UpdateUserInfoAction : AbstractQueryAction
    {
        public UpdateUserInfoAction()
        {
            _action = UpdateUserInfo;
        }

        private SQLQueryResult UpdateUserInfo(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblUser modifiedUser = paramaters[0] as tblUser;
            string userNameBeforeChanged = paramaters[1] as string;
            string imageFolder = paramaters[2] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                var x = appDBContext.tblUsers.Where<tblUser>(user => user.Username.Equals(userNameBeforeChanged)).First();
                x.FullName = modifiedUser.FullName;
                x.Address = modifiedUser.Address;
                x.Phone = modifiedUser.Phone;
                x.Email = modifiedUser.Email;
                x.Link = modifiedUser.Link;
                x.Job = modifiedUser.Job;
                x.Password = modifiedUser.Password;

                if (!SaveImageToFile(modifiedUser.Username, imageFolder, ImageType.User))
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                    return result;
                }

                appDBContext.SaveChanges();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
            }

            return result;
        }

    }
}
