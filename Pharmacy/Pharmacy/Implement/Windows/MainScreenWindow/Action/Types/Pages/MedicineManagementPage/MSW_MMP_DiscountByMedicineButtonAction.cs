﻿using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage
{
    public class MSW_MMP_DiscountByMedicineButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private MedicineManagementPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as MedicineManagementPageViewModel;
            DataGrid ctrl = dataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedMedicine = _viewModel.MedicineItemSource[ctrl.SelectedIndex];
            _pageHost.UpdateCurrentPageSource(PageSource.DISCOUNT_BY_MEDICINE_PAGE);
            return true;
        }
    }
}