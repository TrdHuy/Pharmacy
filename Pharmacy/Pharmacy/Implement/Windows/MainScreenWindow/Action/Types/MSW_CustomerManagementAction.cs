﻿using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_CustomerManagementAction : MSW_ButtonAction
    {
        public MSW_CustomerManagementAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
        public override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_MANAGEMENT_PAGE);
        }
    }
}
