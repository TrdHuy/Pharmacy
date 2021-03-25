using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerManagement.OVs
{
    public class MSW_CMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_CMP_ButtonCommandOV");

        public RunInputCommand AddNewCustomerButtonCommand { get; set; }
        public RunInputCommand EditButtonCommand { get; set; }
        public RunInputCommand DeleteButtonCommand { get; set; }
        public RunInputCommand HistoryButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_CMP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            this.ParentsModel = parentsModel;

            EditButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_EDIT_BUTTON
                    , paramaters);
            });

            DeleteButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_DELETE_BUTTON
                    , paramaters);
            });

            HistoryButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_HISTORY_BUTTON
                    , paramaters);
            });

            AddNewCustomerButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_ADD_BUTTON
                    , paramaters);
            });

        }

    }
}
