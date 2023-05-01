using System;
using System.Collections.Generic;
using System.Text;

namespace ACDMAutomation.API.DTO_AuthAPI
{
    public class APIAuthentication
    {
        //public string authEnvironment { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string site { get; set; }
        public string SiteId { get; set; }
        public string AuthAPIURL { get; set; }
        public string ConfigurationAPIURL { get; set; }
        public string AirlinesAPIURL { get; set; }
        public string DailyAPIURL { get; set; }
        public string BEARER_TOKEN { get; set; }
        public string JWT_TOKEN { get; set; }
        public string BASE_URL { get; set; }
        public string AircraftTypeAPIURL { get; set; }
        public string FlightAPIURL { get; set; }
        public string SignatureKey { get; set; }
        public string Product { get; set; }
        public string Name { get; set; }


    }
}