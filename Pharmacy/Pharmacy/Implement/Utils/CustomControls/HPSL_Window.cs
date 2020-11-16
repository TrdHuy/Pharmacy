using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Pharmacy.Implement.Utils.CustomControls
{
    public class HPSL_Window : Window
    {
        public static readonly DependencyProperty TitleBarColorProperty =
            DependencyProperty.Register("TitleBarColor", typeof(Color), typeof(HPSL_Window),
              new FrameworkPropertyMetadata(
                    Colors.Black,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    OnTitleBarColorChange));

        public static readonly DependencyProperty TitleBarHeightProperty =
            DependencyProperty.Register("TitleBarHeight", typeof(Double), typeof(HPSL_Window),
                new PropertyMetadata(42.0));

        public static readonly DependencyProperty IsEnableMenuTabProperty =
           DependencyProperty.Register("IsEnableMenuTab", typeof(bool), typeof(HPSL_Window),
               new PropertyMetadata(false));

        public static readonly DependencyProperty MenuTabWidthProperty =
            DependencyProperty.Register("MenuTabWidth", typeof(double), typeof(HPSL_Window),
              new PropertyMetadata(0.0));

        public static readonly DependencyProperty MenuTabExpandedWidthProperty =
            DependencyProperty.Register("MenuTabExpandedWidth", typeof(double), typeof(HPSL_Window),
              new PropertyMetadata(0.0));

        public static readonly DependencyProperty TitleBarBackgroundProperty =
            DependencyProperty.Register("TitleBarBackground", typeof(Brush), typeof(HPSL_Window),
                new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty MenuTabContentProperty =
            DependencyProperty.Register("MenuTabContent", typeof(object), typeof(HPSL_Window), new UIPropertyMetadata(null));

        //public static readonly DependencyProperty CloseCommandProperty =
        //    DependencyProperty.Register("CloseCommand", typeof(RunInputCommand), typeof(HPSL_Window));

        //public static readonly DependencyProperty MinimizeCommandProperty =
        //    DependencyProperty.Register("MinimizeCommand", typeof(RunInputCommand), typeof(HPSL_Window));

        public HPSL_Window() : base()
        {
            this.DefaultStyleKey = typeof(HPSL_Window);
            CloseCommand = new RunInputCommand(CloseCommandExecute);
            MinimizeCommand = new RunInputCommand(MinimizeCommandExecute);
            MaximizeCommand = new RunInputCommand(MaximizeCommandExecute);
        }

        public ICommand CloseCommand
        {
            get; set;
        }
        public ICommand MinimizeCommand
        {
            get; set;
        }
        public ICommand MaximizeCommand
        {
            get; set;
        }
        public Color TitleBarColor
        {
            get { return (Color)GetValue(TitleBarColorProperty); }
            set { SetValue(TitleBarColorProperty, value); }
        }
        public Double TitleBarHeight
        {
            get { return (Double)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }
        public Brush TitleBarBackground
        {
            get { return (Brush)GetValue(TitleBarBackgroundProperty); }
            set { SetValue(TitleBarBackgroundProperty, value); }
        }
        public bool IsEnableMenuTab
        {
            get { return (bool)GetValue(IsEnableMenuTabProperty); }
            set { SetValue(IsEnableMenuTabProperty, value); }
        }
        public Double MenuTabWidth
        {
            get { return (Double)GetValue(MenuTabWidthProperty); }
            set { SetValue(MenuTabWidthProperty, value); }
        }

        public Double MenuTabExpandedWidth
        {
            get { return (Double)GetValue(MenuTabExpandedWidthProperty); }
            set { SetValue(MenuTabExpandedWidthProperty, value); }
        }

        public object MenuTabContent
        {
            get { return GetValue(MenuTabContentProperty); }
            set { SetValue(MenuTabContentProperty, value); }
        }

        private static void OnMenuTabWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid MenuTab = (d as Grid);
            int a = 1;
        }

        private static void OnTitleBarColorChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as HPSL_Window).TitleBarBackground = new SolidColorBrush((Color)e.NewValue);
        }

        private void CloseCommandExecute(object obj)
        {
            this.Close();
        }

        private void MinimizeCommandExecute(object obj)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeCommandExecute(object obj)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }
    }

    //[TemplatePart(Name ="PART_AlternativeContentPresenter", Type = typeof(ContentPresenter))]
    //public class EnhancedItemControl : ItemsControl
    //{
    //    public object AlternativeContent
    //    {
    //        get { return (object)GetValue(AlternativeContentProperty); }
    //        set { SetValue(AlternativeContentProperty, value); }
    //    }

    //    public static readonly DependencyProperty AlternativeContentProperty =
    //        DependencyProperty.Register("AlternativeContent", typeof(object), typeof(EnhancedItemControl), new UIPropertyMetadata(null));

    //    static EnhancedItemControl()
    //    {
    //        DefaultStyleKeyProperty.OverrideMetadata(
    //            typeof(EnhancedItemControl)
    //            , new FrameworkPropertyMetadata(typeof(EnhancedItemControl)));
    //    }

    //    public override void OnApplyTemplate()
    //    {
    //        base.OnApplyTemplate();
    //    }
    //}
}
