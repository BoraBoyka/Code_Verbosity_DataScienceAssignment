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
    public class CrudAPIWakeTurbulenceCategory : CrudAPIBaseMethods<WakeTurbulenceCategoryDto>
    {
        public CrudAPIWakeTurbulenceCategory(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        [Then(@"Execute Crud WakeTurbulenceCategory API And Set DTO Objects for Crud API WakeTurbulenceCategory for site claim ""([^""]*)""")]
        public void ThenExecuteCrudWakeTurbulenceCategoryAPIAndSetDTOObjectsForCrudAPIWakeTurbulenceCategoryForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud WakeTurbulenceCategory API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudWakeTurbulenceCategoryAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is WakeTurbulenceCategoryDto wakeTurbulenceCategoryDto)
                {
                    Assert.AreEqual(wakeTurbulenceCategoryDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + wakeTurbulenceCategoryDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(wakeTurbulenceCategoryDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + wakeTurbulenceCategoryDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(wakeTurbulenceCategoryDto.Category, compareDictionary["Category"], "Category values do not match:" + wakeTurbulenceCategoryDto.Category + " != " + compareDictionary["Category"]);
                    Assert.AreEqual(wakeTurbulenceCategoryDto.CategoryName, compareDictionary["CategoryName"], "CategoryName values do not match:" + wakeTurbulenceCategoryDto.CategoryName + " != " + compareDictionary["CategoryName"]);
                }
                else
                {
                    Assert.Fail("Return values which are not wake turbulence category type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Execute Crud Post WakeTurbulenceCategory API and Set DTO Objects for Crud API WakeTurbulenceCategory")]
        public void ThenExecuteCrudPostWakeTurbulenceCategoryAPIAndSetDTOObjectsForCrudAPIWakeTurbulenceCategory()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new WakeTurbulenceCategoryDto() { Id = 0, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "G", CategoryName = "Light" };
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, "Failed_To_Update_Crud_API_Post_WakeTurbulenceCategory_Configuration");
            AddOrUpdateScenarioContext("addedIdValue", (dtoResultSingleRecord.Id).ToString());
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud WakeTurbulenceCategory API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudWakeTurbulenceCategoryAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> wakeTurbulenceCategoryDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(wakeTurbulenceCategoryDetails, dtoResultSingleRecord);
        }
        [Then(@"again Execute Crud Post WakeTurbulenceCategory API with a duplicate Category and Category name value and verify that it should return error via API for Crud WakeTurbulenceCategory API")]
        public void ThenAgainExecuteCrudPostWakeTurbulenceCategoryAPIWithADuplicateCategoryAndCategoryNameValueAndVerifyThatItShouldReturnErrorViaAPIForCrudWakeTurbulenceCategoryAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new WakeTurbulenceCategoryDto() { Id = 0, SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = (sqlResponseDetails["Category"]), CategoryName = (sqlResponseDetails["CategoryName"]) };
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, "Failed_To_Update_Crud_API_Post_WakeTurbulenceCategory_Configuration");
                APIResponseErrorValidation(restResponse, "Cannot insert duplicate key row in object 'resources.WakeTurbulenceCategory' with unique index 'IX_WakeTurbulenceCategorySite'. The duplicate key value is (G, 1).", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Configuration");
            }
        }
        [Then(@"Generate new put WakeTurbulenceCategory API URL using database fetched values and Execute Crud Put API WakeTurbulenceCategory and Set DTO Objects for Crud API WakeTurbulenceCategory")]
        public void ThenGenerateNewPutWakeTurbulenceCategoryAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIWakeTurbulenceCategoryAndSetDTOObjectsForCrudAPIWakeTurbulenceCategory()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);
            string CategoryDB = sqlResponseDetails["Category"];
            string CategoryNameDB = sqlResponseDetails["CategoryName"];

            var putRequestBody = new WakeTurbulenceCategoryDto() { Id = IdValueDB, SiteId = SiteIdDB, Category = CategoryDB, CategoryName = CategoryNameDB + '1' };
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, "Failed_To_Update_Crud_API_Put_WakeTurbulenceCategory_Configuration");
        }
        [Then(@"Generate new Get WakeTurbulenceCategory by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API WakeTurbulenceCategory")]
        public void ThenGenerateNewGetWakeTurbulenceCategoryByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIWakeTurbulenceCategory(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<WakeTurbulenceCategoryDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud WakeTurbulenceCategory")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudWakeTurbulenceCategory(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new Delete WakeTurbulenceCategory API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeTurbulenceCategory")]
        public IRestResponse ThenGenerateNewDeleteWakeTurbulenceCategoryAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIWakeTurbulenceCategory()
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, "Id", "Failed_To_Execute_Get_By_API");
                IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_WakeTurbulenceCategory");
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_WakeTurbulenceCategory");
                return null;
            }
        }

        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud WakeTurbulenceCategory")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudWakeTurbulenceCategory()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Execute Crud Post ""([^""]*)"" API and Set DTO Objects for Crud API Add WakeTurbulenceCategory")]
        public void ThenExecuteCrudPostAPIAndSetDTOObjectsForCrudAPIAddWakeTurbulenceCategory(string inputURL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string appendedURL = APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL + "/" + inputURL;
                List<WakeTurbulenceCategoryDto> wakeTurbulenceCategory = new();
                wakeTurbulenceCategory.Add(new WakeTurbulenceCategoryDto() { Id = 0, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "G", CategoryName = "Test"});
                wakeTurbulenceCategory.Add(new WakeTurbulenceCategoryDto() { Id = 0, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "H", CategoryName = "Dummy_Test"});
                dtoResultList = PostAPIWithDeserializeList(wakeTurbulenceCategory, appendedURL, "Failed_To_Update_Crud_API_Post_WakeTurbulenceCateogory_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_WakeTurbulenceCategoryListConfiguration");
            }
        }
        [Then(@"Execute Crud WakeTurbulenceCategory API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudWakeTurbulenceCategoryAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN,siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_IncorrectSiteId_Crud_API_WakeTurbulenceCategory_MessageVerification");
            }
        }
        [Then(@"Generate new Get WakeTurbulenceCategory by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetWakeTurbulenceCategoryByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_WakeTurbulenceCategoryType_MessageVerification");
            }
        }
        [Then(@"Generate new Delete WakeTurbulenceCategory API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteWakeTurbulenceCategoryAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteWakeTurbulenceCategoryAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIWakeTurbulenceCategory();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for WakeTurbulenceCategory Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_WakeTurbulenceCategoryType");
            }
        }
        [Then(@"Execute Crud Post WakeTurbulenceCategory API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostWakeTurbulenceCategoryAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new WakeTurbulenceCategoryDto() { Id = 0, SiteId = Int32.Parse(sqlResponseDetails["Id"]), Category = "G", CategoryName = "Light" };
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, "Failed_To_Update_Crud_API_Post_WakeTurbulenceCategory_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<WakeTurbulenceCategoryDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put WakeTurbulenceCategory API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutWakeTurbulenceCategoryAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);
                string CategoryDB = sqlResponseDetails["Category"];
                string CategoryNameDB = sqlResponseDetails["CategoryName"];

                var putRequestBody = new WakeTurbulenceCategoryDto() { Id = IdValueDB, SiteId = SiteIdDB, Category = CategoryDB, CategoryName = CategoryNameDB };
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, "Failed_To_Update_Crud_API_Put_WakeTurbulenceCategory_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"Execute Crud Put ""([^""]*)"" API and Set DTO Objects for Crud API Add WakeTurbulenceCategory")]
        public void ThenExecuteCrudPutAPIAndSetDTOObjectsForCrudAPIAddWakeTurbulenceCategory(string inputURL)
        {
            try
            {
                List<object> sqlResponseDetails = GetMultipleSQLResponseDetails();
                Dictionary<string, string> firstRowDetails = (Dictionary<string, string>)sqlResponseDetails.First();
                Dictionary<string, string> secondRowDetails = (Dictionary<string, string>)sqlResponseDetails.Last();
                string appendedURL = APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL + "/" + inputURL;
                List<WakeTurbulenceCategoryDto> wakeTurbulenceCategory = new();

                wakeTurbulenceCategory.Add(new WakeTurbulenceCategoryDto() { Id = Int32.Parse(firstRowDetails["Id"]), SiteId = Int32.Parse(firstRowDetails["SiteId"]), Category = firstRowDetails["Category"], CategoryName = firstRowDetails["CategoryName"] + '1' });
                wakeTurbulenceCategory.Add(new WakeTurbulenceCategoryDto() { Id = Int32.Parse(secondRowDetails["Id"]), SiteId = Int32.Parse(secondRowDetails["SiteId"]), Category = secondRowDetails["Category"], CategoryName = secondRowDetails["CategoryName"] + '2' });
                dtoResultList = PutAPIWithDeserializeList(wakeTurbulenceCategory, appendedURL, "Failed_To_Update_Crud_API_Put_WakeTurbulenceCategory_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_WakeTurbulenceCategoryListConfiguration");
            }
        }
        [Then(@"Generate new Get WakeTurbulenceCategory by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API WakeTurbulenceCategory")]
        public void ThenGenerateNewGetWakeTurbulenceCategoryByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIWakeTurbulenceCategory(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.WakeTurbulenceCategoryAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with WakeTurbulenceCategory: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_WakeTurbulenceCategory");
            }
        }
    }
}
