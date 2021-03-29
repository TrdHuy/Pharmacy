using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pharmacy.Implement.Utils.InputCommand
{
    public class CommandExecuterModel : ICommand, IDestroyable, ICanelable
    {
        public event EventHandler CanExecuteChanged;
        public event NotifyIsCompletedChangedHandler CompletedChanged;
        public event NotifyIsCanceledChangedHandler CanceledChanged;

        private Func<object, ICommandExecuter> _action;
        private ICommandExecuter _commandExecuterCache;

        public CommandExecuterModel(Func<object, ICommandExecuter> hpssAction)
        {
            _action = hpssAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_commandExecuterCache != null)
            {
                _commandExecuterCache.IsCompletedChanged -= OnCommandExecuterCompletedChanged;
                _commandExecuterCache.IsCanceledChanged -= OnCommandExecuterCanceledChanged;
            }

            _commandExecuterCache = _action?.Invoke(parameter);
            
            if (_commandExecuterCache != null)
            {
                _commandExecuterCache.IsCompletedChanged += OnCommandExecuterCompletedChanged;
                _commandExecuterCache.IsCanceledChanged += OnCommandExecuterCanceledChanged;
            }
        }

        private void OnCommandExecuterCompletedChanged(object sender, ExecuterStatusArgs arg)
        {
            CompletedChanged?.Invoke(sender, arg);
        }

        private void OnCommandExecuterCanceledChanged(object sender, ExecuterStatusArgs arg)
        {
            CanceledChanged?.Invoke(sender, arg);
        }

        public void OnDestroy()
        {
            if (_commandExecuterCache != null)
            {
                _commandExecuterCache.OnDestroy();
            }
        }

        public void OnCancel()
        {
            if (_commandExecuterCache != null)
            {
                _commandExecuterCache.OnCancel();
            }
        }

        public bool IsCompleted
        {
            get
            {
                return _commandExecuterCache == null ? throw new NullReferenceException("Current cache is null") : _commandExecuterCache.IsCompleted;
            }
        }

        public bool IsCaneled
        {
            get
            {
                return _commandExecuterCache == null ? throw new NullReferenceException("Current cache is null") : _commandExecuterCache.IsCanceled;
            }
        }
    }
}
