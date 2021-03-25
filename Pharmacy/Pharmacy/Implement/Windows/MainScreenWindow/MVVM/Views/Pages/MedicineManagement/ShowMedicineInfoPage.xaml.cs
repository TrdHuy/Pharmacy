using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ShowMedicineInfo;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.MedicineManagement
{
    /// <summary>
    /// Interaction logic for ShowMedicineInfoPage.xaml
    /// </summary>
    public partial class ShowMedicineInfoPage : Page
    {
        public ShowMedicineInfoPage()
        {
            InitializeComponent();
            DataContext = new ShowMedicineInfoPageViewModel();
        }
    }
}
