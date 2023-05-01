using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDMAutomation.Shared.PageObjects;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using ACDMAutomation.Shared.Hooks;

namespace ACDMAutomation.Shared.Steps
{
    [Binding]
    public class JWTAuthSteps : APIBaseMethods
    {
        public JWTAuthSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }

        [Then(@"Generate JWT Authorization Token for Environment Info stored in ConfigSetting")]
        public async Task ThenGenerateJWTAuthorizationTokenForEnvironmentInfoStoredInConfigSetting()
        {
            await GenerateJWTAuthorizationToken(HookInitialization.startup.ClientId.ToString(), HookInitialization.startup.ClientSecret.ToString());
        }
        [Then(@"Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting")]
        public async Task ThenGenerateJWTAuthorizationTokenForSiteClaimThatDoesnotExistForEnvironmentInfoStoredInConfigSetting()
        {
            await GenerateJWTAuthorizationToken(HookInitialization.startup.ClientIdForSiteNotExist.ToString(), HookInitialization.startup.ClientSecretForSiteNotExist.ToString());
        }

        [Then(@"Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting")]
        public async Task ThenGenerateJWTAuthorizationTokenForOneParticularSiteClaimThatExistForEnvironmentInfoStoredInConfigSetting()
        {
            await GenerateJWTAuthorizationToken(HookInitialization.startup.ClientIdForOneSite.ToString(), HookInitialization.startup.ClientSecretForOneSite.ToString());
        }
        [Then(@"Generate JWT Authorization Token for no Site Claims for the user for Environment Info stored in ConfigSetting")]
        public async Task ThenGenerateJWTAuthorizationTokenForNoSiteClaimsForTheUserForEnvironmentInfoStoredInConfigSetting()
        {
            await GenerateJWTAuthorizationToken(HookInitialization.startup.ClientIdForNoSiteClaims.ToString(), HookInitialization.startup.ClientSecretForNoSiteClaims.ToString());
        }
        [Then(@"Generate JWT Authorization Token for Site Claims for the user that does not exist in database for Environment Info stored in ConfigSetting")]
        public async Task ThenGenerateJWTAuthorizationTokenForSiteClaimsForTheUserThatDoesNotExistInDatabaseForEnvironmentInfoStoredInConfigSetting()
        {
            await GenerateJWTAuthorizationToken(HookInitialization.startup.ClientIdForSiteClaimsNotInDB.ToString(), HookInitialization.startup.ClientSecretForSiteClaimsNotInDB.ToString());
        }

        [Then(@"Generate JWT Authorization Token for No Role Access")]
        public async Task ThenGenerateJWTAuthorizationTokenForNoRoleAccess()
        {
            await GenerateJWTAuthorizationToken(HookInitialization.startup.ClientIdForNoRoleAccess.ToString(), HookInitialization.startup.ClientSecretForNoRoleAccess.ToString());            
        }
        [Then(@"Generate JWT Authorization Token for User with with Aircraft Read Access and user to not have Aircraft Write Access for Environment Info stored in ConfigSetting")]
        public async Task ThenGenerateJWTAuthorizationTokenForUserWithWithAircraftReadAccessAndUserToNotHaveAircraftWriteAccessForEnvironmentInfoStoredInConfigSetting()
        {
            await GenerateJWTAuthorizationToken(HookInitialization.startup.ClientIdForAircraftReadAccess.ToString(), HookInitialization.startup.ClientSecretForAircraftReadAccess.ToString());
        }
        [Then(@"Generate JWT Authorization Token for User with Aircraft Write Access and user to not have Aircraft Read Access for Environment Info stored in ConfigSetting")]
        public async Task ThenGenerateJWTAuthorizationTokenForUserWithAircraftWriteAccessAndUserToNotHaveAircraftReadAccessForEnvironmentInfoStoredInConfigSetting()
        {
            await GenerateJWTAuthorizationToken(HookInitialization.startup.ClientIdForAircraftWriteAccess.ToString(), HookInitialization.startup.ClientSecretForAircraftWriteAccess.ToString());
        }
        public async Task GenerateJWTAuthorizationToken(string clientId, string clientSecret)
        {
            try
            {
                AddOrUpdateScenarioContext("apiConfigDTO", apiConfigDTO);
                apiConfigDTO.JWT_TOKEN = await GetToken(clientId, clientSecret, HookInitialization.startup.Authority, String.Format(HookInitialization.startup.ApiScope, clientId));
                Assert.IsTrue(true, "Token_Generated_Successfully");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed to generate JWT auth token");
            }
        }

        /// <summary>
        /// Gets Azure token
        /// </summary>
        /// <returns>token</returns>
        public async Task<string> GetToken(string clientId, string clientSecret, string authority, string apiScope)
        {
            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority(new Uri(authority))
                    .Build();

            // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator
            string[] scopes = new string[] { apiScope };
            AuthenticationResult result = null;
            result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            return result?.AccessToken;
        }
    }
}
