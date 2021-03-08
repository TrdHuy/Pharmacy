using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.CustomControls;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.BaseWindow.MVVM.Models.VOs;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels
{
    public class MainScreenWindowViewModel : BaseViewModel
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private PageSourceWatcher _pageSourceWatcher;

        public EventHandleCommand PagePreviewNavigateEventCommand { get; set; }

        public Uri CurrentPageSource
        {
            get
            {
                return _pageHost.CurrentPageOV.PageUri;
            }
            set
            {
                _pageHost.UpdatePageOVUri(value);
            }
        }

        public long PageLoadingDelayTime
        {
            get
            {
                return _pageHost.CurrentPageOV.LoadingDelayTime;
            }
        }

        public DashboardWindowContentType ContentType
        {
            get
            {
                return RUNE.IS_SUPPORT_WINDOW_NAVIGATION_BUTTON_PANEL ?
                    DashboardWindowContentType.PageType : DashboardWindowContentType.NormalContentType;
            }
        }

        protected override void InitPropertiesRegistry()
        {
            PropRegister("PageLoadingDelayTime");
            PropRegister("CurrentPageSource");
        }

        public MainScreenWindowViewModel()
        {
            _pageSourceWatcher = new PageSourceWatcher(OnPageSourceChange);
            _pageHost.Subcribe(_pageSourceWatcher);
            PagePreviewNavigateEventCommand = new EventHandleCommand(OnPagePreviewNavigate);
        }

        private void OnPagePreviewNavigate(object sender, EventArgs e, object paramater)
        {
            var x = CurrentPageSource.OriginalString;

            switch (x)
            {
                case PharmacyDefinitions.SELLING_PAGE_URI_ORIGINAL_STRING:

                    if (RUNE.IS_SUPPORT_WINDOW_NAVIGATION_BUTTON_PANEL)
                    {
                        SQLQueryCustodian.DeactiveAllRegistrationsOfType(typeof(MSW_SP_AddOrderDetailAction));
                    }

                    // Disable navigate when the call back from this action be not executed
                    if (!SQLQueryCustodian.IsAllCallbackHandled(typeof(MSW_SP_InstantiateNewOrderAction)) &&
                        RUNE.IS_SUPPORT_WINDOW_NAVIGATION_BUTTON_PANEL)
                    {
                        // Stop navigating
                        (e as PreviewPageNavigateArgs).Handled = true;

                        App.Current.ShowApplicationMessageBox("Vui lòng chờ hoàn tất thao tác!",
                            HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                            HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                            OwnerWindow.MainScreen,
                            "Thông báo!!");
                    }
                    break;
                default:

                    // Disable navigate when the call back from this action be not executed
                    if (!SQLQueryCustodian.IsAllCallbackHandled(typeof(SQLQueryCustodian)) &&
                        RUNE.IS_SUPPORT_WINDOW_NAVIGATION_BUTTON_PANEL)
                    {
                        // Stop navigating
                        (e as PreviewPageNavigateArgs).Handled = true;

                        App.Current.ShowApplicationMessageBox("Vui lòng chờ hoàn tất thao tác!",
                            HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                            HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                            OwnerWindow.MainScreen,
                            "Thông báo!!");
                    }
                    break;
            }
        }

        private void OnPageSourceChange(PageVO newSource)
        {
            Invalidate("PageLoadingDelayTime");
            Invalidate("CurrentPageSource");

            //Rollback the manipulation to the entity data
            DbManager.Instance.RollBack();

            //Every time navigate to new source, the view model of those source page will be instantiated again
            //_mainScreenWindow.Navigate(newSource);
        }

    }

    internal class PageSourceWatcher : Base.Observable.ObserverPattern.IObserver<PageVO>
    {
        private Action<PageVO> OnPageSourceChange;

        internal PageSourceWatcher(Action<PageVO> onSourceChange)
        {
            OnPageSourceChange = onSourceChange;
        }
        public void Update(PageVO value)
        {
            OnPageSourceChange?.Invoke(value);
        }
    }
}
