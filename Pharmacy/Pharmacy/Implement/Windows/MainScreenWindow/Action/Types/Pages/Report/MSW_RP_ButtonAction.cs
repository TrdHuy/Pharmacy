using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.Report
{
    internal class MSW_RP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected ReportPageViewModel RPViewModel
        {
            get
            {
                return ViewModel as ReportPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_RP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}