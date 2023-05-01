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
    public class CrudAPIWakeSeparationTime : CrudAPIBaseMethods<WakeSeparationTimeDto>
    {
        public CrudAPIWakeSeparationTime(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        [Then(@"Execute Crud WakeSeparationTime API And Set DTO Objects for Crud API WakeSeparationTime for site claim ""([^""]*)""")]
        public void ThenExecuteCrudWakeSeparationTimeAPIAndSetDTOObjectsForCrudAPIWakeSeparationTimeForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.WakeSeparationTimeAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud WakeSeparationTime API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudWakeSeparationTimeAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is WakeSeparationTimeDto wakeSeparationTimeDto)
                {
                    Assert.AreEqual(wakeSeparationTimeDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + wakeSeparationTimeDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(wakeSeparationTimeDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + wakeSeparationTimeDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(wakeSeparationTimeDto.LeaderWakeTurbulenceCategory.Id, Int32.Parse(compareDictionary["LeaderWakeTurbulenceCategoryId"]), "LeaderWakeTurbulenceCategoryId values do not match:" + wakeSeparationTimeDto.LeaderWakeTurbulenceCategory.Id + " != " + compareDictionary["LeaderWakeTurbulenceCategoryId"]);
                    Assert.AreEqual(wakeSeparationTimeDto.FollowerWakeTurbulenceCategory.Id, Int32.Parse(compareDictionary["FollowerWakeTurbulenceCategoryId"]), "FollowerWakeTurbulenceCategoryId values do not match:" + wakeSeparationTimeDto.FollowerWakeTurbulenceCategory.Id + " != " + compareDictionary["FollowerWakeTurbulenceCategoryId"]);
                    Assert.AreEqual(wakeSeparationTimeDto.SeparationSeconds, Int32.Parse(compareDictionary["SeparationSeconds"]), "SeparationSeconds do not match:" + wakeSeparationTimeDto.SeparationSeconds + " != " + compareDictionary["SeparationSeconds"]);
                }
                else
                {
                    Assert.Fail("Return values which are not wake separation times type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Execute Crud Post WakeSeparationTime API and Set DTO Objects for Crud API WakeSeparationTime")]
        public void ThenExecuteCrudPostWakeSeparationTimeAPIAndSetDTOObjectsForCrudAPIWakeSeparationTime()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new WakeSeparationTimeDto(0, Int32.Parse(sqlResponseDetails["Id"]),
            new WakeTurbulenceCategoryDto { Id = 1, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "A", CategoryName = "Super Heavy" },
            new WakeTurbulenceCategoryDto { Id = 1, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "A", CategoryName = "Super Heavy" }, 60);
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Post_WakeSeparationTime_Configuration");
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud WakeSeparationTime API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudWakeSeparationTimeAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> wakeSeparationTimeDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(wakeSeparationTimeDetails, dtoResultSingleRecord);
        }
        [Then(@"Generate new Delete WakeSeparationTime API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeSeparationTime")]
        public IRestResponse ThenGenerateNewDeleteWakeSeparationTimeAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIWakeSeparationTime()
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Id", "Failed_To_Execute_Get_By_API");
                IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_WakeSeparationTime");
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_WakeSeparationTime");
                return null;
            }
        }
        [Then(@"Generate new Get WakeSeparationTime by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API WakeSeparationTime")]
        public void ThenGenerateNewGetWakeSeparationTimeByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIWakeSeparationTime(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.WakeSeparationTimeAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<WakeSeparationTimeDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud WakeSeparationTime")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudWakeSeparationTime(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.WakeSeparationTimeAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new put WakeSeparationTime API URL using database fetched values and Execute Crud Put API WakeSeparationTime and Set DTO Objects for Crud API WakeSeparationTime")]
        public void ThenGenerateNewPutWakeSeparationTimeAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIWakeSeparationTimeAndSetDTOObjectsForCrudAPIWakeSeparationTime()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var putRequestBody = new WakeSeparationTimeDto(Int32.Parse(sqlResponseDetails["Id"]), Int32.Parse(sqlResponseDetails["SiteId"]),
            new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["LeaderWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" },
            new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["FollowerWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy"}, 20);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Put_WakeSeparationTime_Configuration");
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud WakeSeparationTime")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudWakeSeparationTime()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Generate new Get WakeSeparationTime using Leader Category and Follower Category ""([^""]*)"" in the API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API WakeSeparationTime")]
        public void ThenGenerateNewGetWakeSeparationTimeUsingLeaderCategoryAndFollowerCategoryInTheAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIWakeSeparationTime(string colName)
        {
            var restOBJSetup = ExecuteGetByAPIForMultiInputValues(APITests.apiConfigDTO.WakeSeparationTimeAPIURL, colName, "Failed_To_Execute_Get_By_API");
            var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultSingleRecord = restResponse.Content;
        }
        [Then(@"Compare values from API response set for the one field to DB record set for Crud WakeSeparationTime API")]
        public void ThenCompareValuesFromAPIResponseSetForTheOneFieldToDBRecordSetForCrudWakeSeparationTimeAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> wakeSeparationTimeDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            Assert.AreEqual(Int32.Parse(dtoResultSingleRecord), Int32.Parse(wakeSeparationTimeDetails["SeparationSeconds"]), "SeparationSeconds do not match:" + dtoResultSingleRecord + " != " + wakeSeparationTimeDetails["SeparationSeconds"]);
        }
        [Then(@"Execute Crud Post ""([^""]*)"" API and Set DTO Objects for Crud API Add WakeSeparationTime")]
        public void ThenExecuteCrudPostAPIAndSetDTOObjectsForCrudAPIAddWakeSeparationTime(string inputURL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string appendedURL = APITests.apiConfigDTO.WakeSeparationTimeAPIURL + "/" + inputURL;
                List<WakeSeparationTimeDto> wakeSeparationTime = new();
                wakeSeparationTime.Add(new WakeSeparationTimeDto(0, Int32.Parse(sqlResponseDetails["Id"]), new WakeTurbulenceCategoryDto() { Id = 1, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "A", CategoryName = "Super Heavy"}, new WakeTurbulenceCategoryDto() { Id = 1, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "A", CategoryName = "Super Heavy" }, 60));
                wakeSeparationTime.Add(new WakeSeparationTimeDto(0, Int32.Parse(sqlResponseDetails["Id"]), new WakeTurbulenceCategoryDto() { Id = 2, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "B", CategoryName = "Upper Heavy" }, new WakeTurbulenceCategoryDto() { Id = 2, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "B", CategoryName = "Upper Heavy" }, 20));
                dtoResultList = PostAPIWithDeserializeList(wakeSeparationTime, appendedURL, "Failed_To_Update_Crud_API_Post_WakeSeparationTime_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_WakeSeparationTimeListConfiguration");
            }
        }
        [Then(@"Execute Crud WakeSeparationTime API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudWakeSeparationTimeAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.WakeSeparationTimeAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_WakeSeparationTime_MessageVerification");
            }
        }
        [Then(@"Generate new Get WakeSeparationTime by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetWakeSeparationTimeByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.WakeSeparationTimeAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN,siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_WakeSeparationTime_MessageVerification");
            }
        }
        [Then(@"Generate new Delete WakeSeparationTime API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteWakeSeparationTimeAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteWakeSeparationTimeAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIWakeSeparationTime();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for WakeSeparationTime Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_WakeSeparationTimes");
            }
        }
        [Then(@"Execute Crud Post WakeSeparationTime API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostWakeSeparationTimeAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new WakeSeparationTimeDto(0, Int32.Parse(sqlResponseDetails["Id"]),
                new WakeTurbulenceCategoryDto() { Id = 1, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "A", CategoryName = "Super Heavy" },
                new WakeTurbulenceCategoryDto() { Id = 1, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "A", CategoryName = "Super Heavy" }, 60);

                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Post_WakeSeparationTimes_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<WakeSeparationTimeDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put WakeSeparationTime API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutWakeSeparationTimeAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var putRequestBody = new WakeSeparationTimeDto(Int32.Parse(sqlResponseDetails["Id"]), Int32.Parse(sqlResponseDetails["SiteId"]),
                new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["LeaderWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" },
                new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["FollowerWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" }, 20);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Put_WakeSeparationTimes_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"again Execute Crud Post WakeSeparationTime API with a duplicate Leader and Follower Id value and verify that it should return error via API for Crud WakeSeparationTime API")]
        public void ThenAgainExecuteCrudPostWakeSeparationTimeAPIWithADuplicateLeaderAndFollowerIdValueAndVerifyThatItShouldReturnErrorViaAPIForCrudWakeSeparationTimeAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new WakeSeparationTimeDto(0, Int32.Parse(sqlResponseDetails["SiteId"]),
                new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["LeaderWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" },
                new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["FollowerWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" }, 60);

                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Post_WakeSeparationTimes_Configuration");
                APIResponseErrorValidation(restResponse, "Violation of UNIQUE KEY constraint 'AK_SeparationTimes_LeaderAndFollower'. Cannot insert duplicate key in object 'resources.WakeSeparationTimes'. The duplicate key value is (1, 1, 1).", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Configuration");
            }
        }
        [Then(@"Generate new put ""([^""]*)"" API and Set DTO Objects for Crud API WakeSeparationTime")]
        public void ThenGenerateNewPutAPIAndSetDTOObjectsForCrudAPIWakeSeparationTime(string inputURL)
        {
            try
            {
                List<object> sqlResponseDetails = GetMultipleSQLResponseDetails();
                Dictionary<string, string> firstRowDetails = (Dictionary<string, string>)sqlResponseDetails.First();
                Dictionary<string, string> secondRowDetails = (Dictionary<string, string>)sqlResponseDetails.Last();
                string appendedURL = APITests.apiConfigDTO.WakeSeparationTimeAPIURL + "/" + inputURL;
                List<WakeSeparationTimeDto> wakeSeparationTime = new();
                wakeSeparationTime.Add(new WakeSeparationTimeDto(Int32.Parse(firstRowDetails["Id"]), Int32.Parse(firstRowDetails["SiteId"]), new WakeTurbulenceCategoryDto() { Id = 1, SiteId = Int32.Parse(firstRowDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" }, new WakeTurbulenceCategoryDto() { Id = 1, SiteId = Int32.Parse(firstRowDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" }, 10));
                wakeSeparationTime.Add(new WakeSeparationTimeDto(Int32.Parse(secondRowDetails["Id"]), Int32.Parse(secondRowDetails["SiteId"]), new WakeTurbulenceCategoryDto() { Id = 2, SiteId = Int32.Parse(secondRowDetails["SiteId"]), Category = "B", CategoryName = "Upper Heavy" }, new WakeTurbulenceCategoryDto() { Id = 2, SiteId = Int32.Parse(secondRowDetails["SiteId"]), Category = "B", CategoryName = "Upper Heavy" }, 30));
                dtoResultList = PutAPIWithDeserializeList(wakeSeparationTime, appendedURL, "Failed_To_Update_Crud_API_Put_WakeSeparationTime_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_WakeSeparationTimeListConfiguration");
            }
        }
        [Then(@"again Execute Crud Put WakeSeparationTime API and try to update the Leader Id and verify that it should return error via API for Crud WakeSeparationTime API")]
        public void ThenAgainExecuteCrudPutWakeSeparationTimeAPIAndTryToUpdateTheLeaderIdAndVerifyThatItShouldReturnErrorViaAPIForCrudWakeSeparationTimeAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var putRequestBody = new WakeSeparationTimeDto(Int32.Parse(sqlResponseDetails["Id"]), Int32.Parse(sqlResponseDetails["SiteId"]),
                new WakeTurbulenceCategoryDto() { Id = 2, SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "B", CategoryName = "Upper Heavy" },
                new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["FollowerWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" }, 20);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Put_WakeSeparationTime_Configuration");
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                string responseDataMessage = DynamicData.ToString();
                Assert.AreEqual("Cannot update leader or follower Category for a specific record", responseDataMessage, "Error response message matches:" + responseDataMessage);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_WakeSeparationTimeAPI");
            }
        }
        [Then(@"again Execute Crud Put WakeSeparationTime API and try to update the Follower Id and verify that it should return error via API for Crud WakeSeparationTime API")]
        public void ThenAgainExecuteCrudPutWakeSeparationTimeAPIAndTryToUpdateTheFollowerIdAndVerifyThatItShouldReturnErrorViaAPIForCrudWakeSeparationTimeAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var putRequestBody = new WakeSeparationTimeDto(Int32.Parse(sqlResponseDetails["Id"]), Int32.Parse(sqlResponseDetails["SiteId"]),
                new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["LeaderWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" },
                new WakeTurbulenceCategoryDto() { Id = 2, SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "B", CategoryName = "Upper Heavy" }, 20);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Put_WakeSeparationTime_Configuration");
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                string responseDataMessage = DynamicData.ToString();
                Assert.AreEqual("Cannot update leader or follower Category for a specific record", responseDataMessage, "Error response message matches:" + responseDataMessage);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_WakeSeparationTimeAPI");
            }
        }
        [Then(@"again Execute Crud Put WakeSeparationTime API and try to update the Leader Id and Separation Seconds and verify that it should return error via API for Crud WakeSeparationTime API")]
        public void ThenAgainExecuteCrudPutWakeSeparationTimeAPIAndTryToUpdateTheLeaderIdAndSeparationSecondsAndVerifyThatItShouldReturnErrorViaAPIForCrudWakeSeparationTimeAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var putRequestBody = new WakeSeparationTimeDto(Int32.Parse(sqlResponseDetails["Id"]), Int32.Parse(sqlResponseDetails["SiteId"]),
                new WakeTurbulenceCategoryDto() { Id = 2, SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "B", CategoryName = "Upper Heavy" },
                new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["FollowerWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" }, 120);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Put_WakeSeparationTime_Configuration");
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                string responseDataMessage = DynamicData.ToString();
                Assert.AreEqual("Cannot update leader or follower Category for a specific record", responseDataMessage, "Error response message matches:" + responseDataMessage);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_WakeSeparationTimeAPI");
            }
        }
        [Then(@"again Execute Crud Put WakeSeparationTime API and try to update the Follower Id and Separation Seconds and verify that it should return error via API for Crud WakeSeparationTime API")]
        public void ThenAgainExecuteCrudPutWakeSeparationTimeAPIAndTryToUpdateTheFollowerIdAndSeparationSecondsAndVerifyThatItShouldReturnErrorViaAPIForCrudWakeSeparationTimeAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var putRequestBody = new WakeSeparationTimeDto(Int32.Parse(sqlResponseDetails["Id"]),  Int32.Parse(sqlResponseDetails["SiteId"]),
                    new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["LeaderWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" },
                    new WakeTurbulenceCategoryDto() { Id = 2, SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "B", CategoryName = "Upper Heavy" }, 120);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Put_WakeSeparationTime_Configuration");
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                string responseDataMessage = DynamicData.ToString();
                Assert.AreEqual("Cannot update leader or follower Category for a specific record", responseDataMessage, "Error response message matches:" + responseDataMessage);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_WakeSeparationTimeAPI");
            }
        }
        [Then(@"again Execute Crud Put WakeSeparationTime API and try to update Separation Seconds and keep Leader and Follower details the same and verify that this time the update should happen correctly")]
        public void ThenAgainExecuteCrudPutWakeSeparationTimeAPIAndTryToUpdateSeparationSecondsAndKeepLeaderAndFollowerDetailsTheSameAndVerifyThatThisTimeTheUpdateShouldHappenCorrectly()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var putRequestBody = new WakeSeparationTimeDto(Int32.Parse(sqlResponseDetails["Id"]), Int32.Parse(sqlResponseDetails["SiteId"]),
            new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["LeaderWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" },
            new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["FollowerWakeTurbulenceCategoryId"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = "A", CategoryName = "Super Heavy" }, 120);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.WakeSeparationTimeAPIURL, "Failed_To_Update_Crud_API_Put_WakeSeparationTime_Configuration");
        }
        [Then(@"Generate new Get WakeSeparationTime by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API WakeSeparationTime")]
        public void ThenGenerateNewGetWakeSeparationTimeByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIWakeSeparationTime(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.WakeSeparationTimeAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with wakeSeparationId: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_WakeSeparationTime");
            }
        }
    }
}