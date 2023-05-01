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
    public class CrudAPIAirlineSteps : CrudAPIBaseMethods<AirlineDto>
    {
        public CrudAPIAirlineSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        [Then(@"Execute Crud Airline API And Set DTO Objects for Crud API Airline for site claim ""([^""]*)""")]
        public void ThenExecuteCrudAirlineAPIAndSetDTOObjectsForCrudAPIAirlineForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AirlinesAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Execute Crud Airline API And Set DTO Objects for Crud API Airline")]
        public void ThenExecuteCrudAirlineAPIAndSetDTOObjectsForCrudAPIAirline()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AirlinesAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Compare values from API response set to DB record set for Crud Airline API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudAirlineAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is AirlineDto airlineDto)
                {
                    Assert.AreEqual(airlineDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + airlineDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(airlineDto.Iata, compareDictionary["Iata"], "Iata values do not match:" + airlineDto.Iata + " != " + compareDictionary["Iata"]);
                    Assert.AreEqual(airlineDto.Icao, compareDictionary["Icao"], "Icao values do not match:" + airlineDto.Icao + " != " + compareDictionary["Icao"]);
                    Assert.AreEqual(airlineDto.Name, compareDictionary["Name"], "Name values do not match:" + airlineDto.Name + " != " + compareDictionary["Name"]);
                }
                else
                {
                    Assert.Fail("Return values which are not airline type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
     
        [Then(@"Execute Crud Post Airline API and Set DTO Objects for Crud API Airline")]
        public void ThenExecuteCrudPostAirlineAPIAndSetDTOObjectsForCrudAPIAirline()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new AirlineDto(0, "AI" + (_scenarioContext.Get<string>("valueSQLData")), "AI" + (_scenarioContext.Get<string>("valueSQLData")), "DummyTestName", Int32.Parse(sqlResponseDetails["Id"]));
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Post_Airline_Configuration");
        }

        [Then(@"again Execute Crud Post Airline API with a duplicate Iata value and verify that it should return error via API for Crud Airline API")]
        public void ThenAgainExecuteCrudPostAirlineAPIWithADuplicateIataValueAndVerifyThatItShouldReturnErrorViaAPIForCrudAirlineAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new AirlineDto(0, "AI", "AI", "DummyTestName", Int32.Parse(sqlResponseDetails["SiteId"]));
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Post_Airline_Configuration");
                APIResponseErrorValidation(restResponse, "Cannot insert duplicate key row in object 'resources.Airline' with unique index 'IX_Airline_Iata'. The duplicate key value is (1, AI).", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Configuration");
            }
        }

        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Airline API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudAirlineAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> airlineDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(airlineDetails, dtoResultSingleRecord);
        }

        [Then(@"Generate new Get airline by ""(.*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Airline")]
        public void ThenGenerateNewGetAirlineByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIAirline(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AirlinesAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<AirlineDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration");
            }
        }

        [Then(@"Validate the GET API operation by ""(.*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Airline")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesnTExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudAirline(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.AirlinesAPIURL, colName, "Failed_To_Execute_Get_By_API","No Content");
        }

        [Then(@"Generate new put airline API URL using database fetched values and Execute Crud Put API Airline and Set DTO Objects for Crud API Airline")]
        public void ThenGenerateNewPutAirlineAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIAirlineAndSetDTOObjectsForCrudAPIAirline()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
            string NameDB = sqlResponseDetails["Name"];
            int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);

            var putRequestBody = new AirlineDto(IdValueDB, "ZH" + (_scenarioContext.Get<string>("valueSQLData")), "ZH" +  (_scenarioContext.Get<string>("valueSQLData")), NameDB, SiteIdDB);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Put_Airline_Configuration");
        }

        [Then(@"Generate new Delete airline API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Airline")]
        public IRestResponse ThenGenerateNewDeleteAirlineAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAirline()
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AirlinesAPIURL, "Id", "Failed_To_Execute_Get_By_API");
                IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Airline");
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Airline");
                return null;
            }
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Airline")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudAirline()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }

        [Then(@"Generate new ""(.*)"" using ""(.*)"" in the API URL and Execute Get By ""(.*)"" API request and Set DTO Objects for Crud API Airline")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestAndSetDTOObjectsForCrudAPIAirline(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                dtoResultList = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Execute_Get_By_API_Request");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AirlineAPI");
            }
        }

        [Then(@"Generate new ""(.*)"" using ""(.*)"" in the API URL and Execute Crud Get By ""(.*)"" request and Set DTO Objects for Crud API Airline")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestAndSetDTOObjectsForCrudAPIAirline(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var restResponse = ExecuteGetByAPIRequest(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Execute_Get_By_API_Request");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<AirlineDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AirlineAPI");
            }
        }
        [Then(@"Execute Crud Airline API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudAirlineAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AirlinesAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_Airline_MessageVerification");
            }
        }

        [Then(@"Generate new Get Airline by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetAirlineByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AirlinesAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_AirlineType_MessageVerification");
            }
        }

        [Then(@"Generate new Delete Airline API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteAirlineAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteAirlineAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAirline();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for airline Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_AirlineType");
            }
        }

        [Then(@"Execute Crud Post Airline API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostAirlineAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new AirlineDto(0, "AI", "AI", "DummyTestName", Int32.Parse(sqlResponseDetails["Id"]));
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Post_Airline_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<AirlineDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }

        [Then(@"Generate new put Airline API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutAirlineAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                string IataDB = sqlResponseDetails["Iata"];
                string IcaoDB = sqlResponseDetails["Icao"];
                string NameDB = sqlResponseDetails["Name"];
                int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = new AirlineDto(IdValueDB, IcaoDB + (_scenarioContext.Get<string>("valueSQLData")), IataDB + (_scenarioContext.Get<string>("valueSQLData")), NameDB, SiteIdDB);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Put_Airline_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Get By ""([^""]*)"" API request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body for Crud API Airline")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnEmptyListInTheResponseBodyForCrudAPIAirline(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Execute_Get_By_API_Request");
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
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AirlineAPI");
            }
        }
        [Then(@"Generate new Get airline by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Airline")]
        public void ThenGenerateNewGetAirlineByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIAirline(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AirlinesAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with AirlineId: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_Airline");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body for Crud API Airline")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnEmptyListInTheResponseBodyForCrudAPIAirline(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Execute_Get_By_API_Request");
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
                else if (colName.Equals("ICAO"))
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
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AirlineAPI");
            }
        }
    }
}
