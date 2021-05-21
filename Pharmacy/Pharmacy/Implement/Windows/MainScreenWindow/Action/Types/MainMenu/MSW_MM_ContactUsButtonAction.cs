using Pharmacy.Base.AsyncTask;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.MainMenu
{
    internal class MSW_MM_ContactUsButtonAction : BaseCommandExecuter
    {
        private const string HPSS_CONTACT_MESSENGER_ENDPOINT = "https://m.me/HpssCustomerServices";
        private AsyncTask contactHPSSTask;

        public object AIPViewmodel { get; private set; }

        public MSW_MM_ContactUsButtonAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            try
            {
                contactHPSSTask = new AsyncTask(OpenBrowserToHPSSContactEndpoint
                    , null
                    , TaskFinishedCallback
                    , 5000);
                AsyncTask.AsyncExecute(contactHPSSTask);
            }
            catch (Exception ex)
            {
                Logger.E(ex.Message);
            }
        }
        protected override void SetCompleteFlagAfterExecuteCommand()
        {
            if (contactHPSSTask != null)
            {
                IsCompleted = ((IAsyncTask)contactHPSSTask).IsCompleted;
                IsCanceled = ((IAsyncTask)contactHPSSTask).IsCanceled;
            }
        }

        private void TaskFinishedCallback(AsyncTaskResult obj)
        {
            SetCompleteFlagAfterExecuteCommand();
        }

        private async Task<AsyncTaskResult> OpenBrowserToHPSSContactEndpoint()
        {
            var result = new AsyncTaskResult(null, MessageAsyncTaskResult.Non);

            try
            {
                await Task.Delay(1000);
                Process.Start(HPSS_CONTACT_MESSENGER_ENDPOINT);
            }
            catch (Exception e)
            {
                Logger.E(e.Message);
                App.Current.ShowApplicationMessageBox("Trình duyệt hiện tại không được hỗ trợ!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Lỗi!");
            }
            return result;
        }
    }
}
