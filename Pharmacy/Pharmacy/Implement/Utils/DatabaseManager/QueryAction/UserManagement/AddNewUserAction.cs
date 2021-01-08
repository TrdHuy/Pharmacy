using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.UserManagement
{
    public class AddNewUserAction : AbstractQueryAction
    {
        public AddNewUserAction()
        {
            _action = AddNewUser;
        }

        private SQLQueryResult AddNewUser(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblUser newUser = paramaters[0] as tblUser;
            string imageFolder = paramaters[1] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                appDBContext.tblUsers.Add(newUser);

                if (!SaveImageToFile(newUser.Username, imageFolder, ImageType.User))
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                    return result;
                }

                appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
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