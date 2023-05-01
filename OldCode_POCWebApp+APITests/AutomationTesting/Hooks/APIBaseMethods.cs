using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDM.Bindings.CommonAPIUtils.Hooks;
using ACDMAutomation.API.DTO_AuthAPI;
using ACDMAutomation.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acdm.InformationServices.Dto;
using JWTMakerLib;
using ACDMAutomation.Steps;

namespace ACDMAutomation.Hooks
{
    [Binding]
    public class APIBaseMethods
    {
        public static APIAuthentication apiConfigDTO = new();
        protected TokenCreator tokenCreator = new();
        public static RestAPICommonMethods restAPIUtil = new();
        public virtual Dictionary<string, string> GetSQLResponseDetails()
        {
            return (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
        }
        public IRestResponse ExecuteGetAPI(string token, string failureMessage, RestClient restOBJSetup)
        {
            try
            {
                var restOBJRequest = restAPIUtil.CreateGetRequest(token);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, failureMessage);
                return null;
            }
        }
    }
}
