using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions.Entities;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.Report
{
    public class MSW_RP_InitSellingReportButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private ReportPageViewModel _viewModel;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private ReportViewer _reportViewer;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as ReportPageViewModel;
            _reportViewer = dataTransfer[1] as ReportViewer;

            if (_viewModel.SellingReportEndDate == null
                || _viewModel.SellingReportStartDate == null
                || _viewModel.SellingReportEndDate < _viewModel.SellingReportStartDate)
            {
                MessageBox.Show("Kiểm tra lại ngày bắt đầu và kết thúc!");
                _viewModel.IsInitSellingReportButtonRunning = false;
                return false;
            }

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_ORDERS_BY_DATE_CMD_KEY
                    , _sqlCmdObserver
                    , _viewModel.SellingReportStartDate
                    , _viewModel.SellingReportEndDate);
            return true;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                PharmacyDBDataSet.CustomerOrdersDataTable tbl = new PharmacyDBDataSet.CustomerOrdersDataTable();
                int index = 0;
                foreach (tblOrder item in queryResult.Result as List<tblOrder>)
                {
                    foreach (var itemDetail in item.tblOrderDetails)
                    {
                        decimal totalPrice = ((decimal)itemDetail.Quantity * itemDetail.tblMedicine.AskingPrice * (decimal)(100 - itemDetail.PromoPercent) / 100);
                        decimal totalProfit = totalPrice - (decimal)itemDetail.Quantity * itemDetail.tblMedicine.BidPrice;

                        tbl.AddCustomerOrdersRow(item.OrderID + "",
                            item.OrderTime.ToString("dd/MM/yyyy HH:mm"),
                            item.tblCustomer.CustomerName,
                            ++index + "",
                            itemDetail.MedicineID,
                            itemDetail.tblMedicine.MedicineName,
                            itemDetail.tblMedicine.tblMedicineUnit.MedicineUnitName,
                            itemDetail.Quantity + "",
                            itemDetail.tblMedicine.AskingPrice + "",
                            itemDetail.PromoPercent + "",
                            totalPrice + "",
                            totalProfit + "");
                    }
                }

                _reportViewer.Reset();

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = tbl;
                _reportViewer.LocalReport.DataSources.Add(reportDataSource);

                _reportViewer.LocalReport.ReportPath = Path.GetFullPath(@"../../Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/SellingReport.rdlc");

                ReportParameter[] reportParameters = new ReportParameter[1];
                reportParameters[0] = new ReportParameter("NgayBaoCao",
                    "Từ " + _viewModel.SellingReportStartDate.ToString("dd/MM/yyyy") + " đến " + _viewModel.SellingReportEndDate.ToString("dd/MM/yyyy"));
                _reportViewer.LocalReport.SetParameters(reportParameters);

                _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                _reportViewer.ZoomMode = ZoomMode.Percent;
                _reportViewer.ZoomPercent = 100;
                _reportViewer.RefreshReport();
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi khởi tạo báo cáo!");
            }
            _viewModel.IsInitSellingReportButtonRunning = false;
        }
    }
}
