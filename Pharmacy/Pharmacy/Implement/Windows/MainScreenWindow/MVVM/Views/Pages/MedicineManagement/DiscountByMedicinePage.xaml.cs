using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.DiscountByMedicine;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.MedicineManagement
{
    /// <summary>
    /// Interaction logic for DiscountByMedicinePage.xaml
    /// </summary>
    public partial class DiscountByMedicinePage : QuotableEventPage
    {
        public DiscountByMedicinePage()
        {
            InitializeComponent();
            DataContext = new DiscountByMedicinePageViewModel();
        }
    }
}
