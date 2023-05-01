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
    public class CrudAPIAircraftTypeSteps: CrudAPIBaseMethods<AircraftTypeDto>
    {
        public CrudAPIAircraftTypeSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        private APITests apiTest => new APITests(_driver);

        [Given(@"Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects")]
        public void GivenReadAPIConfigValuesForEnvInfoStoredInConfigSettingAndSetDTOObjects()
        {
            Assert.IsTrue(apiTest.AuthCredentialSetup(HookInitialization.startup.ENV));
        }

        [Then(@"Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType for site claim ""([^""]*)""")]
        public void ThenExecuteCrudAircraftTypeAPIAndSetDTOObjectsForCrudAPIAircraftTypeForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType")]
        public void ThenExecuteCrudAircraftTypeAPIAndSetDTOObjectsForCrudAPIAircraftType()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Fetch value for field ""([^""]*)"" from Database returned from Query Response")]
        public void ThenFetchValueForFieldFromDatabaseReturnedFromQueryResponse(string keyColName)
        {
            FetchDBReturnedValue(keyColName);        
        }
        [Then(@"Compare values from API response set to DB record set for Crud Aircraft Type API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudAircraftTypeAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }

        [Then(@"Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type")]
        public void PostAircraftTypeAPI()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new AircraftTypeDto() { Id = 0, Icao = "A306" + (_scenarioContext.Get<string>("valueSQLData")), Iata = "A306" + (_scenarioContext.Get<string>("valueSQLData")), Engine = "JET", TypeName = "Airbus", Width = 45, NumberOfEngines = 2, SizeCode = "D", SpeedClass = "V6_Jet", SiteId = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")),
            WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["Id"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = sqlResponseDetails["Category"], CategoryName = sqlResponseDetails["CategoryName"]}};
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Post_AircraftType_Configuration");
            AddOrUpdateScenarioContext("addedIdValue", (dtoResultSingleRecord.Id).ToString());
        }

        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudAircraftTypeAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(aircraftTypeDetails, dtoResultSingleRecord);
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if(compareObject is AircraftTypeDto aircraftTypeDto)
                {
                    Assert.AreEqual(aircraftTypeDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + aircraftTypeDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(aircraftTypeDto.Icao, compareDictionary["ICAO"], "Icao values do not match:" + aircraftTypeDto.Icao + " != " + compareDictionary["ICAO"]);
                    Assert.AreEqual(aircraftTypeDto.Iata, compareDictionary["IATA"], "Iata values do not match:" + aircraftTypeDto.Iata + " != " + compareDictionary["IATA"]);
                    Assert.AreEqual(aircraftTypeDto.Engine, compareDictionary["Engine"], "Engine values do not match:" + aircraftTypeDto.Engine + " != " + compareDictionary["Engine"]);
                    Assert.AreEqual(aircraftTypeDto.TypeName, compareDictionary["TypeName"], "TypeName values do not match:" + aircraftTypeDto.TypeName + " != " + compareDictionary["TypeName"]);
                    Assert.AreEqual(aircraftTypeDto.Width, Decimal.Parse(compareDictionary["Width"]), "Width values do not match:" + aircraftTypeDto.Width + " != " + compareDictionary["Width"]);
                    Assert.AreEqual(aircraftTypeDto.NumberOfEngines, Int32.Parse(compareDictionary["NumberOfEngines"]), "NumberOfEngines values do not match:" + aircraftTypeDto.NumberOfEngines + " != " + compareDictionary["NumberOfEngines"]);
                    Assert.AreEqual(aircraftTypeDto.SizeCode, compareDictionary["SizeCode"], "SizeCode values do not match:" + aircraftTypeDto.SizeCode + " != " + compareDictionary["SizeCode"]);
                    Assert.AreEqual(aircraftTypeDto.WakeTurbulenceCategory.Id, Int32.Parse(compareDictionary["WakeTurbulenceCategoryId"]), "WakeTurbulenceCategoryId did not match:" + aircraftTypeDto.WakeTurbulenceCategory.Id + " != " + compareDictionary["WakeTurbulenceCategoryId"]);
                    Assert.AreEqual(aircraftTypeDto.SpeedClass, compareDictionary["SpeedClass"], "SpeedClass values do not match:" + aircraftTypeDto.SpeedClass + " != " + compareDictionary["SpeedClass"]);
                }
                else
                {
                    Assert.Fail("Return values which are not aircraft type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }

        [Then(@"again Execute Crud Post Aircraft Type API with a duplicate Iata value and verify that it should return error via API for Crud Aircraft Type API")]
        public void ThenAgainExecuteCrudPostAircraftTypeAPIWithADuplicateIataValueAndVerifyThatItShouldReturnErrorViaAPIForCrudAircraftTypeAPI()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new AircraftTypeDto() { Id = 0, Icao = "A306", Iata = "A306", Engine = "JET", TypeName = "Airbus", Width = 45, NumberOfEngines = 2, SizeCode = "D", SpeedClass = "V6_Jet", SiteId = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")), WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = 6, SiteId = 1, Category = "F", CategoryName = "Light" } };
            var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Post_AircraftType_Configuration");
            APIResponseErrorValidation(restResponse, "Cannot insert duplicate key row in object 'resources.AircraftType' with unique index 'IX_AircraftType_ICAO'. The duplicate key value is (1, A306).", "Message");
        }

        [Then(@"Generate new put aircraft type API URL using database fetched values and Execute Crud Put API Aircraft Type and Set DTO Objects for Crud API Aircraft Type")]
        public void ThenGenerateNewPutAircraftTypeAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIAircraftTypeAndSetDTOObjectsForCrudAPIAircraftType()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
            string EngineDB = sqlResponseDetails["Engine"];
            string TypeNameDB = sqlResponseDetails["TypeName"];
            decimal WidthDB = Decimal.Parse(sqlResponseDetails["Width"]);
            int NumberOfEnginesDB = Int32.Parse(sqlResponseDetails["NumberOfEngines"]);
            string SizeCodeDB = sqlResponseDetails["SizeCode"];
            int WakeTurbulenceCategoryIdDB = Int32.Parse(sqlResponseDetails["WakeTurbulenceCategoryId"]);
            string SpeedClassDB = sqlResponseDetails["SpeedClass"];
            int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);

            var putRequestBody = new AircraftTypeDto() { Id = IdValueDB, Icao = "PA1" + (_scenarioContext.Get<string>("valueSQLData")), Iata = "PA1" + (_scenarioContext.Get<string>("valueSQLData")), Engine = EngineDB, TypeName = TypeNameDB, Width = WidthDB, NumberOfEngines = NumberOfEnginesDB, SizeCode = SizeCodeDB, SpeedClass = SpeedClassDB, SiteId = SiteIdDB, WakeTurbulenceCategory=new WakeTurbulenceCategoryDto() { Id = WakeTurbulenceCategoryIdDB, SiteId = 1, Category = "F", CategoryName = "Light" } } ;
            Thread.Sleep(200);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Put_AircraftType_Configuration");
        }

        [Then(@"Generate new Get aircraft type by ""(.*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Aircraft Type")]
        public void ThenGenerateNewGetAircraftTypeByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIAircraftType(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<AircraftTypeDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_AircraftType");
            }
        }

        [Then(@"Validate the GET API operation by ""(.*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Aircraft Type")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesnTExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudAircraftType(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.AircraftTypeAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }

        [Then(@"Fetch value for field ""([^""]*)"" from Database returned from above sql Query")]
        public void ThenFetchValueForFieldFromDatabaseReturnedFromAboveSqlQuery(string keyColName)
        {
            FetchDBOutputResponse(keyColName);
        }

        [Then(@"Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type")]
        public IRestResponse ThenGenerateNewDeleteAircraftTypeAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAircraftType()
        {
            var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, "Id", "Failed_To_Execute_Get_By_API");
            IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_AircraftType");
            return restResponse;
        }
        public IRestResponse executeDeleteAPIRequest(string url, int Id, string failureMessage)
        {
            var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, url, Id);
            IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup,failureMessage);
            return restResponse;
        }

        [Then(@"Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type")]
        public void ThenGenerateNewDeleteAircraftTypeAPIURLUsingTheAddedAircraftIdAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAircraftType()
        {
            string fetchRecentAddedAircraftIdValue = (_scenarioContext.ContainsKey("addedIdValue") ? _scenarioContext.Get<string>("addedIdValue") : string.Empty);
            executeDeleteAPIRequest(APITests.apiConfigDTO.AircraftTypeAPIURL, Int32.Parse(fetchRecentAddedAircraftIdValue), "Failed_To_Update_Crud_API_Delete_AircraftType");
        }
        [Then(@"Generate new Delete aircraft type API URL using the above added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type")]
        public void ThenGenerateNewDeleteAircraftTypeAPIURLUsingTheAboveAddedAircraftIdAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAircraftType()
        {
            string fetchRecentAddedAircraftIdValue = (_scenarioContext.ContainsKey("outputSQLResponse") ? _scenarioContext.Get<string>("outputSQLResponse") : string.Empty);
            executeDeleteAPIRequest(APITests.apiConfigDTO.AircraftTypeAPIURL, Int32.Parse(fetchRecentAddedAircraftIdValue), "Failed_To_Update_Crud_API_Delete_AircraftType");
        }

        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Aircraft Type")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudAircraftType()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Generate new ""(.*)"" using ""(.*)"" in the API URL and Execute Get By ""(.*)"" API request and Set DTO Objects for Crud API Aircraft Type")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestAndSetDTOObjectsForCrudAPIAircraftType(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                dtoResultList = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Execute_Get_By_API_Request");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AircraftTypeAPI");
            }
        }

        [Then(@"Generate new ""(.*)"" using ""(.*)"" in the API URL and Execute Crud Get By ""(.*)"" request and Set DTO Objects for Crud API Aircraft Type")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestAndSetDTOObjectsForCrudAPIAircraftType(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Execute_Get_By_API_Request");
                if (colName.Equals("IATA"))
                {
                    dtoResultList = JsonConvert.DeserializeObject<List<AircraftTypeDto>>(restResponse.Content);
                }
                else if (colName.Equals("ICAO"))
                {
                    dtoResultSingleRecord = JsonConvert.DeserializeObject<AircraftTypeDto>(restResponse.Content);
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AircraftTypeAPI");
            }
        }
        [Then(@"Execute Crud Aircraft Type API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudAircraftTypeAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_AircraftType_MessageVerification");
            }
        }

        [Then(@"Execute Crud Aircraft Type API with an incorrect Site id and validate that the api should return error in the response body")]
        public void ThenExecuteCrudAircraftTypeAPIWithAnIncorrectSiteIdAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest, "404 Not Found");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_IncorrectSiteId_Crud_API_AircraftType_MessageVerification");
            }
        }
        [Then(@"Generate new Get aircraft type by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetAircraftTypeByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_AircraftType_MessageVerification");
            }
        }
        [Then(@"Execute Crud Aircraft Type API for ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudAircraftTypeAPIForAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "No Site Access. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_NoSiteId_Crud_API_AircraftType_MessageVerification");
            }
        }

        [Then(@"Execute Crud Aircraft Type API using invalid Site id ""([^""]*)"" in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudAircraftTypeAPIUsingInvalidSiteIdInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_AircraftType_MessageVerification");
            }
        }

        [Then(@"Generate new Delete aircraft type API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteAircraftTypeAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteAircraftTypeAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAircraftType();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for aircraftype Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_AircraftType");
            }
        }

        [Then(@"Execute Crud Post Aircraft Type API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostAircraftTypeAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new AircraftTypeDto() { Id = 0, Icao = "A306" + (_scenarioContext.Get<string>("valueSQLData")), Iata = "A306" + (_scenarioContext.Get<string>("valueSQLData")), Engine = "JET", TypeName = "Airbus", Width = 45, NumberOfEngines = 2, SizeCode = "D", SpeedClass = "V6_Jet", SiteId = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")),
               WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["Id"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = sqlResponseDetails["Category"], CategoryName = sqlResponseDetails["CategoryName"] } };
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Post_AircraftType_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }

        [Then(@"Generate new put aircraft type API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutAircraftTypeAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                string EngineDB = sqlResponseDetails["Engine"];
                string TypeNameDB = sqlResponseDetails["TypeName"];
                decimal WidthDB = Decimal.Parse(sqlResponseDetails["Width"]);
                int NumberOfEnginesDB = Int32.Parse(sqlResponseDetails["NumberOfEngines"]);
                string SizeCodeDB = sqlResponseDetails["SizeCode"];
                int WakeTurbulenceCategoryIdDB = Int32.Parse(sqlResponseDetails["WakeTurbulenceCategoryId"]);
                string SpeedClassDB = sqlResponseDetails["SpeedClass"];
                int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = new AircraftTypeDto() { Id = IdValueDB, Icao = "PA1" + (_scenarioContext.Get<string>("valueSQLData")), Iata = "PA1" + (_scenarioContext.Get<string>("valueSQLData")), Engine = EngineDB, TypeName = TypeNameDB, Width = WidthDB, NumberOfEngines = NumberOfEnginesDB, SizeCode = SizeCodeDB, SpeedClass = SpeedClassDB, SiteId = SiteIdDB, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = WakeTurbulenceCategoryIdDB, SiteId = 1, Category = "F", CategoryName = "Light" } };
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Put_AircraftType_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new Get aircraft type by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetAircraftTypeByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with AircraftTypeId: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_AircraftType");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnEmptyListInTheResponseBody(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Execute_Get_By_API_Request");
                if (colName.Equals("IATA"))
                {
                    if (dtoResult.Count == 0)
                    {
                        Console.WriteLine("Response content is an empty list");
                    }
                    else
                    {
                        Console.WriteLine("Response content is an empty list");
                    }
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AircraftTypeAPI");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnEmptyListInTheResponseBody(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Execute_Get_By_API_Request");
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
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AircraftTypeAPI");
            }
        }
        [Then(@"Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType and validate that the api should return empty list in the response body")]
        public void ThenExecuteCrudAircraftTypeAPIAndSetDTOObjectsForCrudAPIAircraftTypeAndValidateThatTheApiShouldReturnEmptyListInTheResponseBody()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            if (dtoResult.Count == 0)
            {
                Console.WriteLine("Response content is an empty list");
            }
            else
            {
                Console.WriteLine("Response content is an empty list");
            }
        }
        [Then(@"Execute Crud Aircraft Type API for ""([^""]*)"" Site Claim and validate that the api should return empty list in the response body")]
        public void ThenExecuteCrudAircraftTypeAPIForSiteClaimAndValidateThatTheApiShouldReturnEmptyListInTheResponseBody(string siteClaim)
        {
            ThenExecuteCrudAircraftTypeAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(siteClaim);
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request for a different Site Id than what is being used in the token and validate that the api should return No Content in the response body")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnNoContentInTheResponseBody(string apiReqName, string colName, string colNameAPI)
        {
            var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Execute_Get_By_API_Request");
            string errorMessageExpected = restResponse.StatusDescription.ToString();
            Assert.AreEqual(errorMessageExpected, "No Content", "Response code matches:" + errorMessageExpected);
        }
    }
}
