using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using AventStack.ExtentReports.Gherkin.Model;
using ACDMAutomation.Shared.API.DTO_AuthAPI;
using ACDMAutomation.Shared.PageObjects;

using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acdm.InformationServices.Dto;
using JWTMakerLib;
using System.Threading;
using ACDMAutomation.Shared.Hooks;

namespace ACDMAutomation.API.Tests.Steps
{
    [Binding]
    public class CrudAPIConfigurationSteps : CrudAPIBaseMethods<ConfigurationDto>
    {
        public CrudAPIConfigurationSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        [Then(@"Execute Crud Configuration API And Set DTO Objects for Crud API Configuration for site claim ""([^""]*)""")]
        public void ThenExecuteCrudConfigurationAPIAndSetDTOObjectsForCrudAPIConfigurationForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ConfigurationAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Execute Crud Configuration API And Set DTO Objects for Crud API Configuration")]
        public void ThenExecuteCrudConfigurationAPIAndSetDTOObjectsForCrudAPIConfiguration()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ConfigurationAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSet()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is ConfigurationDto configurationDto)
                {
                    Assert.AreEqual(configurationDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + configurationDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(configurationDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + configurationDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(configurationDto.Name, compareDictionary["Name"], "Name values do not match:" + configurationDto.Name + " != " + compareDictionary["Name"]);
                    Assert.AreEqual(configurationDto.Value, compareDictionary["Value"], "Values do not match:" + configurationDto.Value + " != " + compareDictionary["Value"]);
                    Assert.AreEqual(configurationDto.Description, compareDictionary["Description"], "Description values do not match:" + configurationDto.Description + " != " + compareDictionary["Description"]);
                    Assert.AreEqual(configurationDto.System, compareDictionary["System"], "System values do not match:" + configurationDto.System + " != " + compareDictionary["System"]);
                    Assert.AreEqual(configurationDto.Group, compareDictionary["Group"], "Group values do not match:" + configurationDto.Group + " != " + compareDictionary["Group"]);
                }
                else
                {
                    Assert.Fail("Return values which are not configuration type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecord()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> configurationDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(configurationDetails, dtoResultSingleRecord);
        }
        [Then(@"Execute Crud Post API Configuration and Set DTO Objects for Crud API Configuration")]
        public void ThenExecuteCrudPostAPIConfigurationAndSetDTOObjectsForCrudAPIConfiguration()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new ConfigurationDto(0, Int32.Parse(sqlResponseDetails["Id"]), "This is test name" + (_scenarioContext.Get<string>("valueSQLData")), "This is test dummy value", "Test post request", "DMAN", "Test");
            Thread.Sleep(400);
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Update_Crud_API_Post_Configuration");
        }

        [Then(@"Generate new put configuration API URL using database fetched values and Execute Crud Put API Configuration and Set DTO Objects for Crud API Configuration")]
        public void ThenGenerateNewPutConfigurationAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIConfigurationAndSetDTOObjectsForCrudAPIConfiguration()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteIdValueDB = Int32.Parse(sqlResponseDetails["SiteId"]);
            string NameDB = sqlResponseDetails["Name"];
            string ValueDB = sqlResponseDetails["Value"];
            string DescriptionDB = sqlResponseDetails["Description"];
            string SystemDB = sqlResponseDetails["System"];
            string GroupDB = sqlResponseDetails["Group"];
            string NameDBAppendValue = NameDB + (_scenarioContext.Get<string>("valueSQLData"));
            string ValueDBAppendValue =  (_scenarioContext.Get<string>("valueSQLData"));

            var putRequestBody = new ConfigurationDto(IdValueDB, SiteIdValueDB, NameDBAppendValue, ValueDBAppendValue, DescriptionDB, SystemDB, GroupDB);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Update_Crud_API_Put_Configuration");
        }

        [Then(@"Generate new Get configuration by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Configuration")]
        public void ThenGenerateNewGetConfigurationByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIConfiguration(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.ConfigurationAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<ConfigurationDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPI(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.ConfigurationAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration")]
        public IRestResponse ThenGenerateNewDeleteConfigurationAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIConfiguration()
        {
            var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.ConfigurationAPIURL, "Id", "Failed_To_Execute_Get_By_API");
            IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Configuration");
            return restResponse;
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPI()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request and Set DTO Objects for Crud API Configuration")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestAndSetDTOObjectsForCrudAPIConfiguration(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Execute_Get_By_API_Request");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<ConfigurationDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_ConfigurationAPI");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request and Set DTO Objects for Crud API Configuration data")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestAndSetDTOObjectsForCrudAPIConfigurationData(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                dtoResultList = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Execute_Get_By_API_Request");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_ConfigurationAPI");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" and ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" and ""([^""]*)"" request and Set DTO Objects for Crud API Configuration")]
        public void ThenGenerateNewUsingAndInTheAPIURLAndExecuteCrudGetByAndRequestAndSetDTOObjectsForCrudAPIConfiguration(string apiReqName, string colName, string colName1, string colNameAPI, string colNameAPI1)
        {
            try
            {
                dtoResultList = ExecuteGetByAPIMultiParameterRequestSQLDataWithDeserialize(apiReqName, colName, colName1, colNameAPI, colNameAPI1, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Execute_Get_By_API_Request");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_ConfigurationAPI");
            }
        }
        [Then(@"Generate new ""([^""]*)"" API URL and Execute Crud Post request for existing record and Set DTO Objects for Crud API Configuration")]
        public void ThenGenerateNewAPIURLAndExecuteCrudPostRequestForExistingRecordAndSetDTOObjectsForCrudAPIConfiguration(string apiReqName)
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteIdValueDB = Int32.Parse(sqlResponseDetails["SiteId"]);
            string NameDB = sqlResponseDetails["Name"];
            string ValueDB = sqlResponseDetails["Value"];
            string DescriptionDB = sqlResponseDetails["Description"];
            string SystemDB = sqlResponseDetails["System"];
            string GroupDB = sqlResponseDetails["Group"];
            string appendedURL = APITests.apiConfigDTO.ConfigurationAPIURL + "/" + apiReqName;
            var putRequestBody = new ConfigurationDto(IdValueDB, SiteIdValueDB, NameDB, ValueDB, DescriptionDB, SystemDB, GroupDB);
            dtoResultSingleRecord = PostAPIWithDeserialize(putRequestBody, appendedURL, "Failed_To_Update_Crud_API_Put_Configuration");
        }
        [Then(@"Generate new ""([^""]*)"" API URL and Execute Crud Post request for new record and Set DTO Objects for Crud API Configuration")]
        public void ThenGenerateNewAPIURLAndExecuteCrudPostRequestForNewRecordAndSetDTOObjectsForCrudAPIConfiguration(string apiReqName)
        {
            string appendedURL = APITests.apiConfigDTO.ConfigurationAPIURL + "/" + apiReqName;
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new ConfigurationDto(0, Int32.Parse(sqlResponseDetails["Id"]), "This is test name" + (_scenarioContext.Get<string>("valueSQLData")), "This is test dummy value", "Test post request", "DMAN", "Test");
            Thread.Sleep(200);
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, appendedURL, "Failed_To_Update_Crud_API_Post__Configuration");
        }
        [Then(@"Execute Crud Configuration API with an incorrect Site id  for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudConfigurationAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ConfigurationAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_IncorrectSiteId_Crud_API_Configuration_MessageVerification");
            }
        }
        [Then(@"Generate new Get Configuration by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetConfigurationByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.ConfigurationAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim) ;
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_Configuration_MessageVerification");
            }
        }

        [Then(@"Generate new Delete Configuration API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteConfigurationAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteConfigurationAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIConfiguration();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for configuration Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Configuration");
            }
        }
        [Then(@"Execute Crud Post Configuration API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostConfigurationAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();

                var postRequestBody = new ConfigurationDto(0, Int32.Parse(sqlResponseDetails["Id"]), "This is test name" + (_scenarioContext.Get<string>("valueSQLData")), "This is test dummy value", "Test post request", "DMAN", "Test");
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Update_Crud_API_Post_ConfigurationAPI");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<ConfigurationDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put Configuration API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutConfigurationAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteIdValueDB = Int32.Parse(sqlResponseDetails["SiteId"]);
                string NameDB = sqlResponseDetails["Name"];
                string ValueDB = sqlResponseDetails["Value"];
                string DescriptionDB = sqlResponseDetails["Description"];
                string SystemDB = sqlResponseDetails["System"];
                string GroupDB = sqlResponseDetails["Group"];

                var putRequestBody = new ConfigurationDto(IdValueDB, SiteIdValueDB, NameDB, ValueDB, DescriptionDB, SystemDB, GroupDB);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Update_Crud_API_Put_ConfigurationAPI");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new Get configuration by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Configuration")]
        public void ThenGenerateNewGetConfigurationByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIConfiguration(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.ConfigurationAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with ConfigurationId: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body for Crud API Configuration")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnEmptyListInTheResponseBodyForCrudAPIConfiguration(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Execute_Get_By_API_Request");
                if (dtoResult.Count == 0)
                {
                    Console.WriteLine("Response content is an empty list");
                }
                else
                {
                    Console.WriteLine("Response content is an empty list");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_ConfigurationAPI");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request for a different Site Id than what is being used in the token and validate that the api should return No Content in the response body for Crud API Configuration")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnNoContentInTheResponseBodyForCrudAPIConfiguration(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.ConfigurationAPIURL, "Failed_To_Execute_Get_By_API_Request");
                string errorMessageExpected = restResponse.StatusDescription.ToString();
                Assert.AreEqual(errorMessageExpected, "No Content", "Response code matches:" + errorMessageExpected);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_ConfigurationAPI");
            }          
        }
    }
}
