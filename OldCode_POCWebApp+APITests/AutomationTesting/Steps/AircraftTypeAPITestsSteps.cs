using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDM.Bindings.CommonAPIUtils.Hooks;
using AventStack.ExtentReports.Gherkin.Model;
using ACDM.Bindings.PageObjects;
using ACDMAutomation.API.DTO_AuthAPI;
using Newtonsoft.Json.Linq;
using ACDMAutomation.PageObjects;
using ACDMAutomation.ACDMAutomation.Driver;
using ACDM.Bindings.Hooks;
using ACDMAutomation.API.DTO_DailyAPI;
using static ACDMAutomation.API.DTO_DailyAPI.APIDaily;
using Acdm.InformationServices.Dto;
using ACDMAutomation.Steps;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACDMAutomation.Steps
{
    [Binding]
    public class AircraftTypeAPITestsSteps
    {
        private CommonOperationUtils CommonOperUtils => new CommonOperationUtils(_driver);
        private APITests apiTest => new APITests(_driver);
        private IWebDriver _driver;
        public static APIAuthentication apiConfigDTO = new APIAuthentication();
        public static APIDaily _apiDailyDTO;
        public static AircraftTypeDto _aircraftTypeDTO;
        List<AircraftTypeDto> _aircraftTypeAPIDTO = new List<AircraftTypeDto>();
        public AircraftTypeAPITestsSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects")]
        public void GivenReadAPIConfigValuesForEnvInfoStoredInConfigSettingAndSetDTOObjects()
        {
            Assert.IsTrue(apiTest.AuthCredentialSetup(HookInitialization.startup.ENV));
        }

        //[Given(@"Generate Bearer Token for Env Info stored in ConfigSetting")]
        //public void GivenGenerateBearerTokenForEnvInfoStoredInConfigSetting()
        //{
        //    {
        //        apiConfigDTO.BEARER_TOKEN = apiTest.GenerateBearerToken();
        //        Assert.IsTrue(true, "Token_Generated_Successfully");
        //    }
        //}

        [Then(@"Execute Daily API And Set DTO Objects")]
        public void ThenExecuteDailyAPIAndSetDTOObjects()
        {
            try
            {
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.DailyAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                dynamic jsonResponse = restAPIUtil.DeSerializeJSON(restResponse.Content);
                dynamic DynamicData = JsonConvert.DeserializeObject(jsonResponse.ToString());
                List<FlightDTO> _fieldAPIDTO = new List<FlightDTO>();
                foreach (dynamic rootIterator in DynamicData)
                {
                    JObject _jsonObject = JObject.Parse(rootIterator["fields"].ToString());
                    dynamic _fieldDTOObject = _jsonObject.ToObject<FlightDTO>();
                    _fieldAPIDTO.Add(_fieldDTOObject);
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Daily_API_DTO");
            }
        }

        [Then(@"Execute Aircraft Type API And Set DTO Objects for AircraftType")]
        public void ThenExecuteAircraftTypeAPIAndSetDTOObjectsForAircraftType()
        {
            try
            {
                var restAPIUtil = new RestAPICommonMethods();
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.AircraftTypeAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                var restResponse = restAPIUtil.GetResponse(restOBJSetup, restOBJRequest);
                dynamic jsonResponse = restAPIUtil.DeSerializeJSON(restResponse.Content);
                dynamic DynamicData = JsonConvert.DeserializeObject(jsonResponse.ToString());
                foreach (dynamic rootIterator in DynamicData)
                {
                    JObject _jsonObject = JObject.Parse(rootIterator.ToString());
                    dynamic _fieldDTOObject = _jsonObject.ToObject<AircraftTypeDto>();
                    _aircraftTypeAPIDTO.Add(_fieldDTOObject);
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Daily_API_DTO");
            }
        }
        [Then(@"Compare values from API response to DB record set")]
        public void ThenCompareValuesFromAPIResponseToDBRecordSet()
        {
            for (int i = 0; i < _aircraftTypeAPIDTO.Count; i++)
            {
                Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(SQLGenericSteps.sqlResponseList).ElementAt<object>(i);
                Assert.AreEqual(_aircraftTypeAPIDTO[i].Id, Int32.Parse(aircraftTypeDetails.GetValueOrDefault("Id")), "Id did not match:"+_aircraftTypeAPIDTO[i].Id + " != " + aircraftTypeDetails.GetValueOrDefault("Id"));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].Icao,aircraftTypeDetails.GetValueOrDefault("ICAO"), "ICAO values do not match:"+_aircraftTypeAPIDTO[i].Icao + " != " +aircraftTypeDetails.GetValueOrDefault("ICAO"));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].Iata, aircraftTypeDetails.GetValueOrDefault("IATA"), "ICAO values do not match:" + _aircraftTypeAPIDTO[i].Iata + " != " + aircraftTypeDetails.GetValueOrDefault("IATA"));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].Engine, aircraftTypeDetails.GetValueOrDefault("Engine"), "Engine values do not match:" + _aircraftTypeAPIDTO[i].Engine + " != " + aircraftTypeDetails.GetValueOrDefault("Engine"));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].TypeName, aircraftTypeDetails.GetValueOrDefault("TypeName"), "TypeName values do not match:" + _aircraftTypeAPIDTO[i].TypeName + " != " + aircraftTypeDetails.GetValueOrDefault("TypeName"));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].Width, (Convert.ToDecimal(aircraftTypeDetails.GetValueOrDefault("Width"))), "Width do not match:" + _aircraftTypeAPIDTO[i].Width + " != " + (Convert.ToDecimal(aircraftTypeDetails.GetValueOrDefault("Width"))));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].NumberOfEngines, Int32.Parse(aircraftTypeDetails.GetValueOrDefault("NumberOfEngines")), "Number Of Engines do not match:" + _aircraftTypeAPIDTO[i].NumberOfEngines + " != " + aircraftTypeDetails.GetValueOrDefault("NumberOfEngines"));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].SizeCode, aircraftTypeDetails.GetValueOrDefault("SizeCode"), "SizeCode values do not match:" + _aircraftTypeAPIDTO[i].SizeCode + " != " + aircraftTypeDetails.GetValueOrDefault("SizeCode"));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].Wvc, aircraftTypeDetails.GetValueOrDefault("Wvc"), "Wvc values do not match:" + _aircraftTypeAPIDTO[i].Wvc + " != " + aircraftTypeDetails.GetValueOrDefault("Wvc"));
                Assert.AreEqual(_aircraftTypeAPIDTO[i].SpeedClass, aircraftTypeDetails.GetValueOrDefault("SpeedClass"), "Speed class values do not match:" + _aircraftTypeAPIDTO[i].SpeedClass + " != " + aircraftTypeDetails.GetValueOrDefault("SpeedClass"));
            }
        }
        //[Then(@"Compare values from API response to Fetched values for field ""(.*)"" and ""(.*)"" and ""(.*)"" displayed on ""(.*)"" from application")]
        //public void ThenCompareValuesFromAPIResponseToFetchedValuesForFieldAndAndDisplayedOnFromApplication(string elementNameRow, string elementNameCol, string elementNameCol1, string pageName)
        //{
        //    List<AircraftTypeDto> List = new List<AircraftTypeDto>();
        //    for (int j = 1; j <= _aircraftTypeAPIDTO.Count; j++)
        //    {
        //        AircraftTypeDto objectUI = new AircraftTypeDto();
        //        string countCol= CommonOperationUtils.GetSQLResponse(SQLGenericSteps.SQLGenericSteps.sqlResponseList);
        //        int countColumns = Int32.Parse(countCol);
        //        for (int i = 1; i <= countColumns; i++)
        //        {
        //            string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, j);
        //            string elementColXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol, i);
        //            string finalXpath = elementRowXpath + elementColXpath;
        //            string textElementUI = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);
        //            string elementRowXpathColHeader = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, 1);
        //            string elementColXpathColHeader = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol1, i);
        //            string finalXpathHeader = elementRowXpathColHeader + elementColXpathColHeader;
        //            string UIheader = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpathHeader);

        //            if (UIheader.Equals("Id"))
        //            {
        //                objectUI.Id = (Int32.Parse(textElementUI));
        //            }
        //            else if (UIheader.Equals("Icao"))
        //            {
        //                objectUI.Icao = textElementUI;
        //            }
        //            else if (UIheader.Equals("Iata"))
        //            {
        //                objectUI.Iata = textElementUI;
        //            }
        //            else if (UIheader.Equals("Engine"))
        //            {
        //                objectUI.Engine = textElementUI;
        //            }
        //            else if (UIheader.Equals("Type Name"))
        //            {
        //                objectUI.TypeName = textElementUI;
        //            }
        //            else if (UIheader.Equals("Width"))
        //            {
        //                objectUI.Width = Convert.ToDecimal(textElementUI);
        //            }
        //            else if (UIheader.Equals("Number Of Engines"))
        //            {
        //                objectUI.NumberOfEngines = (Int32.Parse(textElementUI));
        //            }
        //            else if (UIheader.Equals("Size Code"))
        //            {
        //                objectUI.SizeCode = textElementUI;
        //            }
        //            else if (UIheader.Equals("Wvc"))
        //            {
        //                objectUI.Wvc = textElementUI;
        //            }
        //            else if (UIheader.Equals("Speed Class"))
        //            {
        //                objectUI.SpeedClass = textElementUI;
        //            }
        //        }
        //        List.Add(objectUI);
        //    }
            //for(int i=0;i< _aircraftTypeAPIDTO.Count; i++)
            //{
            //    if (_aircraftTypeAPIDTO[i].ValueEquals(List[i]))
            //    {
            //        Assert.IsTrue(true, "Compared_Values_Match_API_UI");
            //    }
            //    else
            //    {
            //        Assert.IsFalse(true, "Compared_Values_Do_Not_Match_API_UI");
            //    }
            //}
        //}

    }
}
