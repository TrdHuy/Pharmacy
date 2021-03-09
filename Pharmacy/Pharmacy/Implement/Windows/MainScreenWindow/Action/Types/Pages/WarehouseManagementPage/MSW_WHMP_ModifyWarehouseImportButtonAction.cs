﻿using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage
{
    public class MSW_WHMP_ModifyWarehouseImportButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private WarehouseManagementPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as WarehouseManagementPageViewModel;
            DataGrid ctrl = dataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedWarehouseImport = _viewModel.LstWarehouseImport[ctrl.SelectedIndex];
            _pageHost.UpdateCurrentPageSource(PageSource.MODIFY_WAREHOUSE_IMPORT_PAGE);

            return true;
        }
    }
}