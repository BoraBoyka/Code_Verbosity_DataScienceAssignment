using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace ACDMAutomation.Shared.Driver
{
    public partial class Startup
    {
        public string BrowserType { get; set; }
        public string OS { get; set; }
        public string ApplicationENV_URL { get; set; }
        public string ChromeVersion { get; set; }
        public string EdgeVersion { get; set; }
        public string ConnectionString { get; set; }
        public string ENV { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientIdForSiteNotExist { get; set; }
        public string ClientSecretForSiteNotExist { get; set; }
        public string ClientIdForOneSite { get; set; }
        public string ClientSecretForOneSite { get; set; }
        public string ClientIdForNoSiteClaims { get; set; }
        public string ClientSecretForNoSiteClaims { get; set; }
        public string ClientIdForSiteClaimsNotInDB { get; set; }
        public string ClientSecretForSiteClaimsNotInDB { get; set; }
        public string Authority { get; set; }
        public string ApiScope { get; set; }
        public string LogInUserName { get; set; }
        public string LogInPassword { get; set; }
        public string ClientIdForNoRoleAccess { get; set; }
        public string ClientSecretForNoRoleAccess { get; set; }
        public string ClientIdForAircraftReadAccess { get; set; }
        public string ClientSecretForAircraftReadAccess { get; set; }
        public string ClientIdForAircraftWriteAccess { get; set; }
        public string ClientSecretForAircraftWriteAccess { get; set; }
    }
}