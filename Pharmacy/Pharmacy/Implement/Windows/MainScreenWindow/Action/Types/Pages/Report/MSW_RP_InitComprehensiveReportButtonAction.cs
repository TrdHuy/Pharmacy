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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.Report
{
    public class MSW_RP_InitComprehensiveReportButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private ReportPageViewModel _viewModel;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private ReportViewer _reportViewer;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as ReportPageViewModel;
            _reportViewer = dataTransfer[1] as ReportViewer;

            if (_viewModel.ComprehensiveReportEndDate == null
                || _viewModel.ComprehensiveReportStartDate == null
                || _viewModel.ComprehensiveReportEndDate < _viewModel.ComprehensiveReportStartDate)
            {
                MessageBox.Show("Kiểm tra lại ngày bắt đầu và kết thúc!");
                _viewModel.IsInitComprehensiveReportButtonRunning = false;
                return false;
            }

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.GET_ALL_ACTIVE_INFO_FOR_COMPREHENSIVE_REPORT_CMD_KEY
                , PharmacyDefinitions.GET_ALL_ACTIVE_INFO_FOR_COMPREHENSIVE_REPORT_DELAY_TIME
                , _sqlCmdObserver
                , _viewModel.ComprehensiveReportStartDate
                , _viewModel.ComprehensiveReportEndDate
                , _viewModel);
            return true;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                _reportViewer.Reset();

                _reportViewer.LocalReport.ReportPath = Path.GetFullPath(@"../../Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/ComprehensiveReport.rdlc");

                MSW_RP_ComprehensiveReportOV result = queryResult.Result as MSW_RP_ComprehensiveReportOV;

                ReportParameter[] reportParameters = new ReportParameter[9];
                reportParameters[0] = new ReportParameter("NgayBaoCao",
                    "Từ " + _viewModel.ComprehensiveReportStartDate.ToString("dd/MM/yyyy") + " đến " + _viewModel.ComprehensiveReportEndDate.ToString("dd/MM/yyyy"));
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
            _viewModel.IsInitComprehensiveReportButtonRunning = false;
        }
    }
}
