using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.PharmacyDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    class SQLResultHandler : Pharmacy.Base.Observable.ObserverPattern.IObservable<SQLQueryResult>
    {
        private const string DataConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\PharmacyDB.mdf;Integrated Security=True";

        private List<Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult>> _observers;
        
        private PharmacyDBDataSet _pharmacyAppDataSet;
        private tblUserTableAdapter _userDataTblApdapter;
        private SQLQueryResult _result;

        public SQLResultHandler()
        {
            _observers = new List<Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult>>();
            _pharmacyAppDataSet = new PharmacyDBDataSet();
            _userDataTblApdapter = new tblUserTableAdapter();
            RefreshDataset();
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
            NotifyChange();
        }

        public void Subcribe(Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unsubcribe(Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult> observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void NotifyChange()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_result);
            }
        }

        private SQLQueryResult CheckUserAvail(string[] paramaters)
        {
            string name = paramaters[0];
            string pass = paramaters[1];

            try
            {
                DataTable tblUser = _pharmacyAppDataSet.Tables["tblUser"];

                var x = tblUser.AsEnumerable().Where(user =>
                    user.Field<string>("UserName").Equals(name)
                    && user.Field<string>("UserPassword").Equals(pass));
                SQLQueryResult result = new SQLQueryResult(x,"");
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
            }
            return null;
        }

        private void RefreshDataset()
        {
            _userDataTblApdapter.Fill(_pharmacyAppDataSet.tblUser);
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
