using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.ViewModels;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type
{
    internal class LSW_ButtonAction : BaseViewModelCommandExecuter
    {
        protected LoginScreenWindowViewModel LSWViewModel
        {
            get
            {
                return ViewModel as LoginScreenWindowViewModel;
            }
        }

        public LSW_ButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
        }
    }
}

