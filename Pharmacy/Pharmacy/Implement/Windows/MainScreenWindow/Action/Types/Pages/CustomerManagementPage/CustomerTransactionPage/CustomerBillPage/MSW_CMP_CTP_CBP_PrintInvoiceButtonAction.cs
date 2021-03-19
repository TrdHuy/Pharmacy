using Microsoft.Reporting.WinForms;
using Pharmacy.Implement.Utils.CustomControls;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    public class MSW_CMP_CTP_CBP_PrintInvoiceButtonAction : Base.UIEventHandler.Action.IAction
    {
        private CustomerBillPageViewModel _viewModel;
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerBillPageViewModel;

            if (_viewModel.IsOrderModified)
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

            return true;
        }

        private void PrintInvoice()
        {
            try
            {
                ReportViewer report = new ReportViewer();
                report.LocalReport.ReportPath = Path.GetFullPath(@"../../Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/SellingInvoice.rdlc");

                ReportParameter[] reportParameters = new ReportParameter[10];
                reportParameters[0] = new ReportParameter("NgayBaoCao", "Ngày xuất: " + _viewModel.CurrentCustomerOrder.OrderTime.Hour + ":" + _viewModel.CurrentCustomerOrder.OrderTime.Minute + " " + _viewModel.CurrentCustomerOrder.OrderTime.Day + "/" + _viewModel.CurrentCustomerOrder.OrderTime.Month + "/" + _viewModel.CurrentCustomerOrder.OrderTime.Year);
                reportParameters[1] = new ReportParameter("KhachHang", _viewModel.CurrentCustomerOrder.tblCustomer.CustomerName);
                reportParameters[2] = new ReportParameter("SDT", _viewModel.CurrentCustomerOrder.tblCustomer.Phone);
                reportParameters[3] = new ReportParameter("DiaChi", _viewModel.CurrentCustomerOrder.tblCustomer.Address);
                reportParameters[4] = new ReportParameter("ThanhTien", _viewModel.MedicineOV.MedicineCost.ToString());
                reportParameters[5] = new ReportParameter("CongNo", _viewModel.MedicineOV.DebtCost.ToString());
                reportParameters[6] = new ReportParameter("TongCong", _viewModel.MedicineOV.TotalCost.ToString());
                reportParameters[7] = new ReportParameter("DaTra", _viewModel.MedicineOV.PaidAmount.ToString());
                reportParameters[8] = new ReportParameter("ConLai", _viewModel.MedicineOV.RestAmount.ToString());
                reportParameters[9] = new ReportParameter("GhiChu", _viewModel.CurrentCustomerOrder.OrderDescription);
                report.LocalReport.SetParameters(reportParameters);


                PharmacyDBDataSet.InvoiceDetailListDataTable tbl = new PharmacyDBDataSet.InvoiceDetailListDataTable();

                int id = 1;
                foreach (var item in _viewModel.CurrentCustomerOrder.tblOrderDetails)
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
                App.Current.ShowApplicationMessageBox("Lỗi in hóa đơn!",
                                  HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                                  HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                                  OwnerWindow.MainScreen,
                                  "Lỗi!!");
            }
        }
    }
}
