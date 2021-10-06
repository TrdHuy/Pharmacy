using System;
using System.Collections.Generic;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Linq;
using Microsoft.Reporting.WinForms;
using System.IO;
using Pharmacy.Implement.Utils.CustomControls;
using System.Collections.ObjectModel;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Views.UserControls;
using System.Drawing.Printing;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage
{
    internal class MSW_MMP_PrintMedicineListButtonAction : MSW_MMP_ButtonAction
    {
        public MSW_MMP_PrintMedicineListButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
       
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            var lstMedicine = MMPViewModel.MedicineItemSource;

            var lstCaoDon = lstMedicine.Where(o => o.MedicineTypeID == 2).ToList();
            var lstDuocLieu = lstMedicine.Where(o => o.MedicineTypeID == 1).ToList();
            
            PrintInvoice(lstCaoDon, lstDuocLieu);
        }

        private bool PrintInvoice(List<tblMedicine> lstCaoDon, List<tblMedicine> lstDuocLieu)
        {
            try
            {
                PrintPreviewControl printPreview = new PrintPreviewControl();
                printPreview.Report.Reset();

                PopupScreenWindow.MVVM.Views.PopupScreenWindow popupWindow = new PopupScreenWindow.MVVM.Views.PopupScreenWindow();
                popupWindow.Height = 600d;
                popupWindow.Width = 800d;
                popupWindow.Content = printPreview;

                printPreview.Report.LocalReport.ReportPath = Path.GetFullPath(@"Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/MedicineListReport.rdlc");

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
                printPreview.Report.LocalReport.DataSources.Add(reportDataSource);

                ReportDataSource reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "DataSet2";
                reportDataSource2.Value = tblDuocLieu;
                printPreview.Report.LocalReport.DataSources.Add(reportDataSource2);

                printPreview.Report.SetDisplayMode(DisplayMode.PrintLayout);
                printPreview.Report.ZoomMode = ZoomMode.Percent;
                printPreview.Report.ZoomPercent = 100;
                printPreview.Report.RefreshReport();

                var pageSettings = new PageSettings();
                pageSettings.PaperSize = printPreview.Report.LocalReport.GetDefaultPageSettings().PaperSize;
                pageSettings.Landscape = printPreview.Report.LocalReport.GetDefaultPageSettings().IsLandscape;
                pageSettings.Margins = printPreview.Report.LocalReport.GetDefaultPageSettings().Margins;
                printPreview.Report.SetPageSettings(pageSettings);

                popupWindow.ShowDialog();
                //LocalReportExtensions.Print(report.LocalReport);
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