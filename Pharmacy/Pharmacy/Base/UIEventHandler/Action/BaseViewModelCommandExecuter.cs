using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public abstract class BaseViewModelCommandExecuter : ICommandExecuter
    {
        protected virtual BaseViewModel ViewModel { get; set; }
        protected ILogger Log { get; set; }

        public BaseViewModelCommandExecuter(BaseViewModel viewModel, ILogger logger)
        {
            this.ViewModel = viewModel;
            this.Log = logger;
        }

        public virtual bool CanExecute(params object[] dataTransfer)
        {
            return true;
        }

        public bool Execute(params object[] dataTransfer)
        {
            if (CanExecute(dataTransfer))
            {
                ExecuteVM(dataTransfer);
                return true;
            }
            return false;
        }

        protected abstract void ExecuteVM(params object[] dataTransfer);
    }
}
