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
    public class CrudAPIFlightSteps : CrudAPIBaseMethods<FlightDto>
    {
        public CrudAPIFlightSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        {  }
        [Then(@"Execute Crud Flight API And Set DTO Objects for Crud API Flight for site claim ""([^""]*)""")]
        public void ThenExecuteCrudFlightAPIAndSetDTOObjectsForCrudAPIFlightForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.FlightId).ToList();
        }

        [Then(@"Execute Crud Flight API And Set DTO Objects for Crud API Flight")]
        public void ThenExecuteCrudFlightAPIAndSetDTOObjectsForCrudAPIFlight()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.FlightId).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudFlightAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }

        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is FlightDto flightTypeDto)
                {
                    Assert.AreEqual(flightTypeDto.FlightId, Int32.Parse(compareDictionary["FlightId"]), "FlightId did not match:" + flightTypeDto.FlightId + " != " + compareDictionary["FlightId"]);
                    Assert.AreEqual(flightTypeDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + flightTypeDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(flightTypeDto.FlightNumber, compareDictionary["FlightNumber"], "FlightNumber values do not match:" + flightTypeDto.FlightNumber + " != " + compareDictionary["FlightNumber"]);
                    Assert.AreEqual(flightTypeDto.CallSign, compareDictionary["CallSign"], "CallSign do not match:" + flightTypeDto.CallSign + " != " + compareDictionary["CallSign"]);
                    Assert.AreEqual(flightTypeDto.FlightRule.ToString(), compareDictionary["FlightRule"], "FlightRule values do not match:" + flightTypeDto.FlightRule + " != " + compareDictionary["FlightRule"]);
                    Assert.AreEqual(flightTypeDto.DeicingRequest.ToString(), compareDictionary["DeicingRequest"], "DeicingRequest do not match:" + flightTypeDto.DeicingRequest + " != " + compareDictionary["DeicingRequest"]);
                    Assert.AreEqual(flightTypeDto.Runway, compareDictionary["Runway"], "Runway values do not match:" + flightTypeDto.Runway + " != " + compareDictionary["Runway"]);
                    Assert.AreEqual(flightTypeDto.Stand, compareDictionary["Stand"], "Stand do not match:" + flightTypeDto.Stand + " != " + compareDictionary["Stand"]);
                    Assert.AreEqual(flightTypeDto.AirlinePriority, Int32.Parse(compareDictionary["AirlinePriority"]), "AirlinePriority values do not match:" + flightTypeDto.AirlinePriority + " != " + compareDictionary["AirlinePriority"]);
                    Assert.AreEqual(flightTypeDto.EstimatedOffBlockTime, ParseDateTime(compareDictionary["EstimatedOffBlockTime"]), "EstimatedOffBlockTime do not match:" + flightTypeDto.EstimatedOffBlockTime + " != " + compareDictionary["EstimatedOffBlockTime"]);
                    Assert.AreEqual(flightTypeDto.ScheduledOffBlockTime, ParseDateTime(compareDictionary["ScheduledOffBlockTime"]), "ScheduledOffBlockTime values do not match:" + flightTypeDto.ScheduledOffBlockTime + " != " + compareDictionary["ScheduledOffBlockTime"]);
                    Assert.AreEqual(flightTypeDto.TargetOffBlockTime, ParseDateTime(compareDictionary["TargetOffBlockTime"]), "TargetOffBlockTime do not match:" + flightTypeDto.TargetOffBlockTime + " != " + compareDictionary["TargetOffBlockTime"]);
                    Assert.AreEqual(flightTypeDto.TargetTakeOffTime, ParseDateTime(compareDictionary["TargetTakeOffTime"]), "TargetTakeOffTime values do not match:" + flightTypeDto.TargetTakeOffTime + " != " + compareDictionary["TargetTakeOffTime"]);
                    Assert.AreEqual(flightTypeDto.TargetStartUpApprovalTime, ParseDateTime(compareDictionary["TargetStartUpApprovalTime"]), "TargetStartUpApprovalTime do not match:" + flightTypeDto.TargetStartUpApprovalTime + " != " + compareDictionary["TargetStartUpApprovalTime"]);
                    Assert.AreEqual(flightTypeDto.EstimatedTaxiOutTimeInSeconds, Int32.Parse(compareDictionary["EstimatedTaxiOutTimeInSeconds"]), "EstimatedTaxiOutTimeInSeconds values do not match:" + flightTypeDto.EstimatedTaxiOutTimeInSeconds + " != " + compareDictionary["EstimatedTaxiOutTimeInSeconds"]);
                    Assert.AreEqual(flightTypeDto.EstimatedRunwayWaitTimeInSeconds, Int32.Parse(compareDictionary["EstimatedRunwayWaitTimeInSeconds"]), "EstimatedRunwayWaitTimeInSeconds do not match:" + flightTypeDto.EstimatedRunwayWaitTimeInSeconds + " != " + compareDictionary["EstimatedRunwayWaitTimeInSeconds"]);
                    Assert.AreEqual(flightTypeDto.EstimatedDeicingTimeInSeconds, Int32.Parse(compareDictionary["EstimatedDeicingTimeInSeconds"]), "EstimatedDeicingTimeInSeconds values do not match:" + flightTypeDto.EstimatedDeicingTimeInSeconds + " != " + compareDictionary["EstimatedDeicingTimeInSeconds"]);
                    Assert.AreEqual(flightTypeDto.StartupDurationInSeconds, Int32.Parse(compareDictionary["StartupDurationInSeconds"]), "StartupDurationInSeconds do not match:" + flightTypeDto.StartupDurationInSeconds + " != " + compareDictionary["StartupDurationInSeconds"]);
                    Assert.AreEqual(flightTypeDto.PushbackDurationInSeconds, Int32.Parse(compareDictionary["PushbackDurationInSeconds"]), "PushbackDurationInSeconds values do not match:" + flightTypeDto.PushbackDurationInSeconds + " != " + compareDictionary["PushbackDurationInSeconds"]);
                    Assert.AreEqual(flightTypeDto.CoordinatedDepartureWindow.Begin, ParseDateTime(compareDictionary["CoordinatedDepartureWindowBegin"]), "CoordinatedDepartureWindow Begin time do not match:" + flightTypeDto.CoordinatedDepartureWindow.Begin + " != " + compareDictionary["CoordinatedDepartureWindowBegin"]);
                    Assert.AreEqual(flightTypeDto.CoordinatedDepartureWindow.End, ParseDateTime(compareDictionary["CoordinatedDepartureWindowEnd"]), "CoordinatedDepartureWindow End time do not match:" + flightTypeDto.CoordinatedDepartureWindow.End + " != " + compareDictionary["CoordinatedDepartureWindowEnd"]);
                    Assert.AreEqual(flightTypeDto.CoordinatedDepartureTime, ParseDateTime(compareDictionary["CoordinatedDepartureTime"]), "CoordinatedDepartureTime do not match:" + flightTypeDto.CoordinatedDepartureTime + " != " + compareDictionary["CoordinatedDepartureTime"]);
                    Assert.AreEqual(flightTypeDto.AtcSlot.Begin, ParseDateTime(compareDictionary["AtcSlotBegin"]), "AtcSlot Begin time do not match:" + flightTypeDto.AtcSlot.Begin + " != " + compareDictionary["AtcSlotBegin"]);
                    Assert.AreEqual(flightTypeDto.AtcSlot.End, ParseDateTime(compareDictionary["AtcSlotEnd"]), "AtcSlot End time do not match:" + flightTypeDto.AtcSlot.End + " != " + compareDictionary["AtcSlotEnd"]);
                    Assert.AreEqual(flightTypeDto.TargetTakeOffTimeRequested, ParseDateTime(compareDictionary["TargetTakeOffTimeRequested"]), "TargetTakeOffTimeRequested do not match:" + flightTypeDto.TargetTakeOffTimeRequested + " != " + compareDictionary["TargetTakeOffTimeRequested"]);
                    Assert.AreEqual(flightTypeDto.IsManualFixed, Boolean.Parse(compareDictionary["IsManualFixed"]), "IsManualFixed do not match:" + flightTypeDto.IsManualFixed + " != " + compareDictionary["IsManualFixed"]);
                    Assert.AreEqual(flightTypeDto.ParkPosition, Int32.Parse(compareDictionary["ParkPosition"]), "ParkPosition do not match:" + flightTypeDto.ParkPosition + " != " + compareDictionary["ParkPosition"]);
                    Assert.AreEqual(flightTypeDto.Comment, compareDictionary["Comment"], "Comment do not match:" + flightTypeDto.Comment + " != " + compareDictionary["Comment"]);
                    Assert.AreEqual(flightTypeDto.AircraftType.Id.ToString(), compareDictionary["AircraftType"], "AircraftTypeId do not match:" + flightTypeDto.AircraftType.Id + " != " + compareDictionary["AircraftType"]);
                    Assert.AreEqual(flightTypeDto.EstimatedTaxiTimeToPadInSeconds, Int32.Parse(compareDictionary["EstimatedTaxiTimeToPadInSeconds"]), "EstimatedTaxiTimeToPadInSeconds do not match:" + flightTypeDto.EstimatedTaxiTimeToPadInSeconds + " != " + compareDictionary["EstimatedTaxiTimeToPadInSeconds"]);
                }
                else
                {
                    Assert.Fail("Return values which are not flight type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }

        [Then(@"Generate new Get flight by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Flight")]
        public void ThenGenerateNewGetFlightByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIFlight(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.FlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<FlightDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_FlightType");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudFlightAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> flightDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(flightDetails, dtoResultSingleRecord);
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Flight")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudFlight(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.FlightAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new Delete flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Flight")]
        public IRestResponse ThenGenerateNewDeleteFlightAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIFlight()
        {
            var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.FlightAPIURL, "FlightId", "Failed_To_Execute_Get_By_API");
            IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_FlightType");
            return restResponse;
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Flight")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudFlight()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Fetch value for field ""([^""]*)"" against Aircraft Type Id fetched from database response to the above query")]
        public void ThenFetchValueForFieldAgainstAircraftTypeIdFetchedFromDatabaseResponseToTheAboveQuery(string keyColName)
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> sqlQueryDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
            AddOrUpdateScenarioContext("outputSQLResponse", sqlQueryDetails[keyColName]);
            string outputSQLResponse = (_scenarioContext.Get<string>("outputSQLResponse"));
            Console.WriteLine(outputSQLResponse);
            if (outputSQLResponse.Equals(null) || outputSQLResponse.Equals(""))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_FieldValue_Database");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request and Set DTO Objects for Crud API Flight")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestAndSetDTOObjectsForCrudAPIFlight(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Execute_Get_By_API_Request");
                dtoResultList = dtoResult.OrderBy(x => x.FlightId).ToList();
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_FlightAPI");
            }
        }
        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" with values ""([^""]*)"" and ""([^""]*)"" and Execute Crud Get By Time window request and Set DTO Objects for Crud API Flight")]
        public void ThenGenerateNewGetUsingAndAndWithValuesAndAndExecuteCrudGetByTimeWindowRequestAndSetDTOObjectsForCrudAPIFlight(string apiReqName, string firstColName, string secondColName, string thirdColName, string firstColValue, string secondColValue)
        {
            string thirdColValue = (_scenarioContext.Get<string>("valueSQLData"));
            var restOBJSetup = restAPIUtil.SetURLByThreeStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, apiReqName, firstColName, firstColValue, secondColName, secondColValue, thirdColName, thirdColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.FlightId).ToList();
        }
        [Then(@"Execute Crud Post Flight API and Set DTO Objects for Crud API Flight")]
        public void ThenExecuteCrudPostFlightAPIAndSetDTOObjectsForCrudAPIFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);

                var postRequestBody = PostRequestMethodBody(IdValueDB, SiteId, SiteId1);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Update_Crud_API_Post_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_FlightConfiguration");
            }
        }
        [Then(@"Execute Crud Post Flight API for negative test and verify that error should be thrown via API")]
        public void ThenExecuteCrudPostFlightAPIForNegativeTestAndVerifyThatErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);

                int SiteId1 = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                var postRequestBody = PostRequestMethodBody(IdValueDB, SiteId, SiteId1);

                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Update_Crud_API_Post_Flight_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<FlightDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_FlightConfiguration_Negative scenario");
            }
        }
        [Then(@"Execute Crud Post Flight API using a new aircraft type Id and error should be thrown via API")]
        public void ThenExecuteCrudPostFlightAPIUsingANewAircraftTypeIdAndErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);

                var postRequestBody = PostRequestMethodBody(0, SiteId, SiteId1);
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Update_Crud_API_Post_Flight_Configuration");
                APIResponseErrorValidation(restResponse, "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Flight_ToAircraftType\". The conflict occurred in database \"acdm-data\", table \"resources.AircraftType\", column 'Id'.", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_FlightConfiguration");
            }
        }
        [Then(@"Generate new put Flight API URL using database fetched values and Execute Crud Put API Flight and Set DTO Objects for Crud API Flight")]
        public void ThenGenerateNewPutFlightAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIFlightAndSetDTOObjectsForCrudAPIFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Engine = sqlResponseDetails["Engine"];
                var putRequestBody = PutRequestMethodBody(Engine);
                dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Update_Crud_API_Put_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_FlightConfiguration");
            }
        }
        [Then(@"Execute Crud Post ""([^""]*)"" API and Set DTO Objects for Crud API Add Flight")]
        public void ThenExecuteCrudPostAPIAndSetDTOObjectsForCrudAPIAddFlight(string inputURL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                string Icao = sqlResponseDetails["ICAO"];
                string Iata = sqlResponseDetails["IATA"];
                string Engine = sqlResponseDetails["Engine"];
                string TypeName = sqlResponseDetails["TypeName"];
                decimal Width = Decimal.Parse(sqlResponseDetails["Width"]);
                int NumberOfEngines = Int32.Parse(sqlResponseDetails["NumberOfEngines"]);
                string SizeCode = sqlResponseDetails["SizeCode"];
                int WakeTurbulenceCategoryIdDB = Int32.Parse(sqlResponseDetails["WakeTurbulenceCategoryId"]);
                string SpeedClass = sqlResponseDetails["SpeedClass"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string IcaoNewValue = "LR2T" + (_scenarioContext.Get<string>("valueSQLData"));
                string appendedURL = APITests.apiConfigDTO.FlightAPIURL + "/" + inputURL;

                List<FlightDto> flights = new();
                flights.Add(new FlightDto(0, SiteId, "AI102")
                {
                    CallSign = "AirIndia",
                    FlightRule = (FlightRule)1,
                    DeicingRequest = (DeicingRequest)1,
                    Runway = "06R",
                    Stand = "A22",
                    AirlinePriority = 0,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ScheduledOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetOffBlockTime = DateTimeOffset.Parse("2022-05-02T14:15:06"),
                    TargetTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedTaxiOutTimeInSeconds = 2,
                    EstimatedRunwayWaitTimeInSeconds = 4,
                    EstimatedDeicingTimeInSeconds = 5,
                    StartupDurationInSeconds = 7,
                    PushbackDurationInSeconds = 8,
                    CoordinatedDepartureWindow = {
                                Begin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                                End = DateTimeOffset.Parse("2022-05-06T14:15:06")
                            },
                    CoordinatedDepartureTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AtcSlot = {
                                Begin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                                End = DateTimeOffset.Parse("2022-05-06T14:15:06")
                            },
                    TargetTakeOffTimeRequested = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    IsManualFixed = true,
                    ParkPosition = 0,
                    Comment = "Test comment",
                    AircraftType = new AircraftTypeDto() { Id = IdValueDB, Icao = Icao, Iata = Iata, Engine = Engine, TypeName = TypeName, Width = Width, NumberOfEngines = NumberOfEngines, SizeCode = SizeCode, SpeedClass = SpeedClass, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = WakeTurbulenceCategoryIdDB, SiteId = 1, Category = "F", CategoryName = "Light" } },
                    EstimatedTaxiTimeToPadInSeconds = 0
                });
                flights.Add(new FlightDto(0, SiteId, "AI101")
                {
                    CallSign = "AirIndia",
                    FlightRule = (FlightRule)1,
                    DeicingRequest = (DeicingRequest)1,
                    Runway = "06R",
                    Stand = "A22",
                    AirlinePriority = 0,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ScheduledOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetOffBlockTime = DateTimeOffset.Parse("2022-05-02T14:15:06"),
                    TargetTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedTaxiOutTimeInSeconds = 2,
                    EstimatedRunwayWaitTimeInSeconds = 4,
                    EstimatedDeicingTimeInSeconds = 5,
                    StartupDurationInSeconds = 7,
                    PushbackDurationInSeconds = 8,
                    CoordinatedDepartureWindow = {
                                Begin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                                End = DateTimeOffset.Parse("2022-05-06T14:15:06")
                            },
                    CoordinatedDepartureTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AtcSlot = {
                                Begin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                                End = DateTimeOffset.Parse("2022-05-06T14:15:06")
                            },
                    TargetTakeOffTimeRequested = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    IsManualFixed = true,
                    ParkPosition = 0,
                    Comment = "Test comment",
                    AircraftType = new AircraftTypeDto() { Id = IdValueDB, Icao = IcaoNewValue, Iata = IcaoNewValue, Engine = Engine, TypeName = TypeName, Width = Width, NumberOfEngines = NumberOfEngines, SizeCode = SizeCode, SpeedClass = SpeedClass, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = WakeTurbulenceCategoryIdDB, SiteId = 1, Category = "F", CategoryName = "Light" } },
                    EstimatedTaxiTimeToPadInSeconds = 0
                });
                dtoResultList = PostAPIWithDeserializeList(flights, appendedURL, "Failed_To_Update_Crud_API_Post_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_FlightListConfiguration");
            }
        }
        [Then(@"Execute Crud Flight API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudFlightAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_Flight_MessageVerification");
            }
        }
        [Then(@"Generate new Get Flight by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetFlightByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.FlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_FlightType_MessageVerification");
            }
        }
        [Then(@"Generate new Delete Flight API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteFlightAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteFlightAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIFlight();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for flight Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Flight");
            }
        }
        [Then(@"Execute Crud Post Flight API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostFlightAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);

                var postRequestBody = PostRequestMethodBody(IdValueDB, SiteId, SiteId1);
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Update_Crud_API_Post_Flight_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<FlightDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put Flight API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutFlightAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Engine = sqlResponseDetails["Engine"];
                var putRequestBody = PutRequestMethodBody(Engine);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Update_Crud_API_Put_Flight_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        public FlightDto PostRequestMethodBody(int Id, int SiteId, int SiteId1)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Icao = sqlResponseDetails["ICAO"];
                string Iata = sqlResponseDetails["IATA"];
                string Engine = sqlResponseDetails["Engine"];
                string TypeName = sqlResponseDetails["TypeName"];
                decimal Width = Decimal.Parse(sqlResponseDetails["Width"]);
                int NumberOfEngines = Int32.Parse(sqlResponseDetails["NumberOfEngines"]);
                string SizeCode = sqlResponseDetails["SizeCode"];
                int WakeTurbulenceCategoryId = Int32.Parse(sqlResponseDetails["WakeTurbulenceCategoryId"]);
                string SpeedClass = sqlResponseDetails["SpeedClass"];

                var postRequestBody = new FlightDto(0, SiteId, "AI101")
                {
                    CallSign = "AirIndia",
                    FlightRule = (FlightRule)1,
                    DeicingRequest = (DeicingRequest)1,
                    Runway = "06R",
                    Stand = "A22",
                    AirlinePriority = 0,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ScheduledOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetOffBlockTime = DateTimeOffset.Parse("2022-05-02T14:15:06"),
                    TargetTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedTaxiOutTimeInSeconds = 2,
                    EstimatedRunwayWaitTimeInSeconds = 4,
                    EstimatedDeicingTimeInSeconds = 5,
                    StartupDurationInSeconds = 7,
                    PushbackDurationInSeconds = 8,
                    CoordinatedDepartureWindow = {
                            Begin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                            End = DateTimeOffset.Parse("2022-05-06T14:15:06")
                        },
                    CoordinatedDepartureTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AtcSlot = {
                            Begin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                            End = DateTimeOffset.Parse("2022-05-06T14:15:06")
                        },
                    TargetTakeOffTimeRequested = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    IsManualFixed = true,
                    ParkPosition = 0,
                    Comment = "Test comment",
                    AircraftType = new AircraftTypeDto() { Id = Id, Icao = Icao, Iata = Iata, Engine = Engine, TypeName = TypeName, Width = Width, NumberOfEngines = NumberOfEngines, SizeCode = SizeCode, SpeedClass = SpeedClass, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = WakeTurbulenceCategoryId, SiteId = 1, Category = "F", CategoryName = "Light" } },
                    EstimatedTaxiTimeToPadInSeconds = 0
                };
                return postRequestBody;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_FlightAPI");
                return null;
            }
        }
        public FlightDto PutRequestMethodBody(string Engine)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                string Icao = sqlResponseDetails["ICAO"];
                string Iata = sqlResponseDetails["IATA"];
                string TypeName = sqlResponseDetails["TypeName"];
                decimal Width = Decimal.Parse(sqlResponseDetails["Width"]);
                int NumberOfEngines = Int32.Parse(sqlResponseDetails["NumberOfEngines"]);
                string SizeCode = sqlResponseDetails["SizeCode"];
                int WakeTurbulenceCategoryId = Int32.Parse(sqlResponseDetails["WakeTurbulenceCategoryId"]);
                string SpeedClass = sqlResponseDetails["SpeedClass"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int IdFlightValueDB = Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));

                var putRequestBody = new FlightDto(IdFlightValueDB, SiteId, "AI102")
                {
                    CallSign = "AirIndia12",
                    FlightRule = (FlightRule)1,
                    DeicingRequest = (DeicingRequest)2,
                    Runway = "06R",
                    Stand = "A22",
                    AirlinePriority = 0,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ScheduledOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedTaxiOutTimeInSeconds = 2,
                    EstimatedRunwayWaitTimeInSeconds = 4,
                    EstimatedDeicingTimeInSeconds = 5,
                    StartupDurationInSeconds = 7,
                    PushbackDurationInSeconds = 8,
                    CoordinatedDepartureWindow = {
                            Begin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                            End = DateTimeOffset.Parse("2022-05-06T14:15:06")
                        },
                    CoordinatedDepartureTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AtcSlot = {
                            Begin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                            End = DateTimeOffset.Parse("2022-05-06T14:15:06")
                        },
                    TargetTakeOffTimeRequested = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    IsManualFixed = true,
                    ParkPosition = 0,
                    Comment = "Test updated comment",
                    AircraftType = new AircraftTypeDto() { Id = IdValueDB, Icao = Icao, Iata = Iata, Engine = Engine, TypeName = TypeName, Width = Width, NumberOfEngines = NumberOfEngines, SizeCode = SizeCode, SpeedClass = SpeedClass, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = WakeTurbulenceCategoryId, SiteId = 1, Category = "F", CategoryName = "Light" } },
                    EstimatedTaxiTimeToPadInSeconds = 0
                };
                return putRequestBody;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_FlightAPI");
                return null;
            }
        }
        [Then(@"Generate new put Flight API URL for negative test and verify that error should be thrown via API")]
        public void ThenGenerateNewPutFlightAPIURLForNegativeTestAndVerifyThatErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Engine = sqlResponseDetails["Engine"];
                var putRequestBody = PutRequestMethodBody(Engine);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Update_Crud_API_Put_Flight_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_NegativeTest");
            }
        }
        [Then(@"Generate new Get flight by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Flight")]
        public void ThenGenerateNewGetFlightByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIFlight(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.FlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with FlightId: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_Flight");
            }
        }
        [Then(@"Generate new ""([^""]*)"" using ""([^""]*)"" in the API URL and Execute Crud Get By ""([^""]*)"" request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Flight")]
        public void ThenGenerateNewUsingInTheAPIURLAndExecuteCrudGetByRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIFlight(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.FlightAPIURL, "Failed_To_Execute_Get_By_API_Request");
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
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_FlightAPI");
            }
        }
        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" with values ""([^""]*)"" and ""([^""]*)"" and Execute Crud Get By Time window request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Flight")]
        public void ThenGenerateNewGetUsingAndAndWithValuesAndAndExecuteCrudGetByTimeWindowRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIFlight(string apiReqName, string firstColName, string secondColName, string thirdColName, string firstColValue, string secondColValue)
        {
            string thirdColValue = (_scenarioContext.Get<string>("valueSQLData"));
            var restOBJSetup = restAPIUtil.SetURLByThreeStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, apiReqName, firstColName, firstColValue, secondColName, secondColValue, thirdColName, thirdColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            if (dtoResult.Count == 0)
            {
                Console.WriteLine("Response content is an empty list");
            }
            else
            {
                Console.WriteLine("Response content is not an empty list");
            }
        }                
    }
}











