using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.UserControls.MainMenuControl.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.UserControls
{
    internal class MainMenuControlViewModel : BaseViewModel
    {
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        public MSW_MMC_ButtonCommandOV ButtonCommandOV { get; set; }

        public MainMenuControlViewModel()
        {
            ButtonCommandOV = new MSW_MMC_ButtonCommandOV(this);
        }
    }
}
