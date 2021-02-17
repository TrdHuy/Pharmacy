using Pharmacy.Base.UIEventHandler.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    public class MSW_NotImplementedAction : IAction
    {
        public bool Execute(object[] dataTransfer)
        {
            App.Current.ShowApplicationMessageBox("Chức năng này chưa được triển khai ở phiên bản hiện tại!\nVui lòng liên hệ CSKH để được tư vấn thêm!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.MainScreen,
                "Thông báo!");
            return true;
        }
    }
}
