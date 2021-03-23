using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.HttpServices.AzureFunctions
{
    public interface IHPSSRequestBody
    {
        HPSSCustomerRequestID RequestID { get; set; }
    }

    public enum HPSSCustomerRequestID
    {
        None = 0,
        PharmarcyPackage_CheckAppUpdate = 1,
        PharmarcyPackage_CheckAppInfo = 2,
    }
}
