using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pharmacy.Implement.Utils.InputCommand
{
    public class RunInputCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> actionObj;

        public RunInputCommand(Action<object> obj)
        {
            actionObj = obj;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            actionObj?.Invoke(parameter);
        }
    }
}
