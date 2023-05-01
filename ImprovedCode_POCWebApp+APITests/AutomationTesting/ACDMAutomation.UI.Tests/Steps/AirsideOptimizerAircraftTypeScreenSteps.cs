using Acdm.InformationServices.Dto;
using ACDMAutomation.Shared.Hooks;
using ACDMAutomation.Shared.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using ACDMAutomation.Shared.Utils;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace ACDMAutomation.UI.Tests.Steps
{
    [Binding]
    public class AirsideOptimizerAircraftTypeScreenSteps : CrudAPIBaseMethods<AircraftTypeDto>
    {
        public AirsideOptimizerAircraftTypeScreenSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        private CommonOperationUtils CommonOperUtils => new(_driver);
        private Utilities Utilities => new(_driver);
        [Then(@"Compare values from DB record set to Fetched values for field ""([^""]*)"" and ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" displayed on ""([^""]*)"" from application")]
        public void ThenCompareValuesFromDBRecordSetToFetchedValuesForFieldAndAndDisplayedOnFromApplication(string elementNameRow, string elementNameCol, string elementNameColHeader, string elementNameID, string elementNameIATA, string elementNameICAO, string elementNameEngine, string elementNameTypeName, string elementNameNoOfEngines, string elementNameSizeCode, string elementNameSpeedClass, string elementNameWidth, string elementNameCategoryName, string pageName)
        {
            Thread.Sleep(200);
            List<AircraftTypeDto> List = new();
            int countRows = Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
            int countColumns = Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));
            int countNoRows = 0;
            if (countRows < 10)
            {
                countNoRows = countRows;
            }
            else
            {
                countNoRows = 10;
            }

            for (int j = 1; j <= countNoRows; j++)
            {
                AircraftTypeDto objectUI = new();
                for (int i = 1; i <= countColumns; i++)
                {
                    string UIheaderID = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameID));
                    string UIheaderIATA = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameIATA)); ;
                    string UIheaderICAO = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameICAO));
                    string UIheaderEngine = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameEngine));
                    string UIheaderTypeName = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameTypeName));
                    string UIheaderNoOfEngines = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameNoOfEngines));
                    string UIheaderSpeedClass = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameSpeedClass));
                    if (UIheaderSpeedClass == "")
                    {
                        UIheaderSpeedClass = CommonOperationUtils.GetTextFromHiddenElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameSpeedClass));
                    }
                    string UIheaderSizeCode = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameSizeCode));
                    if (UIheaderSizeCode == "")
                    {
                        UIheaderSizeCode = CommonOperationUtils.GetTextFromHiddenElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameSizeCode));
                    }
                    string UIheaderWidth = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameWidth));
                    if (UIheaderWidth == "")
                    {
                        UIheaderWidth = CommonOperationUtils.GetTextFromHiddenElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameWidth));
                    }
                    string UIheaderCategoryName = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameCategoryName));
                    if (UIheaderCategoryName == "")
                    {
                        UIheaderCategoryName = CommonOperationUtils.GetTextFromHiddenElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameCategoryName));
                    }

                    string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, j);
                    string elementColXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol, i);
                    string finalXpath = elementRowXpath + elementColXpath;
                    string textElementUI = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);

                    if (UIheaderID.Equals("ID") && i == 1)
                    {
                        objectUI.Id = Int32.Parse((textElementUI));
                    }
                    else if (UIheaderIATA.Equals("IATA") && i == 2)
                    {
                        objectUI.Iata = textElementUI;
                    }
                    else if (UIheaderICAO.Equals("ICAO") && i == 3)
                    {
                        objectUI.Icao = textElementUI;
                    }
                    else if (UIheaderEngine.Equals("ENGINE") && i == 4)
                    {
                        objectUI.Engine = textElementUI;
                    }
                    else if (UIheaderTypeName.Equals("TYPE NAME") && i == 5)
                    {
                        objectUI.TypeName = textElementUI;
                    }
                    else if (UIheaderNoOfEngines.Equals("# OF ENGINES") && i == 6)
                    {
                        objectUI.NumberOfEngines = Int32.Parse(textElementUI);
                    }
                    else if (UIheaderSizeCode.Equals("SIZE CODE") && i == 7)
                    {
                        objectUI.SizeCode = textElementUI;
                    }
                    else if (UIheaderSpeedClass.Equals("SPEED CLASS") && i == 8)
                    {
                        objectUI.SpeedClass = textElementUI;
                    }
                    else if (UIheaderWidth.Equals("WIDTH") && i == 9)
                    {
                        objectUI.Width = Decimal.Parse(textElementUI);
                    }
                    else if (UIheaderCategoryName.Equals("CATEGORY NAME") && i == 10)
                    {
                        objectUI.WakeTurbulenceCategory.CategoryName = textElementUI;
                    }
                }
                List.Add(objectUI);
            }
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            for (int i = 0; i < countNoRows; i++)
            {
                Thread.Sleep(200);
                Dictionary<string, string> sqlDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(i);
                APIDBComparison(sqlDetails, List[i]);
            }
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                Thread.Sleep(200);
                if (compareObject is AircraftTypeDto aircraftTypeDto)
                {
                    Assert.AreEqual(aircraftTypeDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + aircraftTypeDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(aircraftTypeDto.Icao, compareDictionary["ICAO"], "Icao values do not match:" + aircraftTypeDto.Icao + " != " + compareDictionary["ICAO"]);
                    Assert.AreEqual(aircraftTypeDto.Iata, compareDictionary["IATA"], "Iata values do not match:" + aircraftTypeDto.Iata + " != " + compareDictionary["IATA"]);
                    Assert.AreEqual(aircraftTypeDto.Engine, compareDictionary["Engine"], "Engine values do not match:" + aircraftTypeDto.Engine + " != " + compareDictionary["Engine"]);
                    Assert.AreEqual(aircraftTypeDto.TypeName, compareDictionary["TypeName"], "TypeName values do not match:" + aircraftTypeDto.TypeName + " != " + compareDictionary["TypeName"]);
                    Assert.AreEqual(aircraftTypeDto.Width, Decimal.Parse(compareDictionary["Width"]), "Width values do not match:" + aircraftTypeDto.Width + " != " + compareDictionary["Width"]);
                    Assert.AreEqual(aircraftTypeDto.NumberOfEngines, Int32.Parse(compareDictionary["NumberOfEngines"]), "NumberOfEngines values do not match:" + aircraftTypeDto.NumberOfEngines + " != " + compareDictionary["NumberOfEngines"]);
                    Assert.AreEqual(aircraftTypeDto.SizeCode, compareDictionary["SizeCode"], "SizeCode values do not match:" + aircraftTypeDto.SizeCode + " != " + compareDictionary["SizeCode"]);
                    Assert.AreEqual(aircraftTypeDto.WakeTurbulenceCategory.CategoryName, compareDictionary["CategoryName"], "WakeTurbulenceCategoryName did not match:" + aircraftTypeDto.WakeTurbulenceCategory.CategoryName + " != " + compareDictionary["CategoryName"]);
                    Assert.AreEqual(aircraftTypeDto.SpeedClass, compareDictionary["SpeedClass"], "SpeedClass values do not match:" + aircraftTypeDto.SpeedClass + " != " + compareDictionary["SpeedClass"]);
                }
                else
                {
                    Assert.Fail("Return values which are not aircraft type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Enter values against ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" fields displayed on ""([^""]*)""")]
        public void ThenEnterValuesAgainstAndFieldsDisplayedOn(string elementNameIATA, string elementNameICAO, string elementNameEngine, string elementNameTypeName, string elementNameSizeCode, string elementNameSpeedClass, string elementNameCategoryName, string elementNameCategoryValue, string elementNameNoOfEngines, string elementNameWidth, string pageName, Table table)
        {
            Thread.Sleep(300);
            for (int i = 0; i < 1; i++)
            {
                string elementValueIATAFinal = table.Rows[i][0].ToString() + Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));
                string elementValueICAOFinal = table.Rows[i][1].ToString() + Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameIATA, elementValueIATAFinal));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameICAO, elementValueICAOFinal));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameEngine, table.Rows[i][2].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameTypeName, table.Rows[i][3].ToString()));
                CommonOperUtils.ClickElement(_driver, pageName, elementNameNoOfEngines);
                CommonOperUtils.ClickElement(_driver, pageName, elementNameNoOfEngines);
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSizeCode, table.Rows[i][4].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSpeedClass, table.Rows[i][5].ToString()));
                CommonOperUtils.ClickElement(_driver, pageName, elementNameWidth);
                CommonOperUtils.ClickElement(_driver, pageName, elementNameCategoryName);
                Thread.Sleep(300);
                CommonOperUtils.ClickElement(_driver, pageName, elementNameCategoryValue);
                Thread.Sleep(1000);
            }
        }

        [Then(@"Validate the Added values for ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" fields in Application against Database")]
        public void ThenValidateTheAddedValuesForAndFieldsInApplicationAgainstDatabase(string keyId, string keyIATA, string keyICAO, string keyEngine, string keyTypeName, string keySizeCode, string keySpeedClass, string keyCategoryName, string keyNumberOfEngines, string keyWidth, Table table)
        {
            Thread.Sleep(1000);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            for (int i = 0; i < 1; i++)
            {
                Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
                string valueIATAExpectedUI = table.Rows[i][0].ToString() + Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));
                string valueICAOExpectedUI = table.Rows[i][1].ToString() + Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));

                string valueIdActual = aircraftTypeDetails[keyId];
                string valueIATAActual = aircraftTypeDetails[keyIATA];
                string valueICAOActual = aircraftTypeDetails[keyICAO];
                string valueEngineActual = aircraftTypeDetails[keyEngine];
                string valueTypeNameActual = aircraftTypeDetails[keyTypeName];
                string valueNumberOfEnginesActual = aircraftTypeDetails[keyNumberOfEngines];
                string valueSizeCodeActual = aircraftTypeDetails[keySizeCode];
                string valueSpeedClassActual = aircraftTypeDetails[keySpeedClass];
                string valueWidthActual = aircraftTypeDetails[keyWidth];
                string valueCategoryNameActual = aircraftTypeDetails[keyCategoryName];

                Thread.Sleep(200);
                string valueIdExpected = (_scenarioContext.Get<string>("valueSQLResponse"));
                Assert.IsTrue(CommonOperUtils.ValueComparison(valueIdExpected, valueIdActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(valueIATAExpectedUI, valueIATAActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(valueICAOExpectedUI, valueICAOActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][2].ToString(), valueEngineActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][3].ToString(), valueTypeNameActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][7].ToString(), valueNumberOfEnginesActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][4].ToString(), valueSizeCodeActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][5].ToString(), valueSpeedClassActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][8].ToString(), valueWidthActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][6].ToString(), valueCategoryNameActual));
            }
        }

        [Then(@"Update value ""([^""]*)"" and ""([^""]*)"" against ""([^""]*)"" and ""([^""]*)"" fields displayed on ""([^""]*)""")]
        public void ThenUpdateValueAndAgainstAndFieldsDisplayedOn(string elementValueIATA, string elementValueICAO, string elementNameIATA, string elementNameICAO, string pageName)
        {
            Thread.Sleep(500);
            string elementValueIATAFinal = elementValueIATA + Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
            string elementValueICAOFinal = elementValueICAO + Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
            Console.WriteLine(elementValueIATAFinal);
            Console.WriteLine(elementValueICAOFinal);

            Thread.Sleep(300);
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameIATA, elementValueIATAFinal));
            Thread.Sleep(300);
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameICAO, elementValueICAOFinal));
            Thread.Sleep(300);
        }

        [Then(@"Update value ""([^""]*)"" against ""([^""]*)"" dropdown field displayed on ""([^""]*)""")]
        public void ThenUpdateValueAgainstDropdownFieldDisplayedOn(string elementNameCategoryName, string elementNameCategoryNameValueSelect, string pageName)
        {
            Thread.Sleep(300);
            CommonOperUtils.ClickElement(_driver, pageName, elementNameCategoryName);
            Thread.Sleep(500);
            CommonOperUtils.ClickElement(_driver, pageName, elementNameCategoryNameValueSelect);
            Thread.Sleep(1000);
        }
        [Then(@"Validate the Updated values for ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" ""([^""]*)"" fields in Application against Database")]
        public void ThenValidateTheUpdatedValuesForAndFieldsInApplicationAgainstDatabase(string keyIATA, string valueIATAExpected, string keyICAO, string valueICAOExpected, string keyEngine, string valueEngineExpected, string keyTypeName, string valueTypeNameExpected, string keySizeCode, string valueSizeCodeExpected, string keySpeedClass, string valueSpeedClassExpected, string keyCategoryName, string valueCategoryNameExpected)
        {
            Thread.Sleep(500);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
            string valueIATAExpectedUI = valueIATAExpected + Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));
            string valueICAOExpectedUI = valueICAOExpected + Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse"));

            Console.WriteLine(valueIATAExpectedUI);
            Console.WriteLine(valueICAOExpectedUI);
            string valueIATAActual = aircraftTypeDetails[keyIATA];
            string valueICAOActual = aircraftTypeDetails[keyICAO];
            string valueEngineActual = aircraftTypeDetails[keyEngine];
            string valueTypeNameActual = aircraftTypeDetails[keyTypeName];
            string valueSizeCodeActual = aircraftTypeDetails[keySizeCode];
            string valueSpeedClassActual = aircraftTypeDetails[keySpeedClass];
            string valueCategoryNameActual = aircraftTypeDetails[keyCategoryName];

            Thread.Sleep(500);
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueIATAExpectedUI, valueIATAActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueICAOExpectedUI, valueICAOActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueEngineExpected, valueEngineActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueTypeNameExpected, valueTypeNameActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueSizeCodeExpected, valueSizeCodeActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueSpeedClassExpected, valueSpeedClassActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueCategoryNameExpected, valueCategoryNameActual));
        }
        [Then(@"Click ""([^""]*)"" And Enter values on the Add Screen against ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" and click ""([^""]*)"" and then click ""([^""]*)"" popup displayed on ""([^""]*)""")]
        public void ThenClickAndEnterValuesOnTheAddScreenAgainstAndAndClickAndThenClickPopupDisplayedOn(string elementNameAdd, string elementNameIATA, string elementNameICAO, string elementNameEngine, string elementNameTypeName, string elementNameNoOfEngines, string elementNameSizeCode, string elementNameSpeedClass, string elementNameWidth, string elementNameCategoryName, string elementNameCategoryValue, string elementNameUpdate, string elementNameClosePopUp, string pageName, Table table)
        {
            Thread.Sleep(200);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameAdd));
                Random random = new();
                string elementValueIATAFinal = table.Rows[i][0].ToString() + random.Next(10, 50);
                string elementValueICAOFinal = table.Rows[i][1].ToString() + random.Next(10, 50);

                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameIATA, elementValueIATAFinal));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameICAO, elementValueICAOFinal));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameEngine, table.Rows[i][2].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameTypeName, table.Rows[i][3].ToString()));
                CommonOperUtils.ClickElement(_driver, pageName, elementNameNoOfEngines);
                CommonOperUtils.ClickElement(_driver, pageName, elementNameNoOfEngines);
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSizeCode, table.Rows[i][4].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSpeedClass, table.Rows[i][5].ToString()));
                CommonOperUtils.ClickElement(_driver, pageName, elementNameWidth);
                Thread.Sleep(300);
                CommonOperUtils.ClickElement(_driver, pageName, elementNameCategoryName);
                Thread.Sleep(500);
                CommonOperUtils.ClickElement(_driver, pageName, elementNameCategoryValue);
                Thread.Sleep(1000);
                Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameUpdate));
                Thread.Sleep(500);
                Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameClosePopUp));
                Thread.Sleep(500);
            }
        }
        [Then(@"Validate the Added values for ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" fields in Application against Database")]
        public void ThenValidateTheAddedValuesForAndFieldsInApplicationAgainstDatabase(string keyEngine, string keyTypeName, string keyNumberOfEngines, string keySizeCode, string keySpeedClass, string keyWidth, string keyCategoryName, Table table)
        {
            Thread.Sleep(500);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            for (int i = 0; i < resultData.Count; i++)
            {
                Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(i);
                string valueEngineActual = aircraftTypeDetails[keyEngine];
                string valueTypeNameActual = aircraftTypeDetails[keyTypeName];
                string valueNumberOfEnginesActual = aircraftTypeDetails[keyNumberOfEngines];
                string valueSizeCodeActual = aircraftTypeDetails[keySizeCode];
                string valueSpeedClassActual = aircraftTypeDetails[keySpeedClass];
                string valueWidthActual = aircraftTypeDetails[keyWidth];
                string valueCategoryNameActual = aircraftTypeDetails[keyCategoryName];

                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][2].ToString(), valueEngineActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][3].ToString(), valueTypeNameActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][4].ToString(), valueSizeCodeActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][5].ToString(), valueSpeedClassActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][6].ToString(), valueCategoryNameActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][7].ToString(), valueNumberOfEnginesActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][8].ToString(), valueWidthActual));
            }
        }
        [Then(@"Refresh the WebPage")]
        public void ThenRefreshTheWebPage()
        {
            _driver.Navigate().Refresh();
            Thread.Sleep(1000);
        }
        [Then(@"Enter duplicate value ""([^""]*)"" against ""([^""]*)"" and update a negative value against ""([^""]*)"" and validate the ""([^""]*)"" are displayed appropriately on screen fields displayed on ""([^""]*)""")]
        public void ThenEnterDuplicateValueAgainstAndUpdateANegativeValueAgainstAndValidateTheAreDisplayedAppropriatelyOnScreenFieldsDisplayedOn(string icaoValue, string elementNameICAO, string elementNameNoOfEngines, string errorMessagesActualUI, string pageName, Table table)
        {
            Thread.Sleep(300);
            List<string> errorMessagesScreenExpected = new();
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameICAO, icaoValue));
            CommonOperUtils.ClickElement(_driver, pageName, elementNameNoOfEngines);
            CommonOperUtils.ClickElement(_driver, pageName, elementNameNoOfEngines);
            CommonOperUtils.ClickElement(_driver, pageName, elementNameNoOfEngines);
            for (int i = 0; i < 1; i++)
            {
                errorMessagesScreenExpected = table.Rows[i][0].ToString().Split('^').ToList();
            }
            int countNoOfMessages = errorMessagesScreenExpected.Count;
            for (int i = 1; i < countNoOfMessages; i++)
            {
                string errorMessagesScreenActualXPath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, errorMessagesActualUI, i);
                string errorMessagesScreenActual = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, errorMessagesScreenActualXPath);
                Assert.IsTrue(CommonOperUtils.ValueComparison(errorMessagesScreenActual, errorMessagesScreenExpected[i]));
                Thread.Sleep(300);
            }
        }
        [Then(@"Double click the ""([^""]*)"" ""([^""]*)"" displayed on ""([^""]*)""")]
        public void ThenDoubleClickTheDisplayedOn(string elementTypeNameSortColumn, string AircraftType_TypeNameDescendingSortValueSelect, string pageName)
        {
            Thread.Sleep(500);
            CommonOperUtils.scrollUntilElementIsFound("//th[@data-text='SIZE CODE']");
            CommonOperUtils.DoubleClickElement(_driver, pageName, elementTypeNameSortColumn);
            Thread.Sleep(500);
            CommonOperUtils.DoubleClickElement(_driver, pageName, AircraftType_TypeNameDescendingSortValueSelect);
            Thread.Sleep(500);
        }
    }
}
