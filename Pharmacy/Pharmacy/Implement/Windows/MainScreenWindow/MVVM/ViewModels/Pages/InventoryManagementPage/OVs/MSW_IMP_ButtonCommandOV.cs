using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage.OVs
{
    internal class MSW_IMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_InvMP_ButtonCommandOV");

        public CommandExecuterModel SearchButtonCommand { get; set; }
        public CommandExecuterModel ExportExcelButtonCommand { get; set; }
        public CommandExecuterModel ClearButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_IMP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            SearchButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_InvMP_SEARCH_BUTTON
                    , paramaters);
            });
            ExportExcelButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_InvMP_EXPORT_EXCEL_FILE_BUTTON
                    , paramaters);
            });
            ClearButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_InvMP_CLEAR_BUTTON
                    , paramaters);
            });
        }

    }
}

