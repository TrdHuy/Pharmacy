using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.Extensions;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    internal class MSW_LockedBuilderAlternativeAction : MSW_BaseAlternativeButtonAction
    {
        public MSW_LockedBuilderAlternativeAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
        protected override void ExecuteCommand()
        {
            App.Current.ShowApplicationMessageBox(KeyActionListener.Current.GetMSWFactoryStatus().GetStringValue(),
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.MainScreen,
                "Thông báo!");
        }
    }
}
