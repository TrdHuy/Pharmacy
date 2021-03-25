﻿using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SettingPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SettingPage
{
    internal class MSW_SeP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected SettingPageViewModel SPViewModel
        {
            get
            {
                return ViewModel as SettingPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_SeP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}