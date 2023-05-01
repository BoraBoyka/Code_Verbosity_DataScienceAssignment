using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using ACDM.Bindings.Hooks;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.Linq;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACDM.Bindings.PageObjects
{
    public class CommonOperationUtils
    {
        private readonly IWebDriver _driver;
        public CommonOperationUtils(IWebDriver driver) => _driver = driver;
        public IWebElement PageLoadLink => _driver.FindElement(By.XPath("//button[text()='Add']"));
        public IWebElement SubmitBtnLink => _driver.FindElement(By.XPath("//button[@type='submit']"));

        /// <summary>
        /// Method for Click Operation 
        /// </summary>
        /// <param name="objDriver">Browser Object<param>
        /// <param name="pageName">XML FileName Where Element To Be Checked</param>
        /// <param name="elementName">Element Xpath to Be Searched</param>
        /// <returns>Element Is Clickable Or Not</returns>
        public bool ClickElement(IWebDriver objDriver, string pageName, string elementName)
        {
            try
            {
                objDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                IWebElement clickElement = objDriver.FindElement(By.XPath(XMLReaderUtil(pageName, elementName)));
                clickElement.Click();
                CommonOperationUtils.SynchroniseUntilTheElementIsDisplayedAndVisible(_driver, clickElement, 3);
                return true;
            }
            catch
            {
                Assert.IsFalse(true, $"Failed_To_Click_Element={elementName}");
                return false;
            }
        }
        /// <summary>
        /// Method for Double Click Operation
        /// </summary>
        /// <param name="objDriver">Browser Object</param>
        /// <param name="pageName">XML FileName Where Element To Be Checked</param>
        /// <param name="elementName">Element Xpath to Be Searched</param>
        /// <returns>Element Is Clickable Or Not</returns>
        public bool DoubleClickElement(IWebDriver objDriver, string pageName, string elementName)
        {
            try
            {
                Thread.Sleep(100);
                objDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Actions act = new (_driver);
                IWebElement ele = objDriver.FindElement(By.XPath(XMLReaderUtil(pageName, elementName)));
                act.DoubleClick(ele).Perform();
                CommonOperationUtils.SynchroniseUntilTheElementIsDisplayedAndVisible(_driver, ele, 3);
                return true;
            }
            catch
            {
                Assert.IsFalse(true, $"Failed_To_Click_Element={elementName}");
                return false;
            }
        }
        /// <summary>
        /// Method for Click Operation on Multiple rows
        /// </summary>
        /// <param name="objDriver">Browser Objec</param>
        /// <param name="pageName">XML FileName Where Element To Be Checked</param>
        /// <param name="elementName">Element Xpath to Be Searched</param>
        /// <param name="rowNum">Row Number to be clicked</param>
        /// <returns>Element Is Clickable Or Not</returns>
        public bool ClickElementMultipleRows(IWebDriver objDriver, string pageName, string elementName, int rowNum)
        {
            try
            {
                objDriver.FindElement(By.XPath(XMLReaderUtilAppendData(pageName, elementName, rowNum))).Click();
                return true;
            }
            catch
            {
                Assert.IsFalse(true, $"Failed_To_Click_Element={elementName}");
                return false;
            }
        }

        /// <summary> Method for Entering Text in a textbox.
        /// </summary>
        /// <param name="objDriver">Browser Object</param>
        /// <param name="pageName">XML FileName Where Element To Be Checked</param>
        /// <param name="elementName">Element Xpath to Be Searched</param>
        /// <param name="ElementValue">Value To Be Entered At Runtime</param>
        /// <returns>Element Exist Or Not</returns>        
        public bool SetElement(IWebDriver objDriver, string pageName, string elementName, string elementValue)
        {
            try
            {
                objDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                IWebElement setElement = objDriver.FindElement(By.XPath(XMLReaderUtil(pageName, elementName)));
                ClearElement(_driver, pageName, elementName);
                EnterValuesInATextBox(_driver, setElement, elementName, elementValue);
                return true;
            }
            catch
            {
                Assert.IsFalse(true, $"Failed_To_Set_Element={elementName}");
                return false;
            }
        }

        /// <summary>
        /// Method for Clearing Text in a textbox
        /// </summary>
        /// <param name="objDriver">Browser Object</param>
        /// <param name="elementXPATH">Xpath of element</param>
        /// <param name="elementName">Name of element</param>
        /// <returns>Element Value is cleared Or Not</returns>
        public bool ClearElement(IWebDriver objDriver, string pageName, string elementName)
        {
            try
            {
                objDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                IWebElement elementXPATH = objDriver.FindElement(By.XPath(XMLReaderUtil(pageName, elementName)));
                elementXPATH.Clear();
                String expectedValueClearElement = elementXPATH.Text;
                Assert.IsTrue(SynchroniseWaitTimeUntilTextIsPresent(_driver, elementXPATH, 10, expectedValueClearElement));
                return true;
            }
            catch
            {
                Assert.IsFalse(true, $"Failed_To_Clear_Text_From_Element={elementName}");
                return false;
            }
        }
        /// <summary>
        /// Method for Entering Text in a textbox
        /// </summary>
        /// <param name="objDriver">Browser Object</param>
        /// <param name="elementXPATH">Xpath of element</param>
        /// <param name="elementName">Name of element</param>
        /// <param name="elementValue">Value of element</param>
        /// <returns>Text entered into a textbox or not</returns>
        public bool EnterValuesInATextBox(IWebDriver objDriver, IWebElement elementXPATH, string elementName, string elementValue)
        {
            try
            {
                objDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                elementXPATH.SendKeys(elementValue);
                elementXPATH.SendKeys(Keys.Tab);
                String expectedValueActualText = elementXPATH.Text;
                Assert.IsTrue(SynchroniseWaitTimeUntilTextIsPresent(_driver, elementXPATH, 10, expectedValueActualText));
                return true;
            }
            catch
            {
                Assert.IsFalse(true, $"Failed_To_Enter_Text_Into_Element={elementName}");
                return false;
            }
        }
        /// <summary>
        /// Method for Fetching Locators Value From XML Repository
        /// </summary>
        /// <param name="repoFileName">XML File Name</param>
        /// <param name="elementName">Element Name</param>
        /// <returns>Return ElementValue(Xpath)</returns>
        public static string XMLReaderUtil(string repoFileName, string elementName)
        {
            try
            {
                string elementXpathText = null;
                //string objectRepoFile = $"{System.IO.Directory.GetParent(@"../../").FullName}\\ACDMAutomation\\ObjectRepository\\{repoFileName}.XML";
                string objectRepoFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "..\\..\\..\\..\\..\\ObjectRepository\\" + repoFileName + ".XML");
                Console.WriteLine(objectRepoFile);
                foreach (XElement subsetElement in XElement.Load(objectRepoFile).Elements(elementName))
                {
                    elementXpathText = subsetElement.Value;
                }
                return elementXpathText.ToString();
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Failed_To_Load_OBject_Repo={e.Message}");
                return null;
            }
        }
        /// <summary>
        /// Method for Fetching Locators with appending dynamic row From XML Repository
        /// </summary>
        /// <param name="repoFileName">XML File Name</param>
        /// <param name="elementName">Element Name</param>
        /// <param name="rowNum">Row Number to be appended</param>
        /// <returns>Return ElementValue(Xpath)</returns>
        public static string XMLReaderUtilAppendData(string repoFileName, string elementName, int rowNum)
        {
            try
            {
                string elementXpathText = null;
                //string objectRepoFile = $"{System.IO.Directory.GetParent(@"../../").FullName}\\ACDMAutomation\\ObjectRepository\\{repoFileName}.XML";
                string objectRepoFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "..\\..\\..\\..\\..\\ObjectRepository\\" + repoFileName + ".XML");
                Console.WriteLine(objectRepoFile);
                foreach (XElement subsetElement in XElement.Load(objectRepoFile).Elements(elementName))
                {
                    elementXpathText = subsetElement.Value + '[' + rowNum + ']';
                }
                return elementXpathText;
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Failed_To_Load_OBject_Repo={e.Message}");
                return null;
            }
        }
        /// <summary>
        /// Method to fetch text from an element with Xpath
        /// </summary>
        /// <param name="objDriver">Browser Object</param>
        /// <param name="pageName">XML FileName Where Element To Be Checked</param>
        /// <param name="elementName">Element Name</param>
        /// <returns>Return Element Text</returns>
        public static string GetTextFromElement(IWebDriver objDriver, string pageName, string elementName)
        {
            try
            {
                string elementValue = null;
                objDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                elementValue = objDriver.FindElement(By.XPath(XMLReaderUtil(pageName, elementName))).Text;
                return elementValue.ToString();
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Failed_To_Fetch_Text_Element={e.Message}");
                return null;
            }
        }
        /// <summary>
        /// Method to fetch text from an element
        /// </summary>
        /// <param name="objDriver">Browser Object</param>
        /// <param name="elementXPath">Element XPath</param>
        /// <returns>Fetched text from an element</returns>
        public static string GetTextFromElementWithoutXpath(IWebDriver objDriver, string elementXPath)
        {
            try
            {
                string elementValue = null;
                objDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                elementValue = objDriver.FindElement(By.XPath(elementXPath)).Text;
                return elementValue.ToString();
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Failed_To_Fetch_Text_Element={e.Message}");
                return null;
            }
        }
        /// <summary>
        /// Method to fetch text from multiple elements
        /// </summary>
        /// <param name="objDriver">Browser Object</param>
        /// <param name="pageName">XML FileName Where Element To Be Checked</param>
        /// <param name="elementName">Element Name</param>
        /// <param name="sequenceNum">Sequence Number to be clicked</param>
        /// <returns>Return Element Text</returns>
        public static List<string> GetTextFromMultipleElement(IWebDriver objDriver, string pageName, string elementName, int sequenceNum)
        {
            List<string> Output = new ();
            try
            {
                string elementValue = null;
                for (int i = 1; i <= sequenceNum; i++)
                {
                    elementValue = objDriver.FindElement(By.XPath(XMLReaderUtilAppendData(pageName, elementName, i))).Text;
                    Output.Add(elementValue);
                }
                return Output;
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Failed_To_Fetch_Text_Element={e.Message}");
                return null;
            }
        }
        /// <summary>
        /// Method to compare 2 values
        /// </summary>
        /// <param name="valueExpected">Expected Value</param>
        /// <param name="valueActual">Actual Value</param>
        /// <returns>Value Matches or Not</returns>
        public bool ValueComparison(string valueExpected, string valueActual)
        {
            try
            {
                if (valueExpected.Equals(valueActual))
                {
                    Console.WriteLine("Value matches");
                    Assert.IsTrue(true, $"Compared_Value_Matches");
                    return true;
                }
                else
                {
                    Console.WriteLine("Value do not match");
                    return false;
                }
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Compared_Values_Mismatch={e.Message}");
                return false;
            }
        }
        /// <summary>
        /// Method to create a sql connection
        /// </summary>
        /// <param name="tableName">Table Name</param>

        public static List<object> OpenSqlConnection(string sqlQuery)
        {
            SqlCommand command; // reading and writing into sql db
            SqlDataReader dataReader; //used to get all data from sql query
            SqlConnection connection;
            String connectionString = HookInitialization.startup.ConnectionString.ToString();
            Console.WriteLine(connectionString);
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                List<object> sqlResponse = new ();
                command = new SqlCommand(sqlQuery, connection);
                dataReader = command.ExecuteReader();
                sqlResponse = GetSqlOutput(sqlQuery, connection, dataReader);
                dataReader.Close();
                command.Dispose();
                connection.Close();
                return sqlResponse;
            }
            catch (SqlException ex)
            {
                Assert.IsFalse(true, $"Failed_To_Initialize_DBConnection={ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// Method for running sql queries and returning data
        /// </summary>
        /// <param name="sqlQuery">SQL Query</param>
        /// <param name="connection">SQL Connection</param>
        /// <param name="dataReader">Gets all data from sql query</param>
        /// <returns>Returns query result set</returns>
        public static List<object> GetSqlOutput(string sqlQuery,SqlConnection connection, SqlDataReader dataReader)
        {
            Dictionary<string, string> Output;
            List<object> sqlResponseList = new ();
            try
            {
                var table = dataReader.GetSchemaTable();
                while (dataReader.Read())
                {
                    Output = new Dictionary<string, string>();
                    for (int columnIterator = 0; columnIterator < dataReader.FieldCount; columnIterator++)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Output.Add(row.Field<string>("ColumnName"), (dataReader.GetValue(columnIterator)!=DBNull.Value) ? dataReader.GetValue(columnIterator).ToString() : null);
                            columnIterator++;
                        }
                    }
                    sqlResponseList.Add(Output);
                }
                return sqlResponseList;
            }
            catch (SqlException ex)
            {
                Assert.IsFalse(true, $"Failed_To_Get_SQLResults={ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// Read and Print SQL result set values
        /// </summary>
        /// <param name="keyValue">Value against which record details need to be fetched</param>
        /// <param name="keyToSearch">Field name</param>
        /// <param name="sqlResponseList">SQL Output list</param>
        public static Dictionary<string, string> ReadSQLValue(List<object> sqlResponseList, string keyValue, string keyToSearch)
        {
            Dictionary<string, string> newOutput = null;
            int countRows = sqlResponseList.Count;
            try
            {
                for (int i = 0; i < countRows; i++)
                {
                    Dictionary<string, string> Output = (Dictionary<string, string>)sqlResponseList.ElementAt<object>(i);
                    foreach (KeyValuePair<string, string> test in Output)
                        if ($"{test.Key}".Equals(keyToSearch) && ($"{test.Value}".Equals(keyValue)))
                        {
                            newOutput = Output;
                            break;
                        }
                }
                return newOutput;
            }
            catch (SqlException ex)
            {
                Assert.IsFalse(true, $"Failed_To_read_SQLResponse={ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// Method to fetch sql Response
        /// </summary>
        /// <param name="sqlResponseList"></param>
        /// <returns>SQL Response</returns>
        public static string GetSQLResponse(List<object> sqlResponseList)
        {
            string sqlResponse = null;
            int countRows = sqlResponseList.Count;
            try
            {
                for (int i = 0; i < countRows; i++)
                {
                    Dictionary<string, string> Output = (Dictionary<string, string>)sqlResponseList.ElementAt<object>(i);
                    foreach (KeyValuePair<string, string> test in Output)
                    {
                        sqlResponse = test.Value;
                    }
                }
                return sqlResponse;
            }
            catch (SqlException ex)
            {
                Assert.IsFalse(true, $"Failed_To_read_SQLResponse={ex.Message}");
                return null;
            }
        }
        public bool TestTimeout(string XPath)
        {
            try
            {
                TimeSpan ts = TimeSpan.FromSeconds(15000);
                WebDriverWait wait = new (_driver, ts);
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(XPath)));
                return true;
            }
            catch
            {
                Assert.IsFalse(true, "Element is not visible on the screen");
                return false;
            }
        }
        /// <summary>
        /// An expectation for checking an element is visible and enabled such that you can click it.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="element">IWebElement</param>
        /// <param name="timeoutSeconds">int</param>
        ///
        public static void SynchroniseUntilTheElementIsDisplayedAndEnabled(in IWebDriver driver, in IWebElement element, in int timeoutSeconds)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Time out exception={e.Message}");
            }
        }
        /// <summary>
        /// An expectation for checking an element is visible and displayed such that you can click it.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="element">IWebElement</param>
        /// <param name="timeoutSeconds">int</param>
        public static void SynchroniseUntilTheElementIsDisplayedAndVisible(in IWebDriver driver, in IWebElement element, in int timeoutSeconds)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Time out exception={e.Message}");
            }
        }
        /// <summary>
        /// An expectation for checking an element is visible and text is present in it.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="element">IWebElement</param>
        /// <param name="timeoutSeconds">int</param>
        /// <param name="elementText">ElementText</param>
        /// <returns></returns>
        public static bool SynchroniseWaitTimeUntilTextIsPresent(IWebDriver driver, IWebElement element, int timeoutSeconds, string elementText)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                wait.Until(ExpectedConditions.TextToBePresentInElement(element, elementText));
                return true;
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, $"Time out exception={e.Message}");
                return false;
            }
        }
    }
}









