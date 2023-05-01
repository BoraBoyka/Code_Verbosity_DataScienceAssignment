using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDMAutomation.Shared.API.DTO_AuthAPI;
using ACDMAutomation.Shared.PageObjects;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Acdm.InformationServices.Dto;
using JWTMakerLib;


namespace ACDMAutomation.Shared.Hooks
{
    [Binding]
    public class APIBaseMethods
    {
        public APIAuthentication apiConfigDTO
        {
            get
            {
                return (_scenarioContext.ContainsKey("apiConfigDTO") ? _scenarioContext.Get<APIAuthentication>("apiConfigDTO") : new APIAuthentication());
            }
            set { AddOrUpdateScenarioContext("apiConfigDTO", value); }
        }

        protected TokenCreator tokenCreator = new();
        public RestAPICommonMethods restAPIUtil = new();
        protected readonly ScenarioContext _scenarioContext;
        protected readonly IWebDriver _driver;

        public APIBaseMethods(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _scenarioContext = scenarioContext;
            _driver = driver;
        }
        public virtual Dictionary<string, string> GetSQLResponseDetails()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            return (Dictionary<string, string>)(resultData).ElementAt<object>(0);
        }
        public List<object> GetMultipleSQLResponseDetails()
        {
            List<object> sqlResponseList = new();
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            for (int i = 0; i < resultData.Count; i++)
            {
                Dictionary<string, string> Output = (Dictionary<string, string>)(resultData).ElementAt<object>(i);
                sqlResponseList.Add(Output);
            }
            return sqlResponseList;
        }

        public IRestResponse ExecuteGetAPI(string token, string failureMessage, RestClient restOBJSetup, string siteClaim = "")
        {
            try
            {
                var restOBJRequest = restAPIUtil.CreateGetRequest(token, siteClaim);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
        public static void AddOrUpdateScenarioContext(ScenarioContext context, string key, object value)
        {
            if (context.ContainsKey(key))
            {
                context.Remove(key);
            }
            context.Add(key, value);
        }
        public void AddOrUpdateScenarioContext(string key, object value)
        {
            AddOrUpdateScenarioContext(_scenarioContext, key, value);
        }
    }
}
