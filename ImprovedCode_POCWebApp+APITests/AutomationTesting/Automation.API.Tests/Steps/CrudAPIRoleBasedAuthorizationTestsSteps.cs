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
using System.Threading;
using ACDMAutomation.Shared.Hooks;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Dynamitey.DynamicObjects;

namespace ACDMAutomation.API.Tests.Steps
{
    [Binding]
    public class CrudAPIRoleBasedAuthorizationTestsSteps : CrudAPIBaseMethods<AircraftTypeDto>
    {
        public CrudAPIRoleBasedAuthorizationTestsSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }

        [Then(@"Execute all Get Crud APIs and verify that the response body returns unauthorized error")]
        public void ThenExecuteAllGetCrudAPIsAndVerifyThatTheResponseBodyReturnsUnauthorizedError()
        {
            ReadAllAPIUrlsAndExecuteGetRequests(HookInitialization.startup.ENV);
        }

        public void ReadAllAPIUrlsAndExecuteGetRequests(string envInfo)
        {
            try
            {               
                string filePathAndName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Configuration\\APIConfiguration.json");
                using (StreamReader _reader = new StreamReader(filePathAndName))
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(_reader.ReadToEnd().ToString());
                    JObject _jsonObject = JObject.Parse(jsonResponse[envInfo].ToString());
                    apiConfigDTO = _jsonObject.ToObject<APIAuthentication>();
                    var props = apiConfigDTO.GetType().GetProperties();
                    var propUrls = props.Where(p => p.Name.Contains("APIURL"));
                    foreach (PropertyInfo propInfo in propUrls)
                    {
                        if (propInfo.Name.Contains("FlightPlan"))
                        {
                            var restOBJSetup = RestAPICommonMethods.SetURLByAppendedStringValue(APITests.apiConfigDTO.BASE_URL, propInfo.GetValue(apiConfigDTO).ToString(), "AllUnmatchedFlightplans");
                            var restResponse = ExecuteGetAPI(null, "Failed_To_Execute_Get_API", restOBJSetup);
                            Assert.AreEqual("Unauthorized", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
                        }
                        else
                        {
                            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, propInfo.GetValue(apiConfigDTO).ToString());
                            var restResponse = ExecuteGetAPI(null, "Failed_To_Execute_Get_API", restOBJSetup);
                            Assert.AreEqual("Unauthorized", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
                        }
                    }   
                }
            }
            catch (Exception ex)
            {
                Assert.IsFalse(true, $"ReadAllAPIUrlsAndExecuteGetRequests_Failed={ex.Message}");
            }
        }

        [Then(@"Execute Crud Post Aircraft Type API and validate that the user to get unauthorized error in the response body")]
        public void ThenExecuteCrudPostAircraftTypeAPIAndValidateThatTheUserToGetUnauthorizedErrorInTheResponseBody()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            var postRequestBody = new AircraftTypeDto() { Id = 0, Icao = "A306" + (_scenarioContext.Get<string>("valueSQLData")), Iata = "A306" + (_scenarioContext.Get<string>("valueSQLData")), Engine = "JET", TypeName = "Airbus", Width = 45, NumberOfEngines = 2, SizeCode = "D", SpeedClass = "V6_Jet", SiteId = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")),
            WakeTurbulenceCategory = new WakeTurbulenceCategoryDto() { Id = Int32.Parse(sqlResponseDetails["Id"]), SiteId = Int32.Parse(sqlResponseDetails["SiteId"]), Category = sqlResponseDetails["Category"], CategoryName = sqlResponseDetails["CategoryName"] }};
            var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.AircraftTypeAPIURL, "Failed_To_Update_Crud_API_Post_AircraftType_Configuration");
            Assert.AreEqual("Forbidden", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
        }

        [Then(@"Execute Crud Delete Aircraft Type API and validate that the user to get unauthorized Forbidden error in the response body")]
        public void ThenExecuteCrudDeleteAircraftTypeAPIAndValidateThatTheUserToGetUnauthorizedForbiddenErrorInTheResponseBody()
        {
            var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.AircraftTypeAPIURL, "Id", "Failed_To_Execute_Get_By_API");
            var restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_AircraftType");
            Assert.AreEqual("Forbidden", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            throw new NotImplementedException();
        }
    }
}
