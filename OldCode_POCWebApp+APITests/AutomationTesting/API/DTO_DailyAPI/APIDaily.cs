using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACDMAutomation.API.DTO_DailyAPI
{
   public  class APIDaily
    {        
        public class FieldDTO
        {
            [JsonProperty("field")]
            public string Field { get; set; }

            [JsonProperty("instance")]
            public int Instance { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("override")]
            public int Override { get; set; }

            [JsonProperty("attributes")]
            public object Attributes { get; set; }

            [JsonProperty("readOnly")]
            public bool ReadOnly { get; set; }
        }

        public class FlightDTO
        {
            [JsonProperty("actual")]
            public List<FieldDTO> Actual { get; set; }

            [JsonProperty("city")]
            public List<FieldDTO> City { get; set; }

            [JsonProperty("claim")]
            public List<FieldDTO> Claim { get; set; }

            [JsonProperty("claimremark")]
            public List<FieldDTO> Claimremark { get; set; }

            [JsonProperty("default-language")]
            public List<FieldDTO> DefaultLanguage { get; set; }

            [JsonProperty("gate")]
            public List<FieldDTO> Gate { get; set; }

            [JsonProperty("gateremark")]
            public List<FieldDTO> Gateremark { get; set; }

            [JsonProperty("schedclaim")]
            public List<FieldDTO> Schedclaim { get; set; }

            [JsonProperty("schedule")]
            public List<FieldDTO> Schedule { get; set; }

            [JsonProperty("status")]
            public List<FieldDTO> Status { get; set; }

            [JsonProperty("string")]
            public List<FieldDTO> String { get; set; }
        }
        public class Codeshare
        {
            [JsonProperty("backlogo")]
            public FieldDTO Backlogo { get; set; }

            [JsonProperty("bulletlogo")]
            public FieldDTO Bulletlogo { get; set; }

            [JsonProperty("gatelogo")]
            public FieldDTO GateLogo { get; set; }

            [JsonProperty("line")]
            public FieldDTO Line { get; set; }

            [JsonProperty("linecode")]
            public FieldDTO Linecode { get; set; }

            [JsonProperty("logo")]
            public FieldDTO Logo { get; set; }

            [JsonProperty("number")]
            public FieldDTO Number { get; set; }
        }

        public class FieldGroups
        {
            [JsonProperty("codeshares")]
            public List<Codeshare> Codeshares { get; set; }
        }

        public class Locations
        {
            [JsonProperty("href")]
            public object Href { get; set; }

            [JsonProperty("operations")]
            public object Operations { get; set; }

            [JsonProperty("linkedEntities")]
            public List<object> LinkedEntities { get; set; }
        }

        public class Links
        {
            [JsonProperty("locations")]
            public Locations Locations { get; set; }
        }

        public class DailyAPIRoot
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("key")]
            public string Key { get; set; }

            [JsonProperty("site")]
            public string Site { get; set; }

            [JsonProperty("scheduleId")]
            public int ScheduleId { get; set; }

            [JsonProperty("adi")]
            public string Adi { get; set; }

            [JsonProperty("lineCode")]
            public string LineCode { get; set; }

            [JsonProperty("number")]
            public string Number { get; set; }

            [JsonProperty("schedule")]
            public DateTime Schedule { get; set; }

            [JsonProperty("actual")]
            public DateTime Actual { get; set; }

            [JsonProperty("groupCode")]
            public string GroupCode { get; set; }

            [JsonProperty("linkedId")]
            public object LinkedId { get; set; }

            [JsonProperty("holdTime")]
            public DateTime HoldTime { get; set; }

            [JsonProperty("siteId")]
            public int SiteId { get; set; }

            [JsonProperty("dateBoundary")]
            public bool DateBoundary { get; set; }

            [JsonProperty("lastChangeID")]
            public int LastChangeID { get; set; }

            [JsonProperty("lastChangeTime")]
            public DateTime LastChangeTime { get; set; }

            [JsonProperty("fields")]
            public FieldDTO Fields { get; set; }

            [JsonProperty("fieldGroups")]
            public FieldGroups FieldGroups { get; set; }

            [JsonProperty("links")]
            public Links Links { get; set; }
        }
    }
}
