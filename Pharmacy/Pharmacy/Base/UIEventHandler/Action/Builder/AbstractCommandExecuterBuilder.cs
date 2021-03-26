using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public abstract class AbstractCommandExecuterBuilder : AbstractActionBuilder, ICommandExecuterBuilder
    {
        public override IAction BuildMainAction(string keyTag)
        {
            return BuildCommandExecuter(keyTag);
        }

        public override IAction BuildAlternativeActionWhenFactoryIsLock(string keyTag)
        {
            return BuildAlternativeCommandExecuterWhenBuilderIsLock(keyTag);
        }

        public abstract ICommandExecuter BuildAlternativeCommandExecuterWhenBuilderIsLock(string keyTag, ILogger logger = null);
        public abstract IViewModelCommandExecuter BuildAlternativeViewModelCommandExecuterWhenBuilderIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null);

        public abstract ICommandExecuter BuildCommandExecuter(string keyTag, ILogger logger = null);
        public abstract IViewModelCommandExecuter BuildViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null);


    }
}
