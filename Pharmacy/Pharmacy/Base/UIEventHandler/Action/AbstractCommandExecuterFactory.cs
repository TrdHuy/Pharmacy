using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public abstract class AbstractCommandExecuterFactory : AbstractActionFactory, ICommandExecuterFactory
    {
        public override IAction CreateMainAction(string keyTag)
        {
            return CreateCommandExecuter(keyTag);
        }

        public override IAction CreateAlternativeActionWhenFactoryIsLock(string keyTag)
        {
            return CreateAlternativeCommandExecuterWhenFactoryIsLock(keyTag);
        }

        public abstract ICommandExecuter CreateAlternativeCommandExecuterWhenFactoryIsLock(string keyTag, ILogger logger = null);
        public abstract IDestroyableViewModelCommandExecuter CreateAlternativeDestroyableViewModelCommandExecuterWhenFactoryIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);
        public abstract IViewModelCommandExecuter CreateAlternativeViewModelCommandExecuterWhenFactoryIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        public abstract ICommandExecuter CreateCommandExecuter(string keyTag, ILogger logger = null);
        public abstract IDestroyableViewModelCommandExecuter CreateDestroyableViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);
        public abstract IViewModelCommandExecuter CreateViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);


    }
}
