using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Builder
{
    internal class BaseCommandExecuterBuilder : AbstractCommandExecuterBuilder
    {
        public override ICommandExecuter CreateAlternativeCommandExecuterWhenBuilderIsLock(string keyTag, ILogger logger = null)
        {
            return null;
        }

        public override IDestroyableViewModelCommandExecuter CreateAlternativeDestroyableViewModelCommandExecuterWhenBuilderIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            return null;
        }

        public override IViewModelCommandExecuter CreateAlternativeViewModelCommandExecuterWhenBuilderIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            return null;
        }

        public override ICommandExecuter CreateCommandExecuter(string keyTag, ILogger logger = null)
        {
            return null;
        }

        public override IDestroyableViewModelCommandExecuter CreateDestroyableViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            return null;
        }

        public override IViewModelCommandExecuter CreateViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            return null;
        }
    }
}
