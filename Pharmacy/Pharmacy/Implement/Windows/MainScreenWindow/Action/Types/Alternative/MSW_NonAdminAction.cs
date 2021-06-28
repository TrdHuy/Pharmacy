using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.Extensions;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    internal class MSW_NonAdminAction : MSW_BaseAlternativeButtonAction
    {
        public MSW_NonAdminAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
        protected override void ExecuteCommand()
        {
            App.Current.ShowApplicationMessageBox("Chức năng này không được hỗ trợ ở phiên người dùng hiện tại!\nVui lòng liên hệ người quản trị hệ thống!",
                 HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                 HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                 OwnerWindow.MainScreen,
                 "Cảnh báo!");
        }
    }
}
