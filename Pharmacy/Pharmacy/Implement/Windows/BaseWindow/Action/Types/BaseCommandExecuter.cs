using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Types
{
    internal class BaseCommandExecuter : AbstractCommandExecuter
    {
        public BaseCommandExecuter(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }

        public override bool CanExecute(object dataTransfer)
        {
            return true;
        }

        public override void ExecuteAlternativeCommand()
        {
        }

        public override void ExecuteCommand()
        {
        }

        public override void SetCompleteFlagAfterExecuteCommand()
        {
            IsCompleted = true;
        }
    }
}
