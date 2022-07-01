using Newtonsoft.Json;
using AlgorithmAppProduction.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlgorithmAppProduction.Services
{
    public static class JadServices
    {
        private static string baseUrl = "http://88.168.248.140:8000/";

        public static async Task<string> GetWorkUnitByOperationId(int operationId)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                string url = baseUrl + "workunit_by_operationid/" + operationId;
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (!(response.StatusCode == System.Net.HttpStatusCode.OK))
                    return null;
                var result = response.Content.ReadAsStringAsync().Result;
                if (result == "[]")
                    return "undefined";
                return JsonConvert.DeserializeObject<List<dynamic>>(result).FirstOrDefault().code;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static async Task<string> PostProduction(List<ProductionOrdersToSend> productionOrdersToSend)
        {
            HttpClient client = new HttpClient();
            string url = baseUrl + "production";
            string json = JsonConvert.SerializeObject(productionOrdersToSend);
            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(productionOrdersToSend));
            requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(url, requestContent);
            if (!(response.StatusCode == System.Net.HttpStatusCode.Created))
                return null;
            return "Success";
        }
    }
}
