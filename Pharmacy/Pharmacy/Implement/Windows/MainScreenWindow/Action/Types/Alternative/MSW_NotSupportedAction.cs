using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    internal class MSW_NotSupportedAction : MSW_BaseAlternativeButtonAction
    {
        public MSW_NotSupportedAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            App.Current.ShowApplicationMessageBox("Chức năng này không được hỗ trợ ở phiên bản hiện tại!\nVui lòng liên hệ CSKH để được tư vấn thêm!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.MainScreen,
                "Thông báo!");
        }
    }
}
