using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage
{
    public class MSW_WHMP_ShowWarehouseImportInfoButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private WarehouseManagementPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as WarehouseManagementPageViewModel;
            object[] param = dataTransfer[1] as object[];
            DataGrid ctrl = param[0] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedWarehouseImport = _viewModel.LstWarehouseImport[ctrl.SelectedIndex];
            _pageHost.UpdateCurrentPageSource(PageSource.ShowWarehouseImportInfoPage);

            return true;
        }
    }
}