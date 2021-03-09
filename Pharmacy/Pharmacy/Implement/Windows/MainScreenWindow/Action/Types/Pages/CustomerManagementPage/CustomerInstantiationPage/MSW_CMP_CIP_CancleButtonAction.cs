﻿using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerInstantiationPage
{
    public class MSW_CMP_CIP_CancleButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _pageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_MANAGEMENT_PAGE);
            return true;
        }
    }
}