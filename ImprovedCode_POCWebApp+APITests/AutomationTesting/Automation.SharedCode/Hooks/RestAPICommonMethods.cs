using System.Diagnostics;
using System.IO;
using ACDMAutomation.Shared.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace ACDMAutomation.Shared.Hooks
{
    public class RestAPICommonMethods
    {
        public static RestClient SetURL(string baseURL, string resourceURL)
        {
            var url = Path.Combine(baseURL, resourceURL);
            var restClient = new RestClient(url);
            return restClient;
        }
        public RestClient SetURLByColumnValue(string baseURL, string resourceURL, int inputColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLByTwoSameColumnValue(string baseURL, string resourceURL, int inputColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputColValue + "/" + inputColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public static RestClient SetURLByAppendedStringValue(string baseURL, string resourceURL, string inputAppendedURLValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAppendedURLValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLAppendParameter(string baseURL, string resourceURL, string inputParameter, int inputParamValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "?" + inputParameter + "=" + inputParamValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLAppendRequestAPIParameter(string baseURL, string resourceURL, string inputAPIRequest, string inputParameter, string inputParamValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + inputParameter + "=" + inputParamValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }

        public RestClient SetURLByTwoColumnValues(string baseURL, string resourceURL, string inputAPIRequest, string firstColName, int firstColValue, string secondColName, string secondColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue + "&" + secondColName + "=" + secondColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLByOneStringColumnValues(string baseURL, string resourceURL, string inputAPIRequest, string firstColName, string firstColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLByTwoInputParameterAndOneIntValue(string baseURL, string resourceURL, string firstAPIRequest, string secondAPIRequest, string firstColName, int firstColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + firstAPIRequest + "/" + secondAPIRequest + "?" + firstColName + "=" + firstColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLByTwoInputParameterAndOneStringValue(string baseURL, string resourceURL, string firstAPIRequest, string secondAPIRequest, string firstColName, string firstColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + firstAPIRequest + "/" + secondAPIRequest + "?" + firstColName + "=" + firstColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLByTwoStringColumnValues(string baseURL, string resourceURL, string inputAPIRequest, string firstColName, string firstColValue, string secondColName, string secondColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue + "&" + secondColName + "=" + secondColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLByThreeStringColumnValues(string baseURL, string resourceURL, string inputAPIRequest, string firstColName, string firstColValue, string secondColName, string secondColValue, string thirdColName, string thirdColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue + "&" + secondColName + "=" + secondColValue + "&" + thirdColName + "=" + thirdColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestClient SetURLByThreeColumnValues(string baseURL, string resourceURL, string inputAPIRequest, string firstColName, int firstColValue, string secondColName, string secondColValue, string thirdColName, string thirdColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue + "&" + secondColName + "=" + secondColValue + "&" + thirdColName + "=" + thirdColValue);
            var restClient = new RestClient(appendedURL);
            return restClient;
        }
        public RestRequest CreatePostRequest(string jsonString)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json; charset=utf-8");
            restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return restRequest;
        }

        public RestRequest CreateGetRequest(string authToken, string siteClaim = "")
        {
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            if (siteClaim != string.Empty)
            {
                restRequest.AddHeader("SelectedSite", siteClaim);
            }
            restRequest = TokenValidation(authToken, restRequest);
            return restRequest;
        }
        public RestRequest TokenValidation(string authToken, RestRequest restRequest)
        {
            if (authToken != null)
            {
                restRequest.AddHeader("Authorization", "bearer " + authToken);
            }
            return restRequest;
        }
        public RestRequest CreatePutRequest(string jsonString)
        {
            var restRequest = new RestRequest(Method.PUT);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return restRequest;
        }
        public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }
        public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest, int timeLimit)
        {
            Stopwatch sw = new();
            sw.Start();
            var result = restClient.Execute(restRequest);
            sw.Stop();
            Assert.IsTrue(timeLimit > sw.ElapsedMilliseconds, "Failed - Response time is more than expected: " + sw.ElapsedMilliseconds);
            return result;
        }
        public DTO GetContent<DTO>(IRestResponse response)
        {
            var content = response.Content;
            DTO deserializeObject = JsonConvert.DeserializeObject<DTO>(content);
            return deserializeObject;
        }
        public dynamic DeSerializeJSON(string jsonString)
        {
            return JsonConvert.DeserializeObject<dynamic>(jsonString.Replace("/", ""));
        }
        public JArray JsonArray(string jsonString)
        {
            return JArray.Parse(jsonString.Replace("/", ""));
        }

    }
}
