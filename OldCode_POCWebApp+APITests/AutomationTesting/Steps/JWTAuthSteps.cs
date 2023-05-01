using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDMAutomation.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JWTMakerLib;
using ACDMAutomation.Hooks;

namespace ACDMAutomation.Steps
{
    [Binding]
    public class JWTAuthSteps: APIBaseMethods
    {
        [Then(@"Generate JWT Authorization Token for Environment Info stored in ConfigSetting")]
        public void ThenGenerateJWTAuthorizationTokenForEnvironmentInfoStoredInConfigSetting()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                apiConfigDTO.JWT_TOKEN = tokenCreator.CreateToken(APITests.apiConfigDTO.SignatureKey, APITests.apiConfigDTO.Product, APITests.apiConfigDTO.Name, 60, new List<string>(), new List<string>() { "Site:" + Int32.Parse(sqlResponseDetails["Id"]) });
                Assert.IsTrue(true, "Token_Generated_Successfully");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed to generate JWT auth token");
            }
        }
    }
}
