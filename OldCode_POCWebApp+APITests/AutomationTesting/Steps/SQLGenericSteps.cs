using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using ACDMAutomation.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JWTMakerLib;
using System.Threading;
using ACDM.Bindings.PageObjects;

namespace ACDMAutomation.Steps
{
    [Binding]
    public class SQLGenericSteps
    {
        public static List<object> sqlResponseList;
        [Then(@"Establish Database Connection While Executing SQL Query ""(.*)""")]
        public static void ThenEstablishDatabaseConnectionWhileExecutingSQLQuery(string queryName)
        {
            Thread.Sleep(300);
            sqlResponseList = CommonOperationUtils.OpenSqlConnection(ACDM.Bindings.Hooks.SQLConstants.SQLQuery(queryName));
        }
    }
}
