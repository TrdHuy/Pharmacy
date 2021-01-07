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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PageOverviewInfo.xaml
    /// </summary>
    public partial class PageOverviewInfo : UserControl
    {
        #region FileName
        public static readonly DependencyProperty FileNameProperty =
                DependencyProperty.Register(
                        "FileName",
                        typeof(string),
                        typeof(PageOverviewInfo),
                        new FrameworkPropertyMetadata(
                                "",
                                null,
                                new CoerceValueCallback(FileNameCoreceValueCallBack)),
                        null);

        private static object FileNameCoreceValueCallBack(DependencyObject d, object baseValue)
        {
            PageOverviewInfo ctrl = d as PageOverviewInfo;
            string imageName = baseValue.ToString();
            ctrl.AvatarSource = 
                FileIOUtil.GetBitmapFromName(imageName, ctrl.FolderName).
                ToImageSource();
            return baseValue;
        }

        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }
        #endregion

        #region FolderName
        public static readonly DependencyProperty FolderNameProperty =
                DependencyProperty.Register(
                        "FolderName",
                        typeof(string),
                        typeof(PageOverviewInfo),
                        new FrameworkPropertyMetadata(
                                "",
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public string FolderName
        {
            get { return (string)GetValue(FolderNameProperty); }
            set { SetValue(FolderNameProperty, value); }
        }
        #endregion

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

        public PageOverviewInfo()
        {
            InitializeComponent();
        }

        private void ImageGridContainerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid ctrl = (Grid)sender;
            if (AvatarBoder.RenderSize.Width >= AvatarBoder.RenderSize.Height)
            {
                ctrl.Width = AvatarBoder.RenderSize.Height;
            }
            else
            {
                ctrl.Width = AvatarBoder.RenderSize.Width;
            }
        }
    }

}
