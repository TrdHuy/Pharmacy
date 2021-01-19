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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage
{
    public class ShowMedicineInfoPageViewModel : AbstractViewModel
    {
        public RunInputCommand CancelButtonCommand { get; set; }
        public ObservableCollection<tblWarehouseImportDetail> LstWarehouseImportDetail { get; set; }
        public ImageSource MedicineImageSource { get; set; }
        public tblMedicine MedicineInfo { get; set; }

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;

        protected override void InitPropertiesRegistry()
        {
        }

        public ShowMedicineInfoPageViewModel()
        {
            CancelButtonCommand = new RunInputCommand(CancelButtonClickEvent);
            UpdateModifyData();
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

        private void CancelButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_SMIP_CANCEL_BUTTON
                , dataTransfer);
        }
    }

}
