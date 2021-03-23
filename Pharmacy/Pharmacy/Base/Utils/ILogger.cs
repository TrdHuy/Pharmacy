using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Utils
{
    public interface ILogger
    {
        void I(string message, string callMemberName = null);

        void D(string message, string callMemberName = null);

        void E(string message, string callMemberName = null);

        void W(string message, string callMemberName = null);

        void F(string message, string callMemberName = null);

        void V(string message, string callMemberName = null);
    }
}
