namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface INavigationAction : IAction
    {
        /// <summary>
        /// true if the action will go to next source
        /// </summary>
        bool IsGotoNewSource { get; }
    }
}
