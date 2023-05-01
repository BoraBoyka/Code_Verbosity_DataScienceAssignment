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
    public class CrudAPIDepartureFlightSteps : CrudAPIBaseMethods<DepartureDto>
    {
        public CrudAPIDepartureFlightSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }

        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is DepartureDto departureFlightDto)
                {
                    Assert.AreEqual(departureFlightDto.Id, Int32.Parse(compareDictionary["Id"]), "DepartureFlightId did not match:" + departureFlightDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(departureFlightDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + departureFlightDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(departureFlightDto.Carrier, compareDictionary["Carrier"], "Carrier values do not match:" + departureFlightDto.Carrier + " != " + compareDictionary["Carrier"]);
                    Assert.AreEqual(departureFlightDto.FlightNumber, compareDictionary["FlightNumber"], "FlightNumber do not match:" + departureFlightDto.FlightNumber + " != " + compareDictionary["FlightNumber"]);
                    Assert.AreEqual(departureFlightDto.Destination, compareDictionary["Destination"], "Destination values do not match:" + departureFlightDto.Destination + " != " + compareDictionary["Destination"]);
                    Assert.AreEqual(departureFlightDto.OperationDate, ParseDateTime(compareDictionary["OperationDate"]), "OperationDate do not match:" + departureFlightDto.OperationDate + " != " + compareDictionary["OperationDate"]);
                    Assert.AreEqual(departureFlightDto.AircraftRegistrationId, compareDictionary["AircraftRegistrationId"], "AircraftRegistrationId values do not match:" + departureFlightDto.AircraftRegistrationId + " != " + compareDictionary["AircraftRegistrationId"]);
                    Assert.AreEqual(departureFlightDto.CallSign, compareDictionary["CallSign"], "CallSign do not match:" + departureFlightDto.CallSign + " != " + compareDictionary["CallSign"]);
                    Assert.AreEqual(departureFlightDto.ArcId, compareDictionary["ArcId"], "ArcId values do not match:" + departureFlightDto.ArcId + " != " + compareDictionary["ArcId"]);
                    Assert.AreEqual(departureFlightDto.Runway, compareDictionary["Runway"], "Runway do not match:" + departureFlightDto.Runway + " != " + compareDictionary["Runway"]);
                    if (departureFlightDto.AircraftType != null)
                    {
                        Assert.AreEqual(departureFlightDto.AircraftType.Id.ToString(), compareDictionary["AircraftType"], "AircraftType do not match:" + departureFlightDto.AircraftType.Id + " != " + compareDictionary["AircraftType"]);
                    }
                    else
                    {
                        Assert.AreEqual(null, compareDictionary["AircraftType"], "AircraftType do not match:" + departureFlightDto.AircraftType + " != " + compareDictionary["AircraftType"]);
                    }
                    Assert.AreEqual(departureFlightDto.Stand, compareDictionary["Stand"], "Stand values do not match:" + departureFlightDto.Stand + " != " + compareDictionary["Stand"]);
                    Assert.AreEqual(departureFlightDto.DeicingRequest.ToString(), compareDictionary["DeicingRequest"], "DeicingRequest do not match:" + departureFlightDto.DeicingRequest + " != " + compareDictionary["DeicingRequest"]);
                    Assert.AreEqual(departureFlightDto.ActualTakeOffTime, ParseDateTime(compareDictionary["ActualTakeOffTime"]), "ActualTakeOffTime values do not match:" + departureFlightDto.ActualTakeOffTime + " != " + compareDictionary["ActualTakeOffTime"]);
                    Assert.AreEqual(departureFlightDto.EstimatedTakeOffTime, ParseDateTime(compareDictionary["EstimatedTakeOffTime"]), "EstimatedTakeOffTime do not match:" + departureFlightDto.EstimatedTakeOffTime + " != " + compareDictionary["EstimatedTakeOffTime"]);
                    Assert.AreEqual(departureFlightDto.TargetTakeOffTime, ParseDateTime(compareDictionary["TargetTakeOffTime"]), "TargetTakeOffTime values do not match:" + departureFlightDto.TargetTakeOffTime + " != " + compareDictionary["TargetTakeOffTime"]);
                    Assert.AreEqual(departureFlightDto.TargetTakeOffTimeRequested, ParseDateTime(compareDictionary["TargetTakeOffTimeRequested"]), "TargetTakeOffTimeRequested do not match:" + departureFlightDto.TargetTakeOffTimeRequested + " != " + compareDictionary["TargetTakeOffTimeRequested"]);
                    Assert.AreEqual(departureFlightDto.EstimatedOffBlockTime, ParseDateTime(compareDictionary["EstimatedOffBlockTime"]), "EstimatedOffBlockTime do not match:" + departureFlightDto.EstimatedOffBlockTime + " != " + compareDictionary["EstimatedOffBlockTime"]);
                    Assert.AreEqual(departureFlightDto.TargetOffBlockTime, ParseDateTime(compareDictionary["TargetOffBlockTime"]), "TargetOffBlockTime values do not match:" + departureFlightDto.TargetOffBlockTime + " != " + compareDictionary["TargetOffBlockTime"]);
                    Assert.AreEqual(departureFlightDto.ActualOffBlocktime, ParseDateTime(compareDictionary["ActualOffBlocktime"]), "ActualOffBlocktime do not match:" + departureFlightDto.ActualOffBlocktime + " != " + compareDictionary["ActualOffBlocktime"]);
                    if (compareDictionary["Field"] != null)
                    {
                        Assert.AreEqual(departureFlightDto.DepartureFieldValues.First(dv => dv.Field == compareDictionary["Field"] && dv.Instance == int.Parse(compareDictionary["Instance"])).Value, compareDictionary["Value"], "Values in Departure filed tables match with the Dto");
                    }
                    else
                        Assert.AreEqual(departureFlightDto.DepartureFieldValues.Count, 0, "There are no values against the record in Departure Value table");
                }
                else
                {
                    Assert.Fail("Return values which are not Departure flight type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }

        [Then(@"Execute Crud Departure Flight API And Set DTO Objects for Crud API Departure Flight for site claim ""([^""]*)""")]
        public void ThenExecuteCrudDepartureFlightAPIAndSetDTOObjectsForCrudAPIDepartureFlightForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud Departure Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudDepartureFlightAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
            List<DepartureValueDto> departureValueDTO = new();
            foreach (DepartureDto dto in dtoResultList)
            {
                departureValueDTO.AddRange(dto.DepartureFieldValues);
            }
        }
        public override void ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI()
        {
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            if (dtoResultList.Any() && resultData.Any())
            {
                int lastDepartureId = dtoResultList.First().Id;
                int valueCount = 0;
                for (int i = 0; i < resultData.Count; i++)
                {
                    valueCount++;
                    Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(i);
                    APIDBComparison(APIDetails, dtoResultList.First(d => d.Id == int.Parse(APIDetails["Id"])));
                    if (lastDepartureId != int.Parse(APIDetails["Id"]))
                    {
                        if (((Dictionary<string, string>)resultData[i - 1])["Field"] != null)
                        {
                            Assert.AreEqual(dtoResultList.First(d => d.Id == lastDepartureId).DepartureFieldValues.Count, valueCount - 1);
                        }
                        lastDepartureId = int.Parse(APIDetails["Id"]);
                        valueCount = 1;
                    }
                }
                if (((Dictionary<string, string>)resultData[resultData.Count - 1])["Field"] != null)
                {
                    Assert.AreEqual(dtoResultList.First(d => d.Id == lastDepartureId).DepartureFieldValues.Count, valueCount);
                }
            }
            else if ((dtoResultList.Any() && !resultData.Any()) || (!dtoResultList.Any() && resultData.Any()))
            {
                Assert.Fail("The values in Dto and resut data from DB do not match", dtoResultList.Count, resultData.Count);
            }
            List<int> resultIdDB = resultData.Select(r => int.Parse(((Dictionary<string, string>)r)["Id"])).Distinct().ToList();
            List<int> resultIdDto = dtoResultList.Select(d => d.Id).ToList();
            Assert.IsTrue(!resultIdDto.Except(resultIdDB).Any());
        }

        [Then(@"Generate new Get Departure flight by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Departure Flight")]
        public void ThenGenerateNewGetDepartureFlightByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIDepartureFlight(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.DepartureFlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<DepartureDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_DepartureFlightType");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Departure Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudDepartureFlightAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> departureFlightDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(departureFlightDetails, dtoResultSingleRecord);
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Departure Flight")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudDepartureFlight(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.DepartureFlightAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new Get Departure flight by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Departure Flight")]
        public void ThenGenerateNewGetDepartureFlightByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIDepartureFlight(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.DepartureFlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with departure Id: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_DepartureFlight");
            }
        }
        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" and ""([^""]*)"" with values ""([^""]*)"" and ""([^""]*)"" and Execute Crud Get By Tobt request and Set DTO Objects for Crud Departure Flight API")]
        public void ThenGenerateNewGetUsingAndWithValuesAndAndExecuteCrudGetByTobtRequestAndSetDTOObjectsForCrudDepartureFlightAPI(string apiReqName, string firstColName, string secondColName, string firstColValue, string secondColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByTwoStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL, apiReqName, firstColName, firstColValue, secondColName, secondColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get By OperationalDate request and Set DTO Objects for Crud Departure Flight API")]
        public void ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetByOperationalDateRequestAndSetDTOObjectsForCrudDepartureFlightAPI(string apiReqName, string firstColName, string firstColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL, apiReqName, firstColName, firstColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Execute Crud Departure Flight API And Set DTO Objects for Crud API Departure Flight")]
        public void ThenExecuteCrudDepartureFlightAPIAndSetDTOObjectsForCrudAPIDepartureFlight()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Execute Crud Departure Flight API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudDepartureFlightAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_DepartureFlight_MessageVerification");
            }
        }
        [Then(@"Generate new Get Departure Flight by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the departure api should return error in the response body")]
        public void ThenGenerateNewGetDepartureFlightByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheDepartureApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.DepartureFlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_DepartureFlightType_MessageVerification");
            }
        }
        [Then(@"Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight")]
        public IRestResponse ThenGenerateNewDeleteDepartureFlightAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIDepartureFlight()
        {
            var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.DepartureFlightAPIURL, "Id", "Failed_To_Execute_Get_By_API");
            IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Departure_FlightType");
            return restResponse;
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Departure Flight")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudDepartureFlight()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Generate new Delete Departure Flight API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteDepartureFlightAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteDepartureFlightAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIDepartureFlight();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for Departure flight Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_DepartureFlight");
            }
        }

        [Then(@"Execute Crud Post Departure Flight API with only sending Icao value in the request body and Set DTO Objects for Crud API Departure Flight")]
        public void ThenExecuteCrudPostDepartureFlightAPIWithOnlySendingIcaoValueInTheRequestBodyAndSetDTOObjectsForCrudAPIDepartureFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId, SiteId1);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Post_Departure_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_DepartureFlightConfiguration");
            }
        }

        [Then(@"Execute Crud Post Departure Flight API and Set DTO Objects for Crud API Departure Flight")]
        public void ThenExecuteCrudPostDepartureFlightAPIAndSetDTOObjectsForCrudAPIDepartureFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId, SiteId1);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Post_Departure_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_DepartureFlightConfiguration");
            }
        }
        public DepartureDto PostRequestMethodBody(string Icao, int SiteId1, int SiteId)
        {
            try
            {
                var postRequestBody = new DepartureDto()
                {
                    Id = 0,
                    SiteId = SiteId1,
                    Carrier = "DL",
                    FlightNumber = "AI101",
                    Destination = "DAY",
                    OperationDate = DateTime.Parse("2022-05-06"),
                    AircraftRegistrationId = "null",
                    CallSign = "AirIndia",
                    ArcId = "null",
                    Runway = "06R",
                    Stand = "A22",
                    DeicingRequest = (DeicingRequest)1,
                    ActualTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTimeRequested = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ActualOffBlocktime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedDeicingDurationSeconds = 2,
                    EstimatedTaxiOutTimeSeconds = 4,
                    Sid = "null",
                    TaxiRoute = "null",
                    DeicingLane = "null",
                    MinManualSeparationSeconds = 2,
                    ControlledDepartureTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ControlledDepartureWindowBegin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ControlledDepartureWindowEnd = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AirlinePriority = 0,
                    AtcSlotBegin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AtcSlotEnd = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ScheduledOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ActualStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedRunwayWaitTimeSeconds = 0,
                    FlightRule = (FlightRule)1,
                    IsManualFixed = true,
                    PushbackDurationSeconds = 0,
                    StartupDurationSeconds = 0,
                    EstimatedTaxiTimeToPadSeconds = 0,
                    EstimatedPadToRunwaySeconds = 0,
                    AircraftType = new AircraftTypeDto() { Icao = Icao, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = SiteId } },
                    DepartureFieldValues = new List<DepartureValueDto> { new DepartureValueDto(){ Field = "Field0",Instance = 1,Value = "0"},
                    new DepartureValueDto(){Field = "Field1",Instance = 1,Value = "1"},
                    new DepartureValueDto(){Field = "Field2",Instance = 1,Value = "2"},
                    new DepartureValueDto(){Field = "Field3",Instance = 1,Value = "3"},
                    new DepartureValueDto(){Field = "Field4",Instance = 1,Value = "4"},
                    new DepartureValueDto(){Field = "Field5",Instance = 1,Value = "5"}}
                };
                return postRequestBody;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_DepartureFlightAPI");
                return null;
            }
        }

        [Then(@"Generate new put Departure Flight API URL using database fetched values and Execute Crud Put API Departure Flight and Set DTO Objects for Crud API Departure Flight")]
        public void ThenGenerateNewPutDepartureFlightAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIDepartureFlightAndSetDTOObjectsForCrudAPIDepartureFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Iata = sqlResponseDetails["IATA"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);

                var putRequestBody = PutRequestMethodBody(Iata, SiteId);
                dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Put_DepartureFlight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_Departure_FlightConfiguration");
            }
        }
        public DepartureDto PutRequestMethodBody(string Iata, int SiteId)
        {
            try
            {
                var putRequestBody = new DepartureDto()
                {
                    Id = Int32.Parse(_scenarioContext.Get<string>("valueSQLData")),
                    SiteId = SiteId,
                    Carrier = "AI",
                    FlightNumber = "AI102",
                    Destination = "JFK",
                    OperationDate = DateTime.Parse("2022-05-06"),
                    AircraftRegistrationId = "null",
                    CallSign = "AirIndia12",
                    ArcId = "null",
                    Runway = "06R",
                    Stand = "A22",
                    DeicingRequest = (DeicingRequest)2,
                    ActualTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTimeRequested = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ActualOffBlocktime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedDeicingDurationSeconds = 2,
                    EstimatedTaxiOutTimeSeconds = 4,
                    Sid = "null",
                    TaxiRoute = "null",
                    DeicingLane = "null",
                    MinManualSeparationSeconds = 2,
                    ControlledDepartureTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ControlledDepartureWindowBegin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ControlledDepartureWindowEnd = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AirlinePriority = 0,
                    AtcSlotBegin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AtcSlotEnd = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ScheduledOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ActualStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedRunwayWaitTimeSeconds = 0,
                    FlightRule = (FlightRule)1,
                    IsManualFixed = true,
                    PushbackDurationSeconds = 0,
                    StartupDurationSeconds = 0,
                    EstimatedTaxiTimeToPadSeconds = 0,
                    EstimatedPadToRunwaySeconds = 0,
                    AircraftType = new AircraftTypeDto() { Iata = Iata, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = SiteId } },
                    DepartureFieldValues = new List<DepartureValueDto> { new DepartureValueDto(){ Field = "Field         0",Instance = 1,Value = "0"},
                    new DepartureValueDto(){Field = "Field         1",Instance = 1,Value = "1"},
                    new DepartureValueDto(){Field = "Field         2",Instance = 1,Value = "2"},
                    new DepartureValueDto(){Field = "Field         3",Instance = 1,Value = "3"},
                    new DepartureValueDto(){Field = "Field         4",Instance = 1,Value = "4"}}
                };
                return putRequestBody;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_Departure_FlightAPI");
                return null;
            }
        }
        [Then(@"Execute Crud Post ""([^""]*)"" API and Set DTO Objects for Crud API Add Departure Flight")]
        public void ThenExecuteCrudPostAPIAndSetDTOObjectsForCrudAPIAddDepartureFlight(string inputURL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Icao = sqlResponseDetails["ICAO"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string IcaoNewValue = "LR2T" + (_scenarioContext.Get<string>("valueSQLData"));
                string appendedURL = APITests.apiConfigDTO.DepartureFlightAPIURL + "/" + inputURL;

                List<DepartureDto> departureFlights = new();
                departureFlights.Add(new DepartureDto()
                {
                    Id = 0,
                    SiteId = SiteId,
                    Carrier = "DL",
                    FlightNumber = "DL111",
                    Destination = "DAY",
                    OperationDate = DateTime.Parse("2022-05-06"),
                    AircraftRegistrationId = "null",
                    CallSign = "AirIndia11",
                    ArcId = "null",
                    Runway = "06R",
                    Stand = "A22",
                    DeicingRequest = (DeicingRequest)1,
                    ActualTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTimeRequested = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ActualOffBlocktime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedDeicingDurationSeconds = 2,
                    EstimatedTaxiOutTimeSeconds = 4,
                    Sid = "null",
                    TaxiRoute = "null",
                    DeicingLane = "null",
                    MinManualSeparationSeconds = 2,
                    ControlledDepartureTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ControlledDepartureWindowBegin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ControlledDepartureWindowEnd = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AirlinePriority = 0,
                    AtcSlotBegin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AtcSlotEnd = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ScheduledOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ActualStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedRunwayWaitTimeSeconds = 0,
                    FlightRule = (FlightRule)1,
                    IsManualFixed = true,
                    PushbackDurationSeconds = 0,
                    StartupDurationSeconds = 0,
                    EstimatedTaxiTimeToPadSeconds = 0,
                    EstimatedPadToRunwaySeconds = 0,
                    AircraftType = new AircraftTypeDto() { Icao = Icao, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = 1 } },
                    DepartureFieldValues = new List<DepartureValueDto> { new DepartureValueDto(){ Field = "Field0",Instance = 1,Value = "0"},
                    new DepartureValueDto(){Field = "Field1",Instance = 1,Value = "1"},
                    new DepartureValueDto(){Field = "Field2",Instance = 1,Value = "2"},
                    new DepartureValueDto(){Field = "Field3",Instance = 1,Value = "3"},
                    new DepartureValueDto(){Field = "Field4",Instance = 1,Value = "4"},
                    new DepartureValueDto(){Field = "Field5",Instance = 1,Value = "5"}}
                });
                departureFlights.Add(new DepartureDto()
                {
                    Id = 0,
                    SiteId = SiteId,
                    Carrier = "DL",
                    FlightNumber = "DL222",
                    Destination = "DAY",
                    OperationDate = DateTime.Parse("2022-05-06"),
                    AircraftRegistrationId = "null",
                    CallSign = "AirIndia22",
                    ArcId = "null",
                    Runway = "06R",
                    Stand = "A22",
                    DeicingRequest = (DeicingRequest)2,
                    ActualTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetTakeOffTimeRequested = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ActualOffBlocktime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedDeicingDurationSeconds = 2,
                    EstimatedTaxiOutTimeSeconds = 4,
                    Sid = "null",
                    TaxiRoute = "null",
                    DeicingLane = "null",
                    MinManualSeparationSeconds = 2,
                    ControlledDepartureTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ControlledDepartureWindowBegin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ControlledDepartureWindowEnd = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AirlinePriority = 0,
                    AtcSlotBegin = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AtcSlotEnd = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ScheduledOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    TargetStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    ActualStartUpApprovalTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedRunwayWaitTimeSeconds = 0,
                    FlightRule = (FlightRule)1,
                    IsManualFixed = true,
                    PushbackDurationSeconds = 0,
                    StartupDurationSeconds = 0,
                    EstimatedTaxiTimeToPadSeconds = 0,
                    EstimatedPadToRunwaySeconds = 0,
                    AircraftType = new AircraftTypeDto() { Icao = IcaoNewValue, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = 1 } },
                    DepartureFieldValues = new List<DepartureValueDto> { new DepartureValueDto(){ Field = "Field0",Instance = 1,Value = "0"},
                    new DepartureValueDto(){Field = "Field1",Instance = 1,Value = "1"},
                    new DepartureValueDto(){Field = "Field2",Instance = 1,Value = "2"},
                    new DepartureValueDto(){Field = "Field3",Instance = 1,Value = "3"},
                    new DepartureValueDto(){Field = "Field4",Instance = 1,Value = "4"},
                    new DepartureValueDto(){Field = "Field5",Instance = 1,Value = "5"}}
                });
                dtoResultList = PostAPIWithDeserializeList(departureFlights, appendedURL, "Failed_To_Update_Crud_API_Post_Departure_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Departure_FlightListConfiguration");
            }
        }
        [Then(@"Execute Crud Post Departure Flight API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostDepartureFlightAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId, SiteId1);
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Post_Departure_Flight_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<DepartureDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put Departure Flight API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutDepartureFlightAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Iata = sqlResponseDetails["IATA"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);

                var putRequestBody = PutRequestMethodBody(Iata, SiteId);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Put_Departure_Flight_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }

        [Then(@"Execute Crud Post Departure Flight API with incorrect SiteId in the child object and Set DTO Objects for Crud API Departure Flight")]
        public void ThenExecuteCrudPostDepartureFlightAPIWithIncorrectSiteIdInTheChildObjectAndSetDTOObjectsForCrudAPIDepartureFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId1, SiteId);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Post_Departure_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_DepartureFlightConfiguration");
            }
        }

        [Then(@"Execute Crud Post Departure Flight API for negative test and verify that error should be thrown via API")]
        public void ThenExecuteCrudPostDepartureFlightAPIForNegativeTestAndVerifyThatErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId, 0);
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Post_Departure_Flight_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<DepartureDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Departure_FlightConfiguration_Negative scenario");
            }
        }

        [Then(@"Execute Crud Post Departure Flight API using the same request body and verify that the response should return the recent added Departure Flight")]
        public void ThenExecuteCrudPostDepartureFlightAPIUsingTheSameRequestBodyAndVerifyThatTheResponseShouldReturnTheRecentAddedDepartureFlight()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> departureFlightDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(departureFlightDetails, dtoResultSingleRecord);
        }

        [Then(@"Execute Crud Post Departure Flight API with partial child without SiteId and Set DTO Objects for Crud API Departure Flight")]
        public void ThenExecuteCrudPostDepartureFlightAPIWithPartialChildWithoutSiteIdAndSetDTOObjectsForCrudAPIDepartureFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Icao = sqlResponseDetails["ICAO"];
                var postRequestBody = PostRequestMethodBody(Icao, 1, 0);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Post_Departure_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_DeparturePartialChild_FlightConfiguration");
            }
        }

        [Then(@"Generate new put Departure Flight API URL with only sending Iata in the request body and Execute Crud Put API Departure Flight and Set DTO Objects for Crud API Departure Flight")]
        public void ThenGenerateNewPutDepartureFlightAPIURLWithOnlySendingIataInTheRequestBodyAndExecuteCrudPutAPIDepartureFlightAndSetDTOObjectsForCrudAPIDepartureFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Iata = sqlResponseDetails["IATA"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = PutRequestMethodBody(Iata, SiteId);
                dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Put_DepartureFlight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_Arrival_FlightConfiguration");
            }
        }

        [Then(@"Generate new put Departure Flight API URL for negative test and verify that error should be thrown via API")]
        public void ThenGenerateNewPutDepartureFlightAPIURLForNegativeTestAndVerifyThatErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Iata = sqlResponseDetails["IATA"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);

                var putRequestBody = PutRequestMethodBody(Iata, SiteId);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.DepartureFlightAPIURL, "Failed_To_Update_Crud_API_Put_Departure_Flight_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_NegativeTest");
            }
        }

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get By Unmatched flight request and Set DTO Objects for Crud Departure Flight API")]
        public void ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetByUnmatchedFlightRequestAndSetDTOObjectsForCrudDepartureFlightAPI(string apiReqName, string firstColName, string firstColValue)
        {
            ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetByOperationalDateRequestAndSetDTOObjectsForCrudDepartureFlightAPI(apiReqName, firstColName, firstColValue);
        }

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get All Departure Unmatched Flights request and Set DTO Objects for Crud Unmatched FlightPlan API")]
        public void ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetAllDepartureUnmatchedFlightsRequestAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string apiReqName, string firstColName, string firstColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, firstColName, firstColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
    }
}












