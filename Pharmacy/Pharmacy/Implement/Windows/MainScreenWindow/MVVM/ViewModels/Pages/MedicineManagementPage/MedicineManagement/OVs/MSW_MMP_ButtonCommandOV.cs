using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement.OVs
{
    internal class MSW_MMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_MMP_ButtonCommandOV");

        public RunInputCommand ExcelImportButtonCommand { get; set; }
        public RunInputCommand PrintMedicineListButtonCommand { get; set; }
        public RunInputCommand AddNewMedicineButtonCommand { get; set; }
        public RunInputCommand EditMedicineButtonCommand { get; set; }
        public RunInputCommand DeleteMedicineButtonCommand { get; set; }
        public RunInputCommand PromoMedicineButtonCommand { get; set; }

        protected override Logger logger => L;
        public MSW_MMP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            ExcelImportButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_EXCEL_IMPORT_BUTTON
                    , paramaters);
            });
            PrintMedicineListButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_PRINT_MEDICINE_BUTTON
                    , paramaters);
            });
            AddNewMedicineButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON
                    , paramaters);
            });
            EditMedicineButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON
                    , paramaters);
            });
            PromoMedicineButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON
                    , paramaters);
            });
            DeleteMedicineButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON
                    , paramaters);
            });
        }

    }
}
