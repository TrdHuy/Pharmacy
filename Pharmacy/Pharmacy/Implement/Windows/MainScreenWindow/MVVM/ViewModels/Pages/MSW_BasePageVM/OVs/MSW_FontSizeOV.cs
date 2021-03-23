using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.Extensions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs
{
    public class MSW_FontSizeOV : BaseViewModel
    {
        private const double MAX_FONT_ZOOM_RATIO = 1.5d;
        private const double MIN_FONT_ZOOM_RATIO = 0.5d;

        private const double BASE_FONT_SMALL = 12d;
        private const double BASE_FONT_XSMALL = 10d;
        private const double BASE_FONT_XXSMALL = 8d;
        private const double BASE_FONT_XXXSMALL = 6d;
        private const double BASE_FONT_REGULAR = 14d;
        private const double BASE_FONT_LARGE = 18d;
        private const double BASE_FONT_XLARGE = 20d;
        private const double BASE_FONT_XXLARGE = 24d;
        private const double BASE_FONT_XXXLARGE = 28d;
        private const double BASE_FONT_XXXXLARGE = 30d;
        private const double BASE_FONT_XXXXXLARGE = 34d;
        private const double BASE_HEADER_FONT = 30d;
        private const double BASE_FLOATING_FONT_LARGE = 22d;

        private double _fontZoomRatio = 1d;
        private tblUser CurrentUser { get { return App.Current.CurrentUser; } }

        public MSW_FontSizeOV(BaseViewModel parentsVM) : base(parentsVM)
        {
            InitFontZoomRatioData();
        }

        private void InitFontZoomRatioData()
        {
            var userData = CurrentUser.GetUserData();
            FontSizeZoomRatio = userData.FontZoomRatio == 0d ? 1d : userData.FontZoomRatio;
        }

        public double FontSizeZoomRatio
        {
            get
            {
                return _fontZoomRatio;
            }
            set
            {
                if (value < MIN_FONT_ZOOM_RATIO)
                {
                    value = MIN_FONT_ZOOM_RATIO;
                }
                else if (value > MAX_FONT_ZOOM_RATIO)
                {
                    value = MAX_FONT_ZOOM_RATIO;
                }

                _fontZoomRatio = value;
            }
        }

        public double FontSizeSmall
        {
            get
            {
                return BASE_FONT_SMALL * FontSizeZoomRatio;
            }
        }

        public double FontSizeXSmall
        {
            get
            {
                return BASE_FONT_XSMALL * FontSizeZoomRatio;
            }
        }
        public double FontSizeXXSmall
        {
            get
            {
                return BASE_FONT_XXSMALL * FontSizeZoomRatio;
            }
        }
        public double FontSizeXXXSmall
        {
            get
            {
                return BASE_FONT_XXXSMALL * FontSizeZoomRatio;
            }
        }
        public double FontSizeRegular
        {
            get
            {
                return BASE_FONT_REGULAR * FontSizeZoomRatio;
            }
        }
        public double FontSizeLarge
        {
            get
            {
                return BASE_FONT_LARGE * FontSizeZoomRatio;
            }
        }
        public double FontSizeXLarge
        {
            get
            {
                return BASE_FONT_XLARGE * FontSizeZoomRatio;
            }
        }
        public double FontSizeXXLarge
        {
            get
            {
                return BASE_FONT_XXLARGE * FontSizeZoomRatio;
            }
        }
        public double FontSizeXXXLarge
        {
            get
            {
                return BASE_FONT_XXXLARGE * FontSizeZoomRatio;
            }
        }
        public double FontSizeXXXXLarge
        {
            get
            {
                return BASE_FONT_XXXXLARGE * FontSizeZoomRatio;
            }
        }
        public double FontSizeXXXXXLarge
        {
            get
            {
                return BASE_FONT_XXXXXLARGE * FontSizeZoomRatio;
            }
        }
        public double HeaderFontSize
        {
            get
            {
                return BASE_HEADER_FONT * FontSizeZoomRatio;
            }
        }
        public double FloatingFontSizeLarge
        {
            get
            {
                return BASE_FLOATING_FONT_LARGE * FontSizeZoomRatio;
            }
        }
    }
}
