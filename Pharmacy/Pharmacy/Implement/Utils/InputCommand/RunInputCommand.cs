using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pharmacy.Implement.Utils.InputCommand
{
    public class RunInputCommand : ICommand, IDestroyable
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> actionObj;
        private Action destroyAction;

        public RunInputCommand(Action<object> obj, Action destroy = null)
        {
            actionObj = obj;
            destroyAction = destroy;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            actionObj?.Invoke(parameter);
        }

        public void OnDestroy()
        {
            destroyAction?.Invoke();
        }
    }
}
