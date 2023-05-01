using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACDMAutomation.Shared.Driver;
using ACDMAutomation.Shared.Utils;

namespace ACDMAutomation.Shared.Hooks
{

    [Binding]
    public class HookInitialization
    {
        static readonly string configSettingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Configuration\\ConfigSettings.json");
        static readonly string ExtentReportPATH = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "..\\..\\..\\TestResults\\ExtentReport\\ExtentReport.html");
        public static string strRelativepath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "DataSheet\\TestData.json");
        public static Startup startup;
        public static object startupLock = new();
        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        public static string scenarioName;
        private static ExtentReports extent;
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;
        public ScenarioContext scenarioContext;
        public FeatureContext featureContext;
        private Utilities utilities;
        private TimeSpan _timeout;

        public HookInitialization(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _objectContainer = objectContainer;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        /// <summary>
        /// Initializaing Extent Report
        /// </summary>
        [BeforeTestRun]
        public static void InitializeReport()
        {
            try
            {
                var htmlReporter = new ExtentHtmlReporter(ExtentReportPATH);
                htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                Assert.IsFalse(false, $"Failed_To_Initialize_ExtentReport={e.Message}");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext) => featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);

        [BeforeScenario]
        [Obsolete("browservalue is obsolete")]
        public void BeforeScenario()
        {
            BeforeTestRun();
            string browserValue = HookInitialization.startup.BrowserType.ToString();
            SelectBrowserInHeadlessMode(browserValue);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }

        public static void BeforeTestRun()
        {
            try
            {
                lock (startupLock)
                {
                    if (startup == null)
                    {
                        startup = new();
                        ConfigurationBuilder builder = new();
                        Console.WriteLine(configSettingPath);
                        builder.AddJsonFile(configSettingPath).AddUserSecrets<HookInitialization>();
                        IConfiguration configuration = builder.Build();
                        configuration.Bind(startup);
                    }
                }
            }
            catch (Exception e)
            {
                Assert.IsFalse(false, $"Failed_To_Initialize_Configuration={e.Message}");
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        /// <summary>
        /// Method for Inserting Steps Into Extent Report 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepName = scenarioContext.StepContext.StepInfo.Text;
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepError = scenarioContext.TestError;
            utilities = new Utilities(_driver);
            try
            {
                if (stepError == null)
                {
                    if (stepType == "Given")
                    {
                        scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                    }
                    else if (stepType == "When")
                    {
                        scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                    }
                    else if (stepType == "Then")
                    {
                        scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                    }
                    else if (stepType == "And")
                    {
                        scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text);
                    }
                }
                else if (stepError != null)
                {
                    if (stepType == "Given")
                    {
                        scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    }
                    else if (stepType == "When")
                    {
                        scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    }
                    else if (stepType == "And")
                    {
                        scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    }
                    else if (stepType == "Then")
                    {
                        scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    }
                    scenario.AddScreenCaptureFromPath(utilities.FailScreenCapture(stepName));
                }

                else if (stepError.ToString() == "StepDefinitionPending")
                {
                    if (stepType == "Given")
                    {
                        scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                    }
                    else if (stepType == "When")
                    {
                        scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                    }
                    else if (stepType == "And")
                    {
                        scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                    }
                    else if (stepType == "Then")
                    {
                        scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                    }
                }
            }
            catch
            {
                Assert.IsFalse(false, $"Failed_TO_Add_Steps={stepType} {scenarioContext.StepContext.StepInfo.Text}");
            }
        }

        /// <summary>
        /// Method for Browser Selection 
        /// </summary>
        /// <param name="browserType">Browser Type To Be Launched Based On Config.Json Selection</param>
        [Obsolete]
        private void SelectBrowserInHeadlessMode(string browserType)
        {
            try
            {
                switch (browserType)
                {
                    case "Chrome":
                        _timeout = TimeSpan.FromMinutes(10);
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), HookInitialization.startup.ChromeVersion.ToString(), (WebDriverManager.Helpers.Architecture)Architecture.X64);
                        _driver = new ChromeDriver();
                        _driver.Manage().Window.Maximize();
                        _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
                        scenarioName = scenarioContext.ScenarioInfo.Title;
                        CreateNode(scenarioName);
                        break;
                    case "Chrome_Headless":
                        _timeout = TimeSpan.FromMinutes(10);
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), HookInitialization.startup.ChromeVersion.ToString(), (WebDriverManager.Helpers.Architecture)Architecture.X64);
                        //create object of chrome options
                        ChromeOptions options = new();
                        //add the headless argument
                        options.AddArguments("headless");
                        //pass the options parameter in the Chrome driver declaration
                        _driver = new ChromeDriver(options);
                        _driver.Manage().Window.Maximize();
                        _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
                        scenarioName = scenarioContext.ScenarioInfo.Title;
                        CreateNode(scenarioName);
                        break;
                    case "Firefox":
                        break;
                    case "Microsoft Edge":
                        _timeout = TimeSpan.FromMinutes(10);
                        new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig(), HookInitialization.startup.EdgeVersion.ToString(), (WebDriverManager.Helpers.Architecture)Architecture.X64);
                        _driver = new EdgeDriver();
                        _driver.Manage().Window.Maximize();
                        _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
                        scenarioName = scenarioContext.ScenarioInfo.Title;
                        CreateNode(scenarioName);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                Assert.IsFalse(false, "Failed_To_Initialize_Browser");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void CreateNode(string scenarioName)
        {
            scenario = featureName.CreateNode<Scenario>(scenarioName);
        }
    }

    enum BrowserType
    {
        Chrome,
        Firefox,
        IE
    }
}