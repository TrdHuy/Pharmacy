using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage
{
    internal class MSW_MMP_DiscountByMedicineButtonAction : MSW_MMP_ButtonAction
    {
        public MSW_MMP_DiscountByMedicineButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            DataGrid ctrl = DataTransfer[0] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedMedicine = ctrl.SelectedItem as tblMedicine;
            PageHost.UpdateCurrentPageSource(PageSource.DISCOUNT_BY_MEDICINE_PAGE);
        }
    }
}