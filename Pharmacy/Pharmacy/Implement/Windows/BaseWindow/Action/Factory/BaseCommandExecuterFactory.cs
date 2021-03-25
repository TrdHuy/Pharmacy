using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Factory
{
    internal class BaseCommandExecuterFactory : AbstractCommandExecuterFactory
    {
        public override ICommandExecuter CreateAlternativeCommandExecuterWhenFactoryIsLock(string keyTag, ILogger logger = null)
        {
            return null;
        }

        public override IDestroyableViewModelCommandExecuter CreateAlternativeDestroyableViewModelCommandExecuterWhenFactoryIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            return null;
        }

        public override IViewModelCommandExecuter CreateAlternativeViewModelCommandExecuterWhenFactoryIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null)
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
