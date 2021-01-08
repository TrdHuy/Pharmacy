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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.UserManagement
{
    /// <summary>
    /// Interaction logic for UserModificationPage.xaml
    /// </summary>
    public partial class UserInstantiationPage : Page
    {
        public UserInstantiationPage()
        {
            InitializeComponent();
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((UserInstantiationPageViewModel)DataContext).NewPasswordChangedCommand.Execute(sender, e, this);
        }

        private void VerifiedPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((UserInstantiationPageViewModel)DataContext).VerifiedPasswordChangedCommand.Execute(sender, e, this);
        }

    }
}
