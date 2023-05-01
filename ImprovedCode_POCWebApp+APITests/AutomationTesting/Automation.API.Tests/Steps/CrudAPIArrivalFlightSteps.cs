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
    public class CrudAPIArrivalFlightSteps : CrudAPIBaseMethods<ArrivalDto>
    {
        public CrudAPIArrivalFlightSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }

        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is ArrivalDto arrivalFlightDto)
                {
                    Assert.AreEqual(arrivalFlightDto.Id, Int32.Parse(compareDictionary["Id"]), "ArrivalFlightId did not match:" + arrivalFlightDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(arrivalFlightDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + arrivalFlightDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(arrivalFlightDto.Carrier, compareDictionary["Carrier"], "Carrier values do not match:" + arrivalFlightDto.Carrier + " != " + compareDictionary["Carrier"]);
                    Assert.AreEqual(arrivalFlightDto.FlightNumber, compareDictionary["FlightNumber"], "FlightNumber do not match:" + arrivalFlightDto.FlightNumber + " != " + compareDictionary["FlightNumber"]);
                    Assert.AreEqual(arrivalFlightDto.Origin, compareDictionary["Origin"], "Origin values do not match:" + arrivalFlightDto.Origin + " != " + compareDictionary["Origin"]);
                    Assert.AreEqual(arrivalFlightDto.Destination, compareDictionary["Destination"], "Destination values do not match:" + arrivalFlightDto.Destination + " != " + compareDictionary["Destination"]);
                    Assert.AreEqual(arrivalFlightDto.OperationDate, ParseDateTime(compareDictionary["OperationDate"]), "OperationDate do not match:" + arrivalFlightDto.OperationDate + " != " + compareDictionary["OperationDate"]);
                    Assert.AreEqual(arrivalFlightDto.AircraftRegistrationId, compareDictionary["AircraftRegistrationId"], "AircraftRegistrationId values do not match:" + arrivalFlightDto.AircraftRegistrationId + " != " + compareDictionary["AircraftRegistrationId"]);
                    Assert.AreEqual(arrivalFlightDto.CallSign, compareDictionary["CallSign"], "CallSign do not match:" + arrivalFlightDto.CallSign + " != " + compareDictionary["CallSign"]);
                    Assert.AreEqual(arrivalFlightDto.ArcId, compareDictionary["ArcId"], "ArcId values do not match:" + arrivalFlightDto.ArcId + " != " + compareDictionary["ArcId"]);
                    Assert.AreEqual(arrivalFlightDto.Runway, compareDictionary["Runway"], "Runway do not match:" + arrivalFlightDto.Runway + " != " + compareDictionary["Runway"]);
                    if (arrivalFlightDto.AircraftType != null)
                    {
                        Assert.AreEqual(arrivalFlightDto.AircraftType.Id.ToString(), compareDictionary["AircraftType"], "AircraftType do not match:" + arrivalFlightDto.AircraftType.Id + " != " + compareDictionary["AircraftType"]);
                    }
                    else
                    {
                        Assert.AreEqual(null, compareDictionary["AircraftType"], "AircraftType do not match:" + arrivalFlightDto.AircraftType + " != " + compareDictionary["AircraftType"]);
                    }
                    Assert.AreEqual(arrivalFlightDto.Stand, compareDictionary["Stand"], "Stand values do not match:" + arrivalFlightDto.Stand + " != " + compareDictionary["Stand"]);
                    Assert.AreEqual(arrivalFlightDto.EstimatedTaxiInTimeSeconds, Int32.Parse(compareDictionary["EstimatedTaxiInTimeSeconds"]), "EstimatedTaxiInTimeSeconds do not match:" + arrivalFlightDto.EstimatedTaxiInTimeSeconds + " != " + compareDictionary["EstimatedTaxiInTimeSeconds"]);
                    Assert.AreEqual(arrivalFlightDto.EstimatedOffBlockTime, ParseDateTime(compareDictionary["EstimatedOffBlockTime"]), "EstimatedOffBlockTime do not match:" + arrivalFlightDto.EstimatedOffBlockTime + " != " + compareDictionary["EstimatedOffBlockTime"]);
                    Assert.AreEqual(arrivalFlightDto.FlightRule.ToString(), compareDictionary["FlightRule"], "FlightRule values do not match:" + arrivalFlightDto.FlightRule.ToString() + " != " + compareDictionary["FlightRule"]);
                    Assert.AreEqual(arrivalFlightDto.FlightPlanId, compareDictionary["FlightPlanId"], "FlightPlanId do not match:" + arrivalFlightDto.FlightPlanId + " != " + compareDictionary["FlightPlanId"]);
                    if (compareDictionary["Field"] != null)
                    {
                        Assert.AreEqual(arrivalFlightDto.ArrivalFieldValues.First(dv => dv.Field == compareDictionary["Field"] && dv.Instance == int.Parse(compareDictionary["Instance"])).Value, compareDictionary["Value"], "Values in Arrival field tables match with the Dto");
                    }
                    else
                        Assert.AreEqual(arrivalFlightDto.ArrivalFieldValues.Count, 0, "There are no values against the record in Arrival Value table");
                }
                else
                {
                    Assert.Fail("Return values which are not Arrival flight type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Execute Crud Arrival Flight API And Set DTO Objects for Crud API Arrival Flight for site claim ""([^""]*)""")]
        public void ThenExecuteCrudArrivalFlightAPIAndSetDTOObjectsForCrudAPIArrivalFlightForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud Arrival Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudArrivalFlightAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
            List<ArrivalValueDto> arrivalValueDTO = new();
            foreach (ArrivalDto dto in dtoResultList)
            {
                arrivalValueDTO.AddRange(dto.ArrivalFieldValues);
            }
        }
        public override void ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI()
        {
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            if (dtoResultList.Any() && resultData.Any())
            {
                int lastArrivalId = dtoResultList.First().Id;
                int valueCount = 0;
                for (int i = 0; i < resultData.Count; i++)
                {
                    valueCount++;
                    Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(i);
                    APIDBComparison(APIDetails, dtoResultList.First(d => d.Id == int.Parse(APIDetails["Id"])));
                    if (lastArrivalId != int.Parse(APIDetails["Id"]))
                    {
                        if (((Dictionary<string, string>)resultData[i - 1])["Field"] != null)
                        {
                            Assert.AreEqual(dtoResultList.First(d => d.Id == lastArrivalId).ArrivalFieldValues.Count, valueCount - 1);
                        }
                        lastArrivalId = int.Parse(APIDetails["Id"]);
                        valueCount = 1;
                    }
                }
                if (((Dictionary<string, string>)resultData[resultData.Count - 1])["Field"] != null)
                {
                    Assert.AreEqual(dtoResultList.First(d => d.Id == lastArrivalId).ArrivalFieldValues.Count, valueCount);
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
        [Then(@"Generate new Get Arrival flight by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Arrival Flight")]
        public void ThenGenerateNewGetArrivalFlightByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIArrivalFlight(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.ArrivalFlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<ArrivalDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_ArrivalFlightType");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Arrival Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudArrivalFlightAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> arrivalFlightDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(arrivalFlightDetails, dtoResultSingleRecord);
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Arrival Flight")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudArrivalFlight(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.ArrivalFlightAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new Get Arrival flight by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Arrival Flight")]
        public void ThenGenerateNewGetArrivalFlightByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIArrivalFlight(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.ArrivalFlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with arrivalbyId: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_ArrivalFlight");
            }
        }
        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get By OperationalDate request and Set DTO Objects for Crud Arrival Flight API")]
        public void ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetByOperationalDateRequestAndSetDTOObjectsForCrudArrivalFlightAPI(string apiReqName, string firstColName, string firstColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL, apiReqName, firstColName, firstColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Execute Crud Arrival Flight API And Set DTO Objects for Crud API Arrival Flight")]
        public void ThenExecuteCrudArrivalFlightAPIAndSetDTOObjectsForCrudAPIArrivalFlight()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Execute Crud Arrival Flight API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudArrivalFlightAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_ArrivalFlight_MessageVerification");
            }
        }
        [Then(@"Generate new Get Arrival Flight by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the Arrival api should return error in the response body")]
        public void ThenGenerateNewGetArrivalFlightByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheArrivalApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.ArrivalFlightAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_ArrivalFlightType_MessageVerification");
            }
        }
        [Then(@"Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight")]
        public IRestResponse ThenGenerateNewDeleteArrivalFlightAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIArrivalFlight()
        {
            var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.ArrivalFlightAPIURL, "Id", "Failed_To_Execute_Get_By_API");
            IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Arrival_FlightType");
            return restResponse;
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Arrival Flight")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudArrivalFlight()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Generate new Delete Arrival Flight API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteArrivalFlightAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteArrivalFlightAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIArrivalFlight();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for Arrival flight Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_ArrivalFlight");
            }
        }
        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get By Unmatched flight request and Set DTO Objects for Crud Arrival Flight API")]
        public void ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetByUnmatchedFlightRequestAndSetDTOObjectsForCrudArrivalFlightAPI(string apiReqName, string firstColName, string firstColValue)
        {
            ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetByOperationalDateRequestAndSetDTOObjectsForCrudArrivalFlightAPI(apiReqName, firstColName, firstColValue);
        }
        [Then(@"Execute Crud Post Arrival Flight API and Set DTO Objects for Crud API Arrival Flight")]
        public void ThenExecuteCrudPostArrivalFlightAPIAndSetDTOObjectsForCrudAPIArrivalFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Icao = sqlResponseDetails["ICAO"];
                var postRequestBody = PostRequestMethodBody(Icao, SiteId, SiteId1);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Post_Arrival_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_ArrivalFlightConfiguration");
            }
        }

        [Then(@"Execute Crud Post Arrival Flight API with incorrect SiteId in the child object and Set DTO Objects for Crud API Arrival Flight")]
        public void ThenExecuteCrudPostArrivalFlightAPIWithIncorrectSiteIdInTheChildObjectAndSetDTOObjectsForCrudAPIArrivalFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId1, SiteId);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Post_Arrival_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_ArrivalFlightConfiguration");
            }
        }

        [Then(@"Execute Crud Post Arrival Flight API with only sending Icao value in the request body and Set DTO Objects for Crud API Arrival Flight")]
        public void ThenExecuteCrudPostArrivalFlightAPIWithOnlySendingIcaoValueInTheRequestBodyAndSetDTOObjectsForCrudAPIArrivalFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId, SiteId1);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Post_Arrival_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_ArrivalFlightConfiguration");
            }
        }

        public ArrivalDto PostRequestMethodBody(string Icao, int SiteId1, int SiteId)
        {
            try
            {
                var postRequestBody = new ArrivalDto()
                {
                    Id = 0,
                    SiteId = SiteId1,
                    Carrier = "DL",
                    FlightNumber = "AI101",
                    Origin = "DAY",
                    OperationDate = DateTime.Parse("2022-05-06"),
                    AircraftRegistrationId = "null",
                    CallSign = "AirIndia",
                    ArcId = "null",
                    Runway = "06R",
                    Stand = "A22",
                    EstimatedTaxiInTimeSeconds = 4,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AircraftType = new AircraftTypeDto() { Icao = Icao, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = SiteId } },
                    ArrivalFieldValues = new List<ArrivalValueDto> { new ArrivalValueDto(){ Field = "Field0",Instance = 1,Value = "0"},
                    new ArrivalValueDto(){Field = "Field1",Instance = 1,Value = "1"},
                    new ArrivalValueDto(){Field = "Field2",Instance = 1,Value = "2"},
                    new ArrivalValueDto(){Field = "Field3",Instance = 1,Value = "3"},
                    new ArrivalValueDto(){Field = "Field4",Instance = 1,Value = "4"},
                    new ArrivalValueDto(){Field = "Field5",Instance = 1,Value = "5"}}
                };
                return postRequestBody;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_ArrivalFlightAPI");
                return null;
            }
        }

        [Then(@"Execute Crud Post Arrival Flight API using the same request body and verify that the response should return the recent added Arrival Flight")]
        public void ThenExecuteCrudPostArrivalFlightAPIUsingTheSameRequestBodyAndVerifyThatTheResponseShouldReturnTheRecentAddedArrivalFlight()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> arrivalFlightDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(arrivalFlightDetails, dtoResultSingleRecord);
        }

        [Then(@"Generate new put Arrival Flight API URL using database fetched values and Execute Crud Put API Arrival Flight and Set DTO Objects for Crud API Arrival Flight")]
        public void ThenGenerateNewPutArrivalFlightAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIArrivalFlightAndSetDTOObjectsForCrudAPIArrivalFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Iata = sqlResponseDetails["IATA"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = PutRequestMethodBody(Iata, SiteId, SiteId1);
                dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Put_ArrivalFlight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_Arrival_FlightConfiguration");
            }
        }

        [Then(@"Generate new put Arrival Flight API URL with only sending Iata in the request body and Execute Crud Put API Arrival Flight and Set DTO Objects for Crud API Arrival Flight")]
        public void ThenGenerateNewPutArrivalFlightAPIURLWithOnlySendingIataInTheRequestBodyAndExecuteCrudPutAPIArrivalFlightAndSetDTOObjectsForCrudAPIArrivalFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Iata = sqlResponseDetails["IATA"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = PutRequestMethodBody(Iata, SiteId, SiteId1);
                dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Put_ArrivalFlight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_Arrival_FlightConfiguration");
            }
        }

        public ArrivalDto PutRequestMethodBody(string Iata, int SiteId, int SiteId1)
        {
            try
            {
                var putRequestBody = new ArrivalDto()
                {
                    Id = Int32.Parse(_scenarioContext.Get<string>("valueSQLData")),
                    SiteId = SiteId,
                    Carrier = "AI",
                    FlightNumber = "AI102",
                    Origin = "JFK",
                    OperationDate = DateTime.Parse("2022-05-06"),
                    AircraftRegistrationId = "null",
                    CallSign = "AirIndia12",
                    ArcId = "null",
                    Runway = "06R",
                    Stand = "A22",
                    EstimatedTaxiInTimeSeconds = 4,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AircraftType = new AircraftTypeDto() { Iata = Iata, SiteId = SiteId1, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = SiteId1 } },
                    ArrivalFieldValues = new List<ArrivalValueDto> { new ArrivalValueDto(){ Field = "Field         0",Instance = 1,Value = "0"},
                    new ArrivalValueDto(){Field = "Field         1",Instance = 1,Value = "1"},
                    new ArrivalValueDto(){Field = "Field         2",Instance = 1,Value = "2"},
                    new ArrivalValueDto(){Field = "Field         3",Instance = 1,Value = "3"},
                    new ArrivalValueDto(){Field = "Field         4",Instance = 1,Value = "4"} }
                };
                return putRequestBody;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_Arrival_FlightAPI");
                return null;
            }
        }
        [Then(@"Execute Crud Post ""([^""]*)"" API and Set DTO Objects for Crud API Add Arrival Flight")]
        public void ThenExecuteCrudPostAPIAndSetDTOObjectsForCrudAPIAddArrivalFlight(string inputURL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Icao = sqlResponseDetails["ICAO"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string IcaoNewValue = "LR2T" + (_scenarioContext.Get<string>("valueSQLData"));
                string appendedURL = APITests.apiConfigDTO.ArrivalFlightAPIURL + "/" + inputURL;

                List<ArrivalDto> arrivalFlights = new();
                arrivalFlights.Add(new ArrivalDto()
                {
                    Id = 0,
                    SiteId = SiteId,
                    Carrier = "DL",
                    FlightNumber = "DL111",
                    Origin = "DAY",
                    OperationDate = DateTime.Parse("2022-05-06"),
                    AircraftRegistrationId = "null",
                    CallSign = "AirIndia11",
                    ArcId = "null",
                    Runway = "06R",
                    Stand = "A22",
                    EstimatedTaxiInTimeSeconds = 4,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AircraftType = new AircraftTypeDto() { Icao = Icao, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = 1 } },
                    ArrivalFieldValues = new List<ArrivalValueDto> { new ArrivalValueDto(){ Field = "Field0",Instance = 1,Value = "0"},
                    new ArrivalValueDto(){Field = "Field1",Instance = 1,Value = "1"},
                    new ArrivalValueDto(){Field = "Field2",Instance = 1,Value = "2"},
                    new ArrivalValueDto(){Field = "Field3",Instance = 1,Value = "3"},
                    new ArrivalValueDto(){Field = "Field4",Instance = 1,Value = "4"},
                    new ArrivalValueDto(){Field = "Field5",Instance = 1,Value = "5"}}
                });
                arrivalFlights.Add(new ArrivalDto()
                {
                    Id = 0,
                    SiteId = SiteId,
                    Carrier = "DL",
                    FlightNumber = "DL222",
                    Origin = "DAY",
                    OperationDate = DateTime.Parse("2022-05-06"),
                    AircraftRegistrationId = "null",
                    CallSign = "AirIndia22",
                    ArcId = "null",
                    Runway = "06R",
                    Stand = "A22",
                    EstimatedTaxiInTimeSeconds = 7,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    AircraftType = new AircraftTypeDto() { Icao = IcaoNewValue, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = 1 } },
                    ArrivalFieldValues = new List<ArrivalValueDto> { new ArrivalValueDto(){ Field = "Field0",Instance = 1,Value = "0"},
                    new ArrivalValueDto(){Field = "Field1",Instance = 1,Value = "1"},
                    new ArrivalValueDto(){Field = "Field2",Instance = 1,Value = "2"},
                    new ArrivalValueDto(){Field = "Field3",Instance = 1,Value = "3"},
                    new ArrivalValueDto(){Field = "Field4",Instance = 1,Value = "4"},
                    new ArrivalValueDto(){Field = "Field5",Instance = 1,Value = "5"}}
                });
                dtoResultList = PostAPIWithDeserializeList(arrivalFlights, appendedURL, "Failed_To_Update_Crud_API_Post_Arrival_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Arrival_FlightListConfiguration");
            }
        }
        [Then(@"Execute Crud Post Arrival Flight API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostArrivalFlightAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId, SiteId1);
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Post_Arrival_Flight_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<ArrivalDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put Arrival Flight API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutArrivalFlightAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Iata = sqlResponseDetails["IATA"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = PutRequestMethodBody(Iata, SiteId, SiteId1);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Put_Arrival_Flight_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"Execute Crud Post Arrival Flight API for negative test and verify that error should be thrown via API")]
        public void ThenExecuteCrudPostArrivalFlightAPIForNegativeTestAndVerifyThatErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Icao = sqlResponseDetails["ICAO"];

                var postRequestBody = PostRequestMethodBody(Icao, SiteId, 0);
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Post_Arrival_Flight_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<ArrivalDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_Arrival_FlightConfiguration_NegativeScenario");
            }
        }
        [Then(@"Generate new put Arrival Flight API URL for negative test and verify that error should be thrown via API")]
        public void ThenGenerateNewPutArrivalFlightAPIURLForNegativeTestAndVerifyThatErrorShouldBeThrownViaAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Iata = sqlResponseDetails["IATA"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);
                var putRequestBody = PutRequestMethodBody(Iata, SiteId, SiteId1);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Put_Arrival_Flight_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_NegativeTest");
            }
        }
        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get All Arrival Unmatched Flights request and Set DTO Objects for Crud Unmatched FlightPlan API")]
        public void ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetAllArrivalUnmatchedFlightsRequestAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string apiReqName, string firstColName, string firstColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, firstColName, firstColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Generate new Get Arrival flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response in above step and Set DTO Objects for Crud API Arrival Flight API")]
        public void ThenGenerateNewGetArrivalFlightByAndUsingWithValueFetchedFromDBResponseInAboveStepAndSetDTOObjectsForCrudAPIArrivalFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int auditHistoryIdValueDB = Int32.Parse(sqlResponseDetails["HistoryId"]);
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneIntValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL, apiReqName, colName, firstColName, auditHistoryIdValueDB);
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_ArrivalFlightType");
            }
        }

        [Then(@"Execute Crud Post Arrival Flight API with partial child without SiteId and Set DTO Objects for Crud API Arrival Flight")]
        public void ThenExecuteCrudPostArrivalFlightAPIWithPartialChildWithoutSiteIdAndSetDTOObjectsForCrudAPIArrivalFlight()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Icao = sqlResponseDetails["ICAO"];
                var postRequestBody = PostRequestMethodBody(Icao, 1, 0);
                dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.ArrivalFlightAPIURL, "Failed_To_Update_Crud_API_Post_Arrival_Flight_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_ArrivalPartialChild_FlightConfiguration");
            }
        }
    }
}











