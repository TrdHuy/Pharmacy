using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.HttpServices.AzureFunctions
{
    public class HPSSBodyRequest : IHPSSRequestBody
    {
        private HPSSCustomerRequestID _requestID = HPSSCustomerRequestID.PharmarcyPackage_CheckAppInfo;

        public HPSSCustomerRequestID RequestID
        {
            get
            {
                return _requestID;
            }
            set
            {
                _requestID = value;
            }
        }

    }
}
