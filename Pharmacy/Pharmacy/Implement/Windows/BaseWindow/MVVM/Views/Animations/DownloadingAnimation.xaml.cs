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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pharmacy.Implement.Windows.BaseWindow.MVVM.Views.Animations
{
    /// <summary>
    /// Interaction logic for DownloadingAnimation.xaml
    /// </summary>
    public partial class DownloadingAnimation : UserControl
    {
        private static double[] STATIC_POS = { 200, 360, 460, 520, 525 };
        private double CacheDistance;
        public DownloadingAnimation()
        {
            InitializeComponent();
            MainGrid.SizeChanged += OnMainGridSizeChanged;
            this.IsVisibleChanged += VisibilityChanged;

        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                SetUpAnimation();
            }
        }

        private void OnMainGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Visibility != Visibility.Visible)
            {
                return;
            }
            SetUpAnimation();
        }

        private void SetUpAnimation()
        {
            var distance = MainGrid.ActualWidth - PurpleTangle.ActualWidth;
            if(distance == CacheDistance)
            {
                return;
            }
            CacheDistance = distance;

            TranslateTransform translateTransform2 =
                new TranslateTransform();
            TranslateTransform translateTransform21 =
                new TranslateTransform();
            TranslateTransform translateTransform22 =
                new TranslateTransform();


            PurpleTangle.RenderTransform = translateTransform2;
            PurpleTangle2.RenderTransform = translateTransform21;
            PurpleTangle3.RenderTransform = translateTransform22;

            DoubleAnimationUsingKeyFrames transformAnimation =
                new DoubleAnimationUsingKeyFrames()
                {
                    Duration = TimeSpan.FromSeconds(2),
                    RepeatBehavior = RepeatBehavior.Forever,
                    BeginTime = TimeSpan.FromSeconds(0)
                };

            DoubleAnimationUsingKeyFrames transformAnimation21 =
                new DoubleAnimationUsingKeyFrames()
                {
                    Duration = TimeSpan.FromSeconds(2),
                    RepeatBehavior = RepeatBehavior.Forever,
                    BeginTime = TimeSpan.FromSeconds(0.3)
                };

            DoubleAnimationUsingKeyFrames transformAnimation22 =
                new DoubleAnimationUsingKeyFrames()
                {
                    Duration = TimeSpan.FromSeconds(2),
                    RepeatBehavior = RepeatBehavior.Forever,
                    BeginTime = TimeSpan.FromSeconds(0.6)
                };

            var poss = CaculatePoint2(MainGrid.ActualWidth - PurpleTangle.ActualWidth);

            for (int i = 0; i < STATIC_POS.Length; i++)
            {
                double percent = Convert.ToDouble(i + 1) / Convert.ToDouble(STATIC_POS.Length);
                transformAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(poss[i], KeyTime.FromPercent(percent)));
                transformAnimation21.KeyFrames.Add(new LinearDoubleKeyFrame(poss[i], KeyTime.FromPercent(percent)));
                transformAnimation22.KeyFrames.Add(new LinearDoubleKeyFrame(poss[i], KeyTime.FromPercent(percent)));
            }

            translateTransform2.BeginAnimation(TranslateTransform.XProperty, transformAnimation);
            translateTransform21.BeginAnimation(TranslateTransform.XProperty, transformAnimation21);
            translateTransform22.BeginAnimation(TranslateTransform.XProperty, transformAnimation22);
        }

        private double[] CaculatePoint2(double distance)
        {
            var len = STATIC_POS.Length;
            var ratio = distance / STATIC_POS[len - 1];
            var res = new double[len];

            for (int i = 0; i < len; i++)
            {
                res[i] = STATIC_POS[i] * ratio;
            }
            return res;
        }

    }
}
