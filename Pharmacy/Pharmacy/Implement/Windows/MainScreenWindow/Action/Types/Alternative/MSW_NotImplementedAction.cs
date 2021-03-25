﻿using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    internal class MSW_NotImplementedAction : MSW_BaseAlternativeButtonAction
    {
        public MSW_NotImplementedAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            App.Current.ShowApplicationMessageBox("Chức năng này chưa được triển khai ở phiên bản hiện tại!\nVui lòng liên hệ CSKH để được tư vấn thêm!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.MainScreen,
                "Thông báo!");
        }
    }
}
