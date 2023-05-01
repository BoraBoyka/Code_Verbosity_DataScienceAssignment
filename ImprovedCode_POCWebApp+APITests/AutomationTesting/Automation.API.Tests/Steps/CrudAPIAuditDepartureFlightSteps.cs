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
    public class CrudAPIAuditDepartureFlightSteps : CrudAPIBaseMethods<AuditDto<DepartureDto>>
    {
        public CrudAPIAuditDepartureFlightSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        { }
        public void APIDBComparisonForAudit<T>(Dictionary<string, string> compareDictionary, T compareObject, string changeType, string previousCallSign, string newCallSign)
        {
            if (compareObject is AuditDto<DepartureDto> auditDepartureFlightDto)
            {
                Assert.AreEqual(auditDepartureFlightDto.HistoryId, (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1), "AuditDepartureFlightId did not match:" + auditDepartureFlightDto.HistoryId + " != " + Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")));
                Assert.AreEqual(auditDepartureFlightDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId values do not match:" + auditDepartureFlightDto.SiteId + " != " + compareDictionary["SiteId"]);
                Assert.AreEqual(auditDepartureFlightDto.ChangeType, changeType, "Change type values do not match:" + auditDepartureFlightDto.ChangeType + " != " + changeType);
                Assert.AreEqual(auditDepartureFlightDto.ChangeTime.ToString(), DateTimeOffset.Parse(compareDictionary["ChangeTime"]).ToString(), "ChangeTime do not match:" + auditDepartureFlightDto.ChangeTime + " != " + DateTimeOffset.Parse(compareDictionary["ChangeTime"]));
                Assert.AreEqual(auditDepartureFlightDto.PreviousRecord.CallSign, previousCallSign, "Previous CallSign value do not match:" + auditDepartureFlightDto.PreviousRecord.CallSign + " != " + previousCallSign);
                if(newCallSign == "")
                {
                    Assert.AreEqual(true,NullReferenceException.ReferenceEquals(auditDepartureFlightDto.NewRecord, null));
                }
                else
                {
                    Assert.AreEqual(auditDepartureFlightDto.NewRecord.CallSign, newCallSign, "New CallSign value do not match:" + auditDepartureFlightDto.NewRecord.CallSign + " != " + newCallSign);
                }
            }
        }

        [Then(@"Generate new Get Departure flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response in above step and Set DTO Objects for Crud Departure Flight API")]
        public void ThenGenerateNewGetDepartureFlightByAndUsingWithValueFetchedFromDBResponseInAboveStepAndSetDTOObjectsForCrudDepartureFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int auditHistoryIdValueDB = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneIntValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL, apiReqName, colName, firstColName, auditHistoryIdValueDB);
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                Assert.AreEqual("OK", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_DepartureAuditFlightType");
            }
        }

        [Then(@"Generate new Get Departure flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response and Set DTO Objects for Crud Departure Flight API")]
        public void ThenGenerateNewGetDepartureFlightByAndUsingWithValueFetchedFromDBResponseAndSetDTOObjectsForCrudDepartureFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string auditHistoryTimeValueDB = (_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL, apiReqName, colName, firstColName, auditHistoryTimeValueDB);
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                Assert.AreEqual("OK", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_DepartureAuditFlightType");
            }
        }

        [Then(@"Generate new Get Departure flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched from DB response in above step and verify the details for recent updated departure record for Crud Departure Flight API")]
        public void ThenGenerateNewGetDepartureFlightByAndUsingWithValueFetchedFromDBResponseInAboveStepAndVerifyTheDetailsForRecentUpdatedDepartureRecordForCrudDepartureFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int auditHistoryIdValueDB = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneIntValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL, apiReqName, colName, firstColName, auditHistoryIdValueDB);
                dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_DepartureAuditFlightType");
            }
        }

        [Then(@"Generate new Get Departure flight by ""([^""]*)"" and ""([^""]*)"" using ""([^""]*)"" with value fetched in above step and verify the details for recent updated departure record for Crud Departure Flight API")]
        public void ThenGenerateNewGetDepartureFlightByAndUsingWithValueFetchedInAboveStepAndVerifyTheDetailsForRecentUpdatedDepartureRecordForCrudDepartureFlightAPI(string apiReqName, string colName, string firstColName)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                string auditHistoryTimeValueDB = (_scenarioContext.Get<string>("valueSQLResponse"));
                var restOBJSetup = restAPIUtil.SetURLByTwoInputParameterAndOneStringValue(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DepartureFlightAPIURL, apiReqName, colName, firstColName, auditHistoryTimeValueDB);
                dtoResultList = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_DepartureAuditFlightType");
            }
        }

        [Then(@"Compare values from API response set to DB record set for Crud Departure Audit Flight API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudDepartureAuditFlightAPI()
        {
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparisonForAudit(APIDetails, dtoResultList.First(d => d.HistoryId == (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1)),"UPDATE", "AirIndia", "AirIndia12");
        }

        [Then(@"Compare values from API response set to DB record set for Crud Departure Audit Flight API For Delete record")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudDepartureAuditFlightAPIForDeleteRecord()
        {
            Console.WriteLine(dtoResultList.Count);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> APIDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparisonForAudit(APIDetails, dtoResultList.First(d => d.HistoryId == (Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")) + 1)), "DELETE", "AirIndia", "");
        }
    }
}












