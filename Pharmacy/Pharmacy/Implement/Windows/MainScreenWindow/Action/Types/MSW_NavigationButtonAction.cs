using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Base.UIEventHandler.Action.Executer;
using Pharmacy.Base.UIEventHandler.Action;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_NavigationButtonAction : BaseCommandExecuter, INavigationCommandExecuter
    {
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_NavigationButtonAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger)
        {
        }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
        }

        protected override void ExecuteAlternativeCommand()
        {
            base.ExecuteAlternativeCommand();
        }

        public bool PreviewGoToNewSource()
        {
            var res = App.Current.ShowApplicationMessageBox("Tác vụ đang được thực hiện, bạn có muốn hủy bỏ tác vụ!"
                , HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo
                , HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question
                , OwnerWindow.MainScreen
                , "Thông báo!");
            if (res == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {
                return true;
            }
            return false;
        }
    }
}
