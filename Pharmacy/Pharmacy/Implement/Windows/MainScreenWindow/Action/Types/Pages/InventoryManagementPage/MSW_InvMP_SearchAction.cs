using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.CustomControls;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.InventoryManagementPage
{

    internal class MSW_InvMP_SearchAction : BaseViewModelCommandExecuter
    {
        protected InventoryManagementPageViewModel IMPViewModel
        {
            get
            {
                return ViewModel as InventoryManagementPageViewModel;
            }
        }

        public MSW_InvMP_SearchAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            var startDate = (DataTransfer[0] as DatePickerCustom)?.SelectedDate;
            var endDate = (DataTransfer[1] as DatePickerCustom)?.SelectedDate;

            if (startDate.HasValue && endDate.HasValue)
            {
                IMPViewModel.IsDataGridLoading = true;

                if (DateTime.Compare(endDate.Value, startDate.Value) < 0)
                {
                    App.Current.ShowApplicationMessageBox("Ngày kết thúc phải lớn hơn ngày bắt đầu!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo");
                    IMPViewModel.IsDataGridLoading = false;
                }
                else
                {
                    SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
                    {
                        if (queryResult.MesResult == MessageQueryResult.Done)
                        {
                            IMPViewModel.InventoryDataSource = (ObservableCollection<object>)(queryResult.Result);
                        }
                        else
                        {
                            App.Current.ShowApplicationMessageBox("Lỗi load dữ liệu kho hàng, vui lòng mở lại ứng dụng hoặc liên hệ CSKH để biết thêm thông tin!",
                                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                                OwnerWindow.MainScreen,
                                "Lỗi!");
                        }
                        IMPViewModel.IsDataGridLoading = false;
                    });
                    DbManager.Instance.ExecuteQueryAsync(true, SQLCommandKey.GET_INVENTORY_DATA_CMD_KEY,
                       PharmacyDefinitions.ADD_NEW_CUSTOMER_DELAY_TIME,
                       _sqlCmdObserver,
                       startDate,
                       endDate.Value.AddDays(1));
                }
            }

        }
    }
}
