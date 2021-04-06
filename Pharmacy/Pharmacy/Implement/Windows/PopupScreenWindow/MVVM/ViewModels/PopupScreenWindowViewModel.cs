using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Models.VOs;
using System;
using System.ComponentModel;
using System.Windows;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels
{
    public class PopupScreenWindowViewModel : BaseViewModel
    {
        private object _content;
        private PSW_ContentVO _psw_ContentVO;
        private Uri _currentPageSource;

        [Bindable(true)]
        public object Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
        public Uri CurrentPageSource
        {
            get
            {
                return _currentPageSource;
            }
            set
            {
                _currentPageSource = value;
                InvalidateOwn();
            }
        }
        
        public long PageLoadingDelayTime { get; set; }

        public PopupScreenWindowViewModel(PSW_ContentVO contentVO)
        {
            _psw_ContentVO = contentVO;

            if (contentVO?.Content is Uri)
            {
                CurrentPageSource = contentVO?.Content as Uri;
            }
            else
            {
                Content = contentVO.Content;
            }

            PageLoadingDelayTime = contentVO?.ContentLoadingDelayTime ?? 0;
        }

       
    }
}
