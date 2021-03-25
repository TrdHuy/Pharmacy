using Newtonsoft.Json;
using Pharmacy.Base.AsyncAction;
using Pharmacy.Base.AsyncTask;
using Pharmacy.Base.HttpServices.AzureFunctions;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.AppInfoPage
{
    internal class MSW_AIP_AppUpdateButtonAction : MSW_AIP_ButtonAction
    {
        private const string HPSS_REQUEST_APP_UPDATE_ENDPOINT = "https://hpss-customer-services20210319004727.azurewebsites.net/api/CheckForUpdate";
        private const string HPSS_REQUEST_APP_UPDATE_FUNCTION_KEY = "lbffmZxUsfmYaMqeybpu2eOs6qJ2ECdy8Fcvz8cS/bQww6fhg2EeSg==";

        public MSW_AIP_AppUpdateButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);

            try
            {
                AsyncTask asyncTask = new AsyncTask(SendRequestAppUpdateToHpssServer);
                AsyncTaskExecuter.AsyncExecute(asyncTask);
            }
            catch (Exception ex)
            {
                Log.E(ex.Message);
            }
        }

        private async Task<AsyncTaskResult> SendRequestAppUpdateToHpssServer()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var endPoint = new Uri(HPSS_REQUEST_APP_UPDATE_ENDPOINT);

                HPSSBodyRequest request = new HPSSBodyRequest();
                request.RequestID = HPSSCustomerRequestID.PharmarcyPackage_CheckAppUpdate;
                var data = JsonConvert.SerializeObject(request);

                var httpContent = new StringContent(data);

                //need this to authorize customer
                httpContent.Headers.Add("x-functions-key", HPSS_REQUEST_APP_UPDATE_FUNCTION_KEY);
                httpContent.Headers.Add("hpss-request-id", HPSSCustomerServiceDefinitions.HPSS_PHARMARCY_CHECK_APP_UPDATE);


                HttpClient client = new HttpClient();
                var response = await client.PostAsync(endPoint, httpContent);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

            }
            catch (Exception e)
            {

            }


            return null;
        }

        protected override void ExecuteOnDestroy()
        {
            base.ExecuteOnDestroy();
        }

    }
}
