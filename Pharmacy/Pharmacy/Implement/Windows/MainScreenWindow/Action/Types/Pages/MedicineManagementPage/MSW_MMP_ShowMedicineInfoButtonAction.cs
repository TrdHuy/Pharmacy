﻿using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage
{
    internal class MSW_MMP_ShowMedicineInfoButtonAction : MSW_MMP_ButtonAction
    {
        public MSW_MMP_ShowMedicineInfoButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
            object[] param = DataTransfer[1] as object[];
            DataGrid ctrl = param[0] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedMedicine = MMPViewModel.MedicineItemSource[ctrl.SelectedIndex];
            PageHost.UpdateCurrentPageSource(PageSource.SHOW_MEDICINE_INFO_PAGE);
            return;
        }
    }
}