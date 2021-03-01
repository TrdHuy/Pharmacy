using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InvoiceManagementPage.OVs
{
    public class MSW_IMP_ButtonCommandOV : BaseViewModel
    {
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;

        public RunInputCommand EditOrderButtonCommand { get; set; }
        public RunInputCommand DeleteOrderButtonCommand { get; set; }

        public MSW_IMP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            EditOrderButtonCommand = new RunInputCommand(EditOrderButtonClickEvent);
            DeleteOrderButtonCommand = new RunInputCommand(DeleteOrderButtonClickEvent);
        }

        private void DeleteOrderButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_IMP_DELETE_BUTTON
                , dataTransfer);
        }

        private void EditOrderButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_IMP_EDIT_BUTTON
                , dataTransfer);
        }
    }
}
