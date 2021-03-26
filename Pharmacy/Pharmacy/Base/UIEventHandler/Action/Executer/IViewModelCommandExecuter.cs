using Pharmacy.Base.MVVM.ViewModels;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface IViewModelCommandExecuter : ICommandExecuter
    {
        BaseViewModel ViewModel { get; }
    }
}
