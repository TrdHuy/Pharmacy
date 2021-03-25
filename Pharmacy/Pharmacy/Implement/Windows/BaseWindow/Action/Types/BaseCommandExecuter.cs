using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Types
{
    internal class BaseCommandExecuter : AbstractCommandExecuter
    {
        public BaseCommandExecuter(ILogger logger) : base(logger) { }

        public override bool CanExecute(object dataTransfer)
        {
            return true;
        }

        public override void ExecuteCommand(object dataTransfer) { }

        public override void SetCompleteFlagAfterExecuteCommand()
        {
            IsCompleted = true;
        }
    }
}
