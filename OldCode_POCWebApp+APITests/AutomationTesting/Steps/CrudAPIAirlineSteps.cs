using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDM.Bindings.CommonAPIUtils.Hooks;
using AventStack.ExtentReports.Gherkin.Model;
using ACDM.Bindings.PageObjects;
using ACDMAutomation.API.DTO_AuthAPI;
using ACDMAutomation.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acdm.InformationServices.Dto;
using JWTMakerLib;

namespace ACDMAutomation.Steps
{
    [Binding]
    public class CrudAPIAirlineSteps : CrudAPIBaseMethods<AirlineDto>
    {
        [Then(@"Execute Crud Airline API And Set DTO Objects for Crud API Airline")]
        public void ThenExecuteCrudAirlineAPIAndSetDTOObjectsForCrudAPIAirline()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AirlinesAPIURL);
            dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
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
            var postRequestBody = new AirlineDto(0, "AI" + AircraftTypeSteps.valueRandomNumber, "AI" + AircraftTypeSteps.valueRandomNumber, "DummyTestName", Int32.Parse(sqlResponseDetails["Id"]));
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Post_Airline_Configuration");
        }
        [Then(@"again Execute Crud Post Airline API with a duplicate Iata value and verify that it shoudl return error via API for Crud Airline API")]
        public void ThenAgainExecuteCrudPostAirlineAPIWithADuplicateIataValueAndVerifyThatItShoudlReturnErrorViaAPIForCrudAirlineAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                var postRequestBody = new AirlineDto(0, "AI", "AI", "DummyTestName", Int32.Parse(sqlResponseDetails["SiteId"]));
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Post_Airline_Configuration");
                APIResponseErrorValidation(restResponse, "Cannot insert duplicate key row in object 'dbo.Airline' with unique index 'IX_Airline_Iata'. The duplicate key value is (1, AI).\nThe statement has been terminated.","Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Configuration");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Airline API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudAirlineAPI()
        {
            Dictionary<string, string> airlineDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
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
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.AirlinesAPIURL, colName, "Failed_To_Execute_Get_By_API");
        }
        [Then(@"Generate new put airline API URL using database fetched values and Execute Crud Put API Airline and Set DTO Objects for Crud API Airline")]
        public void ThenGenerateNewPutAirlineAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIAirlineAndSetDTOObjectsForCrudAPIAirline()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
            string IataDB = sqlResponseDetails["Iata"];
            string IcaoDB = sqlResponseDetails["Icao"];
            string NameDB = sqlResponseDetails["Name"];
            int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);

            var putRequestBody = new AirlineDto(IdValueDB, IcaoDB + AircraftTypeSteps.valueRandomNumber, IataDB + AircraftTypeSteps.valueRandomNumber, NameDB, SiteIdDB);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Put_Airline_Configuration");
        }
        [Then(@"Generate new Delete airline API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Airline")]
        public IRestResponse ThenGenerateNewDeleteAirlineAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAirline()
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AirlinesAPIURL, "Id", "Failed_To_Execute_Get_By_API");
                IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_AircraftType");
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Airline");
                return null;
            }
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Airline")]
        public static void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudAirline()
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
        [Then(@"Execute Crud Airline API with an incorrect Site id and validate that the api should return error in the response body")]
        public static void ThenExecuteCrudAirlineAPIWithAnIncorrectSiteIdAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AirlinesAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_IncorrectSiteId_Crud_API_Airline_MessageVerification");
            }
        }
        [Then(@"Generate new Get Airline by ""([^""]*)"" API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetAirlineByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AirlinesAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest);
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
                ResponseErrorMessageVerification(restResponse, "404 Not Found", "title", "status");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_AircraftType");
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
                var putRequestBody = new AirlineDto(IdValueDB, IcaoDB + AircraftTypeSteps.valueRandomNumber, IataDB + AircraftTypeSteps.valueRandomNumber, NameDB, SiteIdDB);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.AirlinesAPIURL, "Failed_To_Update_Crud_API_Put_Airline_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<AirlineDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
    }
}
