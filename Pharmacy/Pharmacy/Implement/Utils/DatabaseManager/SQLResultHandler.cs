using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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

        public void ExecuteQueryAsync(string SQLCmdKey, params object[] paramaters)
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
                default:
                    break;
            }
            NotifyChange(_result);
        }

        private SQLQueryResult UpdateUserInfo(object[] paramaters)
        {
            tblUser modifiedUser = paramaters[0] as tblUser;
            tblUser curUser = paramaters[1] as tblUser;
            SQLQueryResult result = new SQLQueryResult(null, "");

            try
            {
                var x = _appDBContext.tblUsers.Where<tblUser>(user => user.Username.Equals(curUser.Username)).First();
                x.FullName = modifiedUser.FullName;
                x.Address = modifiedUser.Address;
                x.Phone = modifiedUser.Phone;
                x.Email = modifiedUser.Email;
                x.Link = modifiedUser.Link;
                x.Password = modifiedUser.Password;
                _appDBContext.SaveChanges();
                result = new SQLQueryResult(x, "");
            }
            catch (Exception e)
            {

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

                SQLQueryResult result = new SQLQueryResult(x,"");
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

    }

    internal class SQLCommandKey
    {
        public const string CHECK_USER_AVAIL_CMD_KEY = "check_user_avail";
        public const string UPDATE_USER_INFO_CMD_KEY = "update_user_info";

    }

    public class SQLQueryResult
    {
        private object _result;
        private string _sqlCmd;

        public SQLQueryResult(object result, string cmd)
        {
            _result = result;
            _sqlCmd = cmd;
        }

        public object Result
        {
            get { return _result; }
        }

        public string SQLCommand
        {
            get { return _sqlCmd; }
        }
    }
}
