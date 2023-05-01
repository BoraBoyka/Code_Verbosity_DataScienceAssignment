using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDMAutomation.Shared.API.DTO_AuthAPI;
using ACDMAutomation.Shared.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acdm.InformationServices.Dto;
using JWTMakerLib;
using ACDMAutomation.Shared.Hooks;

namespace ACDMAutomation.Shared.Hooks
{
    public abstract class CrudAPIBaseMethods<T> : APIBaseMethods
    {
        protected List<T> dtoResultList = new();
        protected dynamic dtoResultSingleRecord;
        public CrudAPIBaseMethods(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        public List<T> ExecuteGetAPIWithDeserialize(string token, string failureMessage, RestClient restOBJSetup, string siteClaim = "")
        {
            try
            {
                var restResponse = ExecuteGetAPI(token, failureMessage, restOBJSetup,siteClaim);
                return JsonConvert.DeserializeObject<List<T>>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public RestClient ExecuteGetByAPI(string appURL, string colName, string failureMessage)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                AddOrUpdateScenarioContext("idValueDB", sqlResponseDetails[colName]);
                int idValueDB = Int32.Parse(_scenarioContext.Get<string>("idValueDB"));
                var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, appURL, idValueDB);
                return restOBJSetup;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public RestClient ExecuteGetByAPIForMultiInputValues(string appURL, string colName, string failureMessage)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                AddOrUpdateScenarioContext("idValueDB", sqlResponseDetails[colName]);
                int idValueDB = Int32.Parse(_scenarioContext.Get<string>("idValueDB"));
                var restOBJSetup = restAPIUtil.SetURLByTwoSameColumnValue(APITests.apiConfigDTO.BASE_URL, appURL, idValueDB);
                return restOBJSetup;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public void ExecuteGetDeleteErrorMessageAPI(string appURL, string failureMessage)
        {
            try
            {
                var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, appURL, Int32.Parse(_scenarioContext.Get<string>("idValueDB")));
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                string errorMessageExpected = restResponse.StatusDescription.ToString();
                Assert.AreEqual(errorMessageExpected, "No Content", "Response code matches:" + errorMessageExpected);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
            }
        }
        public IRestResponse ExecuteDeleteAPI(IRestClient restOBJSetup, string failureMessage)
        {
            try
            {
                var request = new RestRequest();
                var requestWithToken = restAPIUtil.TokenValidation(apiConfigDTO.JWT_TOKEN, request);
                var restResponse = restOBJSetup.Delete(requestWithToken);
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public IRestResponse ExecuteGetByAPIRequest(string apiReqName, string colName, string colNameAPI, string appURL, string failureMessage)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                AddOrUpdateScenarioContext("typeNameValueDB", sqlResponseDetails[colName]);
                string typeNameValueDB = (_scenarioContext.Get<string>("typeNameValueDB"));
                var restOBJSetup = restAPIUtil.SetURLAppendRequestAPIParameter(APITests.apiConfigDTO.BASE_URL, appURL, apiReqName, colNameAPI, typeNameValueDB);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public IRestResponse ExecuteGetByAPIMultiParameterRequest(string apiReqName, string colNameAPI, string colNameAPI1, string appURL, string failureMessage, string systemValueDB, string typeNameValueDB)
        {
            try
            {
                var restOBJSetup = restAPIUtil.SetURLByTwoStringColumnValues(APITests.apiConfigDTO.BASE_URL, appURL, apiReqName, colNameAPI, systemValueDB, colNameAPI1, typeNameValueDB);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public List<T> ExecuteGetByAPIRequestWithDeserialize(string apiReqName, string colName, string colNameAPI, string appURL, string failureMessage)
        {
            var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, appURL, failureMessage);
            return JsonConvert.DeserializeObject<List<T>>(restResponse.Content);
        }
        public List<T> ExecuteGetByAPIMultiParameterRequestSQLDataWithDeserialize(string apiReqName, string colName, string colName1, string colNameAPI, string colNameAPI1, string appURL, string failureMessage)
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            AddOrUpdateScenarioContext("systemValueDB", sqlResponseDetails[colName1]);
            string systemValueDB = (_scenarioContext.Get<string>("systemValueDB"));
            AddOrUpdateScenarioContext("typeNameValueDB", sqlResponseDetails[colName]);
            string typeNameValueDB = (_scenarioContext.Get<string>("typeNameValueDB"));

            var restResponse = ExecuteGetByAPIMultiParameterRequest(apiReqName, colNameAPI, colNameAPI1, appURL, failureMessage, systemValueDB, typeNameValueDB);
            return JsonConvert.DeserializeObject<List<T>>(restResponse.Content);
        }
        public IRestResponse ExecuteGetByAPIMultiParameterRequestWithDeserialize(string apiReqName, string colNameAPI, string colNameAPI1, string appURL, string failureMessage, string runwayNameDB, string standNameDB)
        {
            var restResponse = ExecuteGetByAPIMultiParameterRequest(apiReqName, colNameAPI, colNameAPI1, appURL, failureMessage, runwayNameDB, standNameDB);
            return restResponse;
        }
        public void GetResponseAndMessageVerification(RestClient restOBJSetup, RestRequest restOBJRequest, string message)
        {
            try
            {
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                ResponseErrorMessageVerification(restResponse, message, "title", "status");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_GetAPIResponse_MessageVerification");
            }
        }
        public void GetResponseAndErrorMessageVerification(RestClient restOBJSetup, RestRequest restOBJRequest, string errorMessageExpected, string responseData, string responseData1)
        {
            try
            {
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                ResponseErrorMessageVerification(restResponse, errorMessageExpected, responseData, responseData1);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_GetAPIResponse_MessageVerification");
            }
        }
        public static void ResponseErrorMessageVerification(IRestResponse restResponse, string errorMessageExpected, string responseData, string responseData1)
        {
            try
            {
                Console.WriteLine(restResponse.StatusCode.ToString() + " " + restResponse.Content.ToString() + " " + restResponse.StatusDescription.ToString());
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                string responseDataMessageCode = DynamicData[responseData].ToString();
                string responseDataMessage = DynamicData[responseData1].ToString();
                string responseDataFinal = responseDataMessage + " " + responseDataMessageCode;
                Assert.AreEqual(errorMessageExpected, responseDataFinal, "Response code matches:" + responseDataFinal);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_GetAPIResponse_MessageVerification");
            }
        }
        public static IRestRequest GetPostRequestBody(object dtoObject)
        {
            var request = new RestRequest();
            var postRequestBody = dtoObject;
            request.AddJsonBody(postRequestBody);
            return request;
        }
        public IRestResponse PostAPI(object dtoObject, string appURL, string failureMessage)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, appURL);
                var requestWithToken = PostAndPutAPI(dtoObject, appURL, failureMessage, restOBJSetup);
                var restResponse = restOBJSetup.Post(requestWithToken);
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public T PostAPIWithDeserialize(object dtoObject, string appURL, string failureMessage)
        {
            var restResponse = PostAPI(dtoObject, appURL, failureMessage);
            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }
        public List<T> PostAPIWithDeserializeList(object dtoObject, string appURL, string failureMessage)
        {
            var restResponse = PostAPI(dtoObject, appURL, failureMessage);
            return JsonConvert.DeserializeObject<List<T>>(restResponse.Content);
        }
        public List<T> PutAPIWithDeserializeList(object dtoObject, string appURL, string failureMessage)
        {
            var restResponse = PutAPI(dtoObject, appURL, failureMessage);
            return JsonConvert.DeserializeObject<List<T>>(restResponse.Content);
        }
        public IRestResponse PutAPI(object dtoObject, string appURL, string failureMessage)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, appURL);
                var requestWithToken = PostAndPutAPI(dtoObject, appURL, failureMessage, restOBJSetup);
                var restResponse = restOBJSetup.Put(requestWithToken);
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public IRestRequest PostAndPutAPI(object dtoObject, string appURL, string failureMessage, RestClient restOBJSetup)
        {
            try
            {
                var request = GetPostRequestBody(dtoObject);
                var requestWithToken = restAPIUtil.TokenValidation(apiConfigDTO.JWT_TOKEN, (RestRequest)request);
                return requestWithToken;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public T PutAPIWithDeserialize(object dtoObject, string appURL, string failureMessage)
        {
            var restResponse = PutAPI(dtoObject, appURL, failureMessage);
            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }
        public static void APIResponseErrorValidation(IRestResponse restResponse, string errorMessageExpected, string Message)
        {
            try
            {
                Console.WriteLine(restResponse.StatusCode.ToString() + " " + restResponse.Content.ToString() + " " + restResponse.StatusDescription.ToString());
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                string responseDataMessage = DynamicData[Message].ToString();
                Assert.AreEqual(errorMessageExpected, responseDataMessage, "Response code matches:" + responseDataMessage);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Validate_APIResponse_Message");
            }
        }
        public void ValidateIDDoesntExistErrorMessage(string appURL, string colName, string failureMessage, string errorMessageExpected)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(appURL, colName, failureMessage);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                string errorMessageActual = restResponse.StatusDescription.ToString();
                Assert.AreEqual(errorMessageExpected, errorMessageActual, "Response code matches:" + errorMessageActual);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }

        public void ValidateIDNotExistErrorMessage(string appURL, string apiReqName, string colName, string colNameSQL, string failureMessage, string errorMessageExpected)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                AddOrUpdateScenarioContext("idValueDB", sqlResponseDetails[colNameSQL]);
                string idValueDB = _scenarioContext.Get<string>("idValueDB");
                var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, colName, idValueDB);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                string errorMessageActual = restResponse.StatusDescription.ToString();
                Assert.AreEqual(errorMessageExpected, errorMessageActual, "Response code matches:" + errorMessageActual);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }

