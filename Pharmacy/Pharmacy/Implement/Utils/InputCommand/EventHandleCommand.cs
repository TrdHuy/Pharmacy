using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pharmacy.Implement.Utils.InputCommand
{
    public class EventHandleCommand
    {
        private Handle handler;

        public EventHandleCommand(Handle obj)
        {
            handler = obj;
        }

        public void Execute(object sender, EventArgs eventArgs, params object[] parameters)
        {
            handler?.Invoke(sender, eventArgs, parameters);
        }
    }

    public delegate void Handle(object sender, EventArgs e, object paramater);

}
