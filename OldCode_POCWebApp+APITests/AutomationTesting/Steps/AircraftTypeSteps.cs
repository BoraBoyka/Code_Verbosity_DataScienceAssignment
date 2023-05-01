using System;
using TechTalk.SpecFlow;
using ACDM.Bindings.CommonAPIUtils.Hooks;
using ACDM.Bindings.PageObjects;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ACDM.Bindings.Hooks;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using ACDM.Bindings.DataSheet;

namespace ACDMAutomation.Steps
{
    [Binding]
    public class AircraftTypeSteps
    {
        private CommonOperationUtils CommonOperUtils => new (_driver);
        public object _commonOperationUtil;
        private readonly IWebDriver _driver;
        public static Dictionary<string, string> sqlListFinalOutput;
        public static string searchKeyValue;
        public static List<object> sqlResponseList;
        public static List<string> errorTextExpectedScreenMessages;
        public static string textElementUI;
        public static string textElementUIIcao;
        public static string textElementUIIata;
        public static List<string> errorMessagesScreenActual = new ();
        public static string valueTotalRows;
        public static string valueRandomNumber;
        public static string errorMessageText;

        public AircraftTypeSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"Launch Chrome in headless mode")]
        public void GivenLaunchChromeInHeadlessMode()
        {
            string URL = HookInitialization.startup.ApplicationENV_URL.ToString();
            _driver.Navigate().GoToUrl(URL);
            CommonOperationUtils.SynchroniseUntilTheElementIsDisplayedAndEnabled(_driver, CommonOperUtils.PageLoadLink, 3);
        }

        [Given(@"Launch browser with URL")]
        public void GivenLaunchBrowserWithURL()
        {
            string URL = HookInitialization.startup.ApplicationENV_URL.ToString();
            _driver.Navigate().GoToUrl(URL);
            CommonOperationUtils.SynchroniseUntilTheElementIsDisplayedAndEnabled(_driver, CommonOperUtils.PageLoadLink, 3);
        }


        [Then(@"Update value (.*) against ""(.*)"" field displayed on ""(.*)""")]
        public void ThenEditValueAgainstFieldDisplayedOn(string strValue, string elementName, string pageName)
        {
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementName, strValue));
        }

        [Then(@"Fetch ""(.*)"" for all the (.*) displayed on ""(.*)""")]
        public void ThenFetchForAllTheDisplayedOn(string elementName, int sequenceNum, string pageName)
        {
            errorTextExpectedScreenMessages = CommonOperationUtils.GetTextFromMultipleElement(_driver, pageName, elementName, sequenceNum);
            if (errorTextExpectedScreenMessages.Equals(null))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_Text_UIElement");
            }
        }

        [Then(@"Validate the (.*) are displayed appropriately on screen")]
        public void ThenValidateTheAreDisplayedAppropriatelyOnScreen(string errorMessagesScreen)
        {
            errorMessagesScreenActual = errorMessagesScreen.Split('^').ToList();
            int countNoOfMessages = errorMessagesScreenActual.Count;
            for (int i = 0; i < countNoOfMessages; i++)
            {
                Assert.IsTrue(CommonOperUtils.ValueComparison(errorTextExpectedScreenMessages[i], errorMessagesScreenActual[i]));
            }
        }

        [Then(@"Fetch value for field ""(.*)"" and ""(.*)"" displayed on ""(.*)"" for (.*) from application")]
        public void ThenFetchValueForFieldAndDisplayedOnForFromApplication(string elementNameRow, string elementNameCol, string pageName, int rowNum)
        {
            string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, rowNum);
            string elementColXpath = CommonOperationUtils.XMLReaderUtil(pageName, elementNameCol);
            string finalXpath = elementRowXpath + elementColXpath;
            textElementUI = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);
            if (textElementUI.Equals(null))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_Text_UIElement");
            }
        }

        [Then(@"Click ""(.*)"" displayed on ""(.*)""")]
        public void ThenClickDisplayedOn(string elementName, string pageName)
        {
            Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementName));
        }

        [Then(@"Click ""(.*)"" for (.*) displayed on ""(.*)""")]
        public void ThenClickForDisplayedOn(string elementName, int rowNumber, string pageName)
        {
            Assert.IsTrue(CommonOperUtils.ClickElementMultipleRows(_driver, pageName, elementName, rowNumber));
        }

        [Then(@"Clear value against ""(.*)"" for (.*) displayed on ""(.*)""")]
        public void ThenClearValueAgainstForDisplayedOn(string elementName, string fieldNameAppend, string pageName)
        {
            string elementNameFinal = elementName + fieldNameAppend;
            Assert.IsTrue(CommonOperUtils.ClearElement(_driver, pageName, elementNameFinal));
        }

        [Then(@"Clear value against ""(.*)"" displayed on ""(.*)""")]
        public void ThenClearValueAgainstDisplayedOn(string elementName, string pageName)
        {
            Assert.IsTrue(CommonOperUtils.ClearElement(_driver, pageName, elementName));
        }

        //[Then(@"Establish Database Connection While Executing SQL Query ""(.*)""")]
        //public static void ThenEstablishDatabaseConnectionWhileExecutingSQLQuery(string queryName)
        //{
        //    Thread.Sleep(300);
        //    SQLGenericSteps.sqlResponseList = CommonOperationUtils.OpenSqlConnection(ACDM.Bindings.Hooks.SQLConstants.SQLQuery(queryName));
        //}

        [Then(@"validate if SQL Query response has records or not")]
        public static void ThenValidateIfSQLQueryResponseHasRecordsOrNot()
        {
            if (SQLGenericSteps.sqlResponseList.Count == 0)
            {
                Assert.IsFalse(true, "Failed_To_Fetch_DB_Record");
            }
        }

        [Then(@"Fetch Data ""(.*)"" For Column ""(.*)"" from Database")]
        public static void ThenFetchDataForColumnFromDatabase(string searchKeyValueAttribute, string searchKeyFieldName)
        {
            if (searchKeyValueAttribute.Equals("null"))
            {
                searchKeyValueAttribute = searchKeyValue;
                sqlListFinalOutput = CommonOperationUtils.ReadSQLValue(SQLGenericSteps.sqlResponseList, searchKeyValueAttribute, searchKeyFieldName);
            }
            else
            {
                sqlListFinalOutput = CommonOperationUtils.ReadSQLValue(SQLGenericSteps.sqlResponseList, searchKeyValueAttribute, searchKeyFieldName);
            }
        }

        [Then(@"Validate the Updated values for ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) and ""(.*)"" (.*) fields in Application against Database")]
        public void ThenValidateTheUpdatedValuesForAndFieldsInApplicationAgainstDatabase(string keyEngine, string valueEngineExpected, string keyNoOfEngine, string valueNoOfEngineExpected, string keyICAO, string valueICAOExpected, string keyIATA, string valueIATAExpected, string keyTypeName, string valueTypeNameExpected, string keySizeCode, string valueSizeCodeExpected, string keyWidth, string valueWidthExpected, string keyWwc, string valueWwcExpected, string keySpeedClass, string valueSpeedClassExpected)
        {
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)SQLGenericSteps.sqlResponseList.ElementAt<object>(0);
            string valueEngineActual = aircraftTypeDetails[keyEngine];
            string valueICAOActual = aircraftTypeDetails[keyICAO];
            string valueIATAActual = aircraftTypeDetails[keyIATA];
            string valueTypeNameActual = aircraftTypeDetails[keyTypeName];
            string valueNoOfEngineActual = aircraftTypeDetails[keyNoOfEngine];
            string valueSizeCodeActual = aircraftTypeDetails[keySizeCode];
            string valueWidthActual = aircraftTypeDetails[keyWidth];
            string valueWwcActual = aircraftTypeDetails[keyWwc];
            string valueSpeedClassActual = aircraftTypeDetails[keySpeedClass];

            Assert.IsTrue(CommonOperUtils.ValueComparison(valueEngineExpected, valueEngineActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueICAOExpected, valueICAOActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueIATAExpected, valueIATAActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueTypeNameExpected, valueTypeNameActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueNoOfEngineExpected, valueNoOfEngineActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueSizeCodeExpected, valueSizeCodeActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueWidthExpected, valueWidthActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueWwcExpected, valueWwcActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueSpeedClassExpected, valueSpeedClassActual));
        }
        [Then(@"Fetch value for field ""(.*)"" from Database")]
        public static void ThenFetchValueForFieldFromDatabase(string keyTotalRows)
        {
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)SQLGenericSteps.sqlResponseList.ElementAt<object>(0);
            valueTotalRows = aircraftTypeDetails[keyTotalRows];
            Console.WriteLine(valueTotalRows);
            if (valueTotalRows.Equals(null) || valueTotalRows.Equals(""))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_FieldValue_Database");
            }
        }
        [Then(@"Enter values against ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) and ""(.*)"" (.*) fields displayed on ""(.*)""")]
        public void ThenEnterValuesAgainstAndFieldsDisplayedOn(string elementNameEngine, string elementValueEngine, string elementNameICAO, string elementValueICAO, string elementNameIATA, string elementValueIATA, string elementNameType, string elementValueType, string elementNameSizeCode, string elementValueSizeCode, string elementNameWidth, string elementValueWidth, string elementNameWwc, string elementValueWwc, string elementNameSpeedClass, string elementValueSpeedClass, string elementNameNoOfEngine, string elementValueNoOfEngine, string pageName)
        {
            string elementValueICAO1 = elementValueICAO + valueRandomNumber;
            string elementValueIATA1 = elementValueIATA + valueRandomNumber;
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameEngine, elementValueEngine));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameICAO, elementValueICAO1));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameIATA, elementValueIATA1));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameType, elementValueType));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSizeCode, elementValueSizeCode));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameWidth, elementValueWidth));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameWwc, elementValueWwc));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSpeedClass, elementValueSpeedClass));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameNoOfEngine, elementValueNoOfEngine));
        }
        [Then(@"Double Click ""(.*)"" displayed on ""(.*)""")]
        public void ThenDoubleClickDisplayedOn(string elementName, string pageName)
        {
            Assert.IsTrue(CommonOperUtils.DoubleClickElement(_driver, pageName, elementName));
        }
        [Then(@"Fetch value for field ID against entered record using ""(.*)"" and ""(.*)"" displayed on ""(.*)"" application")]
        public void ThenFetchValueForFieldIDAgainstEnteredRecordUsingAndDisplayedOnApplication(string elementNameRow, string elementNameCol, string pageName)
        {
            int totalNumberOfRows = int.Parse(valueTotalRows);
            string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, totalNumberOfRows);
            string elementColXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol, 1);
            string finalXpath = elementRowXpath + elementColXpath;
            textElementUI = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);
        }

        [Then(@"Validate the Added values for ""(.*)"" ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) and ""(.*)"" (.*) fields in Application against Database")]
        public void ThenValidateTheAddedValuesForAndFieldsInApplicationAgainstDatabase(string keyId, string keyEngine, string valueEngineExpected, string keyNoOfEngine, string valueNoOfEngineExpected, string keyICAO, string valueICAOExpected, string keyIATA, string valueIATAExpected, string keyTypeName, string valueTypeNameExpected, string keySizeCode, string valueSizeCodeExpected, string keyWidth, string valueWidthExpected, string keyWwc, string valueWwcExpected, string keySpeedClass, string valueSpeedClassExpected)
        {
            Thread.Sleep(500);
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)SQLGenericSteps.sqlResponseList.ElementAt<object>(0);
            string valueICAOExpected1 = valueICAOExpected + valueRandomNumber;
            string valueIATAExpected1 = valueIATAExpected + valueRandomNumber;

            string valueIdActual = aircraftTypeDetails[keyId];
            string valueEngineActual = aircraftTypeDetails[keyEngine];
            string valueICAOActual = aircraftTypeDetails[keyICAO];
            string valueIATAActual = aircraftTypeDetails[keyIATA];
            string valueTypeNameActual = aircraftTypeDetails[keyTypeName];
            string valueNoOfEngineActual = aircraftTypeDetails[keyNoOfEngine];
            string valueSizeCodeActual = aircraftTypeDetails[keySizeCode];
            string valueWidthActual = aircraftTypeDetails[keyWidth];
            string valueWwcActual = aircraftTypeDetails[keyWwc];
            string valueSpeedClassActual = aircraftTypeDetails[keySpeedClass];

            Thread.Sleep(300);
            Assert.IsTrue(CommonOperUtils.ValueComparison(textElementUI, valueIdActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueEngineExpected, valueEngineActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueICAOExpected1, valueICAOActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueIATAExpected1, valueIATAActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueTypeNameExpected, valueTypeNameActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueNoOfEngineExpected, valueNoOfEngineActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueSizeCodeExpected, valueSizeCodeActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueWidthExpected, valueWidthActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueWwcExpected, valueWwcActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueSpeedClassExpected, valueSpeedClassActual));
        }
        [Then(@"Delete ""(.*)"" for (.*) displayed on ""(.*)""")]
        public void ThenDeleteForDisplayedOn(string elementName, int rowNumber, string pageName)
        {
            Assert.IsTrue(CommonOperUtils.ClickElementMultipleRows(_driver, pageName, elementName, rowNumber));
        }

        [Then(@"Validate the deleted record details from the application against Database")]
        public static void ThenValidateTheDeletedRecordDetailsFromTheApplicationAgainstDatabase()
        {
            if (SQLGenericSteps.sqlResponseList.Count == 0)
            {
                Assert.IsTrue(true, "Record deleted from UI doesn't exist in DB");
            }
            else
            {
                Assert.IsTrue(true, "Record deleted was Clicked cancel from UI and hence still exist in DB");
            }
        }
        [Then(@"Validate the Error ""(.*)"" displayed on ""(.*)""")]
        public void ThenValidateTheErrorDisplayedOn(string elementName, string pageName)
        {
            string toastMessageText = CommonOperationUtils.GetTextFromElement(_driver, pageName, elementName);
            if (toastMessageText.Contains("AircraftType Edited successfully"))
            {
                Assert.IsTrue(true, "Success: " + toastMessageText);
            }
            else if (toastMessageText.Contains("An error occurred while updating the entries."))
            {
                Assert.IsTrue(true, "Failed: " + toastMessageText);
            }
        }
        [Then(@"Fetch value for Icao against (.*) (.*) displayed on ""(.*)"", ""(.*)"" and ""(.*)"" from application")]
        public void ThenFetchValueForIcaoAgainstDisplayedOnAndFromApplication(int rowNum, int colNum, string pageName, string elementNameRow, string elementNameCol)
        {
            string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, rowNum);
            string elementColXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol, colNum);
            string finalXpath = elementRowXpath + elementColXpath;
            textElementUIIcao = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);
            if (textElementUIIcao.Equals(null))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_Text_UIElement");
            }
        }

        [Then(@"Fetch value for Iata against (.*) (.*) displayed on ""(.*)"", ""(.*)"" and ""(.*)"" from application")]
        public void ThenFetchValueForIataAgainstDisplayedOnAndFromApplication(int rowNum, int colNum1, string pageName, string elementNameRow, string elementNameCol)
        {
            string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, rowNum);
            string elementColXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol, colNum1);
            string finalXpath = elementRowXpath + elementColXpath;
            textElementUIIata = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);
            if (textElementUIIata.Equals(null))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_Text_UIElement");
            }
        }
        [Then(@"Enter values against ""(.*)"" (.*) ""(.*)"" ""(.*)"" ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) ""(.*)"" (.*) and ""(.*)"" (.*) fields displayed on ""(.*)""")]
        public void ThenEnterValuesAgainstAndFieldsDisplayedOn(string elementNameEngine, string elementValueEngine, string elementNameICAO, string elementNameIATA, string elementNameType, string elementValueType, string elementNameSizeCode, string elementValueSizeCode, string elementNameWidth, string elementValueWidth, string elementNameWwc, string elementValueWwc, string elementNameSpeedClass, string elementValueSpeedClass, string elementNameNoOfEngine, string elementValueNoOfEngine, string pageName)
        {
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameEngine, elementValueEngine));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameICAO, textElementUIIcao));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameIATA, textElementUIIata));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameType, elementValueType));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSizeCode, elementValueSizeCode));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameWidth, elementValueWidth));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameWwc, elementValueWwc));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSpeedClass, elementValueSpeedClass));
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameNoOfEngine, elementValueNoOfEngine));
        }
        [Then(@"Fetch ""(.*)"" displayed on ""(.*)""")]
        public void ThenFetchDisplayedOn(string elementName, string pageName)
        {
            errorMessageText = CommonOperationUtils.GetTextFromElement(_driver, pageName, elementName);
        }
        [Then(@"Validate the (.*) displayed on screen")]
        public void ThenValidateTheDisplayedOnScreen(string screenMessageActual)
        {
            Assert.IsTrue(CommonOperUtils.ValueComparison(errorMessageText, screenMessageActual));
        }
        [Then(@"Enter value against ""(.*)"" and Clear value against ""(.*)"" displayed on ""(.*)""")]
        public void ThenEnterValueAgainstAndClearValueAgainstDisplayedOn(string elementNameIcao, string elementNamIata, string pageName)
        {
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameIcao, textElementUIIcao));
            Assert.IsTrue(CommonOperUtils.ClearElement(_driver, pageName, elementNamIata));
        }
        [Then(@"Fetch value for field ""(.*)"" from Database returned from Query Response")]
        public static void ThenFetchValueForFieldFromDatabaseReturnedFromQueryResponse(string keyColName)
        {
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)SQLGenericSteps.sqlResponseList.ElementAt<object>(0);
            valueRandomNumber = aircraftTypeDetails[keyColName];
            Console.WriteLine(valueRandomNumber);
            if (valueRandomNumber.Equals(null) || valueRandomNumber.Equals(""))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_FieldValue_Database");
            }
        }
        [Given(@"Enter User name fetched from data sheet for iteration ""(.*)""")]
        public void GivenEnterUserNameFetchedFromDataSheetForIteration(string iteration)
        {
            Assert.IsTrue(CommonOperUtils.ClickElement(_driver, "Aircraft", "UserName"));
            DataSheet.ExecuteJsonQuery("TestData", iteration);
            string strUserName = DataSheet.GetData("UserName");
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, "Aircraft", "UserName", strUserName));
        }

        //[Given(@"Enter User password fetched from data sheet for iteration ""(.*)""")]
        //public void GivenEnterUserPasswordFetchedFromDataSheetForIteration(string iteration)
        //{
        //    Assert.IsTrue(CommonOperUtils.ClickElement(_driver, "Aircraft", "Password"));
        //    string strPassword = DataSheet.GetData("UserPassword");
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, "Aircraft", "Password", strPassword));
        //}

        [Given(@"Click on LOG IN button")]
        public void GivenClickOnLOGINButton()
        {
            CommonOperUtils.ClickElement(_driver, "Aircraft", "LOGIN_BUTTON");
        }

        [Then(@"""(.*)"" Click ""(.*)"" displayed on ""(.*)"" for RowNumber fetched from data sheet")]
        public void ThenClickDisplayedOnForRowNumberFetchedFromDataSheet(string iteration, string elementName, string pageName)
        {
            Thread.Sleep(300);
            DataSheet.ExecuteJsonQuery("TestData", iteration);
            String rowNum = DataSheet.GetData("RowNumber");
            int rowNumber = int.Parse(rowNum);
            Assert.IsTrue(CommonOperUtils.ClickElementMultipleRows(_driver, pageName, elementName, rowNumber));
        }


        //[Then(@"""(.*)"" Update values ""(.*)"" ""(.*)"" ""(.*)""  ""(.*)"" ""(.*)"" against ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" field displayed on ""(.*)""")]
        //public void ThenUpdateValuesAgainstFieldDisplayedOn(string iteration, string elementValueTestDataEngine, string elementValueTestDataNoOfEngine, string elementValueTestDataIcao, string elementValueTestDataIata, string elementValueTestDataTypeName, string elementNameEngine, string elementNameNoOfEngine, string elementNameIcao, string elementNameIata, string elementNameTypeName, string pageName)
        //{
        //    String strValueEngine = DataSheet.GetData(elementValueTestDataEngine);
        //    String strValueNoOfEngine = DataSheet.GetData(elementValueTestDataNoOfEngine);
        //    String strValueIcao = DataSheet.GetData(elementValueTestDataIcao);
        //    String strValueIata = DataSheet.GetData(elementValueTestDataIata);
        //    String strValueTypeName = DataSheet.GetData(elementValueTestDataTypeName);

        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameEngine, strValueEngine));
        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameNoOfEngine, strValueNoOfEngine));
        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameIcao, strValueIcao));
        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameIata, strValueIata));
        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameTypeName, strValueTypeName));
        //}

        //[Then(@"""(.*)"" Update value ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" against ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" field displayed on ""(.*)""")]
        //public void ThenUpdateValueAgainstFieldDisplayedOn(string iteration, string eleValueTestDataSizeCode, string eleValueTestDataWidth, string eleValueTestDataWvc, string elementValueTestDataSpeedClass, string elementNameSizeCode, string elementNameWidth, string elementNameWvc, string elementNameSpeedClass, string pageName)
        //{
        //    String strValueSizeCode = DataSheet.GetData(eleValueTestDataSizeCode);
        //    String strValueWidth = DataSheet.GetData(eleValueTestDataWidth);
        //    String strValueWvc = DataSheet.GetData(eleValueTestDataWvc);
        //    String strValueSpeedClass = DataSheet.GetData(elementValueTestDataSpeedClass);

        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSizeCode, strValueSizeCode));
        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameWidth, strValueWidth));
        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameWvc, strValueWvc));
        //    Thread.Sleep(200);
        //    Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSpeedClass, strValueSpeedClass));
        //}

        //[Then(@"""(.*)"" Fetch value for field ""(.*)"" and ""(.*)"" displayed on ""(.*)"" for RowNumber fetched from data sheet")]
        //public void ThenFetchValueForFieldAndDisplayedOnForRowNumberFetchedFromDataSheet(string iteration, string elementNameRow, string elementNameCol, string pageName)
        //{
        //    String rowNum = DataSheet.GetData("RowNumber");
        //    int rowNumber = int.Parse(rowNum);
        //    string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, rowNumber);
        //    string elementColXpath = CommonOperationUtils.XMLReaderUtil(pageName, elementNameCol);
        //    string finalXpath = elementRowXpath + elementColXpath;
        //    textElementUI = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);
        //    if (textElementUI.Equals(null))
        //    {
        //        Assert.IsFalse(true, "Failed_To_Fetch_Text_UIElement");
        //    }
        //}
        [Then(@"Validate Updated values for ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)"" and ""(.*)"" ""(.*)"" fields in Application against Database")]
        public void ThenValidateUpdatedValuesForAndFieldsInApplicationAgainstDatabase(string keyEngine, string valueEngineExpected, string keyICAO, string valueICAOExpected, string keyIATA, string valueIATAExpected, string keyTypeName, string valueTypeNameExpected, string keySizeCode, string valueSizeCodeExpected, string keyWidth, string valueWidthExpected, string keyWwc, string valueWwcExpected, string keySpeedClass, string valueSpeedClassExpected, string keyNoOfEngine, string valueNoOfEngineExpected)
        {
            Thread.Sleep(300);
            Dictionary<string, string> aircraftTypeDetails = (Dictionary<string, string>)SQLGenericSteps.sqlResponseList.ElementAt<object>(0);
            string valueEngineActual = aircraftTypeDetails[keyEngine];
            string valueICAOActual = aircraftTypeDetails[keyICAO];
            string valueIATAActual = aircraftTypeDetails[keyIATA];
            string valueTypeNameActual = aircraftTypeDetails[keyTypeName];
            string valueNoOfEngineActual = aircraftTypeDetails[keyNoOfEngine];
            string valueSizeCodeActual = aircraftTypeDetails[keySizeCode];
            string valueWidthActual = aircraftTypeDetails[keyWidth];
            string valueWwcActual = aircraftTypeDetails[keyWwc];
            string valueSpeedClassActual = aircraftTypeDetails[keySpeedClass];

            String valueEngineExpectedTestData = DataSheet.GetData(valueEngineExpected);
            String valueICAOExpectedTestData = DataSheet.GetData(valueICAOExpected);
            String valueIATAExpectedTestData = DataSheet.GetData(valueIATAExpected);
            String valueTypeNameExpectedTestData = DataSheet.GetData(valueTypeNameExpected);
            String valueSizeCodeExpectedTestData = DataSheet.GetData(valueSizeCodeExpected);
            String valueWidthExpectedTestData = DataSheet.GetData(valueWidthExpected);
            String valueWwcExpectedTestData = DataSheet.GetData(valueWwcExpected);
            String valueSpeedClassExpectedTestData = DataSheet.GetData(valueSpeedClassExpected);
            String valueNoOfEngineExpectedTestData = DataSheet.GetData(valueNoOfEngineExpected);

            Assert.IsTrue(CommonOperUtils.ValueComparison(valueEngineExpectedTestData, valueEngineActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueICAOExpectedTestData, valueICAOActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueIATAExpectedTestData, valueIATAActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueTypeNameExpectedTestData, valueTypeNameActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueNoOfEngineExpectedTestData, valueNoOfEngineActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueSizeCodeExpectedTestData, valueSizeCodeActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueWidthExpectedTestData, valueWidthActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueWwcExpectedTestData, valueWwcActual));
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueSpeedClassExpectedTestData, valueSpeedClassActual));
        }
    }
}
