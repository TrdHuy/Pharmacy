using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Types
{
    internal class BaseViewModelCommandExecuter : AbstractViewModelCommandExecuter
    {
        public BaseViewModelCommandExecuter(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger)
        {
            DataTransfer = new List<object>();
        }

        protected List<object> DataTransfer;

        public override void ExecuteCommand(object dataTransfer)
        {
            var cast = dataTransfer as object[];
            if (cast != null)
            {
                foreach (object data in cast)
                {
                    DataTransfer.Add(data);
                }
            }
            else if (dataTransfer != null)
            {
                DataTransfer.Add(dataTransfer);
            }
        }
    }
}