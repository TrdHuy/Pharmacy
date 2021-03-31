using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.PersonalInfo
{
    /// <summary>
    /// Interaction logic for PersonalInfoPage.xaml
    /// </summary>
    public partial class PersonalInfoPage : QuotableEventPage
    {
        public PersonalInfoPage()
        {
            InitializeComponent();
        }

        private void ImageGridContainerSizeChanged(object sender, SizeChangedEventArgs e)
        {
           // ((PersonalInfoPageViewModel)DataContext).GridSizeChangedCommand.Execute(sender, e, AvatarBoder, this);
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
