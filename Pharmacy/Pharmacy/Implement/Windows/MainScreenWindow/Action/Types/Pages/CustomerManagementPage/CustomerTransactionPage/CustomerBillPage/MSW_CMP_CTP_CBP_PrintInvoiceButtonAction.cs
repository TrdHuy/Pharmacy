using Microsoft.Reporting.WinForms;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.CustomControls;
using System;
using System.IO;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    internal class MSW_CMP_CTP_CBP_PrintInvoiceButtonAction : MSW_CMP_CTP_CBP_ButtonAction
    {
        public MSW_CMP_CTP_CBP_PrintInvoiceButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
            if (CBPViewModel.IsOrderModified)
            {
                var x = App.Current.ShowApplicationMessageBox("Vui lòng lưu các thay đổi hoặc hoàn tác trước khi xuất hóa đơn!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
            }
            else
            {
                PrintInvoice();
            }

            return;
        }

        private void PrintInvoice()
        {
            try
            {
                ReportViewer report = new ReportViewer();
                report.LocalReport.ReportPath = Path.GetFullPath(@"Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/SellingInvoice.rdlc");

                ReportParameter[] reportParameters = new ReportParameter[10];
                reportParameters[0] = new ReportParameter("NgayBaoCao", "Ngày xuất: " + CBPViewModel.CurrentCustomerOrder.OrderTime.Hour + ":" + CBPViewModel.CurrentCustomerOrder.OrderTime.Minute + " " + CBPViewModel.CurrentCustomerOrder.OrderTime.Day + "/" + CBPViewModel.CurrentCustomerOrder.OrderTime.Month + "/" + CBPViewModel.CurrentCustomerOrder.OrderTime.Year);
                reportParameters[1] = new ReportParameter("KhachHang", CBPViewModel.CurrentCustomerOrder.tblCustomer.CustomerName);
                reportParameters[2] = new ReportParameter("SDT", CBPViewModel.CurrentCustomerOrder.tblCustomer.Phone);
                reportParameters[3] = new ReportParameter("DiaChi", CBPViewModel.CurrentCustomerOrder.tblCustomer.Address);
                reportParameters[4] = new ReportParameter("ThanhTien", CBPViewModel.MedicineOV.MedicineCost.ToString());
                reportParameters[5] = new ReportParameter("CongNo", CBPViewModel.MedicineOV.DebtCost.ToString());
                reportParameters[6] = new ReportParameter("TongCong", CBPViewModel.MedicineOV.TotalCost.ToString());
                reportParameters[7] = new ReportParameter("DaTra", CBPViewModel.MedicineOV.PaidAmount.ToString());
                reportParameters[8] = new ReportParameter("ConLai", CBPViewModel.MedicineOV.RestAmount.ToString());
                reportParameters[9] = new ReportParameter("GhiChu", CBPViewModel.CurrentCustomerOrder.OrderDescription);
                report.LocalReport.SetParameters(reportParameters);


                PharmacyDBDataSet.InvoiceDetailListDataTable tbl = new PharmacyDBDataSet.InvoiceDetailListDataTable();

                int id = 1;
                foreach (var item in CBPViewModel.CurrentCustomerOrder.tblOrderDetails)
                {
                    tbl.AddInvoiceDetailListRow(id++.ToString(), item.tblMedicine.MedicineName, item.tblMedicine.tblMedicineUnit.MedicineUnitName,
                        item.Quantity.ToString(), item.UnitPrice.ToString(), item.PromoPercent.ToString(), item.TotalPrice.ToString());
                }

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = tbl;
                report.LocalReport.DataSources.Add(reportDataSource);

                report.SetDisplayMode(DisplayMode.PrintLayout);
                report.ZoomMode = ZoomMode.Percent;
                report.ZoomPercent = 100;
                report.RefreshReport();

                LocalReportExtensions.Print(report.LocalReport);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                App.Current.ShowApplicationMessageBox("Lỗi in hóa đơn! " + ex.Message,
                                  HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                                  HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                                  OwnerWindow.MainScreen,
                                  "Lỗi!!");
            }
        }
    }
}
