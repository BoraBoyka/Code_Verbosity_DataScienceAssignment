
using ACDMAutomation.Shared.API.DTO_AuthAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using ACDMAutomation.Shared.Hooks;

namespace ACDMAutomation.Shared.PageObjects
{
     public class APITests
     {
        private IWebDriver _driver;
        public static APIAuthentication apiConfigDTO;

        public APITests(IWebDriver driver) => _driver = driver;

        public bool AuthCredentialSetup(string envInfo)
        {
            try
            {
                string filePathAndName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Configuration\\APIConfiguration.json");
                using (StreamReader _reader = new StreamReader(filePathAndName))
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(_reader.ReadToEnd().ToString());
                    JObject _jsonObject = JObject.Parse(jsonResponse[envInfo].ToString());
                    apiConfigDTO = _jsonObject.ToObject<APIAuthentication>();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Assert.IsFalse(true, $"Auth_CredentialsSetup_Failed={ex.Message}");
                return false;
            }
        }
    }
}
