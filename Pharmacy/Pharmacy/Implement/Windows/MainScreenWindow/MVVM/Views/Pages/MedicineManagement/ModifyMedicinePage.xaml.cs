using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ModifyMedicine;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.MedicineManagement
{
    /// <summary>
    /// Interaction logic for ModifyMedicinePage.xaml
    /// </summary>
    public partial class ModifyMedicinePage : Page
    {
        public ModifyMedicinePage()
        {
            InitializeComponent();
            DataContext = new ModifyMedicinePageViewModel();
        }
    }
}
