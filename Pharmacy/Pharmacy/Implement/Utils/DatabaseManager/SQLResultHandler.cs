using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.CustomerManagement;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.UserManagement;
using Pharmacy.Implement.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
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
                    _result = new CheckUserAvailAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.UPDATE_USER_INFO_CMD_KEY:
                    _result = new UpdateUserInfoAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_USER_DATA_CMD_KEY:
                    _result = new GetAllActiveUserDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_NON_ADMIN_USER_DATA_CMD_KEY:
                    _result = new GetAllNonAdminUserDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.CHECK_USER_NAME_EXISTED_CMD_KEY:
                    _result = new CheckUserNameExistedAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.SET_USER_DEACTIVE_CMD_KEY:
                    _result = new SetUserDeactiveAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.ADD_NEW_USER_CMD_KEY:
                    _result = new AddNewUserAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.ADD_NEW_CUSTOMER_CMD_KEY:
                    _result = new AddNewCustomerAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_CMD_KEY:
                    _result = new GetAllActiveCustomerDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.UPDATE_CUSTOMER_INFO_CMD_KEY:
                    _result = new UpdateCustomerInfoAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.SET_CUSTOMER_DEACTIVE_CMD_KEY:
                    _result = new SetCustomerDeactiveAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_DATA_CMD_KEY:
                    _result = new GetAllActiveMedicineDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_MEDICINE_TYPE_DATA_CMD_KEY:
                    _result = new GetAllMedicineTypeDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.SET_MEDICINE_DEACTIVE_CMD_KEY:
                    _result = new SetMedicineDeactiveAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_MEDICINE_UNIT_DATA_CMD_KEY:
                    _result = new GetAllMedicineUnitDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_SUPPLIER_DATA_CMD_KEY:
                    _result = new GetAllActiveSupplierDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.CHECK_MEDICINEID_EXISTED_CMD_KEY:
                    _result = new IsMedicineIDExistedAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.ADD_NEW_MEDICINE_CMD_KEY:
                    _result = new AddNewMedicineAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.MODIFY_MEDICINE_CMD_KEY:
                    _result = new ModifyMedicineAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_STOCK_IN_WAREHOUSE_DATA_CMD_KEY:
                    _result = new GetAllActivteMedicineStockInWarehouseDataAction().Execute(_appDBContext, paramaters);
                    break;
                default:
                    break;
            }

            if (_result.MesResult == MessageQueryResult.Aborted ||
                _result.MesResult == MessageQueryResult.Cancled)
            {
                RollBack();
            }

            NotifyChange(_result);
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

        //Key for getting info of all medicine in database
        public const string GET_ALL_ACTIVE_MEDICINE_DATA_CMD_KEY = "get_all_active_medicine_data";

        //Key for set a medicine deactive
        public const string SET_MEDICINE_DEACTIVE_CMD_KEY = "set_medicine_deactive";

        //Key for getting info of all medicine type in database
        public const string GET_ALL_MEDICINE_TYPE_DATA_CMD_KEY = "get_all_medicine_type_data";

        //Key for getting info of all medicine unit in database
        public const string GET_ALL_MEDICINE_UNIT_DATA_CMD_KEY = "get_all_medicine_unit_data";

        //Key for getting info of all supplier in database
        public const string GET_ALL_ACTIVE_SUPPLIER_DATA_CMD_KEY = "get_all_active_supplier_data";

        //Key for checking does MedicineID exist in database
        public const string CHECK_MEDICINEID_EXISTED_CMD_KEY = "check_medicineid_existed";

        //Key for add new medicine
        public const string ADD_NEW_MEDICINE_CMD_KEY = "add_new_medicine";

        //Key for modify medicine
        public const string MODIFY_MEDICINE_CMD_KEY = "modify_medicine";

        //Key for getting info of all active stocks in warehouse in database
        public const string GET_ALL_ACTIVE_MEDICINE_STOCK_IN_WAREHOUSE_DATA_CMD_KEY = "get_all_active_medicine_stock_in_warehouse_data";
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
