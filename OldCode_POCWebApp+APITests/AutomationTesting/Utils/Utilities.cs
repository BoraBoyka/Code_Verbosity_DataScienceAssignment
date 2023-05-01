using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ACDMAutomation
{
    public class Utilities
    {
        private IWebDriver _driver;
        private int _implicitWaitSec = 20;
        private int _conditionWait = 1;
        private readonly string reportTimeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
        private readonly string screenshotDirPath = System.IO.Directory.GetParent(@"../../").FullName + "\\APVeAutomation\\TestResults\\ExtentReport\\";

        public Utilities(IWebDriver Driver)
        {
            _driver = Driver;
        }

        public void WaitUntilElementClickable(IWebElement element)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMinutes(_conditionWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitUntilElementIsVisible(By by)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMinutes(_conditionWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        public void WaitForPageToLoadJavascript()
        {
            var js = (IJavaScriptExecutor)_driver;
            for (var i = 0; i <= 30; i++)
            {
                var demo = js.ExecuteScript("return document.readyState").ToString();
                if (js.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                {
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        public void Wait() => Thread.Sleep(3000);

        public void ImplicitWait() => _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_implicitWaitSec);


        public void EnterValueIntextBox(IWebElement element, String value)
        {
            WaitUntilElementClickable(element);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.Clear();
            element.SendKeys(value + Keys.Tab);
            Thread.Sleep(1000);
        }

        public void ScrollToElement(IWebElement element)
        {
            WaitUntilElementClickable(element);
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 2px solid red;");
        }


        public string GetElementValue(IWebElement element)
        {
            WaitUntilElementClickable(element);
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 2px solid red;");
            return element.GetAttribute("value");
        }

        public string GetElementText(IWebElement element)
        {
            WaitUntilElementClickable(element);
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 2px solid red;");
            return element.Text;
        }

        public string FailScreenCapture(string tcName)
        {
            string dest = null;
            try
            {
                if (tcName != null)
                {
                    var ss = ((ITakesScreenshot)_driver).GetScreenshot();
                    dest = Path.Combine(screenshotDirPath, reportTimeStamp + "Failed.png");
                    ss.SaveAsFile(dest);
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(false, $"Failed_TO_Add_Steps={tcName}");
            }
            return dest;
        }

        public void WaitForPageToLoad()
        {
            var count = 0;
            var maxCount = 20;
            do
            {
                count++;
                if (count > maxCount)
                {
                    break;
                }
                Thread.Sleep(1000);
            } while (_driver.FindElements(By.XPath("//*[@class='search-view-container loading-center-center-small']")).Count != 0);
        }

        public void SwitchToFrame(string id)
        {
            try
            {
                _driver.SwitchTo().Frame(id);
            }
            catch (NoSuchFrameException)
            {
                Assert.IsFalse(false, $"Failed_TO_Switch_Frame={id}");
            }
        }

        public void SwitchToDefault()
        {
            _driver.SwitchTo().DefaultContent();
        }

        public void JavaScriptClick(IWebElement element)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 2px solid red;");
            js.ExecuteScript("arguments[0].click();", element);
        }

        public void MouseOver(IWebElement webElement)
        {
            var javaScript = "var evObj = document.createEvent('MouseEvents');" +
                "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                "arguments[0].dispatchEvent(evObj);";
            var executor = _driver as IJavaScriptExecutor;
            executor.ExecuteScript(javaScript, webElement);
        }

        public bool IsNumeric(string s)
        {
            foreach (var c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }
            return true;
        }

        public void WaitForApplicationToLoad()
        {
            var count = 0;
            var maxCount = 10;
            do
            {
                count++;
                if (count > maxCount)
                {
                    break;
                }
                Thread.Sleep(1000);
            } while (_driver.FindElements(By.Id("settingsButton")).Count == 0);
        }

        public IWebElement expandRootElement(string shadowelement)
        {
            IWebElement shadow_root = (IWebElement)((IJavaScriptExecutor)_driver).ExecuteScript("return arguments[0].shadowRoot", _driver.FindElement(By.TagName(shadowelement)));
            return shadow_root;
        }

        public IWebElement expandRootShadowElement(IWebElement shadowelement)
        {
            IWebElement shadow_root = (IWebElement)((IJavaScriptExecutor)_driver).ExecuteScript("return arguments[0].shadowRoot", shadowelement);
            return shadow_root;
        }

        public void GetWindowHandles(int no)
        {
            IList<string> windowHandles = new List<string>(_driver.WindowHandles);
            _driver.SwitchTo().Window(windowHandles[no]);
        }

        public String GetTextByJavaScript(IWebElement webElement, int index)
        {
            String text = webElement.GetAttribute("class");
            var js = (IJavaScriptExecutor)_driver;
            return js.ExecuteScript("return document.getElementsByClassName('" + text + "')[0].innerText").ToString();
        }

        public void SelectDropdownByIndex(IWebElement element, int index)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByIndex(index);
        }
        public void SelectDropdownByText(IWebElement element, String text)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }
    }
}
