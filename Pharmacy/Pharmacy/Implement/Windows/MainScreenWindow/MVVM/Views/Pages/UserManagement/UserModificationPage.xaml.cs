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
    public partial class UserModificationPage : Page
    {
        public UserModificationPage()
        {
            InitializeComponent();
        }

        private void ImageGridContainerSizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void CurrentPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
        }

        private void VerifiedPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
        }
    }
}
