using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pharmacy.Base.AsyncTask;
using Pharmacy.Base.HttpServices.AzureFunctions;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.AppImpl.Models.VOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.Action.Types.Pages.BugReportPage
{
    internal class PSW_BRP_SendReportAction : PSW_BRP_ButtonAction
    {
        //private const string HPSS_REQUEST_UPLOAD_VOC_ENDPOINT = "https://hpss-customer-services20210519110840.azurewebsites.net/api/UploadVOCFile";
        private const string HPSS_REQUEST_UPLOAD_VOC_ENDPOINT = "https://hpss-customer-services20210519110840.azurewebsites.net/api/UploadCustomerVOCToQueueHttpTrigger";
        private const string HPSS_REQUEST_UPLOAD_VOC_FUNCTION_KEY = "vKk4bmXQQNcpoXHe72Wt/fkhAPfUEWtgSswLcgIwrIsaGOo9aQgZxQ==";
        
        private CancellationTokenSource cts;
        private AsyncTask requestSendBugReportTask;

        public PSW_BRP_SendReportAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            BRPViewModel.ButtonCommandOV.IsSendReportButtonRunning = true;

            base.ExecuteCommand();

            if (BRPViewModel.IssueInfoOV.IsIssueInfoMeetCondition
                && BRPViewModel.ProductInfoOV.IsProductInfoMeetCondition
                && BRPViewModel.UserInfoOV.IsUserInfoMeetCondition)
            {
                cts = new CancellationTokenSource();
                var ct = cts.Token;
                try
                {
                    requestSendBugReportTask = new AsyncTask(SendRequestSaveBugReportToHpssServer
                        , null
                        , RequestSaveBugReportCallback
                        , 0);
                    AsyncTask.AsyncExecute(requestSendBugReportTask, ct);
                }
                catch (Exception ex)
                {
                    Logger.E(ex.Message);
                }

                HttpClient httpCl = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Vui lòng điền đầy đủ thông tin!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.Default,
                "Cảnh báo!");

                // Set complete flag to unregister action from executing action cache
                IsCompleted = true;

                BRPViewModel.ButtonCommandOV.IsSendReportButtonRunning = false;
            }

        }

        protected override void SetCompleteFlagAfterExecuteCommand()
        {
            if (requestSendBugReportTask != null)
            {
                IsCompleted = ((IAsyncTask)requestSendBugReportTask).IsCompleted;
                IsCanceled = ((IAsyncTask)requestSendBugReportTask).IsCanceled;
            }
        }

        protected override void ExecuteAlternativeCommand()
        {

            base.ExecuteAlternativeCommand();
            App.Current.ShowApplicationMessageBox("Đang chờ server phản hồi, vui lòng chờ trong giây lát!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.Default,
                "Thông báo");
        }

        private async Task<AsyncTaskResult> SendRequestSaveBugReportToHpssServer()
        {

            var result = new AsyncTaskResult(null, MessageAsyncTaskResult.Non);

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var endPoint = new Uri(HPSS_REQUEST_UPLOAD_VOC_ENDPOINT);


                var data = JsonConvert.SerializeObject(new
                {
                    UserName = BRPViewModel.UserInfoOV.FullNameText,
                    Email = BRPViewModel.UserInfoOV.EmailText,
                    Address = BRPViewModel.UserInfoOV.AddressText,
                    Phone = BRPViewModel.UserInfoOV.PhoneText,

                    ProductID = BRPViewModel.ProductInfoOV.ProdIDText,
                    ProductVersion = BRPViewModel.ProductInfoOV.ProdVersionText,

                    IncidentTime = BRPViewModel.IssueInfoOV.IncidentTimeText,
                    IssueDetail = BRPViewModel.IssueInfoOV.IssueDetailText,
                    IssueTitle = BRPViewModel.IssueInfoOV.IssueTitleText,

                });

                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(new StringContent(data), "other-data");

                var logFileStream = new FileStream(BRPViewModel.IssueInfoOV.LogPathText, FileMode.Open);
                form.Add(new StreamContent(logFileStream), "log_file.txt", logFileStream.Name);

                if (!String.IsNullOrEmpty(BRPViewModel.IssueInfoOV.VideoPathText))
                {
                    var attachedFileStream = new FileStream(BRPViewModel.IssueInfoOV.VideoPathText, FileMode.Open);
                    var fileNameParts = BRPViewModel.IssueInfoOV.VideoPathText.Split('.');
                    form.Add(new StreamContent(attachedFileStream), "attached_file." + fileNameParts[fileNameParts.Length - 1], logFileStream.Name);
                }

                //need this to authorize customer
                form.Headers.Add("x-functions-key", HPSS_REQUEST_UPLOAD_VOC_FUNCTION_KEY);
                form.Headers.Add("hpss-request-id", HPSSCustomerServiceDefinitions.HPSS_PHARMACY_UPLOAD_VOC_FILE);


                HttpClient client = new HttpClient();

                Logger.I($"Start a http connection: Endpoint = {endPoint}, Content = {data}");

                BRPViewModel.UploadVOCAlertTextVisibility = System.Windows.Visibility.Visible;
                var response = await client.PostAsync(endPoint, form);


                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var message = JObject.Parse(responseBody)["message"].ToString();

                Logger.I($"Response from server: status code = {response.StatusCode}");

                result.Result = responseBody;
                result.Messsage = message;
                result.MesResult = MessageAsyncTaskResult.Done;

                response.Dispose();
            }
            catch (Exception e)
            {
                Logger.E(e.Message);
                result.Messsage = e.Message;
                result.MesResult = MessageAsyncTaskResult.Aborted;
            }
            finally
            {
            }

            return result;
        }


        private void RequestSaveBugReportCallback(AsyncTaskResult result)
        {
            SetCompleteFlagAfterExecuteCommand();
            BRPViewModel.ButtonCommandOV.IsSendReportButtonRunning = false;
            BRPViewModel.UploadVOCAlertTextVisibility = System.Windows.Visibility.Hidden;

            if (result.MesResult == MessageAsyncTaskResult.Done)
            {
                App.Current.ShowApplicationMessageBox(result.Messsage,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                    OwnerWindow.Default,
                    "Thông báo");

            }
            else
            {
                App.Current.ShowApplicationMessageBox(result.Messsage,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.Default,
                    "Lỗi!");
            }


        }
    }
}
