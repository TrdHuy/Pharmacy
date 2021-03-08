using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.Report
{
    public class MSW_RP_BaseButtonAction : Base.UIEventHandler.Action.IAction
    {
        protected ReportPageViewModel ViewModel { get; set; }
        private SQLQueryCustodian QueryObserver { get; set; }

        public virtual bool Execute(object[] dataTransfer)
        {
            ViewModel = dataTransfer[0] as ReportPageViewModel;

            return true;
        }
    }
}
