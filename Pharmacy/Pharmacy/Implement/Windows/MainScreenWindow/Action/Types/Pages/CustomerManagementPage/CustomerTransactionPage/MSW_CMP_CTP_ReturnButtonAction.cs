﻿using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage
{
    public class MSW_CMP_CTP_ReturnButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _pageHost.UpdateCurrentPageSource(PageSource.CustomerManagementPage);

            return true;
        }
    }
}