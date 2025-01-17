﻿using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.InvoiceManagementPage
{
    internal class MSW_IMP_EditButtonAction : MSW_IMP_ButtonAction
    {
        public MSW_IMP_EditButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {

            MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder = IMPViewModel.CurrentSelectedOrderOV.Order;
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_BILL_PAGE);
        }
    }
}
