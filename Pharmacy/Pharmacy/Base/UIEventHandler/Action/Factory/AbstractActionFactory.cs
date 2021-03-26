using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Collections.Generic;

namespace Pharmacy.Base.UIEventHandler.Action.Factory
{
    public abstract class AbstractActionFactory : IActionFactory
    {
        protected Dictionary<string, IActionBuilder> _builders { get; set; }

        public AbstractActionFactory()
        {
            _builders = new Dictionary<string, IActionBuilder>();
        }

        public Dictionary<string, IActionBuilder> Builders { get => _builders; }

        public abstract IAction CreateAction(string builderID, string keyID, BaseViewModel viewModel = null, ILogger logger = null);

        public void RegisterBuilder(string builderID, IActionBuilder builder)
        {
            if (!_builders.ContainsKey(builderID))
            {
                _builders.Add(builderID, builder);
            }
        }
    }
}
