using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
