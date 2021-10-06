using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ShowMedicineInfo.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ShowMedicineInfo
{
    internal class ShowMedicineInfoPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ShowMedicineInfoPageViewModel");

        public ObservableCollection<tblWarehouseImportDetail> LstWarehouseImportDetail { get; set; }
        public ImageSource MedicineImageSource { get; set; }
        public tblMedicine MedicineInfo { get; set; }
        public MSW_MMP_SMIP_ButtonCommandOV ButtonCommandOV { get; set; }

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_MMP_SMIP_ButtonCommandOV(this);
            UpdateModifyData();
        }

        protected override void OnInitialized()
        {
        }

        private void UpdateModifyData()
        {
            MedicineInfo = MSW_DataFlowHost.Current.CurrentModifiedMedicine;
            GetWarehouseImportDetail();
            MedicineImageSource = FileIOUtil.
                GetBitmapFromName(MedicineInfo.MedicineID.ToString(), FileIOUtil.MEDICINE_IMAGE_FOLDER_NAME).
                ToImageSource();
        }

        private void GetWarehouseImportDetail()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstWarehouseImportDetail = new ObservableCollection<tblWarehouseImportDetail>(queryResult.Result as List<tblWarehouseImportDetail>);
                }
                else
                {
                    LstWarehouseImportDetail = new ObservableCollection<tblWarehouseImportDetail>();
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_STOCK_IN_WAREHOUSE_DATA_CMD_KEY
                    , _sqlCmdObserver
                    , MedicineInfo.MedicineID);
        }

    }

}
