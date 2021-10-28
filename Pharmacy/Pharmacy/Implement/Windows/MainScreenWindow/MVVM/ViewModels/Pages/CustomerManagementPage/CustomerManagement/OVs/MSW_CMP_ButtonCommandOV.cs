using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerManagement.OVs
{
    internal class MSW_CMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_CMP_ButtonCommandOV");

        public CommandExecuterModel AddNewCustomerButtonCommand { get; set; }
        public CommandExecuterModel EditButtonCommand { get; set; }
        public CommandExecuterModel DeleteButtonCommand { get; set; }
        public CommandExecuterModel HistoryButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_CMP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            this.ParentsModel = parentsModel;

            EditButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_EDIT_BUTTON
                    , paramaters);
            });

            AddNewCustomerButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_ADD_BUTTON
                    , paramaters);
            });
            if (App.Current.CurrentUser.IsAdmin)
            {
                DeleteButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_DELETE_BUTTON
                        , paramaters);
                });

                HistoryButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_HISTORY_BUTTON
                        , paramaters);
                });
            }
            else
            {
                DeleteButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON
                        , paramaters);
                });

                HistoryButtonCommand = new CommandExecuterModel((paramaters) =>
                {
                    return OnKey(KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON
                        , paramaters);
                });
            }
        }

    }
}
