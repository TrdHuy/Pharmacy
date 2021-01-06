using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    class SQLResultHandler : BaseObservable<SQLQueryResult>
    {
        private const string DataConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\PharmacyDB.mdf;Integrated Security=True";

        private List<Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult>> _observers;

        private PharmacyDBContext _appDBContext;
        private SQLQueryResult _result;

        public SQLResultHandler()
        {
            _observers = new List<Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult>>();
            _appDBContext = new PharmacyDBContext();
        }

        public void RollBack()
        {
            var changedEntries = _appDBContext.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        internal void Dispose()
        {
            _appDBContext.Dispose();
        }

        public void ExecuteQuery(string SQLCmdKey, params object[] paramaters)
        {

            _result = null;
            switch (SQLCmdKey)
            {
                case SQLCommandKey.CHECK_USER_AVAIL_CMD_KEY:
                    _result = CheckUserAvail(paramaters);
                    break;
                case SQLCommandKey.UPDATE_USER_INFO_CMD_KEY:
                    _result = UpdateUserInfo(paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_USER_DATA_CMD_KEY:
                    _result = GetAllActiveUserData(paramaters);
                    break;
                case SQLCommandKey.GET_ALL_NON_ADMIN_USER_DATA_CMD_KEY:
                    _result = GetAllNonAdminUserData(paramaters);
                    break;
                case SQLCommandKey.CHECK_USER_NAME_EXISTED_CMD_KEY:
                    _result = CheckUserNameExisted(paramaters);
                    break;
                case SQLCommandKey.SET_USER_DEACTIVE_CMD_KEY:
                    _result = SetUserDeactive(paramaters);
                    break;
                case SQLCommandKey.ADD_NEW_USER_CMD_KEY:
                    _result = AddNewUser(paramaters);
                    break;
                case SQLCommandKey.ADD_NEW_CUSTOMER_CMD_KEY:
                    _result = AddNewCustomer(paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_CMD_KEY:
                    _result = GetAllActiveCustomerData(paramaters);
                    break;
                case SQLCommandKey.UPDATE_CUSTOMER_INFO_CMD_KEY:
                    _result = UpdateCustomerInfo(paramaters);
                    break;
                case SQLCommandKey.SET_CUSTOMER_DEACTIVE_CMD_KEY:
                    _result = SetCustomerDeactive(paramaters);
                    break;
                default:
                    break;
            }
            NotifyChange(_result);
        }

        private SQLQueryResult SetCustomerDeactive(object[] paramaters)
        {
            int cusID = Convert.ToInt32(paramaters[0]);
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = _appDBContext.tblCustomers.Where(cus => cus.CustomerID == cusID).
                    First();
                x.IsActive = false;
                _appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message);
            }
            finally
            {
            }
            return result;
        }

        private SQLQueryResult UpdateCustomerInfo(object[] paramaters)
        {
            tblCustomer modifiedCustomer = paramaters[0] as tblCustomer;
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                var x = _appDBContext.tblCustomers.Where<tblCustomer>(cus => cus.CustomerID == modifiedCustomer.CustomerID).First();
                x.CustomerName = modifiedCustomer.CustomerName;
                x.Address = modifiedCustomer.Address;
                x.Phone = modifiedCustomer.Phone;
                x.Email = modifiedCustomer.Email;
                x.CustomerDescription = modifiedCustomer.CustomerDescription;
                _appDBContext.SaveChanges();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }

        private SQLQueryResult GetAllActiveCustomerData(object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = _appDBContext.tblCustomers.
                    Where<tblCustomer>(cus => cus.IsActive).
                    ToList();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (Exception e)
            {
                //Print debug and user log here
            }
            return result;
        }

        private SQLQueryResult AddNewCustomer(object[] paramaters)
        {
            tblCustomer newCustomer = paramaters[0] as tblCustomer;
            string imageFolder = paramaters[1] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                _appDBContext.tblCustomers.Add(newCustomer);

                if (!String.IsNullOrEmpty(imageFolder))
                {
                    try
                    {
                        string file = (_appDBContext.tblCustomers.
                            ToList().
                            Count + 1).ToString();
                        Bitmap cusBit = (Bitmap)Image.FromFile(imageFolder);
                        FileIOUtil.SaveCustomerImageFile(file, cusBit);
                    }
                    catch
                    {
                        App.Current.ShowApplicationMessageBox("Lỗi thêm ảnh đại diện của khách hàng, vui lòng kiểm tra lại!",
                            HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                            HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                            OwnerWindow.MainScreen,
                            "Lỗi!");
                        RollBack();
                        return result;
                    }

                }

                _appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            return result;
        }

        private SQLQueryResult AddNewUser(object[] paramaters)
        {
            tblUser newUser = paramaters[0] as tblUser;
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                _appDBContext.tblUsers.Add(newUser);
                _appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            return result;
        }

        private SQLQueryResult SetUserDeactive(object[] paramaters)
        {
            string name = paramaters[0].ToString();
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = _appDBContext.tblUsers.Where(user => user.Username.Equals(name)).
                    First();
                x.IsActive = false;
                _appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message);
            }
            finally
            {
            }
            return result;
        }

        private SQLQueryResult CheckUserNameExisted(object[] paramaters)
        {
            string name = paramaters[0].ToString();
            try
            {
                var x = _appDBContext.tblUsers.Where(user => user.Username.Equals(name)).
                    ToList();
                bool IsExisted = x.Count > 0;
                SQLQueryResult result = new SQLQueryResult(IsExisted, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        private SQLQueryResult GetAllNonAdminUserData(object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = _appDBContext.tblUsers.
                    Where<tblUser>(user => !user.IsAdmin).
                    ToList();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (Exception e)
            {
                //Print debug and user log here
            }
            return result;
        }

        private SQLQueryResult GetAllActiveUserData(object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = _appDBContext.tblUsers.
                    Where<tblUser>(user => user.IsActive).
                    ToList();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (Exception e)
            {
                //Print debug and user log here
            }
            return result;
        }

        private SQLQueryResult UpdateUserInfo(object[] paramaters)
        {
            tblUser modifiedUser = paramaters[0] as tblUser;
            string userNameBeforeChanged = paramaters[1] as string;
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                var x = _appDBContext.tblUsers.Where<tblUser>(user => user.Username.Equals(userNameBeforeChanged)).First();
                x.FullName = modifiedUser.FullName;
                x.Address = modifiedUser.Address;
                x.Phone = modifiedUser.Phone;
                x.Email = modifiedUser.Email;
                x.Link = modifiedUser.Link;
                x.Job = modifiedUser.Job;
                x.Password = modifiedUser.Password;
                _appDBContext.SaveChanges();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            return result;
        }

        private SQLQueryResult CheckUserAvail(object[] paramaters)
        {
            string name = paramaters[0].ToString();
            string pass = paramaters[1].ToString();

            try
            {
                var x = _appDBContext.tblUsers.Where(user => user.Username.Equals(name)
                && user.Password.Equals(pass)).ToList();

                SQLQueryResult result = new SQLQueryResult(x, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        private void HandleDbEntityValidationException(DbEntityValidationException e)
        {
            //Should implement log writer here for debug purpose
            foreach (var eve in e.EntityValidationErrors)
            {
                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
        }


    }

    internal class SQLCommandKey
    {

        // Key for checking a user avail or not
        public const string CHECK_USER_AVAIL_CMD_KEY = "check_user_avail";

        //Key for updating a user info in database
        public const string UPDATE_USER_INFO_CMD_KEY = "update_user_info";

        //Key for getting info of all user in database
        public const string GET_ALL_ACTIVE_USER_DATA_CMD_KEY = "get_all_active_user_data";

        //Key for getting info of all employee in database
        public const string GET_ALL_EMPLOYEE_DATA_CMD_KEY = "get_all_employee_data";

        //Key for getting info of all non admin user in database
        public const string GET_ALL_NON_ADMIN_USER_DATA_CMD_KEY = "get_all_non_admin_data";

        //Key for checking a user name is existed or not
        public const string CHECK_USER_NAME_EXISTED_CMD_KEY = "check_username_existed";

        //Key for set a user deactive
        public const string SET_USER_DEACTIVE_CMD_KEY = "set_user_deactive";

        //Key for add new user
        public const string ADD_NEW_USER_CMD_KEY = "add_new_user";

        //Key for add new customer
        public const string ADD_NEW_CUSTOMER_CMD_KEY = "add_new_customer";

        //Key for get all active customer
        public const string GET_ALL_ACTIVE_CUSTOMER_CMD_KEY = "get_all_active_customer";

        //Key for updating a customer info in database
        public const string UPDATE_CUSTOMER_INFO_CMD_KEY = "update_customer_info";

        //Key for set a customer deactive
        public const string SET_CUSTOMER_DEACTIVE_CMD_KEY = "set_customer_deactive";

    }

    public class SQLQueryResult
    {
        private object _result;
        private MessageQueryResult _mesResult;

        public SQLQueryResult(object result, MessageQueryResult mesResult)
        {
            _result = result;
            _mesResult = mesResult;
        }

        public object Result
        {
            get { return _result; }
        }

        public MessageQueryResult MesResult
        {
            get { return _mesResult; }
        }
    }

    public enum MessageQueryResult
    {
        Non = 0,

        // The task has done, but there is no result return
        OK = 1,

        // Done the task, and return the result
        Done = 2,

        // Finished the task, but return the null
        Finished = 4,

        // The task was aborted
        Aborted = 5,

        // The task was cancled
        Cancled = 6
    }
}
