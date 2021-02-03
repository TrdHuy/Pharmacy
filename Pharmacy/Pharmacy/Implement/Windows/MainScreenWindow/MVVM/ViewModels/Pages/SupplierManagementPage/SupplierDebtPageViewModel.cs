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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage
{
    public class SupplierDebtPageViewModel : AbstractViewModel
    {
        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand PrintDebtButtonCommand { get; set; }
        public RunInputCommand ShowInvoiceButtonCommand { get; set; }
        public tblSupplier SupplierInfo { get; set; }
        public ObservableCollection<MSW_SMP_SupplierDebtOV> LstDebt { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal PurchasedDebt { get; set; }
        public decimal GrossDebt { get; set; }

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;

        protected override void InitPropertiesRegistry()
        {
        }

        public SupplierDebtPageViewModel()
        {
            CancelButtonCommand = new RunInputCommand(CancelButtonClickEvent);
            PrintDebtButtonCommand = new RunInputCommand(PrintDebtButtonClickEvent);
            ShowInvoiceButtonCommand = new RunInputCommand(ShowInvoiceButtonClickEvent);
            UpdateData();
        }

        private void UpdateData()
        {
            SupplierInfo = MSW_DataFlowHost.Current.CurrentModifiedSupplier;

            LstDebt = new ObservableCollection<MSW_SMP_SupplierDebtOV>();
            foreach (var item in SupplierInfo.tblWarehouseImports.Where(o => o.IsActive))
            {
                if (item.PurchasePrice - item.TotalPrice != 0)
                {
                    MSW_SMP_SupplierDebtOV debt = new MSW_SMP_SupplierDebtOV();
                    debt.ImportID = item.ImportID;
                    debt.ImportTime = item.ImportTime;
                    debt.PurchasedDebt = item.PurchasePrice - item.TotalPrice;
                    debt.DebtType = debt.PurchasedDebt > 0 ? "Trả" : "Nợ";
                    debt.Description = debt.Description;
                    LstDebt.Add(debt);
                }
            }

            TotalDebt = LstDebt.Where(o => o.DebtType == "Nợ").Sum(o => o.PurchasedDebt);
            PurchasedDebt = LstDebt.Where(o => o.DebtType == "Trả").Sum(o => o.PurchasedDebt);
            GrossDebt = TotalDebt - PurchasedDebt;
        }

        private void ShowInvoiceButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_SHOW_INVOICE_BUTTON
                , dataTransfer);
        }

        private void PrintDebtButtonClickEvent(object paramaters)
        {
            //object[] dataTransfer = new object[2];
            //dataTransfer[0] = this;
            //dataTransfer[1] = paramaters;
            //_keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
            //    , KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_CANCEL_BUTTON
            //    , dataTransfer);
        }

        private void CancelButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_CANCEL_BUTTON
                , dataTransfer);
        }
    }

}
