using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface ICommandExecuterBuilder : IActionBuilder
    {
        ICommandExecuter CreateCommandExecuter(string keyTag, ILogger logger);

        IViewModelCommandExecuter CreateViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        IDestroyableViewModelCommandExecuter CreateDestroyableViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        ICommandExecuter CreateAlternativeCommandExecuterWhenBuilderIsLock(string keyTag, ILogger logger);

        IViewModelCommandExecuter CreateAlternativeViewModelCommandExecuterWhenBuilderIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        IDestroyableViewModelCommandExecuter CreateAlternativeDestroyableViewModelCommandExecuterWhenBuilderIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);
    }
}
