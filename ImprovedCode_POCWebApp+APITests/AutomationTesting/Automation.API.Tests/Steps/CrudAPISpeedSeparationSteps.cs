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

namespace ACDMAutomation.API.Tests.Steps
{
    [Binding]
    public class CrudAPISpeedSeparationSteps : CrudAPIBaseMethods<SpeedSeparationDto>
    {
        public CrudAPISpeedSeparationSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        private APITests apiTest => new APITests(_driver);

        [Then(@"Execute Get Crud Speed Separation API And Set DTO Objects for Crud API Speed Separation for site claim ""([^""]*)""")]
        public void ThenExecuteGetCrudSpeedSeparationAPIAndSetDTOObjectsForCrudAPISpeedSeparationForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.SpeedSeparationAPIURL);
            dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
        }

        [Then(@"Compare values from API response set to DB record set for Crud Speed Separation Get API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudSpeedSeparationGetAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is SpeedSeparationDto speedSeparationDto)
                {
                    Assert.AreEqual(speedSeparationDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId did not match:" + speedSeparationDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(speedSeparationDto.Leader, compareDictionary["LeaderSpeedClass"], "LeaderSpeedClass did not match:" + speedSeparationDto.Leader + " != " + compareDictionary["LeaderSpeedClass"]);
                    Assert.AreEqual(speedSeparationDto.Follower, compareDictionary["FollowerSpeedClass"], "FollowerSpeedClass did not match:" + speedSeparationDto.Follower + " != " + compareDictionary["FollowerSpeedClass"]);
                    Assert.AreEqual(speedSeparationDto.SeparationSeconds, Int32.Parse(compareDictionary["SeparationSeconds"]), "SeparationSeconds did not match:" + speedSeparationDto.SeparationSeconds + " != " + compareDictionary["SeparationSeconds"]);
                }
                else
                {
                    Assert.Fail("Return values which are not Speed Separation type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }

        [Then(@"Validate that the response returned from the above step for Speed Separation matrix should be an empty List")]
        public void ThenValidateThatTheResponseReturnedFromTheAboveStepForSpeedSeparationMatrixShouldBeAnEmptyList()
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

        [Then(@"Execute Crud Put Speed Matrix API and Set DTO Objects for Crud Speed Matrix API")]
        public void ThenExecuteCrudPutSpeedMatrixAPIAndSetDTOObjectsForCrudSpeedMatrixAPI()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int siteId = Int32.Parse(sqlResponseDetails["SiteId"]);
            string FollowerSpeedClass = sqlResponseDetails["FollowerSpeedClass"];

            var putRequestBody = new List<SpeedSeparationDto>() { new SpeedSeparationDto() { SiteId = siteId, Leader = "V_C172", Follower = FollowerSpeedClass, SeparationSeconds = 150 } };
            dtoResultList = PutAPIWithDeserializeList(putRequestBody, APITests.apiConfigDTO.SpeedSeparationAPIURL, "Failed_To_Update_Crud_API_Put_Speed_Separation_Configuration");
        }

        [Then(@"Execute Crud Put Speed Separation API for incorrect Site and validate that user should get an error message in the response body")]
        public void ThenExecuteCrudPutSpeedSeparationAPIForIncorrectSiteAndValidateThatUserShouldGetAnErrorMessageInTheResponseBody()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int siteId = Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));
            string LeaderSpeedClass = sqlResponseDetails["LeaderSpeedClass"];
            string FollowerSpeedClass = sqlResponseDetails["FollowerSpeedClass"];

            var putRequestBody = new List<SpeedSeparationDto>() { new SpeedSeparationDto() { SiteId = siteId, Leader = LeaderSpeedClass, Follower = FollowerSpeedClass, SeparationSeconds = 150 } };
            var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.SpeedSeparationAPIURL, "Failed_To_Update_Crud_API_Put_Speed_Separation_Configuration");
            ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
        }
    }
}
