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
    public class CrudAPIAuditUnmatchedFlightPlanSteps : CrudAPIBaseMethods<AuditDto<FlightPlanDto>>
    {
        public CrudAPIAuditUnmatchedFlightPlanSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        { }
        public void APIDBComparisonForAudit<T>(Dictionary<string, string> compareDictionary, T compareObject, string changeType, string previousCallSign, string newCallSign)
        {
            if (compareObject is AuditDto<FlightPlanDto> auditUnmatchedFlightDto)
            {
                Assert.AreEqual(auditUnmatchedFlightDto.HistoryId, (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1), "AuditDepartureFlightId did not match:" + auditUnmatchedFlightDto.HistoryId + " != " + Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")));
                Assert.AreEqual(auditUnmatchedFlightDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + auditUnmatchedFlightDto.SiteId + " != " + compareDictionary["SiteId"]);
                Assert.AreEqual(auditUnmatchedFlightDto.ChangeType, changeType, "Change type values do not match:" + auditUnmatchedFlightDto.ChangeType + " != " + changeType);
                Assert.AreEqual(auditUnmatchedFlightDto.ChangeTime.ToString(), DateTimeOffset.Parse(compareDictionary["ChangeTime"]).ToString(), "ChangeTime do not match:" + auditUnmatchedFlightDto.ChangeTime + " != " + DateTimeOffset.Parse(compareDictionary["ChangeTime"]));
                Assert.AreEqual(auditUnmatchedFlightDto.PreviousRecord.Callsign, previousCallSign, "Previous CallSign value do not match:" + auditUnmatchedFlightDto.PreviousRecord.Callsign + " != " + previousCallSign);
                if(newCallSign == "")
                {
                    Assert.AreEqual(true,NullReferenceException.ReferenceEquals(auditUnmatchedFlightDto.NewRecord, null));
                }
                else
                {
                    Assert.AreEqual(auditUnmatchedFlightDto.NewRecord.Callsign, newCallSign, "New CallSign value do not match:" + auditUnmatchedFlightDto.NewRecord.Callsign + " != " + newCallSign);
                }
            }
        }

        [Then(@"Generate new Get Unmatched flight plan by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response in above step and Set DTO Objects for Crud Unmatched Flight Plan API")]
        public void ThenGenerateNewGetUnmatchedFlightPlanByAndUsingWithValueFetchedFromDBResponseInAboveStepAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int auditHistoryIdValueDB = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneIntValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, colName, firstColName, auditHistoryIdValueDB);
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                Assert.AreEqual("OK", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_UnmatchedAuditFlightPlanType");
            }
        }

        [Then(@"Generate new Get Unmatched flight plan by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response in above step and verify the details for recent updated unmatched flight plan record for Crud Unmatched Flight Plan API")]
        public void ThenGenerateNewGetUnmatchedFlightPlanByAndUsingWithValueFetchedFromDBResponseInAboveStepAndVerifyTheDetailsForRecentUpdatedUnmatchedFlightPlanRecordForCrudUnmatchedFlightPlanAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int auditHistoryIdValueDB = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneIntValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, colName, firstColName, auditHistoryIdValueDB);
                dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_UnmatchedAuditFlightPlanType");
            }
        }

        [Then(@"Generate new Get Unmatched flight plan by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response and Set DTO Objects for Crud Unmatched Flight Plan API")]
        public void ThenGenerateNewGetUnmatchedFlightPlanByAndUsingWithValueFetchedFromDBResponseAndSetDTOObjectsForCrudUnmatchedFlightPlanAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string auditHistoryTimeValueDB = (_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, colName, firstColName, auditHistoryTimeValueDB);
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                Assert.AreEqual("OK", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_UnmatchedAuditFlightPlanType");
            }
        }

        [Then(@"Generate new Get Unmatched flight plan by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched in above step and verify the details for recent updated unmatched flight plan record for Crud Unmatched Flight Plan API")]
        public void ThenGenerateNewGetUnmatchedFlightPlanByAndUsingWithValueFetchedInAboveStepAndVerifyTheDetailsForRecentUpdatedUnmatchedFlightPlanRecordForCrudUnmatchedFlightPlanAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string auditHistoryTimeValueDB = (_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.FlightPlanAPIURL, apiReqName, colName, firstColName, auditHistoryTimeValueDB);
                dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_UnmatchedAuditFlightPlanType");
            }
        }

        [Then(@"Compare values from API response set to DB record set for Crud Unmatched Audit Flight Plan API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudUnmatchedAuditFlightPlanAPI()
        {
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparisonForAudit(APIDetails, dtoResultList.First(d => d.HistoryId == (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1)),"UPDATE", "AirIndia", "AirIndia12");
        }

        [Then(@"Compare values from API response set to DB record set for Crud Unmatched Audit Flight Plan API For Delete record")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudUnmatchedAuditFlightPlanAPIForDeleteRecord()
        {
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparisonForAudit(APIDetails, dtoResultList.First(d => d.HistoryId == (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1)), "DELETE", "AirIndia", "");
        }
    }
}












