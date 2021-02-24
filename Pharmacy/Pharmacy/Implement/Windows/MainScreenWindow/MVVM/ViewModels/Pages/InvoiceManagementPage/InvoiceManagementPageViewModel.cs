using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InvoiceManagementPage.OVs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InvoiceManagementPage
{
    public class InvoiceManagementPageViewModel : AbstractViewModel
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public ObservableCollection<CustomerOrderOV> CustomerOrdersItemSource { get; set; }
        public MSW_IMP_ButtonCommandOV ButtonCommandOV { get; set; }
        public CustomerOrderOV CurrentSelectedOrderOV { get; set; }

        public InvoiceManagementPageViewModel()
        {
            ButtonCommandOV = new MSW_IMP_ButtonCommandOV(this);

            InstantiateItems();
        }

        private void InstantiateItems()
        {
            _sqlCmdObserver = new SQLQueryCustodian(GetAllActiveCustomerOrdersCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_ORDERS_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void GetAllActiveCustomerOrdersCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                var list = queryResult.Result as IEnumerable<tblOrder>;
                CustomerOrdersItemSource = new ObservableCollection<CustomerOrderOV>();
                foreach (tblOrder order in list)
                {
                    CustomerOrdersItemSource.Add(new CustomerOrderOV(order));
                }
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi load dữ liệu hóa đơn, vui lòng mở lại ứng dụng hoặc liên hệ CSKH để biết thêm thông tin!",
                  HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                  HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                  OwnerWindow.MainScreen,
                  "Thông báo!");
            }
        }
    }
}
