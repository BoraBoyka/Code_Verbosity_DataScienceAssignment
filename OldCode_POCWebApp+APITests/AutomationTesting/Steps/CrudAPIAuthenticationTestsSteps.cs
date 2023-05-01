using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDM.Bindings.CommonAPIUtils.Hooks;
using AventStack.ExtentReports.Gherkin.Model;
using ACDM.Bindings.PageObjects;
using ACDMAutomation.API.DTO_AuthAPI;
using ACDMAutomation.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acdm.InformationServices.Dto;
using JWTMakerLib;
using ACDMAutomation.Hooks;

namespace ACDMAutomation.Steps
{
    [Binding]
    public class CrudAPIAuthenticationTestsSteps:APIBaseMethods
    {
        [Then(@"Execute Crud Aircraft Type API and validate that when no token is sent it should return forbidden error via API response")]
        public void ThenExecuteCrudAircraftTypeAPIAndValidateThatWhenNoTokenIsSentItShouldReturnForbiddenErrorViaAPIResponse()
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                var restResponse = ExecuteGetAPI(null, "Failed_To_Execute_Get_API", restOBJSetup);
                Assert.AreEqual("Unauthorized", restResponse.StatusCode.ToString(), "Response code matches:" + restResponse.StatusCode.ToString());
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_Crud_API_AircraftType_NoTokenSent");
            }
        }
        [Then(@"Execute Crud Aircraft Type API and validate that when an invalid token is sent it should return forbidden error via API response")]
        public static void ThenExecuteCrudAircraftTypeAPIAndValidateThatWhenAnInvalidTokenIsSentItShouldReturnForbiddenErrorViaAPIResponse()
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                apiConfigDTO.JWT_TOKEN = CrudAPIAircraftTypeSteps.apiConfigDTO.JWT_TOKEN + "Invalid";
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_Crud_API_AircraftType_InvalidTokenSent");
            }
        }
        public static void GetResponseAndMessageVerification(RestClient restOBJSetup, RestRequest restOBJRequest)
        {
            try
            {
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                var responseData = restResponse.Headers.ToList();
                var DynamicResponseData = responseData[2].ToString();
                Assert.IsTrue(DynamicResponseData.Contains("invalid_token"));
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_GetAPIResponse_MessageVerification");
            }
        }
        [Then(@"Generate JWT Authorization Expired Token for Env Info stored in ConfigSetting")]
        public void GivenGenerateJWTAuthorizationExpiredTokenForEnvInfoStoredInConfigSetting()
        {
            try
            {
                Dictionary<string, string> siteDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(0);
                apiConfigDTO.JWT_TOKEN = tokenCreator.CreateToken(APITests.apiConfigDTO.SignatureKey, APITests.apiConfigDTO.Product, APITests.apiConfigDTO.Name, -2, new List<string>(), new List<string>() { "Site:" + Int32.Parse(siteDetails["Id"]) });
                Assert.IsTrue(true, "Expired_Token_Generated_Successfully");
            }
            catch (Exception ex)
            {
                Assert.IsFalse(true, $"FailedTo_Generate_Expired_JWTToken={ex.Message}");
            }
        }
        [Then(@"Execute Crud Aircraft Type API and validate that when an expired token is sent it should return forbidden error via API response")]
        public static void ThenExecuteCrudAircraftTypeAPIAndValidateThatWhenAnExpiredTokenIsSentItShouldReturnForbiddenErrorViaAPIResponse()
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndMessageVerification(restOBJSetup, restOBJRequest);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_Crud_API_AircraftType_ExpiredTokenSent");
            }
        }
    }
}