        public virtual void ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI()
        {
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            for (int i = 0; i < dtoResultList.Count; i++)
            {
                Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(i);
                APIDBComparison(APIDetails, dtoResultList[i]);
            }
        }
        public abstract void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject);

        public void FetchDBReturnedValue(string keyColName)
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
            AddOrUpdateScenarioContext("valueSQLData", aircraftTypeDetails[keyColName]);
            string valueSQLData = (_scenarioContext.Get<string>("valueSQLData"));
            Console.WriteLine(valueSQLData);
            if (valueSQLData.Equals(null) || valueSQLData.Equals(""))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_FieldValue_Database");
            }
        }
        public void FetchDBOutputResponse(string keyColName)
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
            AddOrUpdateScenarioContext("valueSQLResponse", aircraftTypeDetails[keyColName]);
            string valueSQLResponse = (_scenarioContext.Get<string>("valueSQLResponse"));
            Console.WriteLine(valueSQLResponse);
            if (valueSQLResponse.Equals(null) || valueSQLResponse.Equals(""))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_FieldValue_Database");
            }
        }
        public static DateTimeOffset? ParseDateTime(string dateTimeValue)
        {
            DateTimeOffset result;
            DateTimeOffset.TryParse(dateTimeValue, out result);
            if (result != DateTimeOffset.MinValue)
            {
                return result;
            }
            return null;
        }
    }
}
