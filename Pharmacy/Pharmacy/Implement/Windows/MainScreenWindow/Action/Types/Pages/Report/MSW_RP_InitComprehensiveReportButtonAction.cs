using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using System.Windows;
using Microsoft.Reporting.WinForms;
using System.IO;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.Report
{
    internal class MSW_RP_InitComprehensiveReportButtonAction : MSW_RP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private ReportViewer _reportViewer;
        public MSW_RP_InitComprehensiveReportButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
            base.ExecuteCommand();

            _reportViewer = DataTransfer[1] as ReportViewer;

            if (RPViewModel.ComprehensiveReportEndDate == null
                || RPViewModel.ComprehensiveReportStartDate == null
                || RPViewModel.ComprehensiveReportEndDate < RPViewModel.ComprehensiveReportStartDate)
            {
                MessageBox.Show("Kiểm tra lại ngày bắt đầu và kết thúc!");
                RPViewModel.ButtonCommandOV.IsInitComprehensiveReportButtonRunning = false;
                return;
            }

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.GET_ALL_ACTIVE_INFO_FOR_COMPREHENSIVE_REPORT_CMD_KEY
                , PharmacyDefinitions.GET_ALL_ACTIVE_INFO_FOR_COMPREHENSIVE_REPORT_DELAY_TIME
                , _sqlCmdObserver
                , RPViewModel.ComprehensiveReportStartDate
                , RPViewModel.ComprehensiveReportEndDate
                , RPViewModel);
            return;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                _reportViewer.Reset();

                _reportViewer.LocalReport.ReportPath = Path.GetFullPath(@"Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/ComprehensiveReport.rdlc");

                MSW_RP_ComprehensiveReportOV result = queryResult.Result as MSW_RP_ComprehensiveReportOV;

                ReportParameter[] reportParameters = new ReportParameter[9];
                reportParameters[0] = new ReportParameter("NgayBaoCao",
                    "Từ " + RPViewModel.ComprehensiveReportStartDate.ToString("dd/MM/yyyy") + " đến " + RPViewModel.ComprehensiveReportEndDate.ToString("dd/MM/yyyy"));
                reportParameters[1] = new ReportParameter("TongGiaTriNhap", result.TongGiaTriNhap.ToString());
                reportParameters[2] = new ReportParameter("TongGiaTriXuat", result.TongGiaTriXuat.ToString());
                reportParameters[3] = new ReportParameter("TongNoKH", result.TongNoKH.ToString());
                reportParameters[4] = new ReportParameter("TongNoNCC", result.TongNoNCC.ToString());
                reportParameters[5] = new ReportParameter("ThuBanHang", result.ThuBanHang.ToString());
                reportParameters[6] = new ReportParameter("ThuKhac", result.ThuKhac.ToString());
                reportParameters[7] = new ReportParameter("TongChi", result.TongChi.ToString());
                reportParameters[8] = new ReportParameter("LaiGop", result.LaiGop.ToString());
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
            RPViewModel.ButtonCommandOV.IsInitComprehensiveReportButtonRunning = false;
        }
    }
}
