using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using System.IO;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.Report
{
    internal class MSW_RP_InitSellingReportButtonAction : MSW_RP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private ReportViewer _reportViewer;
        public MSW_RP_InitSellingReportButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            _reportViewer = DataTransfer[1] as ReportViewer;

            if (RPViewModel.SellingReportEndDate == null
                || RPViewModel.SellingReportStartDate == null
                || RPViewModel.SellingReportEndDate < RPViewModel.SellingReportStartDate)
            {
                MessageBox.Show("Kiểm tra lại ngày bắt đầu và kết thúc!");
                RPViewModel.ButtonCommandOV.IsInitSellingReportButtonRunning = false;
                return;
            }

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_ORDERS_BY_DATE_CMD_KEY
                , PharmacyDefinitions.GET_ALL_ACTIVE_CUSTOMER_ORDERS_BY_DATE_DELAY_TIME
                , _sqlCmdObserver
                , RPViewModel.SellingReportStartDate
                , RPViewModel.SellingReportEndDate);
            return;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                PharmacyDBDataSet.CustomerOrdersDataTable tbl = new PharmacyDBDataSet.CustomerOrdersDataTable();
                int index = 0;
                long orderIDNow = 0;
                decimal finalIncome = 0;
                decimal finalProfit = 0;
                foreach (tblOrder item in queryResult.Result as List<tblOrder>)
                {
                    foreach (var itemDetail in item.tblOrderDetails)
                    {
                        decimal totalPrice = itemDetail.TotalPrice;
                        decimal totalProfit = totalPrice - (decimal)itemDetail.Quantity * itemDetail.UnitBidPrice;

                        finalIncome += totalPrice;
                        finalProfit += totalProfit;

                        if (item.OrderID != orderIDNow)
                        {
                            index++;
                            orderIDNow = item.OrderID;
                        }

                        tbl.AddCustomerOrdersRow(index + "",
                            item.OrderTime.ToString("dd/MM/yyyy HH:mm"),
                            item.tblCustomer.CustomerName,
                            index + "",
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

                _reportViewer.LocalReport.ReportPath = Path.GetFullPath(@"Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/SellingReport.rdlc");

                ReportParameter[] reportParameters = new ReportParameter[3];
                reportParameters[0] = new ReportParameter("NgayBaoCao",
                    "Từ " + RPViewModel.SellingReportStartDate.ToString("dd/MM/yyyy") + " đến " + RPViewModel.SellingReportEndDate.ToString("dd/MM/yyyy"));
                reportParameters[1] = new ReportParameter("TongTien", finalIncome.ToString());
                reportParameters[2] = new ReportParameter("LoiNhuan", finalProfit.ToString());
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
            RPViewModel.ButtonCommandOV.IsInitSellingReportButtonRunning = false;
        }
    }
}
