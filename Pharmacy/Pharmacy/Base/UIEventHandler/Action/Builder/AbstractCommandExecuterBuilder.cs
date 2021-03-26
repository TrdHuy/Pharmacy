using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public abstract class AbstractCommandExecuterBuilder : AbstractActionBuilder, ICommandExecuterBuilder
    {
        public override IAction CreateMainAction(string keyTag)
        {
            return CreateCommandExecuter(keyTag);
        }

        public override IAction CreateAlternativeActionWhenFactoryIsLock(string keyTag)
        {
            return CreateAlternativeCommandExecuterWhenBuilderIsLock(keyTag);
        }

        public abstract ICommandExecuter CreateAlternativeCommandExecuterWhenBuilderIsLock(string keyTag, ILogger logger = null);
        public abstract IDestroyableViewModelCommandExecuter CreateAlternativeDestroyableViewModelCommandExecuterWhenBuilderIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);
        public abstract IViewModelCommandExecuter CreateAlternativeViewModelCommandExecuterWhenBuilderIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        public abstract ICommandExecuter CreateCommandExecuter(string keyTag, ILogger logger = null);
        public abstract IDestroyableViewModelCommandExecuter CreateDestroyableViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);
        public abstract IViewModelCommandExecuter CreateViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);


    }
}
