using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement.OVs
{
    public class MSW_MMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_MMP_ButtonCommandOV");

        public RunInputCommand ExcelImportButtonCommand { get; set; }
        public RunInputCommand PrintMedicineListButtonCommand { get; set; }
        public RunInputCommand AddNewMedicineButtonCommand { get; set; }
        public RunInputCommand EditMedicineButtonCommand { get; set; }
        public RunInputCommand DeleteMedicineButtonCommand { get; set; }
        public RunInputCommand PromoMedicineButtonCommand { get; set; }
        public RunInputCommand FilterMedicineTypeCommand { get; set; }
        protected override Logger logger => L;

        public MSW_MMP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            ExcelImportButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_EXCEL_IMPORT_BUTTON,
                       paramaters);
            });
            PrintMedicineListButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_PRINT_MEDICINE_BUTTON,
                       paramaters);
            });
            AddNewMedicineButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON,
                       paramaters);
            });

            if (App.Current.CurrentUser.IsAdmin)
            {
                EditMedicineButtonCommand = new RunInputCommand((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON,
                           paramaters);
                });
                DeleteMedicineButtonCommand = new RunInputCommand((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON,
                           paramaters);
                });
                PromoMedicineButtonCommand = new RunInputCommand((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON,
                           paramaters);
                });
            }
            else
            {
                EditMedicineButtonCommand = new RunInputCommand((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters);
                });
                DeleteMedicineButtonCommand = new RunInputCommand((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters);
                });
                PromoMedicineButtonCommand = new RunInputCommand((paramaters) =>
                {
                    OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON,
                           paramaters);
                });
            }
            
        }

    }
}
