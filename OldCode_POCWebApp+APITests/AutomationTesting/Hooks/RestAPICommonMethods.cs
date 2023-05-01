using System.Diagnostics;
using System.IO;
using ACDMAutomation.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace ACDM.Bindings.CommonAPIUtils.Hooks
{
    public class RestAPICommonMethods
    {
        private RestRequest _restRequest;
        public static ACDMAutomation.Steps.CrudAPIConfigurationSteps postRequest;
        public static RestClient SetURL(string baseURL, string resourceURL)
        {
            var url = Path.Combine(baseURL, resourceURL);
            var _restClient = new RestClient(url);
            return _restClient;
        }
        public RestClient SetURLByColumnValue(string baseURL, string resourceURL, int inputColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputColValue);
            var _restClient = new RestClient(appendedURL);
            return _restClient;
        }
        public RestClient SetURLByAppendedStringValue(string baseURL, string resourceURL, string inputAppendedURLValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAppendedURLValue);
            var _restClient = new RestClient(appendedURL);
            return _restClient;
        }
        public RestClient SetURLAppendParameter(string baseURL, string resourceURL, string inputParameter, int inputParamValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "?" + inputParameter + "=" + inputParamValue);
            var _restClient = new RestClient(appendedURL);
            return _restClient;
        }
        public RestClient SetURLAppendRequestAPIParameter(string baseURL, string resourceURL,string inputAPIRequest,string inputParameter, string inputParamValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + inputParameter + "=" + inputParamValue);
            var _restClient = new RestClient(appendedURL);
            return _restClient;
        }
       
        public RestClient SetURLByTwoColumnValues(string baseURL, string resourceURL, string inputAPIRequest, string firstColName, int firstColValue, string secondColName, string secondColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue + "&" + secondColName + "=" + secondColValue);
            var _restClient = new RestClient(appendedURL);
            return _restClient;
        }
        public RestClient SetURLByTwoStringColumnValues(string baseURL, string resourceURL, string inputAPIRequest, string firstColName, string firstColValue, string secondColName, string secondColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue + "&" + secondColName + "=" + secondColValue);
            var _restClient = new RestClient(appendedURL);
            return _restClient;
        }
        public RestClient SetURLByThreeStringColumnValues(string baseURL, string resourceURL, string inputAPIRequest, string firstColName, string firstColValue, string secondColName, string secondColValue, string thirdColName, string thirdColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue + "&" + secondColName + "=" + secondColValue + "&" + thirdColName + "=" + thirdColValue);
            var _restClient = new RestClient(appendedURL);
            return _restClient;
        }
        public RestClient SetURLByThreeColumnValues(string baseURL, string resourceURL, string inputAPIRequest,string firstColName, int firstColValue, string secondColName, string secondColValue, string thirdColName, string thirdColValue)
        {
            var appendedURL = Path.Combine(baseURL, resourceURL + "/" + inputAPIRequest + "?" + firstColName + "=" + firstColValue + "&" + secondColName + "=" + secondColValue + "&" + thirdColName + "=" + thirdColValue);
            var _restClient = new RestClient(appendedURL);
            return _restClient;
        }
        public RestRequest CreatePostRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.POST);
            _restRequest.AddHeader("Content-Type", "application/json; charset=utf-8");
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }
        
        public RestRequest CreateGetRequest(string authToken)
        {
            _restRequest = new RestRequest(Method.GET);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest=TokenValidation(authToken, _restRequest);
            return _restRequest;
        }
        public RestRequest TokenValidation(string authToken, RestRequest restRequest)
        {
            if (authToken != null)
            {
                restRequest.AddHeader("Authorization", "bearer " +authToken);
            }
            return restRequest;
        }
        public RestRequest CreatePutRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.PUT);
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }
        public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }
        public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest, int timeLimit)
        {
            Stopwatch sw = new();
            sw.Start();
            var result= restClient.Execute(restRequest);
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
