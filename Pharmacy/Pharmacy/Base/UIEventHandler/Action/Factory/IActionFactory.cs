using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Collections.Generic;

namespace Pharmacy.Base.UIEventHandler.Action.Factory
{
    interface IActionFactory
    {
        Dictionary<string, IActionBuilder> Builders { get; }

        IAction CreateAction(string builderID, string keyID, BaseViewModel viewModel = null, ILogger logger = null);

        void RegisterBuilder(string builderID, IActionBuilder builder);
    }
}
