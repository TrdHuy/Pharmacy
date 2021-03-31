using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ShowMedicineInfo;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.MedicineManagement
{
    /// <summary>
    /// Interaction logic for ShowMedicineInfoPage.xaml
    /// </summary>
    public partial class ShowMedicineInfoPage : QuotableEventPage
    {
        public ShowMedicineInfoPage()
        {
            InitializeComponent();
            DataContext = new ShowMedicineInfoPageViewModel();
        }
    }
}
