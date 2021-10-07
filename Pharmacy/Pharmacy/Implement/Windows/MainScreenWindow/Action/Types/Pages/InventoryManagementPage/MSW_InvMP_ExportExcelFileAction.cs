using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.InventoryManagementPage
{

    internal class MSW_InvMP_ExportExcelFileAction : BaseViewModelCommandExecuter
    {
        protected InventoryManagementPageViewModel IMPViewModel
        {
            get
            {
                return ViewModel as InventoryManagementPageViewModel;
            }
        }

        public MSW_InvMP_ExportExcelFileAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
        }
    }
}
