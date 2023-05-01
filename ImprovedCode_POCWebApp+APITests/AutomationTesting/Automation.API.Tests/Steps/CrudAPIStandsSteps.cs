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
    public class CrudAPIStandsSteps : CrudAPIBaseMethods<StandDto>
    {
        public CrudAPIStandsSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        private StandAreaDto StandAreaDto;
        [Then(@"Execute Crud Stand API And Set DTO Objects for Crud API Stand for site claim ""([^""]*)""")]
        public void ThenExecuteCrudStandAPIAndSetDTOObjectsForCrudAPIStandForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.StandAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Execute Crud Stand API And Set DTO Objects for Crud API Stand")]
        public void ThenExecuteCrudStandAPIAndSetDTOObjectsForCrudAPIStand()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.StandAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud Stand API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudStandAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is StandDto standDto)
                {
                    Assert.AreEqual(standDto.Id, Int32.Parse(compareDictionary["Id"]), "StandId did not match:" + standDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(standDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId did not match:" + standDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(standDto.Name, compareDictionary["Name"], "Name did not match:" + standDto.Name + " != " + compareDictionary["Name"]);
                    Assert.AreEqual(standDto.StandAreaDto.Id, Int32.Parse(compareDictionary["StandAreaId"]), "StandAreaId did not match:" + standDto.StandAreaDto.Id + " != " + compareDictionary["StandAreaId"]);
                }
                else
                {
                    Assert.Fail("Return values which are not stand type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Generate new Get Stand by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Stand")]
        public void ThenGenerateNewGetStandByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIStand(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.StandAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<StandDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_StandType");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Stand API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudStandAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> standDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(standDetails, dtoResultSingleRecord);
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Stand")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudStand(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.StandAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request and Set DTO Objects for Crud API Stand")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestAndSetDTOObjectsForCrudAPIStand(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                dtoResultList = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Execute_Get_By_API_Request");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_StandAPI");
            }
        }
        [Then(@"Generate new Delete Stand API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Stand")]
        public IRestResponse ThenGenerateNewDeleteStandAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIStand()
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.StandAPIURL, "Id", "Failed_To_Execute_Get_By_API");
                IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Stands");
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Stands");
                return null;
            }
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Stand")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudStand()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.StandAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Execute Crud Post Stand API and Set DTO Objects for Crud API Stand")]
        public void ThenExecuteCrudPostStandAPIAndSetDTOObjectsForCrudAPIStand()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
            var postRequestBody = PostRequestBody(SiteId);
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Update_Crud_API_Post_Stand_Configuration");
        }
        public StandDto PostRequestBody(int SiteId)
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            string Name = sqlResponseDetails["Name"];
            var postRequestBody = new StandDto() { SiteId = SiteId, Name = "Auto" + (_scenarioContext.Get<string>("valueSQLData")), StandAreaDto = new StandAreaDto() { Name = Name }};
            return postRequestBody;
        }
        [Then(@"again Execute Crud Post Stand API with a duplicate Name value and verify that it should return error via API for Crud Stand API")]
        public void ThenAgainExecuteCrudPostStandAPIWithADuplicateNameValueAndVerifyThatItShouldReturnErrorViaAPIForCrudStandAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new StandDto()
                {
                    SiteId = Int32.Parse(sqlResponseDetails["SiteId"]),
                    Name = "Test",
                    StandAreaDto = new StandAreaDto() { Name = "Test" }
                };
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Update_Crud_API_Post_Stand_Configuration");
                APIResponseErrorValidation(restResponse, "Cannot insert duplicate key row in object 'resources.Stand' with unique index 'IX_Stand_Name'. The duplicate key value is (Test, 1).", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Configuration");
            }
        }
        [Then(@"Generate new put Stand API URL using database fetched values and Execute Crud Put API Stand and Set DTO Objects for Crud API Stand")]
        public void ThenGenerateNewPutStandAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIStandAndSetDTOObjectsForCrudAPIStand()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            string newName = "Auto" + (_scenarioContext.Get<string>("valueSQLData"));
            var putRequestBody = new StandDto()
            {
                Id = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")),
                SiteId = Int32.Parse(sqlResponseDetails["SiteId"]),
                Name = newName,
                StandAreaDto = new StandAreaDto() { Name = sqlResponseDetails["Name"] }
            };
            Thread.Sleep(200);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Update_Crud_API_Put_Stand_Configuration");
        }
        [Then(@"Execute Crud Post ""([^""]*)"" API and Set DTO Objects for Crud API Add Stands")]
        public void ThenExecuteCrudPostAPIAndSetDTOObjectsForCrudAPIAddStands(string inputURL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int Id = Int32.Parse(sqlResponseDetails["Id"]);
                string Name = sqlResponseDetails["Name"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string StandName1NewValue = "Name" + (_scenarioContext.Get<string>("valueSQLData"));
                string StandName2NewValue = "Test" + (_scenarioContext.Get<string>("valueSQLData"));
                string appendedURL = APITests.apiConfigDTO.StandAPIURL + "/" + inputURL;

                List<StandDto> Stands = new();
                Stands.Add(new StandDto() { SiteId = SiteId, Name = StandName1NewValue, StandAreaDto = new StandAreaDto() { Name = Name } } );
                Stands.Add(new StandDto() { SiteId = SiteId, Name = StandName2NewValue, StandAreaDto = new StandAreaDto() { Name = Name } });
                dtoResultList = PostAPIWithDeserializeList(Stands, appendedURL, "Failed_To_Update_Crud_API_Post_Stand_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_StandsListConfiguration");
            }
        }
        [Then(@"Execute Crud Stand API with an incorrect Site id  for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudStandAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.StandAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_Stand_MessageVerification");
            }
        }
        [Then(@"Generate new Get Stand by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetStandByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.StandAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_Stands_MessageVerification");
            }
        }
        [Then(@"Generate new Delete Stand API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteStandAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteStandAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIStand();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for stand Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Stands");
            }
        }
        [Then(@"Execute Crud Post Stand API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostStandAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                var postRequestBody = PostRequestBody(SiteId);
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Update_Crud_API_Post_Stand_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<StandDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put Stand API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutStandAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string newName = "Auto" + (_scenarioContext.Get<string>("valueSQLData"));
                var putRequestBody = new StandDto()
                {
                    Id = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")),
                    SiteId = Int32.Parse(sqlResponseDetails["SiteId"]),
                    Name = newName,
                    StandAreaDto = new StandAreaDto() { Name = sqlResponseDetails["Name"] }
                };
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Update_Crud_API_Put_Stand_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<StandDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"Execute Crud Post Stand API for negative test and verify that error should be thrown via API")]
        public void ThenExecuteCrudPostStandAPIForNegativeTestAndVerifyThatErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                var postRequestBody = PostRequestBody(SiteId);
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Update_Crud_API_Post_Stand_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<StandDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_FlightConfiguration_Negative scenario");
            }
        }
        [Then(@"Generate new put Stand API URL for negative test and verify that error should be thrown via API")]
        public void ThenGenerateNewPutStandAPIURLForNegativeTestAndVerifyThatErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = PostRequestBody(SiteId);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Update_Crud_API_Put_Stand_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_NegativeTest");
            }
        }
        [Then(@"Generate new Get Stand by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Stand")]
        public void ThenGenerateNewGetStandByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIStand(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.StandAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with StandId: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_Stand");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Stand")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIStand(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.StandAPIURL, "Failed_To_Execute_Get_By_API_Request");
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
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_StandAPI");
            }
        }
    }
}
