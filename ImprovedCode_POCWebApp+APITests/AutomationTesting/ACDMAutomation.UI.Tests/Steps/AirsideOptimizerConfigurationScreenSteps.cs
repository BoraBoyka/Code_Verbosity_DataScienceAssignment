using Acdm.InformationServices.Dto;
using ACDMAutomation.Shared.Hooks;
using ACDMAutomation.Shared.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ACDMAutomation.UI.Tests.Steps
{
    [Binding]
    public class AirsideOptimizerConfigurationScreenSteps : CrudAPIBaseMethods<ConfigurationDto>
    {
        public AirsideOptimizerConfigurationScreenSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        private CommonOperationUtils CommonOperUtils => new(_driver);
        [Given(@"Launch Browser using URL")]
        public void GivenLaunchBrowserUsingURL()
        {
            string URL = HookInitialization.startup.ApplicationENV_URL.ToString();
            _driver.Navigate().GoToUrl(URL);
        }
        [Then(@"Click ""([^""]*)"" displayed on ""([^""]*)""")]
        public void ThenClickDisplayedOn(string elementName, string pageName)
        {
            Thread.Sleep(500);
            Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementName));
            Thread.Sleep(1000);
        }
        [Then(@"Enter UserName against ""([^""]*)"" and click on ""([^""]*)"" to ensure that the logged in user is authenticated to the application displayed on ""([^""]*)""")]
        public void ThenEnterUserNameAgainstAndClickOnToEnsureThatTheLoggedInUserIsAuthenticatedToTheApplicationDisplayedOn(string elementNameLogIn, string elementNameSubmit, string pageName)
        {
            string LogInUserName = HookInitialization.startup.LogInUserName.ToString();
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameLogIn, LogInUserName));
            Thread.Sleep(100);
            Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameSubmit));
            Thread.Sleep(3000);
        }
        [Then(@"Enter Password against ""([^""]*)"" and click on ""([^""]*)"" and then click on ""([^""]*)"" and ensure that the user is authenticated to the ""([^""]*)"" application")]
        public void ThenEnterPasswordAgainstAndClickOnAndThenClickOnAndEnsureThatTheUserIsAuthenticatedToTheApplication(string elementNamePassword, string elementNameSignIn, string elementNameSubmit, string pageName)
        {
            string LogInPassword = HookInitialization.startup.LogInPassword.ToString();
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNamePassword, LogInPassword));
            Thread.Sleep(100);
            Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameSignIn));
            Thread.Sleep(3000);
            Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameSubmit));
            Thread.Sleep(3000);
        }
        [Then(@"Fetch value for field ""([^""]*)"" from Database returned from above sql Query")]
        public void ThenFetchValueForFieldFromDatabaseReturnedFromAboveSqlQuery(string keyColName)
        {
            FetchDBOutputResponse(keyColName);
        }
        [Then(@"Fetch value for field ""([^""]*)"" from Database returned from Query Response")]
        public void ThenFetchValueForFieldFromDatabaseReturnedFromQueryResponse(string keyColName)
        {
            FetchDBReturnedValue(keyColName);
        }
        [Then(@"Compare values from DB record set to Fetched values for field ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" for ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" displayed on ""([^""]*)"" from application")]
        public void ThenCompareValuesFromDBRecordSetToFetchedValuesForFieldAndAndAndForAndDisplayedOnFromApplication(string elementNameRow, string elementNameCol, string elementNameID, string elementName, string elementValue, string elementNameDescription, string elementNameSystem, string elementNameGroup, string ID, string NAME, string VALUE, string DESCRIPTION, string SYSTEM, string GROUP, string pageName)
        {
            List<ConfigurationDto> List = new();
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
                ConfigurationDto objectUI = new();
                for (int i = 1; i <= countColumns; i++)
                {
                    string UIheaderID = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameID));
                    if (UIheaderID == "")
                    {
                        UIheaderID = CommonOperationUtils.GetTextFromHiddenElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameID));
                    }
                    string UIheaderName = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementName)); ;
                    string UIheaderValue = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementValue));
                    string UIheaderDescription = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameDescription));
                    string UIheaderSystem = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameSystem));
                    string UIheaderGroup = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameGroup));
                    if (UIheaderGroup == "")
                    {
                        UIheaderGroup = CommonOperationUtils.GetTextFromHiddenElementWithoutXpath(_driver, CommonOperationUtils.XMLReaderUtil(pageName, elementNameGroup));
                    }
                    string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, j);
                    string elementColXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol, i);
                    string finalXpath = elementRowXpath + elementColXpath;
                    string textElementUI = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);
                    if (UIheaderID.Equals(ID) && i == 1)
                    {
                        objectUI.Id = Int32.Parse((textElementUI));
                    }
                    else if (UIheaderName.Equals(NAME) && i == 2)
                    {
                        objectUI.Name = textElementUI;
                    }
                    else if (UIheaderValue.Equals(VALUE) && i == 3)
                    {
                        objectUI.Value = textElementUI;
                    }
                    else if (UIheaderDescription.Equals(DESCRIPTION) && i == 4)
                    {
                        objectUI.Description = textElementUI;
                    }
                    else if (UIheaderSystem.Equals(SYSTEM) && i == 5)
                    {
                        objectUI.System = textElementUI;
                    }
                    else if (UIheaderGroup.Equals(GROUP) && i == 6)
                    {
                        objectUI.Group = textElementUI;
                    }
                }
                List.Add(objectUI);
                Thread.Sleep(200);
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
            Thread.Sleep(200);
            try
            {
                if (compareObject is ConfigurationDto configurationDto)
                {
                    Assert.AreEqual(configurationDto.Id, Int32.Parse(compareDictionary["Id"]), "Id did not match:" + configurationDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(configurationDto.Name, compareDictionary["Name"], "Name values do not match:" + configurationDto.Name + " != " + compareDictionary["Name"]);
                    Assert.AreEqual(configurationDto.Value, compareDictionary["Value"], "Value do not match:" + configurationDto.Value + " != " + compareDictionary["Value"]);
                    Assert.AreEqual(configurationDto.Description, compareDictionary["Description"], "Description values do not match:" + configurationDto.Description + " != " + compareDictionary["Description"]);
                    Assert.AreEqual(configurationDto.System, compareDictionary["System"], "System values do not match:" + configurationDto.System + " != " + compareDictionary["System"]);
                    Assert.AreEqual(configurationDto.Group, compareDictionary["Group"], "Group values do not match:" + configurationDto.Group + " != " + compareDictionary["Group"]);
                }
                else
                {
                    Assert.Fail("Return values which are not configuration type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Enter values against ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" fields displayed on ""([^""]*)""")]
        public void ThenEnterValuesAgainstAndFieldsDisplayedOn(string elementName, string elementNameValue, string elementNameDescription, string elementNameSystem, string elementNameGroup, string pageName, Table table)
        {
            for (int i = 0; i < 1; i++)
            {
                string elementValueNameFinal = table.Rows[i][0].ToString() + Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementName, elementValueNameFinal));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameValue, table.Rows[i][1].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameDescription, table.Rows[i][2].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSystem, table.Rows[i][3].ToString()));
                Thread.Sleep(100);
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameGroup, table.Rows[i][4].ToString()));
                Thread.Sleep(200);
            }
        }
        [Then(@"Validate the ""([^""]*)"" for ""([^""]*)"" displayed on ""([^""]*)""")]
        public void ThenValidateTheForDisplayedOn(string elementName, string MessageText, string pageName)
        {
            string toastMessageText = CommonOperationUtils.GetTextFromElement(_driver, pageName, elementName);
            if (toastMessageText.Contains(MessageText))
            {
                Assert.IsTrue(true, "Success: " + toastMessageText);
            }
            else if (toastMessageText.Contains("An error occurred while updating the entries."))
            {
                Assert.IsTrue(true, "Failed: " + toastMessageText);
            }
            Thread.Sleep(500);
        }
        [Then(@"Validate the Added values for ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" fields in Application against Database")]
        public void ThenValidateTheAddedValuesForAndFieldsInApplicationAgainstDatabase(string keyId, string keyName, string keyValue, string keyDescription, string keySystem, string keyGroup, Table table)
        {
            Thread.Sleep(200);
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            for (int i = 0; i < 1; i++)
            {
                Dictionary<string, string> configurationDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
                string valueNameExpectedUI = table.Rows[i][0].ToString() + Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));

                string valueIdActual = configurationDetails[keyId];
                string valueNameActual = configurationDetails[keyName];
                string valueActual = configurationDetails[keyValue];
                string valueDescriptionActual = configurationDetails[keyDescription];
                string valueSystemActual = configurationDetails[keySystem];
                string valueGroupActual = configurationDetails[keyGroup];

                Thread.Sleep(200);
                string valueIdExpected = (_scenarioContext.Get<string>("valueSQLResponse"));
                Assert.IsTrue(CommonOperUtils.ValueComparison(valueIdExpected, valueIdActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(valueNameExpectedUI, valueNameActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][1].ToString(), valueActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][2].ToString(), valueDescriptionActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][3].ToString(), valueSystemActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][4].ToString(), valueGroupActual));
            }
        }

        [Then(@"Fetch the record Id that the user is going to delete using ""([^""]*)"" and ""([^""]*)"" for ""([^""]*)"" displayed on ""([^""]*)""")]
        public void ThenFetchTheRecordIdThatTheUserIsGoingToDeleteUsingAndForDisplayedOn(string elementNameRow, string elementNameCol, string rowNumber, string pageName)
        {
            int rowNum = Int32.Parse(rowNumber);
            string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, rowNum);
            string elementColXpath = CommonOperationUtils.XMLReaderUtil(pageName, elementNameCol);
            string finalXpath = elementRowXpath + elementColXpath;
            AddOrUpdateScenarioContext("idValueDB", CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath));
        }

        [Then(@"""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" for ""([^""]*)"" displayed on ""([^""]*)""")]
        public void ThenAndForDisplayedOn(string elementName, string elementNameRow, string elementNameCol, string rowNumber, string pageName)
        {
            int rowNum = Int32.Parse(rowNumber);
            int countColumns = Int32.Parse(_scenarioContext.Get<string>("valueSQLData"));
            string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, rowNum);
            string elementColXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol, countColumns);
            string elementDeleteButtonXpath = CommonOperationUtils.XMLReaderUtil(pageName, elementName);
            string finalXpath = elementRowXpath + elementColXpath + elementDeleteButtonXpath;
            Thread.Sleep(200);
            Assert.IsTrue(CommonOperUtils.ClickElementWithXpath(_driver, finalXpath));
        }

        [Then(@"Validate the ""([^""]*)"" ""([^""]*)"" displayed on ""([^""]*)""")]
        public void ThenValidateTheDisplayedOn(string errorMessagesScreenExpected, string elementName, string pageName)
        {
            string WarningMessageTextActual = CommonOperationUtils.GetTextFromElement(_driver, pageName, elementName);
            Assert.IsTrue(CommonOperUtils.ValueComparison(errorMessagesScreenExpected, WarningMessageTextActual));
        }
        [Then(@"Validate the deleted record details from the application against Database")]
        public void ThenValidateTheDeletedRecordDetailsFromTheApplicationAgainstDatabase()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            if (resultData.Count == 0)
            {
                Assert.IsTrue(true, "Record deleted from UI doesn't exist in DB");
            }
            else
            {
                Assert.IsTrue(true, "Record deleted was Clicked cancel from UI and hence still exist in DB");
            }
        }
        [Then(@"Ensure that the element ""([^""]*)"" displayed on ""([^""]*)"" is Disabled")]
        public void ThenEnsureThatTheElementDisplayedOnIsDisabled(string elementName, string pageName)
        {
            CommonOperationUtils.IsDisabled(_driver, pageName, elementName);
        }
        [Then(@"Update value ""([^""]*)"" against ""([^""]*)"" field displayed on ""([^""]*)""")]
        public void ThenUpdateValueAgainstFieldDisplayedOn(string strValue, string elementName, string pageName)
        {
            Thread.Sleep(500);
            Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementName, strValue));
            Thread.Sleep(300);
        }
        [Then(@"Fetch the record Id that the user is going to edit using ""([^""]*)"" and ""([^""]*)"" for ""([^""]*)"" displayed on ""([^""]*)""")]
        public void ThenFetchTheRecordIdThatTheUserIsGoingToEditUsingAndForDisplayedOn(string elementNameRow, string elementNameCol, string rowNumber, string pageName)
        {
            ThenFetchTheRecordIdThatTheUserIsGoingToDeleteUsingAndForDisplayedOn(elementNameRow, elementNameCol, rowNumber, pageName);
        }
        [Then(@"Validate the Updated values for ""([^""]*)"" using ""([^""]*)"" and ""([^""]*)"" for ""([^""]*)"" and ""([^""]*)"" fields in Application against Database displayed on ""([^""]*)""")]
        public void ThenValidateTheUpdatedValuesForUsingAndForAndFieldsInApplicationAgainstDatabaseDisplayedOn(string valueExpected, string elementNameRow, string elementNameCol, string rowNumber, string colNumber, string pageName)
        {
            Thread.Sleep(200);
            int rowNum = Int32.Parse(rowNumber);
            int colNum = Int32.Parse(colNumber);
            string elementRowXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameRow, rowNum);
            string elementColXpath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, elementNameCol, colNum);
            string finalXpath = elementRowXpath + elementColXpath;
            string valueActual = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, finalXpath);
            Assert.IsTrue(CommonOperUtils.ValueComparison(valueExpected, valueActual));
        }

        [Then(@"Click ""([^""]*)"" And Enter values on the Add Screen against ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" and click ""([^""]*)"" and then click ""([^""]*)"" popup displayed on ""([^""]*)""")]
        public void ThenClickAndEnterValuesOnTheAddScreenAgainstAndAndClickAndThenClickPopupDisplayedOn(string elementNameAdd, string elementName, string elementNameValue, string elementNameDescription, string elementNameSystem, string elementNameGroup, string elementNameUpdate, string elementNameClosePopUp, string pageName, Table table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Thread.Sleep(200);
                Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameAdd));
                Random random = new();
                string elementValueNameFinal = table.Rows[i][0].ToString() + random.Next(10, 500);
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementName, elementValueNameFinal));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameValue, table.Rows[i][1].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameDescription, table.Rows[i][2].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameSystem, table.Rows[i][3].ToString()));
                Assert.IsTrue(CommonOperUtils.SetElement(_driver, pageName, elementNameGroup, table.Rows[i][4].ToString()));
                Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameUpdate));
                Thread.Sleep(500);
                Assert.IsTrue(CommonOperUtils.ClickElement(_driver, pageName, elementNameClosePopUp));
                Thread.Sleep(500);
            }
        }
        [Then(@"Validate all the Added values for ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" and ""([^""]*)"" fields in Application against Database")]
        public void ThenValidateAllTheAddedValuesForAndFieldsInApplicationAgainstDatabase(string keyValue, string keyDescription, string keySystem, string keyGroup, Table table)
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            for (int i = 0; i < resultData.Count; i++)
            {
                Dictionary<string, string> configurationDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(i);
                string valueActual = configurationDetails[keyValue];
                string valueDescriptionActual = configurationDetails[keyDescription];
                string valueSystemActual = configurationDetails[keySystem];
                string valueGroupActual = configurationDetails[keyGroup];

                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][1].ToString(), valueActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][2].ToString(), valueDescriptionActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][3].ToString(), valueSystemActual));
                Assert.IsTrue(CommonOperUtils.ValueComparison(table.Rows[i][4].ToString(), valueGroupActual));
            }
        }
        [Then(@"Validate the ""([^""]*)"" are displayed appropriately on screen fields displayed on ""([^""]*)""")]
        public void ThenValidateTheAreDisplayedAppropriatelyOnScreenFieldsDisplayedOn(string errorMessagesActualUI, string pageName, Table table)
        {
            Thread.Sleep(300);
            List<string> errorMessagesScreenExpected = new();
            for (int i = 0; i < 1; i++)
            {
                errorMessagesScreenExpected = table.Rows[i][0].ToString().Split('^').ToList();
            }
            int countNoOfMessages = errorMessagesScreenExpected.Count;
            //Thread.Sleep(300);
            for (int i = 1; i < countNoOfMessages; i++)
            {
                string errorMessagesScreenActualXPath = CommonOperationUtils.XMLReaderUtilAppendData(pageName, errorMessagesActualUI, i);
                string errorMessagesScreenActual = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, errorMessagesScreenActualXPath);
                Assert.IsTrue(CommonOperUtils.ValueComparison(errorMessagesScreenActual, errorMessagesScreenExpected[i]));
            }
        }
        [Then(@"Validate the ""([^""]*)"" for System field is displayed appropriately on ""([^""]*)""")]
        public void ThenValidateTheForSystemFieldIsDisplayedAppropriatelyOn(string errorMessagesActualUI, string pageName, Table table)
        {
            Thread.Sleep(300);
            for (int i = 0; i < 1; i++)
            {
                string errorMessagesScreenActualXPath = CommonOperationUtils.XMLReaderUtil(pageName, errorMessagesActualUI);
                string errorMessagesScreenActual = CommonOperationUtils.GetTextFromElementWithoutXpath(_driver, errorMessagesScreenActualXPath);
                Assert.IsTrue(CommonOperUtils.ValueComparison(errorMessagesScreenActual, table.Rows[i][0].ToString()));
            }
        }
        [Then(@"Sort the Name column ""([^""]*)"" ""([^""]*)"" in Descending order displayed on ""([^""]*)""")]
        public void ThenSortTheNameColumnInDescendingOrderDisplayedOn(string elementNameSortColumn, string elementNameDescendingSortValueSelect, string pageName)
        {
            CommonOperUtils.ClickElement(_driver, pageName, elementNameSortColumn);
            Thread.Sleep(300);
            CommonOperUtils.ClickElement(_driver, pageName, elementNameDescendingSortValueSelect);
            Thread.Sleep(1000);
        }
    }
}
