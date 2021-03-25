﻿using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage.OVs
{
    internal class MSW_UMP_ButtomCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_UMP_ButtomCommandOV");

        public CommandModel EditButtonCommand { get; set; }
        public CommandModel DeleteUserButtonCommand { get; set; }
        public CommandModel AddNewUserButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_UMP_ButtomCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            AddNewUserButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_ADD_BUTTON
                , paramaters);
            });
            DeleteUserButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_DELETE_BUTTON
                , paramaters);
            });
            EditButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_EDIT_BUTTON
                , paramaters);
            });
        }

    }
}

