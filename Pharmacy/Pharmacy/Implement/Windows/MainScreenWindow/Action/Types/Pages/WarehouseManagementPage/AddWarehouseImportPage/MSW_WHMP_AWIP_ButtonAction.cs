﻿using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.AddWarehouseImportPage
{
    internal class MSW_WHMP_AWIP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected AddWarehouseImportPageViewModel AWIPViewModel
        {
            get
            {
                return ViewModel as AddWarehouseImportPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_WHMP_AWIP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}
