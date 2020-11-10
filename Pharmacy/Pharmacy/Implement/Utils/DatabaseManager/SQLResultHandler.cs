using Pharmacy.Base.Observable.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async void ExecuteQueryAsync(string SQLCmdKey, params string[] paramaters)
        {

            _result = null;
            switch (SQLCmdKey)
            {
                case SQLCommandKey.CHECK_USER_AVAIL_CMD_KEY:
                    _result = CheckUserAvail(paramaters);
                    break;
                default:
                    break;
            }
            NotifyChange(_result);
        }

        private SQLQueryResult CheckUserAvail(string[] paramaters)
        {
            string name = paramaters[0];
            string pass = paramaters[1];

            try
            {
                var x = _appDBContext.Users.Where(user => user.UserName.Equals(name)
                && user.UserPassword.Equals(pass)).ToList();

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
