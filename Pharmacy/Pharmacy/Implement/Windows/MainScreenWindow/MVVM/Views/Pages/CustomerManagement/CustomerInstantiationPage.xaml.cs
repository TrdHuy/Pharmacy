using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.CustomerManagement
{
    /// <summary>
    /// Interaction logic for CustomerInstantiationPage.xaml
    /// </summary>
    public partial class CustomerInstantiationPage : Page
    {
        public CustomerInstantiationPage()
        {
            InitializeComponent();
        }

        private void ImageGridContainerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((CustomerInstantiationPageViewModel)DataContext).GridSizeChangedCommand.Execute(sender, e, AvatarBoder, this);
        }

    }
}
