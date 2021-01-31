using Pharmacy.Base.Converter;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using static HPSolutionCCDevPackage.netFramework.AtumImageView;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PageOverviewInfo.xaml
    /// </summary>
    public partial class PageOverviewInfo : UserControl
    {

        #region CameraButtonVisibility
        public static readonly DependencyProperty CamButVisibilityProperty =
                DependencyProperty.Register(
                        "CamButVisibility",
                        typeof(Visibility),
                        typeof(PageOverviewInfo),
                        new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(CamButVisibilityChangedCallBack)));

        private static void CamButVisibilityChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public Visibility CamButVisibility
        {
            get { return (Visibility)GetValue(CamButVisibilityProperty); }
            set
            {
                SetValue(CamButVisibilityProperty, value);
            }
        }
        #endregion

        #region CameraButtonCommand
        public static readonly DependencyProperty CamButCmdProperty =
                DependencyProperty.Register(
                        "CamButCmd",
                        typeof(ICommand),
                        typeof(PageOverviewInfo),
                        new PropertyMetadata(null, new PropertyChangedCallback(CamButCmdChangedCallBack)));

        private static void CamButCmdChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public ICommand CamButCmd
        {
            get { return (ICommand)GetValue(CamButCmdProperty); }
            set { SetValue(CamButCmdProperty, value); }
        }
        #endregion

        #region CamTip
        public static readonly DependencyProperty CamTipProperty =
                DependencyProperty.Register(
                        "CamTip",
                        typeof(string),
                        typeof(PageOverviewInfo),
                        new FrameworkPropertyMetadata(
                                "",
                                null,
                                null),
                        null);


        public string CamTip
        {
            get { return (string)GetValue(CamTipProperty); }
            set { SetValue(CamTipProperty, value); }
        }
        #endregion

        #region MainTiltle
        public static readonly DependencyProperty MainTiltleProperty =
                DependencyProperty.Register(
                        "MainTiltle",
                        typeof(string),
                        typeof(PageOverviewInfo),
                        new FrameworkPropertyMetadata(
                                default(string),
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public string MainTiltle
        {
            get { return (string)GetValue(MainTiltleProperty); }
            set { SetValue(MainTiltleProperty, value); }
        }
        #endregion

        #region SubTiltle
        public static readonly DependencyProperty SubTiltleProperty =
                DependencyProperty.Register(
                        "SubTiltle",
                        typeof(string),
                        typeof(PageOverviewInfo),
                        new FrameworkPropertyMetadata(
                                default(string),
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public string SubTiltle
        {
            get { return (string)GetValue(SubTiltleProperty); }
            set { SetValue(SubTiltleProperty, value); }
        }
        #endregion

        #region SubTiltle2nd
        public static readonly DependencyProperty SubTiltle2ndProperty =
                DependencyProperty.Register(
                        "SubTiltle2nd",
                        typeof(string),
                        typeof(PageOverviewInfo),
                        new FrameworkPropertyMetadata(
                                default(string),
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public string SubTiltle2nd
        {
            get { return (string)GetValue(SubTiltle2ndProperty); }
            set { SetValue(SubTiltle2ndProperty, value); }
        }
        #endregion

        #region AvatarSource
        public static readonly DependencyProperty AvatarSourceProperty =
                DependencyProperty.Register(
                        "AvatarSource",
                        typeof(ImageSource),
                        typeof(PageOverviewInfo),
                        new FrameworkPropertyMetadata(
                                default(ImageSource),
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public ImageSource AvatarSource
        {
            get { return (ImageSource)GetValue(AvatarSourceProperty); }
            set { SetValue(AvatarSourceProperty, value); }
        }
        #endregion

        #region IsSupportChangeImageLocation
        public static readonly DependencyProperty IsSupportChangeImageLocationProperty =
                DependencyProperty.Register(
                        "IsSupportChangeImageLocation",
                        typeof(bool),
                        typeof(PageOverviewInfo),
                        new PropertyMetadata(default(bool)));

        public bool IsSupportChangeImageLocation
        {
            get { return (bool)GetValue(IsSupportChangeImageLocationProperty); }
            set { SetValue(IsSupportChangeImageLocationProperty, value); }
        }
        #endregion

        #region IsSupportChangeImageZoom
        public static readonly DependencyProperty IsSupportChangeImageZoomProperty =
                DependencyProperty.Register(
                        "IsSupportChangeImageZoom",
                        typeof(bool),
                        typeof(PageOverviewInfo),
                        new PropertyMetadata(default(bool)));

        public bool IsSupportChangeImageZoom
        {
            get { return (bool)GetValue(IsSupportChangeImageZoomProperty); }
            set { SetValue(IsSupportChangeImageZoomProperty, value); }
        }
        #endregion

        #region IsSupportLocatorWindow
        public static readonly DependencyProperty IsSupportLocatorWindowProperty =
                DependencyProperty.Register(
                        "IsSupportLocatorWindow",
                        typeof(bool),
                        typeof(PageOverviewInfo),
                        new PropertyMetadata(default(bool)));

        public bool IsSupportLocatorWindow
        {
            get
            {
                return (bool)GetValue(IsSupportLocatorWindowProperty);
            }
            set
            {
                SetValue(IsSupportLocatorWindowProperty, value);
            }
        }
        #endregion

        #region AtumImageData
        public static readonly DependencyProperty AtumImageDataProperty =
                DependencyProperty.Register(
                        "AtumImageData",
                        typeof(AtumUserData),
                        typeof(PageOverviewInfo),
                        new PropertyMetadata(default(AtumUserData)));

        public AtumUserData AtumImageData
        {
            get
            {
                return (AtumUserData)GetValue(AtumImageDataProperty);
            }
            set
            {
                SetValue(AtumImageDataProperty, value);
            }
        }
        #endregion

        public PageOverviewInfo()
        {
            InitializeComponent();
            SizeChanged += PageOverViewInfoSizeChange;
        }

        private void PageOverViewInfoSizeChange(object sender, SizeChangedEventArgs e)
        {
            if (AvatarGrid.RenderSize.Width >= AvatarGrid.RenderSize.Height)
            {
                Avatar.Width = AvatarGrid.ActualHeight;
            }
            else
            {
                Avatar.Width = AvatarGrid.ActualWidth;
            }
        }
    }

}
