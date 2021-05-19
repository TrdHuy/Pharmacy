using Pharmacy.Config;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerManagement.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerManagement
{
    internal class CustomerManagementPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("CustomerManagementPageViewModel");

        private SQLQueryCustodian _sqlCmdObserver;
        private string _searchText;
        private string _tip;
        private bool _isDataGridLoading;
        private ObservableCollection<tblCustomer> _source;

        public int DelayTextChangedHandler { get; set; }

        [Bindable(true)]
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

        public MSW_CMP_ButtonCommandOV ButtonCommandOV { get; set; }
        public MSW_CMP_EventCommandOV EventCommandOV { get; set; }

        [Bindable(true)]
        public bool IsDataGridLoading
        {
            get
            {
                return _isDataGridLoading;
            }
            set
            {
                _isDataGridLoading = value;
                InvalidateOwn();
            }
        }


        [Bindable(true)]
        public ObservableCollection<tblCustomer> CustomerItemSource
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                InvalidateOwn();
            }
        }


        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            InstantiateTipText();
            ButtonCommandOV = new MSW_CMP_ButtonCommandOV(this);
            EventCommandOV = new MSW_CMP_EventCommandOV(this);
            InstantiateItems();
        }

        protected override void OnInitialized()
        {
        }

        public override void OnUnloaded(object sender)
        {
            EventCommandOV.OnDestroy();
            base.OnUnloaded(sender);
        }

        public override void OnPreviewBindingDataContextInCache()
        {
            InstantiateItems();
            SearchText = "";
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
                CustomerItemSource = new ObservableCollection<tblCustomer>(result);
                if (CustomerItemSource.Count <= 5000)
                {
                    DelayTextChangedHandler = 1000;
                }
                else if (CustomerItemSource.Count <= 20000)
                {
                    DelayTextChangedHandler = 3000;
                }
                else
                {
                    DelayTextChangedHandler = 5000;
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
    }
}
