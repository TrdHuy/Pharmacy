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

        public LSW_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}

