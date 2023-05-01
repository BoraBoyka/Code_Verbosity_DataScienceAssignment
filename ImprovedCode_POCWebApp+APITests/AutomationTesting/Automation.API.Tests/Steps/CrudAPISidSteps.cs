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
using Gherkin;
using System.Security.Policy;

namespace ACDMAutomation.API.Tests.Steps
{
    [Binding]
    public class CrudAPISidSteps : CrudAPIBaseMethods<SidDto>
    {
        public CrudAPISidSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        private APITests apiTest => new APITests(_driver);

        [Then(@"Execute Get Crud Sid API And Set DTO Objects for Crud API Sid for site claim ""([^""]*)""")]
        public void ThenExecuteGetCrudSidAPIAndSetDTOObjectsForCrudAPISidForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.SidAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.SidFullName).ToList();
        }

        [Then(@"Compare values from API response set to DB record set for Crud Sid Get API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudSidGetAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }

        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Sid API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudSidAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> sidDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(sidDetails, dtoResultSingleRecord);
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is SidDto sidDto)
                {
                    Assert.AreEqual(sidDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId did not match:" + sidDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(sidDto.SidShortName, compareDictionary["SidShortName"], "SidShortName did not match:" + sidDto.SidShortName + " != " + compareDictionary["SidShortName"]);
                    Assert.AreEqual(sidDto.SidFullName, compareDictionary["SidFullName"], "SidFullName did not match:" + sidDto.SidFullName + " != " + compareDictionary["SidFullName"]);
                    Assert.AreEqual(sidDto.Runway.Id, Int32.Parse(compareDictionary["Runway"]), "Runway did not match:" + sidDto.Runway.Id + " != " + compareDictionary["Runway"]);                                 
                }
                else
                {
                    Assert.Fail("Return values which are not Sid type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }

        [Then(@"Validate that the response returned from the above step should be an empty List")]
        public void ThenValidateThatTheResponseReturnedFromTheAboveStepShouldBeAnEmptyList()
        {
            if (dtoResultList.Count == 0)
            {
                Assert.IsTrue(true, "Response content is an empty list");
            }
            else
            {
                Assert.IsFalse(true, "Response content is not an empty list");
            }
        }

        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request and Set DTO Objects for Crud API Sid")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestAndSetDTOObjectsForCrudAPISid(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.SidAPIURL, "Failed_To_Execute_Get_By_API_Request");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<SidDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_NameColumn_SidAPI");
            }
        }

        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Sid")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPISid(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
                Dictionary<string, string> sidDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
                AddOrUpdateScenarioContext("valueSQLData", sidDetails[colName]);
                string valueSQLData = (_scenarioContext.Get<string>("valueSQLData"));
                var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.SidAPIURL, "Failed_To_Execute_Get_By_API_Request");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site with Sid shortname: " + valueSQLData + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_ShortNameColumn_SidAPI");
            }
        }

        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Full Name Sid")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIFullNameSid(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
                Dictionary<string, string> sidDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
                AddOrUpdateScenarioContext("valueSQLData", sidDetails[colName]);
                string valueSQLData = (_scenarioContext.Get<string>("valueSQLData"));
                var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.SidAPIURL, "Failed_To_Execute_Get_By_API_Request");               
                ResponseErrorMessageVerification(restResponse, "User has no access to the site with SidFullName: " + valueSQLData + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_FullNameColumn_SidAPI");
            }
        }

        [Then(@"Execute Crud Put Sid API and Set DTO Objects for Crud Sid API")]
        public void ThenExecuteCrudPutSidAPIAndSetDTOObjectsForCrudSidAPI()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
            string Name = sqlResponseDetails["Name"];
            var putRequestBody = new List<SidDto>() { new SidDto() { SiteId = SiteId, SidShortName = "AAA", SidFullName = "ABC", Runway = new RunwayDto() { Name = Name } } };
            dtoResultList = PutAPIWithDeserializeList(putRequestBody, APITests.apiConfigDTO.SidAPIURL, "Failed_To_Update_Crud_API_Put_Sid_Configuration");
        }

        [Then(@"again Execute Crud Put Sid API using the same sid short and full name and validate that it should return the same result in the response body as we have above")]
        public void ThenAgainExecuteCrudPutSidAPIUsingTheSameSidShortAndFullNameAndValidateThatItShouldReturnTheSameResultInTheResponseBodyAsWeHaveAbove()
        {
            ThenExecuteCrudPutSidAPIAndSetDTOObjectsForCrudSidAPI();
        }
        [Then(@"Execute Crud Put Sid API for incorrect Site and validate that user should get an error message in the response body")]
        public void ThenExecuteCrudPutSidAPIForIncorrectSiteAndValidateThatUserShouldGetAnErrorMessageInTheResponseBody()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            string Name = sqlResponseDetails["Name"];
            int SiteId = Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));
            var putRequestBody = new List<SidDto>() { new SidDto() { SiteId = SiteId, SidShortName = "AAA", SidFullName = "ABC", Runway = new RunwayDto() { Name = Name } } };
            var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.SidAPIURL, "Failed_To_Update_Crud_API_Put_Sid_Configuration");
            ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
        }
    }
}
