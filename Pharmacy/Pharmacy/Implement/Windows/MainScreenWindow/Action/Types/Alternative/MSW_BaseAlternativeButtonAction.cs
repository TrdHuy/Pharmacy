using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    internal class MSW_BaseAlternativeButtonAction : BaseCommandExecuter
    {
        public MSW_BaseAlternativeButtonAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
        }
    }
}



