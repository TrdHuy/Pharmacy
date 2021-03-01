using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SettingPage.OVs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SettingPage
{
    public class SettingPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("SettingPageViewModel");

        public tblUser CurrentUser { get { return App.Current.CurrentUser; } }
        public ObservableCollection<ColorOV> ColorsSource { get; set; }
        public ObservableCollection<LanguageOV> LanguagesSource { get; set; }
        public ObservableCollection<UIOV> UIsSource { get; set; }
        public MSW_SeP_ButtonCommandOV ButtonCommandOV { get; set; }

        public double FontSizeRatio
        {
            get
            {
                return FontSizeOV.FontSizeZoomRatio;
            }
            set
            {
                FontSizeOV.FontSizeZoomRatio = value;
                Invalidate("FontSizeOV");
            }
        }

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            InstantiateSource();
            ButtonCommandOV = new MSW_SeP_ButtonCommandOV(this);

        }

        protected override void OnInitialized()
        {
        }

        private void InstantiateSource()
        {
            InstantiateColors();
            InstantiateLanguages();
            InstantiateUIs();

        }

        private void InstantiateUIs()
        {
            UIsSource = new ObservableCollection<UIOV>();

            var ui_1 = new UIOV("Cơ bản");

            UIsSource.Add(ui_1);
        }

        private void InstantiateLanguages()
        {
            LanguagesSource = new ObservableCollection<LanguageOV>();

            var language_1 = new LanguageOV("Tiếng Việt");
            var language_2 = new LanguageOV("English");

            LanguagesSource.Add(language_1);
            LanguagesSource.Add(language_2);
        }

        private void InstantiateColors()
        {
            ColorsSource = new ObservableCollection<ColorOV>();

            var color_1 = new ColorOV((Color)App.Current.Resources["NormalTheme_1st_Color"], "Cơ bản");
            var color_2 = new ColorOV((Color)App.Current.Resources["NormalTheme_LightBlue"], "Xanh lá sáng");

            ColorsSource.Add(color_1);
            ColorsSource.Add(color_2);
        }
    }

    public class UIOV
    {
        public string UIName { get; set; }

        public UIOV(string name)
        {
            UIName = name;
        }

        public override string ToString()
        {
            return UIName;
        }
    }

    public class LanguageOV : BaseViewModel
    {
        public string LanguageName { get; set; }

        public LanguageOV(string name)
        {
            LanguageName = name;
        }

        public override string ToString()
        {
            return LanguageName;
        }
    }

    public class ColorOV : BaseViewModel
    {
        public SolidColorBrush ColorBrush { get; set; }
        public string ColorName { get; set; }

        public ColorOV(Color c, string name)
        {
            ColorBrush = new SolidColorBrush(c);
            ColorName = name;
        }

        public override string ToString()
        {
            return ColorName;
        }
    }
}
