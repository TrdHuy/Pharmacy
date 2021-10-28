using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage.OVs
{
    public class MSW_SP_OrderDetailOV : OrderDetailOV
    {
        public MSW_SP_OrderDetailOV(BaseViewModel parent) : base(parent) { }

        protected override bool ShouldUpdateQuantity(string quantityToString)
        {
            try
            {
                var quantityLeft = 0d;
                var inputQuantity = Convert.ToDouble(quantityToString);
                var useMaxQuantity = false;

                if (inputQuantity < 0) return false;

                var queryObserver = new SQLQueryCustodian((res) =>
                {
                    quantityLeft = Convert.ToDouble(res.Result);
                    var tmp = (ParentsModel as SellingPageViewModel).CustomerOrderDetailItemSource.Where(o => o.MedicineID == MedicineID).FirstOrDefault();
                    if (tmp != null)
                    {
                        quantityLeft -= tmp.Quantity;
                    }
                });
                DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_MEDICINE_QUANTITY,
                     queryObserver,
                     Medicine);

                var optsSource = new List<OsirisButton>();

                if (inputQuantity > quantityLeft && quantityLeft >= 0)
                {
                    optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm tối đa" });
                    optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm mong muốn (trong kho sẽ bị âm)" });
                    optsSource.Add(new OsirisButton() { TextContent = "Hủy" });

                    var result = App.Current.ShowApplicationMultiOptionMessageBox((quantityLeft <= 0 ? "Sản phẩm này trong kho đã hết (hoặc bị âm) " : "Sản phẩm này trong kho còn")
                        + quantityLeft + "("
                        + MedicineUnitName + ")\n"

                        + "Bạn có muốn tiếp tục nhập?",
                    optsSource,
                    AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                    if (result == 2 || result == -1)
                    {
                        return false;
                    }
                    else if (result == 0)
                    {
                        useMaxQuantity = true;
                    }
                }
                else if (quantityLeft < 0)
                {
                    optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm mong muốn (trong kho sẽ bị âm)" });
                    optsSource.Add(new OsirisButton() { TextContent = "Hủy" });

                    var result = App.Current.ShowApplicationMultiOptionMessageBox((quantityLeft <= 0 ? "Sản phẩm này trong kho đã hết (hoặc bị âm) " : "Sản phẩm này trong kho còn")
                        + quantityLeft + "("
                        + MedicineUnitName + ")\n"

                        + "Bạn có muốn tiếp tục nhập?",
                    optsSource,
                    AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                    if (result == 1 || result == -1)
                    {
                        return false;
                    }
                }

                _quantityToString = useMaxQuantity ? quantityLeft.ToString() : quantityToString;
                try
                {
                    Quantity = Convert.ToDouble(_quantityToString);
                }
                catch
                {
                    Quantity = 0d;
                }
                Invalidate("QuantityToString");

                return false;
            }
            catch (Exception e)
            {
                return base.ShouldUpdateQuantity(quantityToString);
            }
        }
    }
}
