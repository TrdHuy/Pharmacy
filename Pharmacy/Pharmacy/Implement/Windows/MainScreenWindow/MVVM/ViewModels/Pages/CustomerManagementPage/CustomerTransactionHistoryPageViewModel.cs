using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage
{
    public class CustomerTransactionHistoryPageViewModel : AbstractViewModel
    {
        public ObservableCollection<tblOrder> OrderItemSource { get; set; }
        public tblCustomer CurrentModidifiedCustomer { get; set; }

        public EventHandleCommand GridSizeChangedCommand { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }

        public CustomerTransactionHistoryPageViewModel()
        {
            CurrentModidifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;
            GridSizeChangedCommand = new EventHandleCommand(OnGridSizeChangedEvent);

            InstantiateItems();
        }
   
        private void InstantiateItems()
        {
            OrderItemSource = new ObservableCollection<tblOrder>();
            foreach(tblOrder order in CurrentModidifiedCustomer.tblOrders)
            {
                OrderItemSource.Add(order);
            }

            FakeData();
        }

        private void FakeData()
        {
            tblOrder fakeOrder = new tblOrder();
            fakeOrder.CustomerID = 1;
            fakeOrder.IncludeVAT = true;
            fakeOrder.IsActive = true;
            fakeOrder.OrderDescription = "Notification";
            fakeOrder.OrderID = 0;
            fakeOrder.OrderTime = DateTime.Now;
            fakeOrder.PurchasePrice = 300000;
            fakeOrder.TotalPrice = 300000;
            fakeOrder.UserID = "admin";

            OrderItemSource.Add(fakeOrder);
        }

        private void OnGridSizeChangedEvent(object sender, EventArgs e, object paramaters)
        {
            Grid ctrl = (Grid)sender;
            Border avaBorder = (Border)((object[])paramaters)[0];

            if (avaBorder.RenderSize.Width >= avaBorder.RenderSize.Height)
            {
                ctrl.Width = avaBorder.RenderSize.Height;
            }
            else
            {
                ctrl.Width = avaBorder.RenderSize.Width;
            }
        }

    }
}
