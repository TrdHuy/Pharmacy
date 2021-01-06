using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Config;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage
{
    public class CustomerManagementPageViewModel : AbstractViewModel
    {
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private SQLQueryCustodian _sqlCmdObserver;
        private string _searchText;
        private string _tip;

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                InvalidateOwn();
            }
        }

        public string SearchTextBoxToolTip
        {
            get
            {

                return _tip;
            }
        }


        public RunInputCommand AddNewCustomerButtonCommand { get; set; }
        public RunInputCommand EditButtonCommand { get; set; }
        public RunInputCommand DeleteButtonCommand { get; set; }
        public RunInputCommand HistoryButtonCommand { get; set; }
        public EventHandleCommand SearchTextChangedCommand { get; set; }

        public ObservableCollection<tblCustomer> CustomerItemSource { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }

        public CustomerManagementPageViewModel()
        {
            InstantiateTipText();

            CustomerItemSource = new ObservableCollection<tblCustomer>();
            EditButtonCommand = new RunInputCommand(EditButtonClickEvent);
            DeleteButtonCommand = new RunInputCommand(DeleteButtonClickEvent);
            HistoryButtonCommand = new RunInputCommand(HistoryButtonClickEvent);
            AddNewCustomerButtonCommand = new RunInputCommand(AddNewUserButtonClickEvent);
            SearchTextChangedCommand = new EventHandleCommand(OnSearchTextChangedEvent);
            InstantiateItems();
        }

        private void InstantiateTipText()
        {
            _tip = "Hỗ trợ tìm kiếm theo:";
            _tip += RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_NAME ? " tên," : "";
            _tip += RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_PHONE ? " số điện thoại," : "";
            _tip += RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_EMAIL ? " email," : "";
            _tip += RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_ADDRESS ? " địa chỉ," : "";
            if (_tip.Last() == ',') _tip = _tip.Substring(0, _tip.Length - 1);

        }

        private void InstantiateItems()
        {
            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                List<tblCustomer> result = (List<tblCustomer>)queryResult.Result;
                foreach (tblCustomer cus in result)
                {
                    CustomerItemSource.Add(cus);
                }
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi load dữ liệu khách hàng, vui lòng mở lại ứng dụng hoặc liên hệ CSKH để biết thêm thông tin!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
            }

        }

        private void AddNewUserButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_ADD_BUTTON
                , dataTransfer);
        }


        private void HistoryButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_HISTORY_BUTTON
                , dataTransfer);
        }

        private void DeleteButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_DELETE_BUTTON
                , dataTransfer);
        }

        private void EditButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_EDIT_BUTTON
                , dataTransfer);
        }

        private void OnSearchTextChangedEvent(object sender, EventArgs e, object paramaters)
        {
            DataGrid dg = (DataGrid)((object[])paramaters)[0];
            TextBox ctrl = (TextBox)sender;

            dg.Items.Filter = new Predicate<object>(customer => FilterCustomerList(customer as tblCustomer, ctrl.Text));
        }

        private bool FilterCustomerList(tblCustomer customer, string filterText)
        {
            return SearchByName(customer, filterText) ||
                SearchByPhone(customer, filterText) ||
                SearchByEmail(customer, filterText) ||
                SearchByAddress(customer, filterText);
        }

        private bool SearchByName(tblCustomer customer, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_NAME ? (customer.CustomerName.IndexOf(filterText) != -1) : false;
        }
        private bool SearchByPhone(tblCustomer customer, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_PHONE ? (customer.Phone.IndexOf(filterText) != -1) : false;
        }
        private bool SearchByEmail(tblCustomer customer, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_EMAIL ? (customer.Email.IndexOf(filterText) != -1) : false;
        }
        private bool SearchByAddress(tblCustomer customer, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_ADDRESS ? (customer.Address.IndexOf(filterText) != -1) : false;
        }
    }
}
