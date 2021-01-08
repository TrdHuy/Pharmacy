using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages;
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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.PersonalInfo
{
    /// <summary>
    /// Interaction logic for PersonalInfoPage.xaml
    /// </summary>
    public partial class PersonalInfoPage : Page
    {
        public PersonalInfoPage()
        {
            InitializeComponent();
        }

        private void ImageGridContainerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((PersonalInfoPageViewModel)DataContext).GridSizeChangedCommand.Execute(sender, e, AvatarBoder, this);
        }

        private void CurrentPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PersonalInfoPageViewModel)DataContext).CurrentPasswordChangedCommand.Execute(sender, e, this);
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PersonalInfoPageViewModel)DataContext).NewPasswordChangedCommand.Execute(sender, e, this);
        }

        private void VerifiedPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PersonalInfoPageViewModel)DataContext).VerifiedPasswordChangedCommand.Execute(sender, e, this);
        }

    }
}
