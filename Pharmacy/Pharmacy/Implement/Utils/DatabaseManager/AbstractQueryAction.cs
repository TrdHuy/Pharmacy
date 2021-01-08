using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager
{
    public enum ImageType
    {
        User = 1,
        Customer = 2,
        Medicine = 3
    }
    public class AbstractQueryAction
    {
        protected QueryExecute _action;

        public AbstractQueryAction()
        {
        }

        public AbstractQueryAction(QueryExecute queryAction)
        {
            _action = queryAction;
        }

        public virtual bool CanExecute(object[] paramaters)
        {
            return true;
        }

        public virtual SQLQueryResult Execute(PharmacyDBContext appDBContext, object[] paramaters)
        {
            if (CanExecute(paramaters))
            {
                return _action?.Invoke(appDBContext, paramaters);
            }
            else
            {
                return new SQLQueryResult(null, MessageQueryResult.Non);
            }
        }

        protected void HandleDbEntityValidationException(DbEntityValidationException e)
        {
            //Should implement log writer here for debug purpose
            foreach (var eve in e.EntityValidationErrors)
            {
                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
        }

        protected void ShowErrorMessageBox(Exception e)
        {
            App.Current.ShowApplicationMessageBox(e.Message,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Lỗi!!");
        }

        protected bool SaveImageToFile(string imageName, string imageFolder, ImageType imageType)
        {
            if (!String.IsNullOrEmpty(imageFolder))
            {
                try
                {
                    Bitmap bit = (Bitmap)Image.FromFile(imageFolder);
                    switch (imageType)
                    {
                        case ImageType.User:
                            FileIOUtil.SaveUserImageFile(imageName, bit);
                            break;
                        case ImageType.Customer:
                            FileIOUtil.SaveCustomerImageFile(imageName, bit);
                            break;
                        case ImageType.Medicine:
                            break;
                    }
                }
                catch
                {
                    App.Current.ShowApplicationMessageBox("Lỗi thêm ảnh đại diện của khách hàng, vui lòng kiểm tra lại!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                        OwnerWindow.MainScreen,
                        "Lỗi!");
                    return false;
                }

            }

            return true;
        }
    }

    public delegate SQLQueryResult QueryExecute(PharmacyDBContext appDBContext, object[] paramaters);
}
