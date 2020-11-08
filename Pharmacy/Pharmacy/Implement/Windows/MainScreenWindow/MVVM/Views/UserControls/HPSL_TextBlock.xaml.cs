using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.UserControls
{
    /// <summary>
    /// Interaction logic for HPSL_TextBlock.xaml
    /// </summary>
    public partial class HPSL_TextBlock : UserControl , INotifyPropertyChanged
    {
        public static readonly DependencyProperty TitleProperty =
          DependencyProperty.Register("Title"
              , typeof(string)
              , typeof(HPSL_TextBlock));

        public static readonly DependencyProperty TitleFontFamilyProperty =
          DependencyProperty.Register("TitleFontFamily"
              , typeof(FontFamily)
              , typeof(HPSL_TextBlock));

        public static readonly DependencyProperty TitleFontSizeProperty =
          DependencyProperty.Register("TitleFontSize"
              , typeof(double)
              , typeof(HPSL_TextBlock)
              ,new PropertyMetadata(30.0));

        public static readonly DependencyProperty UserInputFontSizeProperty =
          DependencyProperty.Register("UserInputFontSize"
              , typeof(double)
              , typeof(HPSL_TextBlock)
              , new PropertyMetadata(20.0));

        public static readonly DependencyProperty TitleFontColorProperty =
          DependencyProperty.Register("TitleFontColor"
              , typeof(Color)
              , typeof(HPSL_TextBlock));

        public static readonly DependencyProperty UserInputProperty =
          DependencyProperty.Register("UserInput"
              , typeof(string)
              , typeof(HPSL_TextBlock));

        public HPSL_TextBlock()
        {
            InitializeComponent();
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string UserInput
        {
            get { return (string)GetValue(UserInputProperty); }
            set { SetValue(UserInputProperty, value); }
        }

        public double UserInputFontSize
        {
            get { return (double)GetValue(UserInputFontSizeProperty); }
            set { SetValue(UserInputFontSizeProperty, value); }
        }
        public FontFamily TitleFontFamily
        {
            get { return (FontFamily)GetValue(TitleFontFamilyProperty); }
            set { SetValue(TitleFontFamilyProperty, value); }
        }

        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public Color TitleFontColor
        {
            get { return (Color)GetValue(TitleFontColorProperty); }
            set { SetValue(TitleFontColorProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void onChanged(object viewModel, string propertyName)
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(viewModel, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }
    }
}
