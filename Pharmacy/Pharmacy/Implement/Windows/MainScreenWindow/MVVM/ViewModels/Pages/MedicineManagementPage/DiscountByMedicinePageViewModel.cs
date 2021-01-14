using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.Globalization;
using Pharmacy.Config;
using System.Windows.Threading;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage
{
    public class DiscountByMedicinePageViewModel : AbstractViewModel
    {
        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand CreateNewPromoButtonCommand { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand DeleteButtonCommand { get; set; }
        public ImageSource MedicineImageSource { get; set; }
        public tblMedicine MedicineInfo { get; set; }
        public ObservableCollection<tblPromo> LstPromo { get; set; }
        public List<tblCustomer> LstCustomer { get; set; }
        public string[] LstCustomerFilterPathList { get; set; } = new string[] { "CustomerName", "Phone" };
        public int SelectedCustomerCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int PromoPercentCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public bool IsSaveButtonCanPerform
        {
            get
            {
                return SelectedCustomerCheckingStatus == 1
                    && PromoPercentCheckingStatus == 1;
            }
        }
        public bool IsSaveButtonRunning
        {
            get { return _isSaveButtonRunning; }
            set
            {
                _isSaveButtonRunning = value;
                InvalidateOwn();
            }
        }
        public int SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                CheckSelectedCustomer();
                InvalidateOwn();
            }
        }
        public string PromoPercent
        {
            get { return _promoPercent; }
            set
            {
                _promoPercent = value;
                CheckPromoPercent();
                InvalidateOwn();
            }
        }
        public string PromoDescription
        {
            get { return _promoDescription; }
            set
            {
                _promoDescription = value;
                InvalidateOwn();
            }
        }

        private TimeSpan DELAY_TIME_TO_UPDATE_FILTER = TimeSpan.FromMilliseconds(500);
        private DispatcherTimer _timerToUpdateCustomerList;
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private int _selectedCustomer;
        private string _promoPercent = "";
        private string _promoDescription = "";
        private bool _isSaveButtonRunning;
        private List<tblCustomer> _lstCustomerFull;

        protected override void InitPropertiesRegistry()
        {
        }

        public DiscountByMedicinePageViewModel()
        {
            CancelButtonCommand = new RunInputCommand(CancelButtonClickEvent);
            CreateNewPromoButtonCommand = new RunInputCommand(CreateNewPromoButtonClickEvent);
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            DeleteButtonCommand = new RunInputCommand(DeleteButtonClickEvent);
            UpdateMedicineData();
            GetCustomerList();
        }

        public void RefreshPage()
        {
            GetPromoListByMedicine();
            CreateNewPromoButtonClickEvent(null);
        }

        private void GetCustomerList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstCustomer = queryResult.Result as List<tblCustomer>;
                    _lstCustomerFull = queryResult.Result as List<tblCustomer>;
                }
                else
                {
                    LstCustomer = new List<tblCustomer>();
                    _lstCustomerFull = new List<tblCustomer>();
                }
                SelectedCustomer = -1;
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void CreateNewPromoButtonClickEvent(object paramaters)
        {
            PromoDescription = "";
            PromoPercent = "";
            SelectedCustomer = -1;
        }

        private void DeleteButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_DELETE_BUTTON
                , dataTransfer);
        }

        private void SaveButtonClickEvent(object paramaters)
        {
            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_SAVE_BUTTON
                , dataTransfer);
        }

        private void UpdateMedicineData()
        {
            MedicineInfo = MSW_DataFlowHost.Current.CurrentModifiedMedicine;
            MedicineImageSource = FileIOUtil.
                GetBitmapFromName(MedicineInfo.MedicineID.ToString(), FileIOUtil.MEDICINE_IMAGE_FOLDER_NAME).
                ToImageSource();
            GetPromoListByMedicine();
        }

        private void GetPromoListByMedicine()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstPromo = new ObservableCollection<tblPromo>(queryResult.Result as List<tblPromo>);
                }
                else
                {
                    LstPromo = new ObservableCollection<tblPromo>();
                }
                Invalidate("LstPromo");
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_PROMO_BY_MEDICINE_CMD_KEY
                    , _sqlCmdObserver
                    , MedicineInfo.MedicineID);
        }

        private void CancelButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_CANCEL_BUTTON
                , dataTransfer);
        }

        private void CheckSelectedCustomer()
        {
            if (SelectedCustomer >= 0)
            {
                SelectedCustomerCheckingStatus = 1;
                if (LstPromo != null)
                {
                    var existedPromo = LstPromo.Where(o => o.CustomerID == LstCustomer[SelectedCustomer].CustomerID).FirstOrDefault();
                    if (existedPromo != null)
                    {
                        PromoPercent = existedPromo.PromoPercent.ToString();
                        PromoDescription = existedPromo.PromoDescription;
                    }
                }
                else
                {
                    PromoPercent = "";
                    PromoDescription = "";
                }
            }
            else
            {
                SelectedCustomerCheckingStatus = -1;
            }
            Invalidate("SelectedCustomerCheckingStatus");
        }

        private void CheckPromoPercent()
        {
            if (PromoPercent.Trim().Length > 0
              && PharmacyExtension.IsHavingOnlyNumber(PromoPercent.Trim())
              && int.Parse(PromoPercent.Trim()) >= 0)
                PromoPercentCheckingStatus = 1;
            else
                PromoPercentCheckingStatus = -1;
            Invalidate("PromoPercentCheckingStatus");
        }
    }

}
