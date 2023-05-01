using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDMAutomation.Shared.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JWTMakerLib;
using System.Threading;
using ACDMAutomation.Shared.Hooks;
using System.Reflection;

namespace ACDMAutomation.Shared.Steps
{
    [Binding]
    public class SQLGenericSteps
    {
        private readonly ScenarioContext _scenarioContext;
        public SQLGenericSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"Establish Database Connection While Executing SQL Query ""([^""]*)"" and ""([^""]*)""")]
        public void ThenEstablishDatabaseConnectionWhileExecutingSQLQueryAnd(string queryName, string fileName)
        {         
            Type myType = Type.GetType("ACDMAutomation.Shared.Hooks." +fileName);           
            MethodInfo method = myType.GetMethod("SQLQuery");
            object? result = method.Invoke(myType, new object[] { queryName, _scenarioContext });
            var sqlResponseList = CommonOperationUtils.OpenSqlConnection((string)result);
            System.Diagnostics.Debug.WriteLine("Thread: {0}, Count: {1})", Thread.CurrentThread.ManagedThreadId, _scenarioContext.Count);
            if (_scenarioContext.ContainsKey("sqlResponseList"))
            {
                _scenarioContext.Remove("sqlResponseList");
            }
            _scenarioContext.Add("sqlResponseList", sqlResponseList);
        }      
    }
}
