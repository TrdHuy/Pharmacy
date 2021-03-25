﻿using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.UserControls.MainMenuControl.OVs
{
    internal class MSW_MMC_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_CMP_CTP_CBP_ButtonCommandOV");

        protected override Logger logger => L;

        public RunInputCommand SettingPageCommand { get; set; }
        public RunInputCommand PersonalAccountCommand { get; set; }
        public RunInputCommand HomePageCommand { get; set; }
        public RunInputCommand AppInfoCommand { get; set; }

        public MSW_MMC_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SettingPageCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SETTING
                    , paramaters);
            });
            PersonalAccountCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                    , paramaters);
            });
            HomePageCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_HOME_PAGE
                    , paramaters);
            });
            AppInfoCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_APP_INFO
                    , paramaters);
            });
        }
    }
}