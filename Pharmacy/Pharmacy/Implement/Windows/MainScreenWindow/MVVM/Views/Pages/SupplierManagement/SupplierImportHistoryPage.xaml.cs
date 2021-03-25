using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.SupplierImportHistory;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.SupplierManagement
{
    /// <summary>
    /// Interaction logic for SupplierImportHistoryPage.xaml
    /// </summary>
    public partial class SupplierImportHistoryPage : Page
    {
        public SupplierImportHistoryPage()
        {
            InitializeComponent();
            DataContext = new SupplierImportHistoryPageViewModel();
        }
    }
}
