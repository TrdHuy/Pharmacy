using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
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
    public class CustomerTransactionHistoryPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("CustomerTransactionHistoryPageViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private tblOrder _currentSelectedOrder;

        public ObservableCollection<tblOrder> OrderItemSource { get; set; }
        public RunInputCommand DebtsDisplayButtonCommand { get; set; }
        public RunInputCommand BillDisplayButtonCommand { get; set; }
        public RunInputCommand ReturnButtonCommand { get; set; }

        public tblCustomer CurrentModifiedCustomer { get; set; }
        public tblOrder CurrentSelectedOrder
        {
            get
            {
                return _currentSelectedOrder;
            }
            set
            {
                _currentSelectedOrder = value;
                InvalidateOwn();
                Invalidate("CurrentSelectedOrderDetails");
            }
        }

        public string CurrentSelectedOrderDetails
        {
            get
            {
                string orderDetails = "";
                if (CurrentSelectedOrder != null)
                {
                    var listDetails = CurrentSelectedOrder.tblOrderDetails;
                    //var tabPos = GetTabPosAsString(ComputeTabPos());
                    //var stringFormat = "{0," + tabPos[0] + "}" +
                    //    "{1," + tabPos[1] + "}" +
                    //    "{2," + tabPos[2] + "} " +
                    //    "{3}";

                    orderDetails += String.Join("\n", listDetails.Where((content) => content.IsActive).
                        Select((content, a) =>
                            //String.Format(stringFormat
                            //, content.tblMedicine.MedicineName
                            //, content.Quantity
                            //, content.tblMedicine.tblMedicineUnit.MedicineUnitName
                            //, content.TotalPrice)
                            content.tblMedicine.MedicineName + " - " + content.Quantity
                            + " " + content.tblMedicine.tblMedicineUnit.MedicineUnitName + ": " + ((decimal)content.Quantity * content.UnitPrice).ToString(@"#\,##0 VND")
                        ));

                    logger.D("Order detail format: " + orderDetails);
                }

                return orderDetails;
            }
        }


        // Temp save this method to handle tab stop position
        private int[] ComputeTabPos()
        {
            var offsetSpaceP1 = 5;
            var offsetSpace = 3;

            var listDetails = CurrentSelectedOrder.tblOrderDetails;
            var pos1 = listDetails?.Aggregate((o1, o2) => o1.tblMedicine.MedicineName.Length
            > o2.tblMedicine.MedicineName.Length ? o1 : o2).
            tblMedicine.MedicineName.Length;

            pos1 += offsetSpaceP1;

            var pos2 = listDetails?.Aggregate((o1, o2) => o1.Quantity.ToString().Length
            > o2.Quantity.ToString().Length ? o1 : o2).
            Quantity.ToString().Length;

            pos2 += offsetSpace;

            var pos3 = listDetails?.Aggregate((o1, o2) => o1.tblMedicine.tblMedicineUnit.MedicineUnitName.Length
            > o2.tblMedicine.tblMedicineUnit.MedicineUnitName.Length ? o1 : o2).
            tblMedicine.tblMedicineUnit.MedicineUnitName.Length;

            pos3 += offsetSpace;

            var resPos = new int[] { pos1 ?? 0, pos2 ?? 0, pos3 ?? 0 };

            logger.V("space lenght: pos1 = " + pos1 + " pos2 = " + pos2 + " pos3 = " + pos3);

            return resPos;
        }

        private string[] GetTabPosAsString(int[] tabPos)
        {
            var lenght = tabPos.Length;
            var resPos = new string[lenght];
            for (int i = 0; i < lenght; i++)
            {
                // alight to the right, hence must concat char '-'
                resPos[i] = "-" + tabPos[i] + "";
            }
            return resPos;
        }

        protected override void InitPropertiesRegistry()
        {
        }

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CurrentModifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;
            InstantiateItems();
            DebtsDisplayButtonCommand = new RunInputCommand(OnDebtsDisplayButtonClickEvent);
            ReturnButtonCommand = new RunInputCommand(OnReturnButtonClickEvent);
            BillDisplayButtonCommand = new RunInputCommand(OnBillDisplayButtonClickEvent);
        }

        protected override void OnInitialized()
        {
        }

        private void InstantiateItems()
        {
            OrderItemSource = new ObservableCollection<tblOrder>();
            foreach (tblOrder order in CurrentModifiedCustomer.tblOrders)
            {
                if (order.IsActive)
                {
                    OrderItemSource.Add(order);
                }
            }

        }

        private void OnDebtsDisplayButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_DEBTS_BUTTON
                , dataTransfer);
        }


        private void OnReturnButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_RETURN_BUTTON
                , dataTransfer);
        }

        private void OnBillDisplayButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_BILL_BUTTON
                , dataTransfer);
        }



    }
}
