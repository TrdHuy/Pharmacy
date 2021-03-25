using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    internal class MSW_BaseAlternativeButtonAction : BaseCommandExecuter
    {
        public MSW_BaseAlternativeButtonAction(ILogger logger) : base(logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}



