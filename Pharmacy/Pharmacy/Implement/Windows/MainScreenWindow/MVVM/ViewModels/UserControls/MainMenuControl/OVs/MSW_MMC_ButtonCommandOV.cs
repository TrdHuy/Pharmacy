using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.UserControls.MainMenuControl.OVs
{
    internal class MSW_MMC_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_CMP_CTP_CBP_ButtonCommandOV");

        protected override Logger logger => L;

        public CommandModel SettingPageCommand { get; set; }
        public CommandModel PersonalAccountCommand { get; set; }
        public CommandModel HomePageCommand { get; set; }
        public CommandModel AppInfoCommand { get; set; }

        public MSW_MMC_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SettingPageCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SETTING
                    , paramaters
                    , false);
            });
            PersonalAccountCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                    , paramaters
                    , false);
            });
            HomePageCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_HOME_PAGE
                    , paramaters
                    , false);
            });
            AppInfoCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_APP_INFO
                    , paramaters
                    , false);
            });
        }
    }
}
