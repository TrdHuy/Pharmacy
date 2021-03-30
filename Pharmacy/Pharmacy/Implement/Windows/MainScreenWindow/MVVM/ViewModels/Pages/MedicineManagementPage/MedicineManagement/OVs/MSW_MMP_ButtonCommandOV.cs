using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement.OVs
{
    internal class MSW_MMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_MMP_ButtonCommandOV");

        public CommandExecuterModel ExcelImportButtonCommand { get; set; }
        public CommandExecuterModel PrintMedicineListButtonCommand { get; set; }
        public CommandExecuterModel AddNewMedicineButtonCommand { get; set; }
        public CommandExecuterModel EditMedicineButtonCommand { get; set; }
        public CommandExecuterModel DeleteMedicineButtonCommand { get; set; }
        public CommandExecuterModel PromoMedicineButtonCommand { get; set; }

        protected override Logger logger => L;
        public MSW_MMP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            ExcelImportButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_EXCEL_IMPORT_BUTTON
                    , paramaters);
            });
            PrintMedicineListButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_PRINT_MEDICINE_BUTTON
                    , paramaters);
            });
            AddNewMedicineButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON
                    , paramaters);
            });

            if (App.Current.CurrentUser.IsAdmin)
            {
                EditMedicineButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON,
                           paramaters);
                });
                DeleteMedicineButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON,
                           paramaters);
                });
                PromoMedicineButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON,
                           paramaters);
                });
            }
            else
            {
                EditMedicineButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters,
                           false);
                });
                DeleteMedicineButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters,
                           false);
                });
                PromoMedicineButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters,
                           false);
                });
            }
            
        }

    }
}
