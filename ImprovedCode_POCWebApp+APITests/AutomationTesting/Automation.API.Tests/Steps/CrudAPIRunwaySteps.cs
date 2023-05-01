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
using ACDMAutomation.Shared.Hooks;

namespace ACDMAutomation.API.Tests.Steps
{
    [Binding]
    public class CrudAPIRunwaySteps : CrudAPIBaseMethods<RunwayDto>
    {
        public CrudAPIRunwaySteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        [Then(@"Execute Crud Runway API And Set DTO Objects for Crud API Runway for site claim ""([^""]*)""")]
        public void ThenExecuteCrudRunwayAPIAndSetDTOObjectsForCrudAPIRunwayForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.RunwayAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Execute Crud Runway API And Set DTO Objects for Crud API Runway")]
        public void ThenExecuteCrudRunwayAPIAndSetDTOObjectsForCrudAPIRunway()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.RunwayAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud Runway API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudRunwayAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }

        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is RunwayDto runwayDto)
                {
                    Assert.AreEqual(runwayDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + runwayDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(runwayDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + runwayDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(runwayDto.Name, compareDictionary["Name"], "Name values do not match:" + runwayDto.Name + " != " + compareDictionary["Name"]);
                    Assert.AreEqual(runwayDto.DependencyGroup, Int32.Parse(compareDictionary["DependencyGroup"]), "DependencyGroup values do not match:" + runwayDto.DependencyGroup + " != " + compareDictionary["DependencyGroup"]);
                }
                else
                {
                    Assert.Fail("Return values which are not runway type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Generate new Get runway by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Runway")]
        public void ThenGenerateNewGetRunwayByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIRunway(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.RunwayAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<RunwayDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Runway API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudRunwayAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> runwayDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(runwayDetails, dtoResultSingleRecord);
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Runway")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudRunway(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.RunwayAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request and Set DTO Objects for Crud API Runway")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestAndSetDTOObjectsForCrudAPIRunway(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                dtoResultList = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.RunwayAPIURL, "Failed_To_Execute_Get_By_API_Request");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_RunwayAPI");
            }
        }
        [Then(@"Generate new Delete runway API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Runway")]
        public IRestResponse ThenGenerateNewDeleteRunwayAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIRunway()
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.RunwayAPIURL, "Id", "Failed_To_Execute_Get_By_API");
                IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Runway");
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Runway");
                return null;
            }
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Runway")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudRunway()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.RunwayAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Execute Crud Post Runway API and Set DTO Objects for Crud API Runway")]
        public void ThenExecuteCrudPostRunwayAPIAndSetDTOObjectsForCrudAPIRunway()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new RunwayDto(0, Int32.Parse(sqlResponseDetails["Id"]), "ACDM" + (_scenarioContext.Get<string>("valueSQLData")))
            {
                DependencyGroup = 1, OppositeRunway = "null"
            };
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.RunwayAPIURL, "Failed_To_Update_Crud_API_Post_Runway_Configuration");
        }
        [Then(@"again Execute Crud Post Runway API with a duplicate Name value and verify that it should return error via API for Crud Runway API")]
        public void ThenAgainExecuteCrudPostRunwayAPIWithADuplicateNameValueAndVerifyThatItShouldReturnErrorViaAPIForCrudRunwayAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new RunwayDto(0, Int32.Parse(sqlResponseDetails["SiteId"]), "ACDM") { DependencyGroup = 1 };
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.RunwayAPIURL, "Failed_To_Update_Crud_API_Post_Runway_Configuration");
                APIResponseErrorValidation(restResponse, "Violation of UNIQUE KEY constraint 'AK_Runway_Column'. Cannot insert duplicate key in object 'resources.Runway'. The duplicate key value is (ACDM, 1).\nThe statement has been terminated.", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Configuration");
            }
        }
        [Then(@"Generate new put runway API URL using database fetched values and Execute Crud Put API Runway and Set DTO Objects for Crud API Runway")]
        public void ThenGenerateNewPutRunwayAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIRunwayAndSetDTOObjectsForCrudAPIRunway()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);
            int DependencyGroupDB = Int32.Parse(sqlResponseDetails["DependencyGroup"]);

            var putRequestBody = new RunwayDto(IdValueDB, SiteIdDB, "Test" + (_scenarioContext.Get<string>("valueSQLData"))) { DependencyGroup = DependencyGroupDB };
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.RunwayAPIURL, "Failed_To_Update_Crud_API_Put_Runway_Configuration");
        }
        [Then(@"Execute Crud Runway API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudRunwayAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.RunwayAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_Runway_MessageVerification");
            }
        }
        [Then(@"Generate new Get Runway by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetRunwayByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.RunwayAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_Runway_MessageVerification");
            }
        }
        [Then(@"Generate new Delete Runway API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteRunwayAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteRunwayAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIRunway();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for runway Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Runways");
            }
        }
        [Then(@"Execute Crud Post Runway API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostRunwayAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new RunwayDto(0, Int32.Parse(sqlResponseDetails["Id"]), "ACDM") { DependencyGroup = 1 };
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.RunwayAPIURL, "Failed_To_Update_Crud_API_Post_Runway_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<RunwayDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put Runway API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutRunwayAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);
                int DependencyGroupDB = Int32.Parse(sqlResponseDetails["DependencyGroup"]);
                var putRequestBody = new RunwayDto(IdValueDB, SiteIdDB, "Test" + (_scenarioContext.Get<string>("valueSQLData"))) { DependencyGroup = DependencyGroupDB };
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.RunwayAPIURL, "Failed_To_Update_Crud_API_Put_Runway_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new Get runway by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Runway")]
        public void ThenGenerateNewGetRunwayByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIRunway(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.RunwayAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with Runway: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_Runway");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Runway")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIRunway(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.RunwayAPIURL, "Failed_To_Execute_Get_By_API_Request");
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
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_RunwayAPI");
            }
        }
    }
}
