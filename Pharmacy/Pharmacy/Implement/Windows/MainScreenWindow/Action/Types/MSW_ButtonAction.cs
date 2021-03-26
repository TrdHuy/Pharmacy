using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_ButtonAction : BaseCommandExecuter
    {
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_ButtonAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }

        public override void ExecuteCommand()
        {

        }
    }
}
