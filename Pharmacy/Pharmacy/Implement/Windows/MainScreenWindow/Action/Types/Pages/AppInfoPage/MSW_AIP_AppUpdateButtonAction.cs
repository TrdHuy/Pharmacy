using Newtonsoft.Json;
using Pharmacy.Base.AsyncAction;
using Pharmacy.Base.AsyncTask;
using Pharmacy.Base.HttpServices.AzureFunctions;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.AppImpl.Models.VOs;
using Pharmacy.Implement.Utils;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Views.UserControls;
using System.Diagnostics;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.AppInfoPage
{
    internal class MSW_AIP_AppUpdateButtonAction : MSW_AIP_ButtonAction
    {
        private const string HPSS_REQUEST_APP_UPDATE_ENDPOINT = "https://hpss-customer-services20210519110840.azurewebsites.net/api/CheckForUpdate";
        //private const string HPSS_REQUEST_APP_UPDATE_ENDPOINT = "http://localhost:7071/api/CheckForUpdate";
        private const string HPSS_REQUEST_APP_UPDATE_FUNCTION_KEY = "WGNevw9s0XdYmrZnlwNdeuJNAkq9cBBuY1SgKn3Ysh0BIBS3B9aREg==";

        private CancellationTokenSource cts;
        private AsyncTask requestCheckUpdateTask;
        private WebClient downloadFileClient;

        public MSW_AIP_AppUpdateButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        #region override field.
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            cts = new CancellationTokenSource();
            var ct = cts.Token;
            try
            {
                requestCheckUpdateTask = new AsyncTask(SendRequestAppUpdateToHpssServer
                    , null
                    , RequestCheckUpdateCallback
                    , 0);
                AsyncTask.AsyncExecute(requestCheckUpdateTask, ct);
            }
            catch (Exception ex)
            {
                Logger.E(ex.Message);
            }
        }

        protected override void SetCompleteFlagAfterExecuteCommand()
        {
            if (requestCheckUpdateTask != null)
            {
                IsCompleted = ((IAsyncTask)requestCheckUpdateTask).IsCompleted;
                IsCanceled = ((IAsyncTask)requestCheckUpdateTask).IsCanceled;
            }
        }

        protected override void ExecuteAlternativeCommand()
        {
            base.ExecuteAlternativeCommand();
            App.Current.ShowApplicationMessageBox("Đang chờ server phản hồi, vui lòng chờ trong giây lát!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.MainScreen,
                "Thông báo");
        }
        #endregion

        private async Task<AsyncTaskResult> SendRequestAppUpdateToHpssServer()
        {
            var result = new AsyncTaskResult(null, MessageAsyncTaskResult.Non);

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var endPoint = new Uri(HPSS_REQUEST_APP_UPDATE_ENDPOINT);

                HPSSBodyRequest request = new HPSSBodyRequest();

                var data = JsonConvert.SerializeObject(new
                {
                    ProductID = AppInfoVO.ProductID,
                    Company = AppInfoVO.Company,
                    Description = AppInfoVO.Description,
                    ProductName = AppInfoVO.ProductName,
                    ProductVersion = AppInfoVO.ProductVersion,
                    ReleaseDate = AppInfoVO.ReleaseDate,
                });

                var httpContent = new StringContent(data);

                //need this to authorize customer
                httpContent.Headers.Add("x-functions-key", HPSS_REQUEST_APP_UPDATE_FUNCTION_KEY);
                httpContent.Headers.Add("hpss-request-id", HPSSCustomerServiceDefinitions.HPSS_PHARMACY_CHECK_APP_UPDATE);


                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                Logger.I($"Start a http connection: Endpoint = {endPoint}, Content = {data}");

                var response = await client.PostAsync(endPoint, httpContent);


                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                result.Result = responseBody;
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

        private async void RequestCheckUpdateCallback(AsyncTaskResult result)
        {
            SetCompleteFlagAfterExecuteCommand();
            AIPViewmodel.ButtonCommandOV.IsAppUpdateButtonRunning = false;

            if (result.MesResult == MessageAsyncTaskResult.Done)
            {
                dynamic dataResponse = JsonConvert.DeserializeObject(result.Result.ToString());

                try
                {
                    switch (dataResponse.compareStatus.ToString())
                    {
                        case "NotCompatible":
                        case "Equal":
                            App.Current.ShowApplicationMessageBox(dataResponse.message.ToString(),
                                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                                OwnerWindow.MainScreen,
                                "Thông báo");
                            break;
                        case "Lower":
                            var x = App.Current.ShowApplicationMessageBox("Hiện đã có phiên bản mới, bạn có muốn cập nhật không?",
                                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                                OwnerWindow.MainScreen,
                                "Thông báo");


                            if (x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
                            {
                                PopupScreenWindow.MVVM.Views.PopupScreenWindow popupWindow = new PopupScreenWindow.MVVM.Views.PopupScreenWindow();
                                popupWindow.Height = 155d;
                                popupWindow.Width = 430d;
                                popupWindow.ResizeMode = System.Windows.ResizeMode.NoResize;

                                DownloadProgressControl downloadControl = new DownloadProgressControl();
                                downloadControl.YesNoButtonGrid.Visibility = System.Windows.Visibility.Collapsed;
                                downloadControl.CancelButton.Visibility = System.Windows.Visibility.Visible;

                                popupWindow.Content = downloadControl;
                                popupWindow.Show();
                                var path = System.IO.Path.GetTempPath() + dataResponse.fileName.ToString();
                                var totalBytesRecieve = double.Parse(dataResponse.totalBytes.ToString());

                                using (downloadFileClient = new WebClient())
                                {
                                    downloadFileClient.DownloadFileCompleted += (sender, args) =>
                                    {
                                        try
                                        {
                                            downloadControl.TitleText.Text = "Tải về bản cập nhật thành công, bạn có muốn tiến hành cài đặt?";
                                            downloadControl.YesNoButtonGrid.Visibility = System.Windows.Visibility.Visible;
                                            downloadControl.CancelButton.Visibility = System.Windows.Visibility.Collapsed;
                                        }
                                        catch { }
                                        finally
                                        {
                                        }
                                    };

                                    downloadFileClient.DownloadProgressChanged += (sender, args) =>
                                    {
                                        try
                                        {
                                            var percentage = (Math.Truncate(double.Parse(args.BytesReceived.ToString()) / totalBytesRecieve * 100));

                                            downloadControl.ProgressBar.Value = percentage;
                                            downloadControl.PercentText.Text = percentage + "%";
                                        }
                                        catch { }
                                    };

                                    downloadControl.CancelButton.Command = new CommandModel((paramaters) =>
                                    {
                                        downloadFileClient.CancelAsync();
                                        popupWindow.ForceClose();
                                    });

                                    downloadControl.NoButton.Command = new CommandModel((paramaters) =>
                                    {
                                        popupWindow.ForceClose();
                                    });

                                    downloadControl.YesButton.Command = new CommandModel((paramaters) =>
                                    {
                                        popupWindow.ForceClose();
                                        var psi = new ProcessStartInfo()
                                        {
                                            FileName = path,
                                            Verb = "RunAs"
                                        };

                                        Process.Start(psi);

                                        App.Current.ClearSessionID();
                                        App.Current.Shutdown();
                                        return;
                                    });

                                    await downloadFileClient.DownloadFileTaskAsync(new Uri(dataResponse.linkDownload.ToString()), path);
                                }

                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Logger.E(e.Message);
                    App.Current.ShowApplicationMessageBox("Lỗi tải về!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Lỗi!");
                }
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi cập nhật phiên bản, vui lòng liên hệ CSKH để được tư vấn!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Lỗi!");
            }
        }


    }
}
