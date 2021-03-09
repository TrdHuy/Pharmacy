using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Windows.BaseWindow.MVVM.Models.VOs;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Models.VOs;
using Pharmacy.Implement.Windows.PopupScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels
{
    public class PopupScreenWindowViewModel : BaseViewModel
    {
        private object _content;
        private double _titleBarHeight;
        private double _height;
        private double _width;
        private PSW_ContentVO _psw_ContentVO;

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

        public long PageLoadingDelayTime { get; set; }

        public double TitleBarHeight
        {
            get
            {
                return _titleBarHeight;
            }
            set
            {
                _titleBarHeight = value;
                MeasureWindowSize();
                InvalidateOwn();
            }
        }
        public double WindowHeight
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                MeasureWindowSize();
                InvalidateOwn();
            }
        }
        public double WindowWidth
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                MeasureWindowSize();
                InvalidateOwn();
            }
        }

        public PopupScreenWindowViewModel(PSW_ContentVO contentVO, long pageLoadingDelaytime)
        {
            _psw_ContentVO = contentVO;
            _titleBarHeight = 30d;

            _content = contentVO.Content;
            PageLoadingDelayTime = pageLoadingDelaytime;

            MeasureWindowSize();
        }

        private void MeasureWindowSize()
        {
            if(_psw_ContentVO != null)
            {
                _height = _psw_ContentVO.DesignHeight + _titleBarHeight;
                _width = _psw_ContentVO.DesignWidth;
            }
            
        }
    }
}
