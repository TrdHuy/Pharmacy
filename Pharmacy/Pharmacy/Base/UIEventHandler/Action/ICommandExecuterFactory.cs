using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface ICommandExecuterFactory : IActionFactory
    {
        ICommandExecuter CreateCommandExecuter(string keyTag, ILogger logger);

        IViewModelCommandExecuter CreateViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        IDestroyableViewModelCommandExecuter CreateDestroyableViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        ICommandExecuter CreateAlternativeCommandExecuterWhenFactoryIsLock(string keyTag, ILogger logger);

        IViewModelCommandExecuter CreateAlternativeViewModelCommandExecuterWhenFactoryIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        IDestroyableViewModelCommandExecuter CreateAlternativeDestroyableViewModelCommandExecuterWhenFactoryIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);


    }
}
