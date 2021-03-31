using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.ShowWarehouseImportInfo;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.WarehouseManagement
{
    /// <summary>
    /// Interaction logic for ShowWarehouseImportInfoPage.xaml
    /// </summary>
    public partial class ShowWarehouseImportInfoPage : QuotableEventPage
    {
        public ShowWarehouseImportInfoPage()
        {
            InitializeComponent();
            DataContext = new ShowWarehouseImportInfoPageViewModel();
        }
    }
}
