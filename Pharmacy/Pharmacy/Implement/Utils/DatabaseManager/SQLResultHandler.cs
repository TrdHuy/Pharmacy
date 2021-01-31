using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.CustomerManagement;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.Selling;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.SupplierManagement;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.UserManagement;
using Pharmacy.Implement.Utils.DatabaseManager.QueryAction.WarehouseManagement;
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

        public SQLResultHandler(PharmacyDBContext dBContext)
        {
            _observers = new List<Pharmacy.Base.Observable.ObserverPattern.IObserver<SQLQueryResult>>();
            _appDBContext = dBContext;
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

        public SQLQueryResult ExecuteQuery(string SQLCmdKey, params object[] paramaters)
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
                case SQLCommandKey.ADD_MODIFY_PROMO_CMD_KEY:
                    _result = new AddAndModifyPromoAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_PROMO_BY_MEDICINE_CMD_KEY:
                    _result = new GetAllActivePromoByMedicineDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.SET_PROMO_DEACTIVE_CMD_KEY:
                    _result = new SetPromoDeactiveAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.GET_ALL_ACTIVE_WAREHOUSE_IMPORT_DATA_CMD_KEY:
                    _result = new GetAllActiveWarehouseImportDataAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.SET_WAREHOUSE_IMPORT_DEACTIVE_CMD_KEY:
                    _result = new SetWarehouseImportDeactiveAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.ADD_NEW_CUSTOMER_ORDER_CMD_KEY:
                    _result = new AddNewCustomerOrderAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.ADD_NEW_CUSTOMER_ORDER_DEATAIL_CMD_KEY:
                    _result = new AddNewCustomerOrderDetailAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.UPDATE_CUSTOMER_ORDER_DEATAIL_CMD_KEY:
                    _result = new UpdateCustomerOrderAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.ADD_WAREHOUSE_IMPORT_CMD_KEY:
                    _result = new AddNewWarehouseImportAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.MODIFY_WAREHOUSE_IMPORT_CMD_KEY:
                    _result = new ModifyWarehouseImportAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.SET_SUPPLIER_DEACTIVE_CMD_KEY:
                    _result = new SetSupplierDeactiveAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.ADD_SUPPLIER_CMD_KEY:
                    _result = new AddNewSupplierAction().Execute(_appDBContext, paramaters);
                    break;
                case SQLCommandKey.MODIFY_SUPPLIER_CMD_KEY:
                    _result = new ModifySupplierAction().Execute(_appDBContext, paramaters);
                    break;
                default:
                    break;
            }

            if (_result.MesResult == MessageQueryResult.Aborted ||
                _result.MesResult == MessageQueryResult.Cancled)
            {
                RollBack();
            }

            return _result;
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

        //Key for getting active Promo list of medicine
        public const string GET_ALL_ACTIVE_PROMO_BY_MEDICINE_CMD_KEY = "get_all_active_promo_by_medicine_data";

        //Key for add and modify promo
        public const string ADD_MODIFY_PROMO_CMD_KEY = "add_modify_promo";

        //Key for set a promo deactive
        public const string SET_PROMO_DEACTIVE_CMD_KEY = "set_promo_deactive";

        //Key for getting info of all active stocks in warehouse in database
        public const string GET_ALL_ACTIVE_MEDICINE_STOCK_IN_WAREHOUSE_DATA_CMD_KEY = "get_all_active_medicine_stock_in_warehouse_data";

        //Key for getting info of all active warehouse import data in database
        public const string GET_ALL_ACTIVE_WAREHOUSE_IMPORT_DATA_CMD_KEY = "get_all_active_warehouse_import_data";

        //Key for set a warehouse import deactive
        public const string SET_WAREHOUSE_IMPORT_DEACTIVE_CMD_KEY = "set_warehouse_import_deactive";

        //Key for adding new customer order to database
        public const string ADD_NEW_CUSTOMER_ORDER_CMD_KEY = "add_new_customer_order";

        //Key for adding new customer order detail to database
        public const string ADD_NEW_CUSTOMER_ORDER_DEATAIL_CMD_KEY = "add_new_customer_order_detail";

        //Key for updating customer order detail to database
        public const string UPDATE_CUSTOMER_ORDER_DEATAIL_CMD_KEY = "update_customer_order_detail";

        //Key for add new warehouse import
        public const string ADD_WAREHOUSE_IMPORT_CMD_KEY = "add_warehouse_import";

        //Key for modify warehouse import
        public const string MODIFY_WAREHOUSE_IMPORT_CMD_KEY = "modify_warehouse_import";

        //Key for set a supplier deactive
        public const string SET_SUPPLIER_DEACTIVE_CMD_KEY = "set_supplier_deactive";

        //Key for add new supplier
        public const string ADD_SUPPLIER_CMD_KEY = "add_new_supplier";

        //Key for modify supplier
        public const string MODIFY_SUPPLIER_CMD_KEY = "modify_supplier";
    }

    public class SQLQueryResult
    {
        private object _result;
        private MessageQueryResult _mesResult;
        private string _messageToString;

        public SQLQueryResult(object result, MessageQueryResult mesResult, string messageToString = "")
        {
            _result = result;
            _mesResult = mesResult;
            _messageToString = messageToString;
        }

        public object Result
        {
            get { return _result; }
        }

        public MessageQueryResult MesResult
        {
            get { return _mesResult; }
        }

        public string Messsage
        {
            get { return _messageToString; }
        }
    }

    public enum MessageQueryResult
    {
        Non = 0x000000,

        // The task has done, but there is no result return
        OK = 0x000001,

        // Done the task, and return the result
        Done = 0x000010,

        // Finished the task, but return the null
        Finished = 0x000100,

        // The task was aborted
        Aborted = 0x001000,

        // The task was cancled
        Cancled = 0x010000
    }
}
