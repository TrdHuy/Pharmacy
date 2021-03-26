using System;
using System.Collections.Generic;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Linq;
using Microsoft.Reporting.WinForms;
using System.IO;
using Pharmacy.Implement.Utils.CustomControls;
using System.Collections.ObjectModel;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage
{
    internal class MSW_MMP_PrintMedicineListButtonAction : MSW_MMP_ButtonAction
    {
        public MSW_MMP_PrintMedicineListButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
       
        public override void ExecuteCommand()
        {
            base.ExecuteCommand();
            var lstMedicine = DataTransfer[1] as ObservableCollection<tblMedicine>;

            var lstCaoDon = lstMedicine.Where(o => o.MedicineTypeID == 2).ToList();
            var lstDuocLieu = lstMedicine.Where(o => o.MedicineTypeID == 1).ToList();
            
            PrintInvoice(lstCaoDon, lstDuocLieu);
        }

        private bool PrintInvoice(List<tblMedicine> lstCaoDon, List<tblMedicine> lstDuocLieu)
        {
            try
            {
                ReportViewer report = new ReportViewer();
                report.LocalReport.ReportPath = Path.GetFullPath(@"Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/MedicineListReport.rdlc");

                PharmacyDBDataSet.MedicineInfoDataTable tblCaoDon = new PharmacyDBDataSet.MedicineInfoDataTable();
                PharmacyDBDataSet.MedicineInfoDataTable tblDuocLieu = new PharmacyDBDataSet.MedicineInfoDataTable();

                int id = 1;
                foreach (var item in lstCaoDon)
                {
                    tblCaoDon.AddMedicineInfoRow(item.MedicineID, item.MedicineName, item.tblMedicineUnit.MedicineUnitName, item.tblSupplier.SupplierName, item.BidPrice.ToString(), item.AskingPrice.ToString(), id++ + "");
                }

                id = 1;
                foreach (var item in lstDuocLieu)
                {
                    tblDuocLieu.AddMedicineInfoRow(item.MedicineID, item.MedicineName, item.tblMedicineUnit.MedicineUnitName, item.tblSupplier.SupplierName, item.BidPrice.ToString(), item.AskingPrice.ToString(), id++ + "");
                }

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = tblCaoDon;
                report.LocalReport.DataSources.Add(reportDataSource);

                ReportDataSource reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "DataSet2";
                reportDataSource2.Value = tblDuocLieu;
                report.LocalReport.DataSources.Add(reportDataSource2);

                report.SetDisplayMode(DisplayMode.PrintLayout);
                report.ZoomMode = ZoomMode.Percent;
                report.ZoomPercent = 100;
                report.RefreshReport();

                LocalReportExtensions.Print(report.LocalReport);
                return true;
            }
            catch (Exception ex)
            {
                App.Current.ShowApplicationMessageBox("Lỗi in báo cáo!",
                                  HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                                  HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                                  OwnerWindow.MainScreen,
                                  "Lỗi!!");
                return false;
            }
        }
    }
}