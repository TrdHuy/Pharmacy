using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.AppImpl.Models.VOs;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM.OVs;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.PSW_BasePageVM;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM
{
    internal class BugReportPageViewModel : PSW_BasePageViewModel
    {
        private static Logger L = new Logger("BugReportPageViewModel");

        private static readonly double MIN_FONT_SIZE = 11d;
        private static readonly Thickness MIN_PADDING = new Thickness(3d);

        private double _textBoxHeightAndFontSizeRaito;
        private double _textBoxHeightAndPaddingRatio;
        private double _minTextBoxHeight = 0d;
        private double _currentFontSize = MIN_FONT_SIZE;
        private Thickness _currentTBPadding = MIN_PADDING;

        public PSW_BRP_ButtonCommandOV ButtonCommandOV { get; set; }
        public PSW_BRP_UserInfoOV UserInfoOV { get; set; }
        public PSW_BRP_ProductInfoOV ProductInfoOV { get; set; }
        public PSW_BRP_IssueInfoOV IssueInfoOV { get; set; }

        [Bindable(true)]
        public double CurrentFontSize
        {
            get
            {
                return _currentFontSize;
            }
            set
            {
                _currentFontSize = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
        public Thickness CurrentBugReportTBPadding
        {
            get
            {
                return _currentTBPadding;
            }
            set
            {
                _currentTBPadding = value;
                InvalidateOwn();
            }
        }

    
      

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new PSW_BRP_ButtonCommandOV(this);
            UserInfoOV = new PSW_BRP_UserInfoOV();
            ProductInfoOV = new PSW_BRP_ProductInfoOV();
            IssueInfoOV = new PSW_BRP_IssueInfoOV();

            RefreshViewModel();
        }

        public override void OnLoaded(object sender)
        {
            base.OnLoaded(sender);
            var bRP = sender as BugReportPage;
            if (bRP != null)
            {
                var fullNameTB = bRP.FindChild<AkerTextBox>("FullNameTextBox");
                _minTextBoxHeight = fullNameTB?.RenderSize.Height ?? 0d;
                _textBoxHeightAndFontSizeRaito = _minTextBoxHeight / MIN_FONT_SIZE;
                _textBoxHeightAndPaddingRatio = _minTextBoxHeight / MIN_PADDING.Left;
                fullNameTB.SizeChanged += FullNameTBSizeChanged;
            }

            RefreshViewModel();
        }

        private void FullNameTBSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_textBoxHeightAndFontSizeRaito == 0d)
            {
                return;
            }
            CurrentFontSize = e.NewSize.Height / _textBoxHeightAndFontSizeRaito;
            CurrentBugReportTBPadding = new Thickness(e.NewSize.Height / _textBoxHeightAndPaddingRatio);
        }

        public override void RefreshViewModel()
        {
            UserInfoOV.RefreshViewModel();
            ProductInfoOV.RefreshViewModel();
            IssueInfoOV.RefreshViewModel();

            base.RefreshViewModel();
        }

    }
}
