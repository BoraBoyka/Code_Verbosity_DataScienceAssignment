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
    public class CrudAPIAircraftTypeSteps: CrudAPIBaseMethods<AircraftTypeDto>
    {
        public static string valueSQLResponse;

        [Then(@"Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType")]
        public void ThenExecuteCrudAircraftTypeAPIAndSetDTOObjectsForCrudAPIAircraftType()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
            dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
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
            var postRequestBody = new AircraftTypeDto(0, "A306" + AircraftTypeSteps.valueRandomNumber, "A306" + AircraftTypeSteps.valueRandomNumber, "JET", "Airbus", 45, 2, "D", "H", "V6_Jet", Int32.Parse(sqlResponseDetails["Id"]));
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Post_AircraftType_Configuration");
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudAircraftTypeAPI()
        {
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
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
                    Assert.AreEqual(aircraftTypeDto.Wvc, compareDictionary["Wvc"], "Wvc values do not match:" + aircraftTypeDto.Wvc + " != " + compareDictionary["Wvc"]);
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
            var postRequestBody = new AircraftTypeDto(0, "A306", "A306", "JET", "Airbus", 45, 2, "D", "H", "V6_Jet", Int32.Parse(sqlResponseDetails["SiteId"]));
            var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Post_AircraftType_Configuration");
            APIResponseErrorValidation(restResponse, "Cannot insert duplicate key row in object 'dbo.AircraftType' with unique index 'IX_AircraftType_ICAO'. The duplicate key value is (1, A306).\nThe statement has been terminated.", "Message");
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
            string WvcDB = sqlResponseDetails["Wvc"];
            string SpeedClassDB = sqlResponseDetails["SpeedClass"];
            int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);

            var putRequestBody = new AircraftTypeDto(IdValueDB, "PA1" + AircraftTypeSteps.valueRandomNumber, "PA1" + AircraftTypeSteps.valueRandomNumber, EngineDB, TypeNameDB, WidthDB, NumberOfEnginesDB, SizeCodeDB, WvcDB, SpeedClassDB, SiteIdDB);
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
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.AircraftTypeAPIURL, colName, "Failed_To_Execute_Get_By_API");
        }
        [Then(@"Fetch value for field ""([^""]*)"" from Database returned from above sql Query")]
        public static void ThenFetchValueForFieldFromDatabaseReturnedFromAboveSqlQuery(string keyColName)
        {
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)SQLGenericSteps.sqlResponseList.ElementAt<object>(0);
            valueSQLResponse = aircraftTypeDetails[keyColName];
            Console.WriteLine(valueSQLResponse);
            if (valueSQLResponse.Equals(null) || valueSQLResponse.Equals(""))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_FieldValue_Database");
            }
        }
        [Then(@"Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type")]
        public IRestResponse ThenGenerateNewDeleteAircraftTypeAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAircraftType()
        {
            var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, "Id", "Failed_To_Execute_Get_By_API");
            IRestResponse restResponse=ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_AircraftType");
            return restResponse;
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Aircraft Type")]
        public static void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudAircraftType()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        } 
        [Then(@"Generate new ""(.*)"" using ""(.*)"" in the API URL and Execute Get By ""(.*)"" API request and Set DTO Objects for Crud API Aircraft Type")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteGetByAPIRequestAndSetDTOObjectsForCrudAPIAircraftType(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                dtoResultList = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI,APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Execute_Get_By_API_Request");
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
                if(colName.Equals("IATA"))
                {
                    dtoResultList = JsonConvert.DeserializeObject<List<AircraftTypeDto>>(restResponse.Content);
                }
                else if(colName.Equals("ICAO"))
                {
                    dtoResultSingleRecord = JsonConvert.DeserializeObject<AircraftTypeDto>(restResponse.Content);
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_AircraftTypeAPI");
            }
        }
        [Then(@"Execute Crud Aircraft Type API with an incorrect Site id and validate that the api should return error in the response body")]
        public static void ThenExecuteCrudAircraftTypeAPIWithAnIncorrectSiteIdAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_IncorrectSiteId_Crud_API_AircraftType_MessageVerification");
            }
        }
        [Then(@"Generate new Get aircraft type by ""([^""]*)"" API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public  void ThenGenerateNewGetAircraftTypeByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_AircraftType_MessageVerification");
            }
        }
        [Then(@"Generate new Delete aircraft type API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteAircraftTypeAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse=ThenGenerateNewDeleteAircraftTypeAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIAircraftType();
                ResponseErrorMessageVerification(restResponse, "404 Not Found", "title", "status");
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
                var postRequestBody = new AircraftTypeDto(0, "A306" + AircraftTypeSteps.valueRandomNumber, "A306" + AircraftTypeSteps.valueRandomNumber, "JET", "Airbus", 45, 2, "D", "H", "V6_Jet", Int32.Parse(sqlResponseDetails["Id"]));
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
                string WvcDB = sqlResponseDetails["Wvc"];
                string SpeedClassDB = sqlResponseDetails["SpeedClass"];
                int SiteIdDB = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = new AircraftTypeDto(IdValueDB, "PA1" + AircraftTypeSteps.valueRandomNumber, "PA1" + AircraftTypeSteps.valueRandomNumber, EngineDB, TypeNameDB, WidthDB, NumberOfEnginesDB, SizeCodeDB, WvcDB, SpeedClassDB, SiteIdDB);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Put_AircraftType_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<AircraftTypeDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
    }
}
