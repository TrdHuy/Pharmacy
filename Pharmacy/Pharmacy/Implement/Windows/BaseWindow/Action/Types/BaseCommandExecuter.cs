using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Types
{
    internal class BaseCommandExecuter : AbstractCommandExecuter
    {
        public BaseCommandExecuter(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }

        protected override bool CanExecute(object dataTransfer)
        {
            return true;
        }

        protected override void ExecuteAlternativeCommand()
        {
        }

        protected override void ExecuteCommand()
        {
        }

        protected override void ExecuteOnDestroy()
        {
        }

        protected override void SetCompleteFlagAfterExecuteCommand()
        {
            IsCompleted = true;
        }
    }
}
