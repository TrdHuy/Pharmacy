using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage
{
    public class MSW_SMP_DeleteSupplierButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private SupplierManagementPageViewModel _viewModel;
        private DataGrid ctrl;
        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as SupplierManagementPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa nhà cung cấp này?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {

                SQLQueryCustodian sqlQueryObserver = new SQLQueryCustodian((queryResult) =>
                {
                    if (queryResult.MesResult == MessageQueryResult.Done)
                    {
                        App.Current.ShowApplicationMessageBox("Xóa nhà cung cấp thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        _viewModel.SupplierItemSource.RemoveAt(ctrl.SelectedIndex);
                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_SUPPLIER_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    _viewModel.SupplierItemSource[ctrl.SelectedIndex].SupplierID);

                return true;
            }

            return false;
        }
    }
}
