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

        public CommandModel ExcelImportButtonCommand { get; set; }
        public CommandModel PrintMedicineListButtonCommand { get; set; }
        public CommandModel AddNewMedicineButtonCommand { get; set; }
        public CommandModel EditMedicineButtonCommand { get; set; }
        public CommandModel DeleteMedicineButtonCommand { get; set; }
        public CommandModel PromoMedicineButtonCommand { get; set; }

        protected override Logger logger => L;
        public MSW_MMP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            ExcelImportButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_EXCEL_IMPORT_BUTTON
                    , paramaters);
            });
            PrintMedicineListButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_PRINT_MEDICINE_BUTTON
                    , paramaters);
            });
            AddNewMedicineButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON
                    , paramaters);
            });

            if (App.Current.CurrentUser.IsAdmin)
            {
                EditMedicineButtonCommand = new CommandModel((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON,
                           paramaters);
                });
                DeleteMedicineButtonCommand = new CommandModel((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON,
                           paramaters);
                });
                PromoMedicineButtonCommand = new CommandModel((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON,
                           paramaters);
                });
            }
            else
            {
                EditMedicineButtonCommand = new CommandModel((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters,
                           false);
                });
                DeleteMedicineButtonCommand = new CommandModel((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters,
                           false);
                });
                PromoMedicineButtonCommand = new CommandModel((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters,
                           false);
                });
            }
            
        }

    }
}
