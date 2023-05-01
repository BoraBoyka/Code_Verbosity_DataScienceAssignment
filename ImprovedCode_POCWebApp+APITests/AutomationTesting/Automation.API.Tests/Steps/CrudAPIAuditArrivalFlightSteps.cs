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
    public class CrudAPIAuditArrivalFlightSteps : CrudAPIBaseMethods<AuditDto<ArrivalDto>>
    {
        public CrudAPIAuditArrivalFlightSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        { }
        public void APIDBComparisonForAudit<T>(Dictionary<string, string> compareDictionary, T compareObject, string changeType, string previousCallSign, string newCallSign)
        {
            if (compareObject is AuditDto<ArrivalDto> auditArrivalFlightDto)
            {
                Assert.AreEqual(auditArrivalFlightDto.HistoryId, (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1), "AuditDepartureFlightId did not match:" + auditArrivalFlightDto.HistoryId + " != " + Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")));
                Assert.AreEqual(auditArrivalFlightDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + auditArrivalFlightDto.SiteId + " != " + compareDictionary["SiteId"]);
                Assert.AreEqual(auditArrivalFlightDto.ChangeType, changeType, "Change type values do not match:" + auditArrivalFlightDto.ChangeType + " != " + changeType);
                Assert.AreEqual(auditArrivalFlightDto.ChangeTime.ToString(), DateTimeOffset.Parse(compareDictionary["ChangeTime"]).ToString(), "ChangeTime do not match:" + auditArrivalFlightDto.ChangeTime + " != " + DateTimeOffset.Parse(compareDictionary["ChangeTime"]));
                Assert.AreEqual(auditArrivalFlightDto.PreviousRecord.CallSign, previousCallSign, "Previous CallSign value do not match:" + auditArrivalFlightDto.PreviousRecord.CallSign + " != " + previousCallSign);
                if(newCallSign == "")
                {
                    Assert.AreEqual(true,NullReferenceException.ReferenceEquals(auditArrivalFlightDto.NewRecord, null));
                }
                else
                {
                    Assert.AreEqual(auditArrivalFlightDto.NewRecord.CallSign, newCallSign, "New CallSign value do not match:" + auditArrivalFlightDto.NewRecord.CallSign + " != " + newCallSign);
                }
            }
        }

        [Then(@"Generate new Get Arrival flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response in above step and Set DTO Objects for Crud Arrival Flight API")]
        public void ThenGenerateNewGetArrivalFlightByAndUsingWithValueFetchedFromDBResponseInAboveStepAndSetDTOObjectsForCrudArrivalFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int auditHistoryIdValueDB = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneIntValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL, apiReqName, colName, firstColName, auditHistoryIdValueDB);
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                Assert.AreEqual("OK", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_ArrivalAuditFlightType");
            }
        }

        [Then(@"Generate new Get Arrival flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response in above step and verify the details for recent updated departure record for Crud Arrival Flight API")]
        public void ThenGenerateNewGetArrivalFlightByAndUsingWithValueFetchedFromDBResponseInAboveStepAndVerifyTheDetailsForRecentUpdatedDepartureRecordForCrudArrivalFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int auditHistoryIdValueDB = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneIntValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL, apiReqName, colName, firstColName, auditHistoryIdValueDB);
                dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_ArrivalAuditFlightType");
            }
        }

        [Then(@"Generate new Get Arrival flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response and Set DTO Objects for Crud Arrival Flight API")]
        public void ThenGenerateNewGetArrivalFlightByAndUsingWithValueFetchedFromDBResponseAndSetDTOObjectsForCrudArrivalFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string auditHistoryTimeValueDB = (_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL, apiReqName, colName, firstColName, auditHistoryTimeValueDB);
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                Assert.AreEqual("OK", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_ArrivalAuditFlightType");
            }
        }

        [Then(@"Generate new Get Arrival flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response in above step and verify the details for recent updated arrival record for Crud Arrival Flight API")]
        public void ThenGenerateNewGetArrivalFlightByAndUsingWithValueFetchedFromDBResponseInAboveStepAndVerifyTheDetailsForRecentUpdatedArrivalRecordForCrudArrivalFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int auditHistoryIdValueDB = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneIntValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL, apiReqName, colName, firstColName, auditHistoryIdValueDB);
                dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_ArrivalAuditFlightType");
            }
        }

        [Then(@"Generate new Get Arrival flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched in above step and verify the details for recent updated arrival record for Crud Arrival Flight API")]
        public void ThenGenerateNewGetArrivalFlightByAndUsingWithValueFetchedInAboveStepAndVerifyTheDetailsForRecentUpdatedArrivalRecordForCrudArrivalFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string auditHistoryTimeValueDB = (_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.ArrivalFlightAPIURL, apiReqName, colName, firstColName, auditHistoryTimeValueDB);
                dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_ArrivalAuditFlightType");
            }
        }

        [Then(@"Compare values from API response set to DB record set for Crud Arrival Audit Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudArrivalAuditFlightAPI()
        { 
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparisonForAudit(APIDetails, dtoResultList.First(d => d.HistoryId == (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1)),"UPDATE", "AirIndia", "AirIndia12");
        }

        [Then(@"Compare values from API response set to DB record set for Crud Arrival Audit Flight API For Delete record")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudArrivalAuditFlightAPIForDeleteRecord()
        { 
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparisonForAudit(APIDetails, dtoResultList.First(d => d.HistoryId == (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1)), "DELETE", "AirIndia", "");
        }
    }
}












