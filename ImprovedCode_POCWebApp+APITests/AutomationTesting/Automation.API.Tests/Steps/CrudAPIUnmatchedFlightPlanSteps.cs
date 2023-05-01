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
    public class CrudAPIUnmatchedFlightPlanSteps : CrudAPIBaseMethods<FlightPlanDto>
    {
        public CrudAPIUnmatchedFlightPlanSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }

        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is FlightPlanDto unmatchedFlightPlanDto)
                {
                    Assert.AreEqual(unmatchedFlightPlanDto.Id, Int32.Parse(compareDictionary["Id"]), "UnmatchedFlightPlanId did not match:" + unmatchedFlightPlanDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + unmatchedFlightPlanDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.FlightPlanId, compareDictionary["FlightPlanId"], "FlightPlanId values do not match:" + unmatchedFlightPlanDto.FlightPlanId + " != " + compareDictionary["FlightPlanId"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.Callsign, compareDictionary["CallSign"], "CallSign do not match:" + unmatchedFlightPlanDto.Callsign + " != " + compareDictionary["CallSign"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.Carrier, compareDictionary["Carrier"], "Carrier values do not match:" + unmatchedFlightPlanDto.Carrier + " != " + compareDictionary["Carrier"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.FlightNumber, compareDictionary["FlightNumber"], "FlightNumber do not match:" + unmatchedFlightPlanDto.FlightNumber + " != " + compareDictionary["FlightNumber"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.Origin, compareDictionary["Origin"], "Origin values do not match:" + unmatchedFlightPlanDto.Origin + " != " + compareDictionary["Origin"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.Destination, compareDictionary["Destination"], "Destination values do not match:" + unmatchedFlightPlanDto.Destination + " != " + compareDictionary["Destination"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.OperationDate, ParseDateTime(compareDictionary["OperationDate"]), "OperationDate do not match:" + unmatchedFlightPlanDto.OperationDate + " != " + compareDictionary["OperationDate"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.AircraftRegistrationId, compareDictionary["AircraftRegistrationId"], "AircraftRegistrationId values do not match:" + unmatchedFlightPlanDto.AircraftRegistrationId + " != " + compareDictionary["AircraftRegistrationId"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.ArcId, compareDictionary["ArcId"], "ArcId values do not match:" + unmatchedFlightPlanDto.ArcId + " != " + compareDictionary["ArcId"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.AircraftType.Id.ToString(), compareDictionary["AircraftType"], "AircraftType do not match:" + unmatchedFlightPlanDto.AircraftType.Id + " != " + compareDictionary["AircraftType"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.EstimatedOffBlockTime, ParseDateTime(compareDictionary["EstimatedOffBlockTime"]), "EstimatedOffBlockTime do not match:" + unmatchedFlightPlanDto.EstimatedOffBlockTime + " != " + compareDictionary["EstimatedOffBlockTime"]);
                    Assert.AreEqual(unmatchedFlightPlanDto.FlightRule.ToString(), compareDictionary["FlightRule"], "FlightRule values do not match:" + unmatchedFlightPlanDto.FlightRule.ToString() + " != " + compareDictionary["FlightRule"]);
                    if (compareDictionary["Field"] != null)
                    {
                        Assert.AreEqual(unmatchedFlightPlanDto.FlightPlanFieldValues.First(dv => dv.Field == compareDictionary["Field"] && dv.Instance == int.Parse(compareDictionary["Instance"])).Value, compareDictionary["Value"], "Values in Unmatched FlightPlan field tables match with the Dto");
                    }
                    else
                        Assert.AreEqual(unmatchedFlightPlanDto.FlightPlanFieldValues.Count, 0, "There are no values against the record in Unmatched FlightPlan Value table");
                }
                else
                {
                    Assert.Fail("Return values which are not Unmatched FlightPlan type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }

        [Then(@"Execute Crud UnmatchedFlightPlan API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudUnmatchedFlightPlanAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_FlightPlan_MessageVerification");
            }
        }

        [Then(@"Execute Crud UnmatchedFlightPlan API And Set DTO Objects for Crud API FlightPlan for site claim ""([^""]*)"" for ""([^""]*)""")]
        public void ThenExecuteCrudUnmatchedFlightPlanAPIAndSetDTOObjectsForCrudAPIFlightPlanForSiteClaimFor(string siteClaim, string url)
        {
            var restOBJSetup = RestAPICommonMethods.SetURLByAppendedStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, url);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudUnmatchedFlightPlanAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
            List<FlightPlanValueDto> flightPlanValueDTO = new();
            foreach (FlightPlanDto dto in dtoResultList)
            {
                flightPlanValueDTO.AddRange(dto.FlightPlanFieldValues);
            }
        }

        [Then(@"Generate new Get Unmatched flight plan by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Unmatched FlightPlan")]
        public void ThenGenerateNewGetUnmatchedFlightPlanByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPIUnmatchedFlightPlan(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.FlightPlanAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<FlightPlanDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_UnmatchedFlightPlanType");
            }
        }

        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Unmatched Flight Plan API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudUnmatchedFlightPlanAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> unmatchedFlightPlanDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(unmatchedFlightPlanDetails, dtoResultSingleRecord);
        }

        [Then(@"Compare values from API response set to DB record set for flightplanId for Crud Unmatched Flight Plan API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForFlightplanIdForCrudUnmatchedFlightPlanAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> unmatchedFlightPlanDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(unmatchedFlightPlanDetails, dtoResultList.First(d => d.Id == int.Parse(unmatchedFlightPlanDetails["Id"])));
        }

        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL using ""([^""]*)"" ""([^""]*)"" that does not exist fetched from DB and it should return error via API for Crud Unmatched Flight Plan")]
        public void ThenValidateTheGETAPIOperationByAPIURLUsingThatDoesNotExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudUnmatchedFlightPlan(string apiReqName, string colName, string colNameSQL)
        {
            ValidateIDNotExistErrorMessage(APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, colName, colNameSQL, "Failed_To_Execute_Get_By_API", "No Content");
        }

        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL using ""([^""]*)"" ""([^""]*)"" that does not exist fetched from DB and it should return empty list via API for Crud Unmatched Flight Plan")]
        public void ThenValidateTheGETAPIOperationByAPIURLUsingThatDoesNotExistFetchedFromDBAndItShouldReturnEmptyListViaAPIForCrudUnmatchedFlightPlan(string apiReqName, string colName, string colNameAPI)
        {
            try
            {
                var dtoResult = ExecuteGetByAPIRequestWithDeserialize(apiReqName, colName, colNameAPI, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Execute_Get_By_API_Request");
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
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_FlightPlanAPI");
            }
        }

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" ""([^""]*)"" value fetched from DB and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Unmatched Flight Plan")]
        public void ThenGenerateNewGetUsingValueFetchedFromDBAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPIUnmatchedFlightPlan(string apiReqName, string ColName, string colNameSQL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                AddOrUpdateScenarioContext("idValueDB", sqlResponseDetails[colNameSQL]);
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, ColName, fetchedIDValueAPI);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with FlightPlanbyId: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_UnmatchedFlightPlan");
            }
        }

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get By OperationalDate request and Set DTO Objects for Crud Unmatched FlightPlan API")]
        public void ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetByOperationalDateRequestAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string apiReqName, string firstColName, string firstColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, firstColName, firstColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" ""([^""]*)"" value fetched from DB and Execute Crud Get By Unmatched Flights by Id request and Set DTO Objects for Crud Unmatched FlightPlan API")]
        public void ThenGenerateNewGetUsingValueFetchedFromDBAndExecuteCrudGetByUnmatchedFlightsByIdRequestAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string apiReqName, string ColName, string colNameSQL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                AddOrUpdateScenarioContext("idValueDB", sqlResponseDetails[colNameSQL]);
                string idValueDB = _scenarioContext.Get<string>("idValueDB");
                var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, ColName, idValueDB);
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<FlightPlanDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_UnmatchedFlightPlanType");
            }
        }

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" ""([^""]*)"" value fetched from DB and Execute Crud Get By Unmatched Flights by FlightPlanId request and Set DTO Objects for Crud Unmatched FlightPlan API")]
        public void ThenGenerateNewGetUsingValueFetchedFromDBAndExecuteCrudGetByUnmatchedFlightsByFlightPlanIdRequestAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string apiReqName, string ColName, string colNameSQL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                AddOrUpdateScenarioContext("idValueDB", sqlResponseDetails[colNameSQL]);
                string idValueDB = _scenarioContext.Get<string>("idValueDB");
                var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, ColName, idValueDB);
                var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_UnmatchedFlightPlanType");
            }
        }


        [Then(@"Generate new Delete ""([^""]*)"" using ""([^""]*)"" with value ""([^""]*)"" and ""([^""]*)"" with value ""([^""]*)"" and Execute Crud Delete flight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API")]
        public IRestResponse ThenGenerateNewDeleteUsingWithValueAndWithValueAndExecuteCrudDeleteFlightPlanIdRequestAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string appendAPIURL, string firstColName, string firstColValue, string secondColName, string secondColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByTwoStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, appendAPIURL, firstColName, firstColValue, secondColName, secondColValue);
            IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Unmatched_FlightPlan");
            return restResponse;
        }

        [Then(@"Execute Crud UnmatchedFlightPlan API And Set DTO Objects for Crud API UnmatchedFlightPlan for ""([^""]*)""")]
        public void ThenExecuteCrudUnmatchedFlightPlanAPIAndSetDTOObjectsForCrudAPIUnmatchedFlightPlanFor(string url)
        {
            var restOBJSetup = RestAPICommonMethods.SetURLByAppendedStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, url);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Generate new Get Unmatched FlightPlan using ""([^""]*)"" ""([^""]*)"" for different Site id ""([^""]*)"" than what is being used in the token and validate that the unmatched flight plan api should return error in the response body for ""([^""]*)""")]
        public void ThenGenerateNewGetUnmatchedFlightPlanUsingForDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheUnmatchedFlightPlanApiShouldReturnErrorInTheResponseBodyFor(string colName, string colNameSQL, string siteClaim, string apiAppendURL)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                AddOrUpdateScenarioContext("idValueDB", sqlResponseDetails[colNameSQL]);
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiAppendURL, colName, fetchedIDValueAPI);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_UnmatchedFlightPlan_MessageVerification");
            }
        }

        [Then(@"Generate new Delete Unmatched flight plan API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Unmatched Flight Plan")]
        public IRestResponse ThenGenerateNewDeleteUnmatchedFlightPlanAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIUnmatchedFlightPlan()
        {
            var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.FlightPlanAPIURL, "Id", "Failed_To_Execute_Get_By_API");
            IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_Unmatched_FlightPlan");
            return restResponse;
        }

        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Unmatched Flight Plan")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudUnmatchedFlightPlan()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }

        [Then(@"Generate new Delete Unmatched Flight Plan API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteUnmatchedFlightPlanAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteUnmatchedFlightPlanAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPIUnmatchedFlightPlan();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for UnmatchedFlightPlan Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_UnmatchedFlightPlan");
            }
        }
        public override void ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI()
        {
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            if (dtoResultList.Any() && resultData.Any())
            {
                int lastFlightPlanId = dtoResultList.First().Id;
                int valueCount = 0;
                for (int i = 0; i < resultData.Count; i++)
                {
                    valueCount++;
                    Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(i);
                    APIDBComparison(APIDetails, dtoResultList.First(d => d.Id == int.Parse(APIDetails["Id"])));
                    if (lastFlightPlanId != int.Parse(APIDetails["Id"]))
                    {
                        if (((Dictionary<string, string>)resultData[i - 1])["Field"] != null)
                        {
                            Assert.AreEqual(dtoResultList.First(d => d.Id == lastFlightPlanId).FlightPlanFieldValues.Count, valueCount - 1);
                        }
                        lastFlightPlanId = int.Parse(APIDetails["Id"]);
                        valueCount = 1;
                    }
                }
                if (((Dictionary<string, string>)resultData[resultData.Count - 1])["Field"] != null)
                {
                    Assert.AreEqual(dtoResultList.First(d => d.Id == lastFlightPlanId).FlightPlanFieldValues.Count, valueCount);
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

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get By All Flight Plans request and Set DTO Objects for Crud Unmatched FlightPlan API")]
        public void ThenGenerateNewGetUsingWithValuesAndExecuteCrudGetByAllFlightPlansRequestAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string apiReqName, string firstColName, string firstColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByOneStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, firstColName, firstColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Generate new get ""([^""]*)"" using ""([^""]*)"" with value ""([^""]*)"" and ""([^""]*)"" with values ""([^""]*)"" and Execute Crud Get By All Flight Plans by FLight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API")]
        public void ThenGenerateNewGetUsingWithValueAndWithValuesAndExecuteCrudGetByAllFlightPlansByFLightPlanIdRequestAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string appendAPIURL, string firstColName, string firstColValue, string secondColName, string secondColValue)
        {
            var restOBJSetup = restAPIUtil.SetURLByTwoStringColumnValues(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, appendAPIURL, firstColName, firstColValue, secondColName, secondColValue);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Validate that the fetched ""([^""]*)"" value from DB is NULL post deleting the flightplan Id using delete API")]
        public void ThenValidateThatTheFetchedValueFromDBIsNULLPostDeletingTheFlightplanIdUsingDeleteAPI(string keyColName)
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> sqlQueryDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
            AddOrUpdateScenarioContext("outputSQLResponse", sqlQueryDetails[keyColName]);
            string outputSQLResponse = (_scenarioContext.Get<string>("outputSQLResponse"));
            Console.WriteLine(outputSQLResponse);
            if (outputSQLResponse == null)
            {
                Console.WriteLine("Flightplan ID value matches in DB post flightplan delete api");
            }
        }

        [Then(@"Fetch value for site ""([^""]*)"" and use it in the below sql query")]
        public void ThenFetchValueForSiteAndUseItInTheBelowSqlQuery(string siteId)
        {
            AddOrUpdateScenarioContext("valueSiteId", siteId);
            string valueSiteId = (_scenarioContext.Get<string>("valueSiteId"));
        }

        [Then(@"Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan")]
        public void ThenExecuteCrudPostUnmatchedFlightPlanAPIAndSetDTOObjectsForCrudAPIUnmatchedFlightPlan()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                List<FlightPlanDto> flightPlanList = PostRequestMethodBody(SiteId1, SiteId, "AT08998877", DateTime.Parse("2022-05-06"), 0, "AirIndia");
                dtoResultList = PostAPIWithDeserializeList(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Post_Unmatched_FlightPlan_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_UnmatchedFlightConfiguration");
            }
        }

        public List<FlightPlanDto> PostRequestMethodBody(int SiteId, int SiteId1, string flightPlanId, DateTime OpDate, int FlightId, string Callsign)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Icao = sqlResponseDetails["ICAO"];
                List<FlightPlanDto> flightPlan = new();
                var postRequestBody = new FlightPlanDto()
                {
                    Id = FlightId,
                    SiteId = SiteId,
                    Carrier = "DL",
                    FlightNumber = "AI101",
                    Origin = "JFK",
                    Destination = "CVG",
                    OperationDate = OpDate,
                    AircraftRegistrationId = "null",
                    Callsign = Callsign,
                    ArcId = "null",
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedOffBlockDate = DateTime.Parse("2022-05-06T14:15:06"),
                    FlightRule = (FlightRule)1,
                    FlightPlanId = flightPlanId,
                    AircraftType = new AircraftTypeDto() { Icao = Icao, SiteId = SiteId1, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = SiteId1 } },
                    FlightPlanFieldValues = new List<FlightPlanValueDto> { new FlightPlanValueDto(){ Field = "Field0",Instance = 1,Value = "0"},
                    new FlightPlanValueDto(){Field = "Field1",Instance = 1,Value = "1"},
                    new FlightPlanValueDto(){Field = "Field2",Instance = 1,Value = "2"},
                    new FlightPlanValueDto(){Field = "Field3",Instance = 1,Value = "3"},
                    new FlightPlanValueDto(){Field = "Field4",Instance = 1,Value = "4"},
                    new FlightPlanValueDto(){Field = "Field5",Instance = 1,Value = "5"}}
                };
                flightPlan.Add(postRequestBody);
                return flightPlan;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_UnmatchedFlightPlanAPI");
                return null;
            }
        }

        [Then(@"Execute Crud Post Unmatched Flight API using the same request body and verify that the response should return the recent added Unmatched Flight Plan")]
        public void ThenExecuteCrudPostUnmatchedFlightAPIUsingTheSameRequestBodyAndVerifyThatTheResponseShouldReturnTheRecentAddedUnmatchedFlightPlan()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(APIDetails, dtoResultList.First(d => d.Id == int.Parse(APIDetails["Id"])));
        }

        [Then(@"Generate new put Unmatched Flight Plan API URL using database fetched values and Execute Crud Put API Unmatched Flight Plan and Set DTO Objects for Crud API Unmatched Flight Plan")]
        public void ThenGenerateNewPutUnmatchedFlightPlanAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPIUnmatchedFlightPlanAndSetDTOObjectsForCrudAPIUnmatchedFlightPlan()
        {
            try
            {
                List<FlightPlanDto> flightPlanList = PutRequestMethodBody(DateTime.Parse("2023-03-07"), "AT08949799");
                dtoResultList = PutAPIWithDeserializeList(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Put_UnmatchedFlightPlan_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_Unmatched_FlightPlanConfiguration");
            }
        }

        [Then(@"Generate new put Unmatched Flight API URL and Set DTO Objects for Crud API Unmatched Flight Plan")]
        public void ThenGenerateNewPutUnmatchedFlightAPIURLAndSetDTOObjectsForCrudAPIUnmatchedFlightPlan()
        {
            try
            {
                List<FlightPlanDto> flightPlanList = PutRequestMethodBody(DateTime.Parse("2022-05-06"), "AT08998877");
                dtoResultList = PutAPIWithDeserializeList(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Put_UnmatchedFlightPlan_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_Unmatched_FlightPlanConfiguration");
            }
        }
        public List<FlightPlanDto> PutRequestMethodBody(DateTime OperDate, string flightPlanId)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                string Icao = sqlResponseDetails["ICAO"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                List<FlightPlanDto> flightPlan = new();
                var putRequestBody = new FlightPlanDto()
                {
                    SiteId = SiteId,
                    Carrier = "DL",
                    FlightNumber = "AI101",
                    Origin = "JFK",
                    Destination = "CVG",
                    OperationDate = OperDate,
                    AircraftRegistrationId = "null",
                    Callsign = "AirIndia12",
                    ArcId = "12",
                    FlightPlanId = flightPlanId,
                    FlightRule = (FlightRule)1,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedOffBlockDate = DateTime.Parse("2022-05-06T14:15:06"),
                    AircraftType = new AircraftTypeDto() { Id = IdValueDB, Icao = Icao, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = SiteId } },
                    FlightPlanFieldValues = new List<FlightPlanValueDto> { new FlightPlanValueDto(){ Field = "Field         0",Instance = 1,Value = "0"},
                    new FlightPlanValueDto(){Field = "Field         1",Instance = 1,Value = "1"},
                    new FlightPlanValueDto(){Field = "Field         2",Instance = 1,Value = "2"},
                    new FlightPlanValueDto(){Field = "Field         3",Instance = 1,Value = "3"},
                    new FlightPlanValueDto(){Field = "Field         4",Instance = 1,Value = "4"}}
                };
                flightPlan.Add(putRequestBody);
                return flightPlan;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_Unmatched_FlightAPI");
                return null;
            }
        }
        [Then(@"Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudUnmatchedFlightPlanAPIPostRequest()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(APIDetails, dtoResultList.First(d => d.Id == int.Parse(APIDetails["Id"])));
        }
        [Then(@"Generate new patch Unmatched Flight Plan API URL using fetched values and Execute Crud Patch Unmatched Flight Plan API and validate that the record in Arrival table is updated")]
        public void ThenGenerateNewPatchUnmatchedFlightPlanAPIURLUsingFetchedValuesAndExecuteCrudPatchUnmatchedFlightPlanAPIAndValidateThatTheRecordInArrivalTableIsUpdated()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Engine = sqlResponseDetails["Engine"];
                List<FlightPlanDto> flightPlanList = PatchRequestMethodBody("AT089497177", DateTime.Parse("2022-09-06"));
                dtoResultList = PutAPIWithDeserializeList(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Patch_UnmatchedFlightPlan_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Patch_Unmatched_FlightPlanConfiguration");
            }
        }
        [Then(@"Generate new patch Unmatched Flight Plan API URL using fetched values and Execute Crud Patch Unmatched Flight Plan API and validate that the record in Arrival table is updated and the same in unmatched table is deleted")]
        public void ThenGenerateNewPatchUnmatchedFlightPlanAPIURLUsingFetchedValuesAndExecuteCrudPatchUnmatchedFlightPlanAPIAndValidateThatTheRecordInArrivalTableIsUpdatedAndTheSameInUnmatchedTableIsDeleted()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string Engine = sqlResponseDetails["Engine"];
                List<FlightPlanDto> flightPlanList = PatchRequestMethodBody("AT08998877", DateTime.Parse("2022-05-06"));
                dtoResultList = PutAPIWithDeserializeList(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Patch_UnmatchedFlightPlan_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Patch_Unmatched_FlightPlanConfiguration");
            }
        }

        [Then(@"Generate new patch Unmatched Flight Plan API URL using fetched values and Execute Crud Patch Unmatched Flight Plan API and validate that the record in Departure table is updated and the same in unmatched table is deleted")]
        public void ThenGenerateNewPatchUnmatchedFlightPlanAPIURLUsingFetchedValuesAndExecuteCrudPatchUnmatchedFlightPlanAPIAndValidateThatTheRecordInDepartureTableIsUpdatedAndTheSameInUnmatchedTableIsDeleted()
        {
            ThenGenerateNewPatchUnmatchedFlightPlanAPIURLUsingFetchedValuesAndExecuteCrudPatchUnmatchedFlightPlanAPIAndValidateThatTheRecordInArrivalTableIsUpdatedAndTheSameInUnmatchedTableIsDeleted();
        }

        public List<FlightPlanDto> PatchRequestMethodBody(string flightPlanId, DateTime operationDate)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int IdValueDB = Int32.Parse(sqlResponseDetails["Id"]);
                string Icao = sqlResponseDetails["ICAO"];
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                List<FlightPlanDto> flightPlan = new();
                var patchRequestBody = new FlightPlanDto()
                {
                    SiteId = SiteId,
                    Carrier = "DL",
                    FlightNumber = "DL99",
                    Origin = "CVG",
                    Destination = "DAY",
                    OperationDate = operationDate,
                    AircraftRegistrationId = "null",
                    Callsign = "AirIndia1777",
                    ArcId = "17",
                    FlightPlanId = flightPlanId,
                    FlightRule = (FlightRule)1,
                    EstimatedOffBlockTime = DateTimeOffset.Parse("2022-05-06T14:15:06"),
                    EstimatedOffBlockDate = DateTime.Parse("2022-05-06T14:15:06"),
                    AircraftType = new AircraftTypeDto() { Id = IdValueDB, Icao = Icao, SiteId = SiteId, WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { SiteId = SiteId } },
                    FlightPlanFieldValues = new List<FlightPlanValueDto> { new FlightPlanValueDto(){ Field = "Field         0",Instance = 1,Value = "0"},
                    new FlightPlanValueDto(){Field = "Field         1",Instance = 1,Value = "1"},
                    new FlightPlanValueDto(){Field = "Field         2",Instance = 1,Value = "2"},
                    new FlightPlanValueDto(){Field = "Field         3",Instance = 1,Value = "3"},
                    new FlightPlanValueDto(){Field = "Field         4",Instance = 1,Value = "4"}}
                };
                flightPlan.Add(patchRequestBody);
                return flightPlan;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_Unmatched_FlightAPI");
                return null;
            }
        }
        [Then(@"Generate new put Unmatched Flight Plan API URL using fetched values and Execute Crud Put Unmatched Flight Plan API and validate that the record in Arrival table is updated and the same in unmatched table is deleted")]
        public void ThenGenerateNewPutUnmatchedFlightPlanAPIURLUsingFetchedValuesAndExecuteCrudPutUnmatchedFlightPlanAPIAndValidateThatTheRecordInArrivalTableIsUpdatedAndTheSameInUnmatchedTableIsDeleted()
        {
            try
            {
                List<FlightPlanDto> flightPlanList = PutRequestMethodBody(DateTime.Parse("2022-05-06"), "AT08998877");
                dtoResultList = PutAPIWithDeserializeList(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Put_UnmatchedFlightPlan_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Put_Unmatched_FlightPlanConfiguration");
            }
        }
        [Then(@"Generate new post Unmatched Flight Plan API URL using fetched values and Execute Crud Post Unmatched Flight Plan API and validate that the record in Arrival table is updated with the CallSign and the same in unmatched table is deleted")]
        public void ThenGenerateNewPostUnmatchedFlightPlanAPIURLUsingFetchedValuesAndExecuteCrudPostUnmatchedFlightPlanAPIAndValidateThatTheRecordInArrivalTableIsUpdatedWithTheCallSignAndTheSameInUnmatchedTableIsDeleted()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(sqlResponseDetails["SiteId"]);

                List<FlightPlanDto> flightPlanList = PostRequestMethodBody(SiteId1, SiteId, "AT08998877", DateTime.Parse("2022-05-06"), 0, "");
                dtoResultList = PutAPIWithDeserializeList(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Patch_UnmatchedFlightPlan_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Patch_Unmatched_FlightPlanConfiguration");
            }
        }

        [Then(@"Generate new put Unmatched Flight Plan API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutUnmatchedFlightPlanAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                List<FlightPlanDto> flightPlanList = PutRequestMethodBody(DateTime.Parse("2023-03-07"), "AT08949799");
                var restResponse = PutAPI(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Put_UnmatchedFlightPlan_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }

        [Then(@"Execute Crud Post Unmatched Flight Plan API with incorrect SiteId in the child object and Set DTO Objects for Crud API Unmatched Flight Plan")]
        public void ThenExecuteCrudPostUnmatchedFlightPlanAPIWithIncorrectSiteIdInTheChildObjectAndSetDTOObjectsForCrudAPIUnmatchedFlightPlan()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                int SiteId1 = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                List<FlightPlanDto> flightPlanList = PostRequestMethodBody(SiteId, SiteId1, "AT08998877", DateTime.Parse("2022-05-06"), 0, "AirIndia");
                dtoResultList = PostAPIWithDeserializeList(flightPlanList, APITests.apiConfigDTO.FlightPlanAPIURL, "Failed_To_Update_Crud_API_Post_Unmatched_FlightPlan_Configuration");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_UnmatchedFlightConfiguration");
            }
        }
    }
}











