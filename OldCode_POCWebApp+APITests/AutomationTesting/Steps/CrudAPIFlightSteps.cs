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
    public class CrudAPIFlightSteps
    {
        private CommonOperationUtils CommonOperUtils => new(_driver);
        private APITests ApiTest => new(_driver);
        private IWebDriver _driver;
        public static APIAuthentication apiConfigDTO = new();
        public static AircraftTypeDto AircraftType;
        public static FlightDto _crudAPIFlightDTO;
        List<FlightDto> _crudAPIFlightAPIDTO = new();
        private TokenCreator tokenCreator => new();
        public static dynamic _crudAPIFlightDTOSingleRecord;
        public static int IdValueDB;
        public static string IdValueDeletedDB;
        public static string ColNameValueDB;
        public static string Icao;
        public static string Iata;
        public static string Engine;
        public static string TypeName;
        public static decimal Width;
        public static int NumberOfEngines;
        public static string SizeCode;
        public static string Wvc;
        public static string SpeedClass;
        public static int SiteId;
        public static string authorizationToken;
        public CrudAPIFlightSteps(IWebDriver driver)
        {
            _driver = driver;
        }
        [Then(@"Execute Crud Flight API And Set DTO Objects for Crud API Flight")]
        public void ThenExecuteCrudFlightAPIAndSetDTOObjectsForCrudAPIFlight()
        {
            try
            {
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest, 2000);
                _crudAPIFlightAPIDTO = JsonConvert.DeserializeObject<List<FlightDto>>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.Fail("Failed_To_Update_Crud_API_Get_Flight_Configuration");
            }
        }
        [Then(@"Compare values from API response set to DB record set for Crud Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudFlightAPI()
        {
            Console.WriteLine(_crudAPIFlightAPIDTO.Count);
            for (int i = 0; i < _crudAPIFlightAPIDTO.Count; i++)
            {
                Dictionary<string, string> flightDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(i);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].FlightId, Int32.Parse(flightDetails["FlightId"]), "FlightId did not match:" + _crudAPIFlightAPIDTO[i].FlightId + " != " + flightDetails["FlightId"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].SiteId, Int32.Parse(flightDetails["SiteId"]), "SiteId values do not match:" + _crudAPIFlightAPIDTO[i].SiteId + " != " + flightDetails["SiteId"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].FlightNumber, flightDetails["FlightNumber"], "FlightNumber values do not match:" + _crudAPIFlightAPIDTO[i].FlightNumber + " != " + flightDetails["FlightNumber"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].CallSign, flightDetails["CallSign"], "CallSign do not match:" + _crudAPIFlightAPIDTO[i].CallSign + " != " + flightDetails["CallSign"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].FlightRule.ToString(), flightDetails["FlightRule"], "FlightRule values do not match:" + _crudAPIFlightAPIDTO[i].FlightRule + " != " + flightDetails["FlightRule"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].DeicingRequest.ToString(), flightDetails["DeicingRequest"], "DeicingRequest do not match:" + _crudAPIFlightAPIDTO[i].DeicingRequest + " != " + flightDetails["DeicingRequest"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].Runway, flightDetails["Runway"], "Runway values do not match:" + _crudAPIFlightAPIDTO[i].Runway + " != " + flightDetails["Runway"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].Stand, flightDetails["Stand"], "Stand do not match:" + _crudAPIFlightAPIDTO[i].Stand + " != " + flightDetails["Stand"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].AirlinePriority, Int32.Parse(flightDetails["AirlinePriority"]), "AirlinePriority values do not match:" + _crudAPIFlightAPIDTO[i].AirlinePriority + " != " + flightDetails["AirlinePriority"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].EstimatedOffBlockTime, DateTimeOffset.Parse(flightDetails["EstimatedOffBlockTime"]), "EstimatedOffBlockTime do not match:" + _crudAPIFlightAPIDTO[i].EstimatedOffBlockTime + " != " + flightDetails["EstimatedOffBlockTime"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].ScheduledOffBlockTime, DateTimeOffset.Parse(flightDetails["ScheduledOffBlockTime"]), "ScheduledOffBlockTime values do not match:" + _crudAPIFlightAPIDTO[i].ScheduledOffBlockTime + " != " + flightDetails["ScheduledOffBlockTime"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].TargetOffBlockTime, DateTimeOffset.Parse(flightDetails["TargetOffBlockTime"]), "TargetOffBlockTime do not match:" + _crudAPIFlightAPIDTO[i].TargetOffBlockTime + " != " + flightDetails["TargetOffBlockTime"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].TargetTakeOffTime, DateTimeOffset.Parse(flightDetails["TargetTakeOffTime"]), "TargetTakeOffTime values do not match:" + _crudAPIFlightAPIDTO[i].TargetTakeOffTime + " != " + flightDetails["TargetTakeOffTime"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].TargetStartUpApprovalTime, DateTimeOffset.Parse(flightDetails["TargetStartUpApprovalTime"]), "TargetStartUpApprovalTime do not match:" + _crudAPIFlightAPIDTO[i].TargetStartUpApprovalTime + " != " + flightDetails["TargetStartUpApprovalTime"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].EstimatedTaxiOutTimeInSeconds, Int32.Parse(flightDetails["EstimatedTaxiOutTimeInSeconds"]), "EstimatedTaxiOutTimeInSeconds values do not match:" + _crudAPIFlightAPIDTO[i].EstimatedTaxiOutTimeInSeconds + " != " + flightDetails["EstimatedTaxiOutTimeInSeconds"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].EstimatedRunwayWaitTimeInSeconds, Int32.Parse(flightDetails["EstimatedRunwayWaitTimeInSeconds"]), "EstimatedRunwayWaitTimeInSeconds do not match:" + _crudAPIFlightAPIDTO[i].EstimatedRunwayWaitTimeInSeconds + " != " + flightDetails["EstimatedRunwayWaitTimeInSeconds"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].EstimatedDeicingTimeInSeconds, Int32.Parse(flightDetails["EstimatedDeicingTimeInSeconds"]), "EstimatedDeicingTimeInSeconds values do not match:" + _crudAPIFlightAPIDTO[i].EstimatedDeicingTimeInSeconds + " != " + flightDetails["EstimatedDeicingTimeInSeconds"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].StartupDurationInSeconds, Int32.Parse(flightDetails["StartupDurationInSeconds"]), "StartupDurationInSeconds do not match:" + _crudAPIFlightAPIDTO[i].StartupDurationInSeconds + " != " + flightDetails["StartupDurationInSeconds"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].PushbackDurationInSeconds, Int32.Parse(flightDetails["PushbackDurationInSeconds"]), "PushbackDurationInSeconds values do not match:" + _crudAPIFlightAPIDTO[i].PushbackDurationInSeconds + " != " + flightDetails["PushbackDurationInSeconds"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].CoordinatedDepartureWindow.Begin, DateTimeOffset.Parse(flightDetails["CoordinatedDepartureWindowBegin"]), "CoordinatedDepartureWindow Begin time do not match:" + _crudAPIFlightAPIDTO[i].CoordinatedDepartureWindow.Begin + " != " + flightDetails["CoordinatedDepartureWindowBegin"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].CoordinatedDepartureWindow.End, DateTimeOffset.Parse(flightDetails["CoordinatedDepartureWindowEnd"]), "CoordinatedDepartureWindow End time do not match:" + _crudAPIFlightAPIDTO[i].CoordinatedDepartureWindow.End + " != " + flightDetails["CoordinatedDepartureWindowEnd"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].CoordinatedDepartureTime, DateTimeOffset.Parse(flightDetails["CoordinatedDepartureTime"]), "CoordinatedDepartureTime do not match:" + _crudAPIFlightAPIDTO[i].CoordinatedDepartureTime + " != " + flightDetails["CoordinatedDepartureTime"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].AtcSlot.Begin, DateTimeOffset.Parse(flightDetails["AtcSlotBegin"]), "AtcSlot Begin time do not match:" + _crudAPIFlightAPIDTO[i].AtcSlot.Begin + " != " + flightDetails["AtcSlotBegin"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].AtcSlot.End, DateTimeOffset.Parse(flightDetails["AtcSlotEnd"]), "AtcSlot End time do not match:" + _crudAPIFlightAPIDTO[i].AtcSlot.End + " != " + flightDetails["AtcSlotEnd"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].TargetTakeOffTimeRequested, DateTimeOffset.Parse(flightDetails["TargetTakeOffTimeRequested"]), "TargetTakeOffTimeRequested do not match:" + _crudAPIFlightAPIDTO[i].TargetTakeOffTimeRequested + " != " + flightDetails["TargetTakeOffTimeRequested"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].IsManualFixed, Boolean.Parse(flightDetails["IsManualFixed"]), "IsManualFixed do not match:" + _crudAPIFlightAPIDTO[i].IsManualFixed + " != " + flightDetails["IsManualFixed"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].ParkPosition, Int32.Parse(flightDetails["ParkPosition"]), "ParkPosition do not match:" + _crudAPIFlightAPIDTO[i].ParkPosition + " != " + flightDetails["ParkPosition"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].Comment, flightDetails["Comment"], "Comment do not match:" + _crudAPIFlightAPIDTO[i].Comment + " != " + flightDetails["Comment"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].AircraftType.Id.ToString(), flightDetails["AircraftType"], "AircraftTypeId do not match:" + _crudAPIFlightAPIDTO[i].AircraftType.Id + " != " + flightDetails["AircraftType"]);
                Assert.AreEqual(_crudAPIFlightAPIDTO[i].EstimatedTaxiTimeToPadInSeconds, Int32.Parse(flightDetails["EstimatedTaxiTimeToPadInSeconds"]), "EstimatedTaxiTimeToPadInSeconds do not match:" + _crudAPIFlightAPIDTO[i].EstimatedTaxiTimeToPadInSeconds + " != " + flightDetails["EstimatedTaxiTimeToPadInSeconds"]);
            }
        }

        [Then(@"Generate new Get flight by ""(.*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Flight")]
        public static void ThenGenerateNewGetFlightByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIFlight(string colName)
        {
            try
            {
                Dictionary<string, string> flightDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                int IdValueDB = Int32.Parse(flightDetails[colName]);
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, IdValueDB);
                var restOBJRequest = restAPIUtil.CreateGetRequest(CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                _crudAPIFlightDTOSingleRecord = JsonConvert.DeserializeObject<FlightDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_FlightConfiguration");
            }
        }
        [Then(@"Validate the GET API operation by ""(.*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Flight")]
        public static void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesnTExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudFlight(string colName)
        {
            try
            {
                Dictionary<string, string> flightDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                int IdValueDB = Int32.Parse(flightDetails[colName]);
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, IdValueDB);
                var restOBJRequest = restAPIUtil.CreateGetRequest(CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                Console.WriteLine(restResponse.StatusCode.ToString() + " " + restResponse.Content.ToString() + " " + restResponse.StatusDescription.ToString());
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                string responseDataTitle = DynamicData["title"].ToString();
                string responseDataStatus = DynamicData["status"].ToString();
                string responseDataFinal = responseDataStatus + " " + responseDataTitle;
                Assert.AreEqual("404 Not Found", responseDataFinal, "Response code matches:" + responseDataFinal);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Flight_Get_By_Column_Configuration");
            }
        }
        [Then(@"Generate new Delete flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Flight")]
        public static void ThenGenerateNewDeleteFlightAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIFlight()
        {
            try
            {
                Dictionary<string, string> flightDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                IdValueDeletedDB = flightDetails["FlightId"];
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, Int32.Parse(IdValueDeletedDB));
                var request = new RestRequest();
                authorizationToken = CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN;
                var requestWithToken = restAPIUtil.TokenValidation(authorizationToken, request);
                var restResponse = restOBJSetup.Delete(requestWithToken);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_Flight");
            }
        }

        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Flight")]
        public static void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudFlight()
        {
            try
            {
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = restAPIUtil.SetURLByColumnValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, Int32.Parse(IdValueDeletedDB));
                var restOBJRequest = restAPIUtil.CreateGetRequest(CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                Console.WriteLine(restResponse.StatusCode.ToString() + " " + restResponse.Content.ToString() + " " + restResponse.StatusDescription.ToString());
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                string responseDataTitle = DynamicData["title"].ToString();
                string responseDataStatus = DynamicData["status"].ToString();
                string responseDataFinal = responseDataStatus + " " + responseDataTitle;
                Assert.AreEqual("404 Not Found", responseDataFinal, "Response code matches:" + responseDataFinal);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_Crud_API_Delete_By_specificColumnName_ResponseCodeMessage");
            }
        }
        [Then(@"Generate new ""(.*)"" using ""(.*)"" and ""(.*)"" in the API URL and Execute Crud Get By ""(.*)"" and ""(.*)"" request and Set DTO Objects for Crud API Flight")]
        public void ThenGenerateNewUsingAndInTheAPIURLAndExecuteCrudGetByAndRequestAndSetDTOObjectsForCrudAPIFlight(string apiReqName, string colName, string SecondColValue, string firstColName, string secondColName)
        {
            try
            {
                Dictionary<string, string> flightDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                ColNameValueDB = flightDetails[colName];
                SecondColValue = AircraftTypeSteps.valueRandomNumber;
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = restAPIUtil.SetURLByTwoStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, apiReqName, firstColName, ColNameValueDB, secondColName, SecondColValue);
                var restOBJRequest = restAPIUtil.CreateGetRequest(CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                _crudAPIFlightAPIDTO = JsonConvert.DeserializeObject<List<FlightDto>>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_FlightAPI");
            }
        }
        [Then(@"Execute Crud Post Flight API and Set DTO Objects for Crud API Flight")]
        public static void ThenExecuteCrudPostFlightAPIAndSetDTOObjectsForCrudAPIFlight()
        {
            try
            {
                Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                IdValueDB = Int32.Parse(aircraftTypeDetails["Id"]);
                Icao = aircraftTypeDetails["ICAO"];
                Iata = aircraftTypeDetails["IATA"];
                Engine = aircraftTypeDetails["Engine"];
                TypeName = aircraftTypeDetails["TypeName"];
                Width = Decimal.Parse(aircraftTypeDetails["Width"]);
                NumberOfEngines = Int32.Parse(aircraftTypeDetails["NumberOfEngines"]);
                SizeCode = aircraftTypeDetails["SizeCode"];
                Wvc = aircraftTypeDetails["Wvc"];
                SpeedClass = aircraftTypeDetails["SpeedClass"];
                SiteId = Int32.Parse(aircraftTypeDetails["SiteId"]);

                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL);
                var request = new RestRequest();
                var postRequestBody = new FlightDto(0, 20, "AI101")
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
                    AircraftType = new AircraftTypeDto(IdValueDB, Icao, Iata, Engine, TypeName, Width, NumberOfEngines, SizeCode, Wvc, SpeedClass, SiteId),
                    EstimatedTaxiTimeToPadInSeconds = 0
                };
                request.AddJsonBody(postRequestBody);
                authorizationToken = CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN;
                var requestWithToken = restAPIUtil.TokenValidation(authorizationToken, request);
                var restResponse = restOBJSetup.Post(requestWithToken);
                Console.WriteLine(restResponse.StatusCode.ToString() + " " + restResponse.Content.ToString());
                _crudAPIFlightDTOSingleRecord = JsonConvert.DeserializeObject<FlightDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_FlightConfiguration");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Flight API")]
        public static void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudFlightAPI()
        {
            Dictionary<string, string> flightDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.FlightId, Int32.Parse(flightDetails["FlightId"]), "FlightId did not match:" + _crudAPIFlightDTOSingleRecord.FlightId + " != " + flightDetails["FlightId"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.SiteId, Int32.Parse(flightDetails["SiteId"]), "SiteId values do not match:" + _crudAPIFlightDTOSingleRecord.SiteId + " != " + flightDetails["SiteId"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.FlightNumber, flightDetails["FlightNumber"], "FlightNumber values do not match:" + _crudAPIFlightDTOSingleRecord.FlightNumber + " != " + flightDetails["FlightNumber"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.CallSign, flightDetails["CallSign"], "CallSign do not match:" + _crudAPIFlightDTOSingleRecord.CallSign + " != " + flightDetails["CallSign"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.FlightRule.ToString(), flightDetails["FlightRule"], "FlightRule values do not match:" + _crudAPIFlightDTOSingleRecord.FlightRule + " != " + flightDetails["FlightRule"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.DeicingRequest.ToString(), flightDetails["DeicingRequest"], "DeicingRequest do not match:" + _crudAPIFlightDTOSingleRecord.DeicingRequest + " != " + flightDetails["DeicingRequest"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.Runway, flightDetails["Runway"], "Runway values do not match:" + _crudAPIFlightDTOSingleRecord.Runway + " != " + flightDetails["Runway"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.Stand, flightDetails["Stand"], "Stand do not match:" + _crudAPIFlightDTOSingleRecord.Stand + " != " + flightDetails["Stand"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.AirlinePriority, Int32.Parse(flightDetails["AirlinePriority"]), "AirlinePriority values do not match:" + _crudAPIFlightDTOSingleRecord.AirlinePriority + " != " + flightDetails["AirlinePriority"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.EstimatedOffBlockTime, DateTimeOffset.Parse(flightDetails["EstimatedOffBlockTime"]), "EstimatedOffBlockTime do not match:" + _crudAPIFlightDTOSingleRecord.EstimatedOffBlockTime + " != " + flightDetails["EstimatedOffBlockTime"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.ScheduledOffBlockTime, DateTimeOffset.Parse(flightDetails["ScheduledOffBlockTime"]), "ScheduledOffBlockTime values do not match:" + _crudAPIFlightDTOSingleRecord.ScheduledOffBlockTime + " != " + flightDetails["ScheduledOffBlockTime"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.TargetOffBlockTime, DateTimeOffset.Parse(flightDetails["TargetOffBlockTime"]), "TargetOffBlockTime do not match:" + _crudAPIFlightDTOSingleRecord.TargetOffBlockTime + " != " + flightDetails["TargetOffBlockTime"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.TargetTakeOffTime, DateTimeOffset.Parse(flightDetails["TargetTakeOffTime"]), "TargetTakeOffTime values do not match:" + _crudAPIFlightDTOSingleRecord.TargetTakeOffTime + " != " + flightDetails["TargetTakeOffTime"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.TargetStartUpApprovalTime, DateTimeOffset.Parse(flightDetails["TargetStartUpApprovalTime"]), "TargetStartUpApprovalTime do not match:" + _crudAPIFlightDTOSingleRecord.TargetStartUpApprovalTime + " != " + flightDetails["TargetStartUpApprovalTime"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.EstimatedTaxiOutTimeInSeconds, Int32.Parse(flightDetails["EstimatedTaxiOutTimeInSeconds"]), "EstimatedTaxiOutTimeInSeconds values do not match:" + _crudAPIFlightDTOSingleRecord.EstimatedTaxiOutTimeInSeconds + " != " + flightDetails["EstimatedTaxiOutTimeInSeconds"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.EstimatedRunwayWaitTimeInSeconds, Int32.Parse(flightDetails["EstimatedRunwayWaitTimeInSeconds"]), "EstimatedRunwayWaitTimeInSeconds do not match:" + _crudAPIFlightDTOSingleRecord.EstimatedRunwayWaitTimeInSeconds + " != " + flightDetails["EstimatedRunwayWaitTimeInSeconds"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.EstimatedDeicingTimeInSeconds, Int32.Parse(flightDetails["EstimatedDeicingTimeInSeconds"]), "EstimatedDeicingTimeInSeconds values do not match:" + _crudAPIFlightDTOSingleRecord.EstimatedDeicingTimeInSeconds + " != " + flightDetails["EstimatedDeicingTimeInSeconds"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.StartupDurationInSeconds, Int32.Parse(flightDetails["StartupDurationInSeconds"]), "StartupDurationInSeconds do not match:" + _crudAPIFlightDTOSingleRecord.StartupDurationInSeconds + " != " + flightDetails["StartupDurationInSeconds"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.PushbackDurationInSeconds, Int32.Parse(flightDetails["PushbackDurationInSeconds"]), "PushbackDurationInSeconds values do not match:" + _crudAPIFlightDTOSingleRecord.PushbackDurationInSeconds + " != " + flightDetails["PushbackDurationInSeconds"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.CoordinatedDepartureWindow.Begin, DateTimeOffset.Parse(flightDetails["CoordinatedDepartureWindowBegin"]), "CoordinatedDepartureWindow Begin time do not match:" + _crudAPIFlightDTOSingleRecord.CoordinatedDepartureWindow.Begin + " != " + flightDetails["CoordinatedDepartureWindowBegin"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.CoordinatedDepartureWindow.End, DateTimeOffset.Parse(flightDetails["CoordinatedDepartureWindowEnd"]), "CoordinatedDepartureWindow End time do not match:" + _crudAPIFlightDTOSingleRecord.CoordinatedDepartureWindow.End + " != " + flightDetails["CoordinatedDepartureWindowEnd"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.CoordinatedDepartureTime, DateTimeOffset.Parse(flightDetails["CoordinatedDepartureTime"]), "CoordinatedDepartureTime do not match:" + _crudAPIFlightDTOSingleRecord.CoordinatedDepartureTime + " != " + flightDetails["CoordinatedDepartureTime"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.AtcSlot.Begin, DateTimeOffset.Parse(flightDetails["AtcSlotBegin"]), "AtcSlot Begin time do not match:" + _crudAPIFlightDTOSingleRecord.AtcSlot.Begin + " != " + flightDetails["AtcSlotBegin"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.AtcSlot.End, DateTimeOffset.Parse(flightDetails["AtcSlotEnd"]), "AtcSlot End time do not match:" + _crudAPIFlightDTOSingleRecord.AtcSlot.End + " != " + flightDetails["AtcSlotEnd"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.TargetTakeOffTimeRequested, DateTimeOffset.Parse(flightDetails["TargetTakeOffTimeRequested"]), "TargetTakeOffTimeRequested do not match:" + _crudAPIFlightDTOSingleRecord.TargetTakeOffTimeRequested + " != " + flightDetails["TargetTakeOffTimeRequested"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.IsManualFixed, Boolean.Parse(flightDetails["IsManualFixed"]), "IsManualFixed do not match:" + _crudAPIFlightDTOSingleRecord.IsManualFixed + " != " + flightDetails["IsManualFixed"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.ParkPosition, Int32.Parse(flightDetails["ParkPosition"]), "ParkPosition do not match:" + _crudAPIFlightDTOSingleRecord.ParkPosition + " != " + flightDetails["ParkPosition"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.Comment, flightDetails["Comment"], "Comment do not match:" + _crudAPIFlightDTOSingleRecord.Comment + " != " + flightDetails["Comment"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.AircraftType.Id.ToString(), flightDetails["AircraftType"], "AircraftTypeId do not match:" + _crudAPIFlightDTOSingleRecord.AircraftType.Id + " != " + flightDetails["AircraftType"]);
            Assert.AreEqual(_crudAPIFlightDTOSingleRecord.EstimatedTaxiTimeToPadInSeconds, Int32.Parse(flightDetails["EstimatedTaxiTimeToPadInSeconds"]), "EstimatedTaxiTimeToPadInSeconds do not match:" + _crudAPIFlightDTOSingleRecord.EstimatedTaxiTimeToPadInSeconds + " != " + flightDetails["EstimatedTaxiTimeToPadInSeconds"]);
        }

        [Then(@"Generate new put Flight API URL using database fetched values and Execute Crud Put API Flight and Set DTO Objects for Crud API Flight")]
        public static void ThenGenerateNewPutFlightAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIFlightAndSetDTOObjectsForCrudAPIFlight()
        {
            try
            {
                Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                IdValueDB = Int32.Parse(aircraftTypeDetails["Id"]);
                Icao = aircraftTypeDetails["ICAO"];
                Iata = aircraftTypeDetails["IATA"];
                Engine = aircraftTypeDetails["Engine"];
                TypeName = aircraftTypeDetails["TypeName"];
                Width = Decimal.Parse(aircraftTypeDetails["Width"]);
                NumberOfEngines = Int32.Parse(aircraftTypeDetails["NumberOfEngines"]);
                SizeCode = aircraftTypeDetails["SizeCode"];
                Wvc = aircraftTypeDetails["Wvc"];
                SpeedClass = aircraftTypeDetails["SpeedClass"];
                SiteId = Int32.Parse(aircraftTypeDetails["SiteId"]);

                int IdFlightValueDB = Int32.Parse(AircraftTypeSteps.valueRandomNumber);
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL);
                var request = new RestRequest();
                var putRequestBody = new FlightDto(IdFlightValueDB, 20, "AI102")
                {
                    CallSign = "AirIndia12",
                    FlightRule = (FlightRule)1,
                    DeicingRequest = (DeicingRequest)1,
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
                    AircraftType = new AircraftTypeDto(IdValueDB, Icao, Iata, Engine, TypeName, Width, NumberOfEngines, SizeCode, Wvc, SpeedClass, SiteId),
                    EstimatedTaxiTimeToPadInSeconds = 0
                };
                request.AddJsonBody(putRequestBody);
                authorizationToken = CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN;
                var requestWithToken = restAPIUtil.TokenValidation(authorizationToken, request);
                var restResponse = restOBJSetup.Put(requestWithToken);
                _crudAPIFlightDTOSingleRecord = JsonConvert.DeserializeObject<FlightDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_FlightConfiguration");
            }
        }
        [Then(@"Execute Crud Post ""(.*)"" API and Set DTO Objects for Crud API Add Flight")]
        public void ThenExecuteCrudPostAPIAndSetDTOObjectsForCrudAPIAddFlight(string inputURL)
        {
            try
            {
                Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                IdValueDB = Int32.Parse(aircraftTypeDetails["Id"]);
                Icao = aircraftTypeDetails["ICAO"];
                Iata = aircraftTypeDetails["IATA"];
                Engine = aircraftTypeDetails["Engine"];
                TypeName = aircraftTypeDetails["TypeName"];
                Width = Decimal.Parse(aircraftTypeDetails["Width"]);
                NumberOfEngines = Int32.Parse(aircraftTypeDetails["NumberOfEngines"]);
                SizeCode = aircraftTypeDetails["SizeCode"];
                Wvc = aircraftTypeDetails["Wvc"];
                SpeedClass = aircraftTypeDetails["SpeedClass"];
                SiteId = Int32.Parse(aircraftTypeDetails["SiteId"]);

                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = restAPIUtil.SetURLByAppendedStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, inputURL);
                var request = new RestRequest();

                List<FlightDto> flights = new();
                flights.Add(new FlightDto(0, 20, "AI102")
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
                    AircraftType = new AircraftTypeDto(IdValueDB, Icao, Iata, Engine, TypeName, Width, NumberOfEngines, SizeCode, Wvc, SpeedClass, SiteId),
                    EstimatedTaxiTimeToPadInSeconds = 0
                });
                flights.Add(new FlightDto(0, 20, "AI101")
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
                    AircraftType = new AircraftTypeDto(IdValueDB, Icao, Iata, Engine, TypeName, Width, NumberOfEngines, SizeCode, Wvc, SpeedClass, SiteId),
                    EstimatedTaxiTimeToPadInSeconds = 0
                });
                request.AddJsonBody(flights);
                authorizationToken = CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN;
                var requestWithToken = restAPIUtil.TokenValidation(authorizationToken, request);
                var restResponse = restOBJSetup.Post(requestWithToken);
                Console.WriteLine(restResponse.StatusCode.ToString() + " " + restResponse.Content.ToString());
                _crudAPIFlightAPIDTO = JsonConvert.DeserializeObject<List<FlightDto>>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_FlightConfiguration");
            }
        }
        [Then(@"Generate new put ""(.*)"" API URL using database fetched values and Execute Crud Put API Update Flight List and Set DTO Objects for Crud API Flight")]
        public void ThenGenerateNewPutAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIUpdateFlightListAndSetDTOObjectsForCrudAPIFlight(string inputURL)
        {
            try
            {
                Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                IdValueDB = Int32.Parse(aircraftTypeDetails["Id"]);
                Icao = aircraftTypeDetails["ICAO"];
                Iata = aircraftTypeDetails["IATA"];
                Engine = aircraftTypeDetails["Engine"];
                TypeName = aircraftTypeDetails["TypeName"];
                Width = Decimal.Parse(aircraftTypeDetails["Width"]);
                NumberOfEngines = Int32.Parse(aircraftTypeDetails["NumberOfEngines"]);
                SizeCode = aircraftTypeDetails["SizeCode"];
                Wvc = aircraftTypeDetails["Wvc"];
                SpeedClass = aircraftTypeDetails["SpeedClass"];
                SiteId = Int32.Parse(aircraftTypeDetails["SiteId"]);

                int IdFlightValueDB = Int32.Parse(AircraftTypeSteps.valueRandomNumber);
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = restAPIUtil.SetURLByAppendedStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, inputURL);
                var request = new RestRequest();
                List<FlightDto> flights = new();
                flights.Add(new FlightDto(IdFlightValueDB, 20, "AI102")
                {
                    CallSign = "AirIndia12",
                    FlightRule = (FlightRule)1,
                    DeicingRequest = (DeicingRequest)1,
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
                    AircraftType = new AircraftTypeDto(IdValueDB, Icao, Iata, Engine, TypeName, Width, NumberOfEngines, SizeCode, Wvc, SpeedClass, SiteId),
                    EstimatedTaxiTimeToPadInSeconds = 0
                });
                request.AddJsonBody(flights);
                authorizationToken = CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN;
                var requestWithToken = restAPIUtil.TokenValidation(authorizationToken, request);
                var restResponse = restOBJSetup.Put(requestWithToken);
                _crudAPIFlightAPIDTO = JsonConvert.DeserializeObject<List<FlightDto>>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_FlightConfiguration");
            }
        }
        [Then(@"Generate new get ""(.*)"" using ""(.*)"" and ""(.*)"" and ""(.*)"" with values ""(.*)"" and ""(.*)"" and Execute Crud Get By Time window request and Set DTO Objects for Crud API Flight")]
        public void ThenGenerateNewGetUsingAndAndWithValuesAndAndExecuteCrudGetByTimeWindowRequestAndSetDTOObjectsForCrudAPIFlight(string apiReqName, string firstColName, string secondColName, string thirdColName, string firstColValue, string secondColValue)
        {
            try
            {
                string thirdColValue = AircraftTypeSteps.valueRandomNumber;
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = restAPIUtil.SetURLByThreeStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL, apiReqName, firstColName, firstColValue, secondColName, secondColValue, thirdColName, thirdColValue);
                var restOBJRequest = restAPIUtil.CreateGetRequest(CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                _crudAPIFlightAPIDTO = JsonConvert.DeserializeObject<List<FlightDto>>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_FlightConfiguration");
            }
        }
        [Then(@"Execute Crud Post Flight API using a new aircraft type Id and error should be thrown via API")]
        public static void ThenExecuteCrudPostFlightAPIUsingANewAircraftTypeIdAndErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                Icao = aircraftTypeDetails["ICAO"];
                Iata = aircraftTypeDetails["IATA"];
                Engine = aircraftTypeDetails["Engine"];
                TypeName = aircraftTypeDetails["TypeName"];
                Width = Decimal.Parse(aircraftTypeDetails["Width"]);
                NumberOfEngines = Int32.Parse(aircraftTypeDetails["NumberOfEngines"]);
                SizeCode = aircraftTypeDetails["SizeCode"];
                Wvc = aircraftTypeDetails["Wvc"];
                SpeedClass = aircraftTypeDetails["SpeedClass"];
                SiteId = Int32.Parse(aircraftTypeDetails["SiteId"]);

                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightAPIURL);
                var request = new RestRequest();
                var postRequestBody = new FlightDto(0, 20, "AI101")
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
                    AircraftType = new AircraftTypeDto(0, Icao, Iata, Engine, TypeName, Width, NumberOfEngines, SizeCode, Wvc, SpeedClass, SiteId),
                    EstimatedTaxiTimeToPadInSeconds = 0
                };
                request.AddJsonBody(postRequestBody);
                authorizationToken = CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN;
                var requestWithToken = restAPIUtil.TokenValidation(authorizationToken, request);
                var restResponse = restOBJSetup.Post(requestWithToken);
                Console.WriteLine(restResponse.StatusCode.ToString() + " " + restResponse.Content.ToString());
                dynamic DynamicData = JsonConvert.DeserializeObject(restResponse.Content);
                Console.WriteLine(DynamicData);
                string errorMessageExpected = "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Flight_ToAircraftType\". The conflict occurred in database \"acdm-data\", table \"dbo.AircraftType\", column 'Id'.\nThe statement has been terminated.";
                Assert.AreEqual(errorMessageExpected, DynamicData, "Response code matches:" + DynamicData);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_FlightConfiguration");
            }
        }
    }
}






