using Microsoft.Reporting.WinForms;
using Pharmacy.Implement.Utils.CustomControls;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage;
using System;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.IO;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    internal class MSW_SP_InstantiateNewOrderAction : MSW_SP_ButtonAction
    {
        private tblOrder _newOrder;
        private SQLQueryCustodian _createNewOrderQueryObserver;
        private decimal _previousDebt;

        public MSW_SP_InstantiateNewOrderAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            if (!CanExecute())
            {
                SPViewModel.ButtonCommandOV.IsInstantiateNewOrderButtonRunning = false;
                return;
            }
            GenerateOrder();

            return;
        }

        private bool CanExecute()
        {
            if (SPViewModel.CustomerOV.CurrentSelectedCustomer == null)
            {
                var x = App.Current.ShowApplicationMessageBox("Vui lòng chọn khách hàng!",
                  HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                  HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                  OwnerWindow.MainScreen,
                  "Thông báo!!");
                return false;
            }
            if (SPViewModel.CustomerOrderDetailItemSource == null
                || SPViewModel.CustomerOrderDetailItemSource.Count == 0)
            {
                var x = App.Current.ShowApplicationMessageBox("Hóa đơn phải có ít nhất 1 sản phẩm!",
                  HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                  HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                  OwnerWindow.MainScreen,
                  "Thông báo!!");
                return false;
            }
            return true;
        }

        private void GenerateOrder()
        {
            try
            {
                _newOrder = new tblOrder();
                _newOrder.IsActive = true;
                _newOrder.OrderTime = DateTime.Now;
                _newOrder.UserID = App.Current.CurrentUser.Username;
                _newOrder.CustomerID = SPViewModel.CustomerOV.CurrentSelectedCustomer.CustomerID;
                _newOrder.OrderDescription = SPViewModel.OrderDescription;
                _newOrder.TotalPrice = SPViewModel.MedicineOV.MedicineCost;
                _newOrder.PurchasePrice = SPViewModel.MedicineOV.PaidAmount;
                foreach (OrderDetailOV vo in SPViewModel.CustomerOrderDetailItemSource)
                {
                    tblOrderDetail oD = new tblOrderDetail()
                    {
                        IsActive = true,
                        Quantity = Convert.ToDouble(vo.Quantity),
                        TotalPrice = vo.TotalPrice,
                        UnitPrice = vo.UnitPrice,
                        MedicineID = vo.MedicineID,
                        PromoPercent = vo.PromoPercent,
                        UnitBidPrice = vo.UnitBidPrice
                    };
                    _newOrder.tblOrderDetails.Add(oD);
                }
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox("Không thể tạo hóa đơn mới, vui lòng liên hệ CSKH!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                   OwnerWindow.MainScreen,
                   "Lỗi!!");
                SPViewModel.ButtonCommandOV.IsInstantiateNewOrderButtonRunning = false;
            }

            _previousDebt = SPViewModel.MedicineOV.DebtCost;

            _createNewOrderQueryObserver = new SQLQueryCustodian(GenerateOrderCallback,
                null,
                typeof(MSW_SP_InstantiateNewOrderAction));
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_NEW_CUSTOMER_ORDER_CMD_KEY,
                PharmacyDefinitions.ADD_NEW_CUSTOMER_ORDER_DELAY_TIME,
                _createNewOrderQueryObserver,
                _newOrder);
        }

        private void GenerateOrderCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                bool refreshCustomer = false;
                bool refreshMedicineBillBoard = true;

                App.Current.ShowApplicationMessageBox("Tạo hóa đơn mới thành công! Đóng thông báo để tiếp tục in hóa đơn",
                  HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                  HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                  OwnerWindow.MainScreen,
                  "Thông báo!!");

                PrintInvoice();

                SPViewModel.RefreshViewModel(refreshCustomer, refreshMedicineBillBoard);
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi tạo hóa đơn mới!",
                  HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                  HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                  OwnerWindow.MainScreen,
                  "Lỗi!!");
            }

            SPViewModel.ButtonCommandOV.IsInstantiateNewOrderButtonRunning = false;
        }

        private void PrintInvoice()
        {
            try
            {
                ReportViewer report = new ReportViewer();
                report.LocalReport.ReportPath = Path.GetFullPath(@"Implement/Windows/MainScreenWindow/MVVM/Views/ReportViewers/SellingInvoice.rdlc");

                ReportParameter[] reportParameters = new ReportParameter[10];
                reportParameters[0] = new ReportParameter("NgayBaoCao", "Ngày xuất: " + _newOrder.OrderTime.Hour + ":" + _newOrder.OrderTime.Minute + " " + _newOrder.OrderTime.Day + "/" + _newOrder.OrderTime.Month + "/" + _newOrder.OrderTime.Year);
                reportParameters[1] = new ReportParameter("KhachHang", _newOrder.tblCustomer.CustomerName);
                reportParameters[2] = new ReportParameter("SDT", _newOrder.tblCustomer.Phone);
                reportParameters[3] = new ReportParameter("DiaChi", _newOrder.tblCustomer.Address);
                reportParameters[4] = new ReportParameter("ThanhTien", SPViewModel.MedicineOV.MedicineCost.ToString());
                reportParameters[5] = new ReportParameter("CongNo", _previousDebt.ToString());
                reportParameters[6] = new ReportParameter("TongCong", (SPViewModel.MedicineOV.MedicineCost + _previousDebt).ToString());
                reportParameters[7] = new ReportParameter("DaTra", SPViewModel.MedicineOV.PaidAmount.ToString());
                reportParameters[8] = new ReportParameter("ConLai", ((SPViewModel.MedicineOV.MedicineCost + _previousDebt) - SPViewModel.MedicineOV.PaidAmount).ToString());
                reportParameters[9] = new ReportParameter("GhiChu", _newOrder.OrderDescription);
                report.LocalReport.SetParameters(reportParameters);


                PharmacyDBDataSet.InvoiceDetailListDataTable tbl = new PharmacyDBDataSet.InvoiceDetailListDataTable();

                int id = 1;
                foreach (var item in _newOrder.tblOrderDetails)
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
