using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_NavigationButtonAction : BaseCommandExecuter, INavigationAction
    {
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;
        private bool _isGotoNewSource;

        public bool IsGotoNewSource { get => _isGotoNewSource; protected set => _isGotoNewSource = value; }

        public MSW_NavigationButtonAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger)
        {
            IsGotoNewSource = true;
        }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
        }

        protected override void ExecuteAlternativeCommand()
        {
            base.ExecuteAlternativeCommand();
            var res = App.Current.ShowApplicationMessageBox("Tác vụ đang được thực hiện, bạn có muốn hủy bỏ tác vụ!"
                , HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo
                , HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question
                , OwnerWindow.MainScreen
                , "Thông báo!");
            if (res == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {
                ExecuteCommand();
            }
        }
    }
}
