using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.SupplierImportHistoryPage
{
    public class MSW_SMP_SIHP_ShowSupplierDebButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private SupplierManagementPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as SupplierManagementPageViewModel;

            _pageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_DEBT_PAGE);

            return true;
        }
    }
}