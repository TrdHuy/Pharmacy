using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.Extensions;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    internal class MSW_AlternativeAction : MSW_BaseAlternativeButtonAction
    {
        public MSW_AlternativeAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            App.Current.ShowApplicationMessageBox(KeyActionListener.Current.GetMSWFactoryStatus().GetStringValue(),
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.MainScreen,
                "Thông báo!");
        }
    }
}
