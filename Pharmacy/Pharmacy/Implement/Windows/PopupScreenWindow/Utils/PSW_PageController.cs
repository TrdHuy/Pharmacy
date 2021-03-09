using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.MVVM.Models.VOs;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.Utils
{
    public class PSW_PageController : BasePageController
    {
        private static PSW_PageController _instance;
        public static PSW_PageController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PSW_PageController();
                }
                return _instance;
            }
        }

        private Lazy<PageVO> DailyReportDetailPage = new Lazy<PageVO>(() =>
          new PageVO(
              new Uri(PharmacyDefinitions.DAILY_REPORT_DETAIL_URI_ORIGINAL_STRING, UriKind.Relative),
              PharmacyDefinitions.DAILY_REPORT_DETAIL_PAGE_LOADING_DELAY_TIME));


        private PSW_PageController()
        {
            CurrentPageOV = new PageVO(NonePage.Value.PageUri,
               NonePage.Value.LoadingDelayTime);
        }

        public override void UpdateCurrentPageSource(PageSource pageSource)
        {
            PreviousePageSource = CurrentPageSource;
            CurrentPageSource = pageSource;

            switch (pageSource)
            {
                case PageSource.DAILY_REPORT_DETAIL_PAGE:
                    CurrentPageOV.PageUri = DailyReportDetailPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = DailyReportDetailPage.Value.LoadingDelayTime;
                    break;
                default:
                    CurrentPageOV.PageUri = NonePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = NonePage.Value.LoadingDelayTime;
                    break;
            }
            NotifyChange(CurrentPageOV);
        }

        public override void UpdatePageOVUri(Uri uri)
        {
            var x = "/" + uri.OriginalString;
            PreviousePageSource = CurrentPageSource;
            switch (x)
            {
                case PharmacyDefinitions.DAILY_REPORT_DETAIL_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.DAILY_REPORT_DETAIL_PAGE;
                    CurrentPageOV.PageUri = DailyReportDetailPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = DailyReportDetailPage.Value.LoadingDelayTime;
                    break;
                default:
                    CurrentPageSource = PageSource.NONE;
                    CurrentPageOV.PageUri = NonePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = NonePage.Value.LoadingDelayTime;
                    break;
            }

            NotifyChange(CurrentPageOV);
        }
    }
}
