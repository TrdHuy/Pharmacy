using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.AddSupplier;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.SupplierManagement
{
    /// <summary>
    /// Interaction logic for AddSupplierPage.xaml
    /// </summary>
    public partial class AddSupplierPage : QuotableEventPage
    {
        public AddSupplierPage()
        {
            InitializeComponent();
            DataContext = new AddSupplierPageViewModel();
        }
    }
}
