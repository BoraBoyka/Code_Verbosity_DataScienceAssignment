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
using MongoDB.Driver;

namespace ACDMAutomation.API.Tests.Steps
{
    [Binding]
    public class CrudAPISidSeparationSteps : CrudAPIBaseMethods<SidSeparationDto>
    {
        public CrudAPISidSeparationSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        private APITests apiTest => new APITests(_driver);

        [Then(@"Execute Get Crud Sid Separation API And Set DTO Objects for Crud API Sid Separation for site claim ""([^""]*)""")]
        public void ThenExecuteGetCrudSidSeparationAPIAndSetDTOObjectsForCrudAPISidSeparationForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.SidSeparationAPIURL);
            dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
        }

        [Then(@"Compare values from API response set to DB record set for Crud Sid Separation Get API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudSidSeparationGetAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is SidSeparationDto sidSeparationDto)
                {
                    Assert.AreEqual(sidSeparationDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId did not match:" + sidSeparationDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(sidSeparationDto.LeaderSidName, compareDictionary["LeaderSid"], "LeaderSidName did not match:" + sidSeparationDto.LeaderSidName + " != " + compareDictionary["LeaderSid"]);
                    Assert.AreEqual(sidSeparationDto.FollowerSidName, compareDictionary["FollowerSid"], "FollowerSidName did not match:" + sidSeparationDto.FollowerSidName + " != " + compareDictionary["FollowerSid"]);
                    Assert.AreEqual(sidSeparationDto.Condition, compareDictionary["Condition"], "Condition did not match:" + sidSeparationDto.Condition + " != " + compareDictionary["Condition"]);
                    Assert.AreEqual(sidSeparationDto.ExtraTimeInMinToBeAddedToSpeedMatrix, Int32.Parse(compareDictionary["ExtraTimeInMinToBeAddedToSpeedMatrix"]), "ExtraTimeInMinToBeAddedToSpeedMatrix did not match:" + sidSeparationDto.ExtraTimeInMinToBeAddedToSpeedMatrix + " != " + compareDictionary["ExtraTimeInMinToBeAddedToSpeedMatrix"]);
                    Assert.AreEqual(sidSeparationDto.LeaderSid.SidShortName, compareDictionary["LeaderSidSidShortName"], "LeaderSidSidShortName did not match:" + sidSeparationDto.LeaderSidName + " != " + compareDictionary["LeaderSidSidShortName"]);
                    Assert.AreEqual(sidSeparationDto.LeaderSid.SidFullName, compareDictionary["LeaderSidSidFullName"], "LeaderSidSidFullName did not match:" + sidSeparationDto.LeaderSid.SidFullName + " != " + compareDictionary["LeaderSidSidFullName"]);
                    Assert.AreEqual(sidSeparationDto.FollowerSid.SidShortName, compareDictionary["FollowerSidSidShortName"], "FollowerSidSidShortName did not match:" + sidSeparationDto.FollowerSid.SidShortName + " != " + compareDictionary["FollowerSidSidShortName"]);                    
                    Assert.AreEqual(sidSeparationDto.FollowerSid.SidFullName, compareDictionary["FollowerSidSidFullName"], "FollowerSidSidFullName did not match:" + sidSeparationDto.FollowerSid.SidFullName + " != " + compareDictionary["FollowerSidSidFullName"]);
                }
                else
                {
                    Assert.Fail("Return values which are not Sid Separation type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }

        [Then(@"Validate that the response returned from the above step for Sid Separation matrix should be an empty List")]
        public void ThenValidateThatTheResponseReturnedFromTheAboveStepForSidSeparationMatrixShouldBeAnEmptyList()
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

        [Then(@"Execute Crud Put Sid Separation API and Set DTO Objects for Crud Sid Separation API")]
        public void ThenExecuteCrudPutSidSeparationAPIAndSetDTOObjectsForCrudSidSeparationAPI()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int runwayId = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
            string Name = sqlResponseDetails["Name"];
            int dependencyGroup = Int32.Parse(sqlResponseDetails["DependencyGroup"]);
            string oppositeRunway = sqlResponseDetails["OppositeRunway"];

            var putRequestBody = new List<SidSeparationDto>() { new SidSeparationDto() { SiteId = SiteId, LeaderSidName = "AAA", FollowerSidName = "ABC", Condition = "VTAB", ExtraTimeInMinToBeAddedToSpeedMatrix = 0 ,
                LeaderSid = new SidDto(){SiteId = SiteId, SidShortName = "AAA" , Runway = new RunwayDto() { Id = runwayId, SiteId = SiteId, Name = Name, DependencyGroup = dependencyGroup, OppositeRunway = oppositeRunway }, SidFullName = "ABC" },
                FollowerSid = new SidDto(){SiteId = SiteId, SidShortName = "AAA" , Runway = new RunwayDto() { Id = runwayId, SiteId = SiteId, Name = Name, DependencyGroup = dependencyGroup, OppositeRunway = oppositeRunway }, SidFullName = "ABC" } } };             
            dtoResultList = PutAPIWithDeserializeList(putRequestBody, APITests.apiConfigDTO.SidSeparationAPIURL, "Failed_To_Update_Crud_API_Put_Sid_Separation_Configuration");
        }

        [Then(@"again Execute Crud Put Sid Separation API using the existing Leader Sid and Follower Sid name and validate that it should return error in the response body")]
        public void ThenAgainExecuteCrudPutSidSeparationAPIUsingTheExistingLeaderSidAndFollowerSidNameAndValidateThatItShouldReturnErrorInTheResponseBody()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int runwayId = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
            string Name = sqlResponseDetails["Name"];
            int dependencyGroup = Int32.Parse(sqlResponseDetails["DependencyGroup"]);
            string oppositeRunway = sqlResponseDetails["OppositeRunway"];
            string sidShortName = sqlResponseDetails["SidShortName"];
            string sidFullName = sqlResponseDetails["SidFullName"];

            var putRequestBody = new List<SidSeparationDto>() { new SidSeparationDto() { SiteId = SiteId, LeaderSidName = sidShortName, FollowerSidName = sidFullName, Condition = "VTAB", ExtraTimeInMinToBeAddedToSpeedMatrix = 0 ,
                LeaderSid = new SidDto(){SiteId = SiteId, SidShortName = sidShortName , Runway = new RunwayDto() { Id = runwayId, SiteId = SiteId, Name = Name, DependencyGroup = dependencyGroup, OppositeRunway = oppositeRunway }, SidFullName = sidFullName },
                FollowerSid = new SidDto(){SiteId = SiteId, SidShortName = sidShortName , Runway = new RunwayDto() { Id = runwayId, SiteId = SiteId, Name = Name, DependencyGroup = dependencyGroup, OppositeRunway = oppositeRunway }, SidFullName = sidFullName } } };

            var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.SidSeparationAPIURL, "Failed_To_Update_Crud_API_Put_Sid_Separation_Configuration");
            ResponseErrorMessageVerification(restResponse, "Violation of PRIMARY KEY constraint 'PK_Sid'. Cannot insert duplicate key in object 'resources.Sid'. The duplicate key value is (1, AAA).\nViolation of PRIMARY KEY constraint 'PK_SidSeparationMatrix'. Cannot insert duplicate key in object 'resources.SidSeparationMatrix'. The duplicate key value is (1, AAA, AAA).\nThe statement has been terminated.\nThe statement has been terminated. BadRequest", "MessageCode", "Message");
        }

        [Then(@"Execute Crud Put Sid Separation API for incorrect Site and validate that user should get an error message in the response body")]
        public void ThenExecuteCrudPutSidSeparationAPIForIncorrectSiteAndValidateThatUserShouldGetAnErrorMessageInTheResponseBody()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int runwayId = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
            string Name = sqlResponseDetails["Name"];
            int dependencyGroup = Int32.Parse(sqlResponseDetails["DependencyGroup"]);
            string oppositeRunway = sqlResponseDetails["OppositeRunway"];

            var putRequestBody = new List<SidSeparationDto>() { new SidSeparationDto() { SiteId = Int32.Parse(_scenarioContext.Get<string>("valueSQLData")), LeaderSidName = "AAA", FollowerSidName = "ABC", Condition = "VTAB", ExtraTimeInMinToBeAddedToSpeedMatrix = 0,
                LeaderSid = new SidDto(){SiteId = SiteId, SidShortName = "AAA" , SidFullName = "ABC", Runway = new RunwayDto() { Id = runwayId, SiteId = SiteId, Name = Name, DependencyGroup = dependencyGroup, OppositeRunway = oppositeRunway } },
                FollowerSid = new SidDto(){SiteId = SiteId, SidShortName = "AAA" , SidFullName = "ABC", Runway = new RunwayDto() { Id = runwayId, SiteId = SiteId, Name = Name, DependencyGroup = dependencyGroup, OppositeRunway = oppositeRunway } } } };

            var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.SidSeparationAPIURL, "Failed_To_Update_Crud_API_Put_Sid_Separation_Configuration");
            ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
        }
    }
}
