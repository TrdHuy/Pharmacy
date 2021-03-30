using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ShowMedicineInfo.OVs
{
    internal class MSW_MMP_SMIP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_MMP_SMIP_ButtonCommandOV");

        public CommandExecuterModel CancelButtonCommand { get; set; }

        protected override Logger logger => L;
        public MSW_MMP_SMIP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            CancelButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_SMIP_CANCEL_BUTTON
                    , paramaters);
            });
        }

    }
}
