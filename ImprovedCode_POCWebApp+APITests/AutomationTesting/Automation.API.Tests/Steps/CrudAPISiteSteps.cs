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
    public class CrudAPISiteSteps : CrudAPIBaseMethods<SiteDto>
    {
        public CrudAPISiteSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        {  }
        [Then(@"Execute Crud Site API And Set DTO Objects for Crud API Site for site claim ""([^""]*)""")]
        public void ThenExecuteCrudSiteAPIAndSetDTOObjectsForCrudAPISiteForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.SiteAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Execute Crud Site API And Set DTO Objects for Crud API Site")]
        public void ThenExecuteCrudSiteAPIAndSetDTOObjectsForCrudAPISite()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.SiteAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud Site API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudSiteAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is SiteDto siteDto)
                {
                    Assert.AreEqual(siteDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + siteDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(siteDto.Iata, compareDictionary["Iata"], "Iata values do not match:" + siteDto.Iata + " != " + compareDictionary["Iata"]);
                    Assert.AreEqual(siteDto.Icao, compareDictionary["Icao"], "Icao values do not match:" + siteDto.Icao + " != " + compareDictionary["Icao"]);
                    Assert.AreEqual(siteDto.Name, compareDictionary["Name"], "Name values do not match:" + siteDto.Name + " != " + compareDictionary["Name"]);
                    Assert.AreEqual(siteDto.IanaTimezone, compareDictionary["IanaTimezone"], "IanaTimezone values do not match:" + siteDto.IanaTimezone + " != " + compareDictionary["IanaTimezone"]);
                    Assert.AreEqual(siteDto.MsTimezone, compareDictionary["MsTimezone"], "MsTimezone values do not match:" + siteDto.MsTimezone + " != " + compareDictionary["MsTimezone"]);
                }
                else
                {
                    Assert.Fail("Return values which are not site type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Generate new Get site by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Site")]
        public void ThenGenerateNewGetSiteByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPISite(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.SiteAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<SiteDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Site API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudSiteAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> siteDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(siteDetails, dtoResultSingleRecord);
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Site")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudSite(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.SiteAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request and Set DTO Objects for Crud API Site")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestAndSetDTOObjectsForCrudAPISite(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                dtoResultList = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.SiteAPIURL, "Failed_To_Execute_Get_By_API_Request");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_SiteAPI");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request and Set DTO Objects for Crud API Site")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestAndSetDTOObjectsForCrudAPISite(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.SiteAPIURL, "Failed_To_Execute_Get_By_API_Request");
                if (colName.Equals("Iata"))
                {
                    dtoResultSingleRecord = JsonConvert.DeserializeObject<SiteDto>(restResponse.Content);
                }
                else if (colName.Equals("Icao"))
                {
                    dtoResultSingleRecord = JsonConvert.DeserializeObject<SiteDto>(restResponse.Content);
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_SiteAPI");
            }
        }
        [Then(@"Execute Crud Site API with an incorrect Site id and validate that the api should return error in the response body")]
        public void ThenExecuteCrudSiteAPIWithAnIncorrectSiteIdAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.SiteAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest, "404 Not Found");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_IncorrectSiteId_Crud_API_Site_MessageVerification");
            }
        }
        [Then(@"Generate new Get Site by ""([^""]*)"" API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetSiteByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.SiteAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest, "404 Not Found");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_Site_MessageVerification");
            }
        }
        [Then(@"Generate new Delete site API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Site")]
        public IRestResponse ThenGenerateNewDeleteSiteAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPISite()
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.SiteAPIURL, "Id", "Failed_To_Execute_Get_By_API");
                IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Site");
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Site");
                return null;
            }
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Site")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudSite()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.SiteAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Execute Crud Post Site API and Set DTO Objects for Crud API Site")]
        public void ThenExecuteCrudPostSiteAPIAndSetDTOObjectsForCrudAPISite()
        {
            var postRequestBody = new SiteDto(0, "TXL", "TDDT", "Munich International Airport", "Europe/Berlin", "W. Europe Standard Time");
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.SiteAPIURL, "Failed_To_Update_Crud_API_Post_Site_Configuration");
        }
        [Then(@"Generate new put site API URL using database fetched values and Execute Crud Put API Site and Set DTO Objects for Crud API Site")]
        public void ThenGenerateNewPutSiteAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPISiteAndSetDTOObjectsForCrudAPISite()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
            string IataDB = sqlResponseDetails["Iata"];
            string IcaoDB = sqlResponseDetails["Icao"];
            string NameDB = sqlResponseDetails["Name"];
            string IanaTimezoneDB = sqlResponseDetails["IanaTimezone"];
            string MsTimezoneDB = sqlResponseDetails["MsTimezone"];

            var putRequestBody = new SiteDto(IdValueDB, IataDB, IcaoDB, "Test " + NameDB, IanaTimezoneDB, MsTimezoneDB);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.SiteAPIURL, "Failed_To_Update_Crud_API_Put_Site_Configuration");
        }
        [Then(@"Execute Crud Site API with an incorrect Site id and validate that the api should still return valid response")]
        public void ThenExecuteCrudSiteAPIWithAnIncorrectSiteIdAndValidateThatTheApiShouldStillReturnValidResponse()
        {
            ThenExecuteCrudSiteAPIAndSetDTOObjectsForCrudAPISite();
        }
    }
}
