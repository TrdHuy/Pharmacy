using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Linq;
using Pharmacy.Config;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.AddMedicine.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.DiscountByMedicine
{
    internal class DiscountByMedicinePageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("DiscountByMedicinePageViewModel");

        public MSW_MMP_DBMP_ButtonCommandOV ButtonCommandOV { get; set; }
        public ImageSource MedicineImageSource { get; set; }
        public tblMedicine MedicineInfo { get; set; }
        public ObservableCollection<tblPromo> LstPromo { get; set; }
        public List<tblCustomer> LstCustomer { get; set; }
        public string[] LstCustomerFilterPathList { get; set; }
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
        public double PromoPercent
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

        private int _selectedCustomer;
        private double _promoPercent;
        private string _promoDescription;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_MMP_DBMP_ButtonCommandOV(this);
            UpdateMedicineData();
            GetCustomerList();
            GetFilterList();
            PromoDescription = "";
            PromoPercent = 0;
        }

        protected override void OnInitialized()
        {
        }

        private void GetFilterList()
        {
            LstCustomerFilterPathList = new string[]
            {
                RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_NAME?"CustomerName":"",
                RUNE.IS_SUPPORT_SEARCH_CUSTOMER_BY_PHONE?"Phone":""
            };
        }

        public void RefreshPage()
        {
            GetPromoListByMedicine();
            PromoDescription = "";
            PromoPercent = 0;
            SelectedCustomer = -1;
        }

        private void GetCustomerList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstCustomer = queryResult.Result as List<tblCustomer>;
                }
                else
                {
                    LstCustomer = new List<tblCustomer>();
                }
                SelectedCustomer = -1;
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_CMD_KEY
                    , _sqlCmdObserver);
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
                        PromoPercent = existedPromo.PromoPercent;
                        PromoDescription = existedPromo.PromoDescription;
                    }
                }
                else
                {
                    PromoPercent = 0;
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
            if (PromoPercent >= 0)
                PromoPercentCheckingStatus = 1;
            else
                PromoPercentCheckingStatus = -1;
            Invalidate("PromoPercentCheckingStatus");
        }
    }

}
