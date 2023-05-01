using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDM.Bindings.CommonAPIUtils.Hooks;
using ACDMAutomation.API.DTO_AuthAPI;
using ACDMAutomation.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acdm.InformationServices.Dto;
using JWTMakerLib;
using ACDMAutomation.Hooks;

namespace ACDMAutomation.Steps
{
    public abstract class CrudAPIBaseMethods<T>: APIBaseMethods
    {
        protected List<T> dtoResultList = new();
        protected static dynamic dtoResultSingleRecord;
        public static string typeNameValueDB;
        public static string idValueDB;
        public static string systemValueDB;
        public List<T> ExecuteGetAPIWithDeserialize(string token, string failureMessage, RestClient restOBJSetup)
        {
            try
            {
                var restResponse=  ExecuteGetAPI(token, failureMessage, restOBJSetup);
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
                idValueDB = sqlResponseDetails[colName];
                var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, appURL, Int32.Parse(idValueDB));
                return restOBJSetup;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public static void ExecuteGetDeleteErrorMessageAPI(string appURL, string failureMessage)
        {
            try
            {
                var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, appURL, Int32.Parse(idValueDB));
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
            }
        }
        public static IRestResponse ExecuteDeleteAPI(IRestClient restOBJSetup, string failureMessage)
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
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails(); ;
                typeNameValueDB = sqlResponseDetails[colName];
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
        public IRestResponse ExecuteGetByAPIMultiParameterRequest(string apiReqName, string colName, string colName1,string colNameAPI, string colNameAPI1, string appURL, string failureMessage)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails(); ;
                systemValueDB = sqlResponseDetails[colName1];
                typeNameValueDB = sqlResponseDetails[colName];
                var restOBJSetup = restAPIUtil.SetURLByTwoStringColumnValues(APITests.apiConfigDTO.BASE_URL, appURL, apiReqName, colNameAPI, systemValueDB, colNameAPI1,typeNameValueDB);
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
            var restResponse = ExecuteGetByAPIRequest(apiReqName,colName,colNameAPI,appURL,failureMessage);
            return JsonConvert.DeserializeObject<List<T>>(restResponse.Content);
        }
        public List<T> ExecuteGetByAPIMultiParameterRequestWithDeserialize(string apiReqName, string colName, string colName1,string colNameAPI, string colNameAPI1, string appURL, string failureMessage)
        {
            var restResponse = ExecuteGetByAPIMultiParameterRequest(apiReqName, colName, colName1,colNameAPI,colNameAPI1, appURL, failureMessage);
            return JsonConvert.DeserializeObject<List<T>>(restResponse.Content);
        }
        public static void GetResponseAndMessageVerification(RestClient restOBJSetup, RestRequest restOBJRequest)
        {
            try
            {
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                ResponseErrorMessageVerification(restResponse, "404 Not Found", "title", "status");
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
           var restResponse= PostAPI(dtoObject, appURL, failureMessage);
           return  JsonConvert.DeserializeObject<T>(restResponse.Content);
        }      
        public IRestResponse PutAPI(object dtoObject, string appURL, string failureMessage)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, appURL);
                var requestWithToken= PostAndPutAPI(dtoObject, appURL, failureMessage, restOBJSetup);
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
        public void ValidateIDDoesntExistErrorMessage (string appURL, string colName, string failureMessage)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(appURL, colName, failureMessage);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI()
        {
            Console.WriteLine(dtoResultList.Count);
            for (int i = 0; i < dtoResultList.Count; i++)
            {
                Dictionary<string, string> APIDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(i);
                APIDBComparison(APIDetails, dtoResultList[i]);
            }
        }
        public abstract void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject);
    }
}
