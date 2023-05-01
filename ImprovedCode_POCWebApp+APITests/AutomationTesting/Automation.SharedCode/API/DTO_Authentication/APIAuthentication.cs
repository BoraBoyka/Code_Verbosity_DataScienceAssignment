using System;
using System.Collections.Generic;
using System.Text;

namespace ACDMAutomation.Shared.API.DTO_AuthAPI
{
    public class APIAuthentication
    {          
        public string userName { get; set; }
        public string password { get; set; }
        public string site { get; set; }
        public string SiteId { get; set; }
        public string ConfigurationAPIURL { get; set; }
        public string AirlinesAPIURL { get; set; }
        public string BEARER_TOKEN { get; set; }
        public string JWT_TOKEN { get; set; }
        public string BASE_URL { get; set; }
        public string AircraftTypeAPIURL { get; set; }
        public string FlightAPIURL { get; set; }
        public string SignatureKey { get; set; }
        public string Product { get; set; }
        public string Name { get; set; }
        public string StandAPIURL { get; set; }
        public string RunwayAPIURL { get; set; }
        public string SiteAPIURL { get; set; }
        public string TaxiSequenceAPIURL { get; set; }
        public string WakeTurbulenceCategoryAPIURL { get; set; }
        public string WakeSeparationTimeAPIURL { get; set; }
        public string ArrivalFlightAPIURL { get; set; }
        public string DepartureFlightAPIURL { get; set; }
        public string FlightPlanAPIURL { get; set; }
        public string SidAPIURL { get; set; }
        public string SidSeparationAPIURL { get; set; }
        public string SpeedSeparationAPIURL { get; set; }

    }
}