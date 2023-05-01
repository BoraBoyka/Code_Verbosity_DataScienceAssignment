﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ACDMAutomation.API.Tests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class CrudAPIRoleBasedAuthorizationTestsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "CrudAPIRoleBasedAuthorizationTests.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "CrudAPIRoleBasedAuthorizationTests", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "CrudAPIRoleBasedAuthorizationTests")))
            {
                global::ACDMAutomation.API.Tests.Features.CrudAPIRoleBasedAuthorizationTestsFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(_testContext);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line hidden
#line 4
testRunner.Given("Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 5
testRunner.Then("Generate JWT Authorization Token for No Role Access", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Validate All Get Crud API and validate that the user should get 403 Forbidden err" +
            "or in the response")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("getAPIRequestForALLAPIsNotAuthorizedErrorValidation")]
        public void ValidateAllGetCrudAPIAndValidateThatTheUserShouldGet403ForbiddenErrorInTheResponse()
        {
            string[] tagsOfScenario = new string[] {
                    "CrudAPIRoleBasedAuthorizationTests",
                    "getAPIRequestForALLAPIsNotAuthorizedErrorValidation"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate All Get Crud API and validate that the user should get 403 Forbidden err" +
                    "or in the response", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 8
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 9
    testRunner.Then("Execute all Get Crud APIs and verify that the response body returns unauthorized " +
                        "error", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Validate Post Crud Aircraft Type API request without having Aircraft Write Role A" +
            "ccess and validate that the user should get 403 Forbidden error in the response")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("postAPIRequestForAircraftWithoutWriteAccess")]
        public void ValidatePostCrudAircraftTypeAPIRequestWithoutHavingAircraftWriteRoleAccessAndValidateThatTheUserShouldGet403ForbiddenErrorInTheResponse()
        {
            string[] tagsOfScenario = new string[] {
                    "CrudAPIRoleBasedAuthorizationTests",
                    "postAPIRequestForAircraftWithoutWriteAccess"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate Post Crud Aircraft Type API request without having Aircraft Write Role A" +
                    "ccess and validate that the user should get 403 Forbidden error in the response", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 12
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 13
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchRandomNumberGenerat" +
                        "or\" and \"SQLConstants_AircraftTypeAndAirline\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 14
    testRunner.And("Fetch value for field \"Random_Number_Generator\" from Database returned from Query" +
                        " Response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 15
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchSiteIdAscending\" an" +
                        "d \"SQLConstants_TaxiSequenceAndSite\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 16
    testRunner.And("Fetch value for field \"Id\" from Database returned from above sql Query", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 17
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchWakeTurbulenceCateg" +
                        "oryData\" and \"SQLConstants_WakeTurbulenceAndSeparation\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 18
    testRunner.And("Generate JWT Authorization Token for User with with Aircraft Read Access and user" +
                        " to not have Aircraft Write Access for Environment Info stored in ConfigSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 19
    testRunner.Then("Execute Crud Post Aircraft Type API and validate that the user to get unauthorize" +
                        "d error in the response body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Validate Delete Crud API Aircraft Type request without having Aircraft Write Role" +
            " Access and validate that the user should get 403 Forbidden error in the respons" +
            "e")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("deleteAPIRequestResponseValidationForAircraftWithoutWriteAccess")]
        public void ValidateDeleteCrudAPIAircraftTypeRequestWithoutHavingAircraftWriteRoleAccessAndValidateThatTheUserShouldGet403ForbiddenErrorInTheResponse()
        {
            string[] tagsOfScenario = new string[] {
                    "CrudAPIRoleBasedAuthorizationTests",
                    "deleteAPIRequestResponseValidationForAircraftWithoutWriteAccess"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate Delete Crud API Aircraft Type request without having Aircraft Write Role" +
                    " Access and validate that the user should get 403 Forbidden error in the respons" +
                    "e", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 22
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 23
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchRandomNumberGenerat" +
                        "or\" and \"SQLConstants_AircraftTypeAndAirline\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 24
    testRunner.And("Fetch value for field \"Random_Number_Generator\" from Database returned from Query" +
                        " Response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 25
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchSiteIdAscending\" an" +
                        "d \"SQLConstants_TaxiSequenceAndSite\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 26
    testRunner.And("Fetch value for field \"Id\" from Database returned from above sql Query", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
    testRunner.Then("Establish Database Connection While Executing SQL Query \"insertNewRecordAircraftT" +
                        "able\" and \"SQLConstants_AircraftTypeAndAirline\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 28
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchRecentAddedAircraft" +
                        "TypeData\" and \"SQLConstants_AircraftTypeAndAirline\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 29
    testRunner.And("Generate JWT Authorization Token for User with with Aircraft Read Access and user" +
                        " to not have Aircraft Write Access for Environment Info stored in ConfigSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 30
    testRunner.Then("Execute Crud Delete Aircraft Type API and validate that the user to get unauthori" +
                        "zed Forbidden error in the response body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 31
    testRunner.Then("Generate JWT Authorization Token for Environment Info stored in ConfigSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 32
    testRunner.And("Generate new Delete aircraft type API URL and Execute Crud Delete API request and" +
                        " Set DTO Objects for Crud API Aircraft Type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Validate Delete Crud API Aircraft Type request with Aircraft Write Role Access an" +
            "d validate that the user should be able to successfully delete the record")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("deleteAPIRequestResponseValidationForAircraftWithWriteAccess")]
        public void ValidateDeleteCrudAPIAircraftTypeRequestWithAircraftWriteRoleAccessAndValidateThatTheUserShouldBeAbleToSuccessfullyDeleteTheRecord()
        {
            string[] tagsOfScenario = new string[] {
                    "CrudAPIRoleBasedAuthorizationTests",
                    "deleteAPIRequestResponseValidationForAircraftWithWriteAccess"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate Delete Crud API Aircraft Type request with Aircraft Write Role Access an" +
                    "d validate that the user should be able to successfully delete the record", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 35
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 36
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchRandomNumberGenerat" +
                        "or\" and \"SQLConstants_AircraftTypeAndAirline\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 37
    testRunner.And("Fetch value for field \"Random_Number_Generator\" from Database returned from Query" +
                        " Response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 38
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchSiteIdAscending\" an" +
                        "d \"SQLConstants_TaxiSequenceAndSite\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 39
    testRunner.And("Fetch value for field \"Id\" from Database returned from above sql Query", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 40
    testRunner.Then("Establish Database Connection While Executing SQL Query \"insertNewRecordAircraftT" +
                        "able\" and \"SQLConstants_AircraftTypeAndAirline\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 41
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchRecentAddedAircraft" +
                        "TypeData\" and \"SQLConstants_AircraftTypeAndAirline\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 42
    testRunner.And("Generate JWT Authorization Token for User with Aircraft Write Access and user to " +
                        "not have Aircraft Read Access for Environment Info stored in ConfigSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 43
    testRunner.And("Generate new Delete aircraft type API URL and Execute Crud Delete API request and" +
                        " Set DTO Objects for Crud API Aircraft Type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Validate Get Crud API Aircraft Type by Id with Aircraft Read Role Access and vali" +
            "date the fetched details for the column")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CrudAPIRoleBasedAuthorizationTests")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("getAPIRequestResponseForIdColumnValidationForAircraftReadAccessRole")]
        public void ValidateGetCrudAPIAircraftTypeByIdWithAircraftReadRoleAccessAndValidateTheFetchedDetailsForTheColumn()
        {
            string[] tagsOfScenario = new string[] {
                    "CrudAPIRoleBasedAuthorizationTests",
                    "getAPIRequestResponseForIdColumnValidationForAircraftReadAccessRole"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate Get Crud API Aircraft Type by Id with Aircraft Read Role Access and vali" +
                    "date the fetched details for the column", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 46
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 47
    testRunner.Then("Establish Database Connection While Executing SQL Query \"fetchAircraftDetailsWith" +
                        "AscendingSiteId\" and \"SQLConstants_AircraftTypeAndAirline\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 48
    testRunner.And("Generate JWT Authorization Token for User with with Aircraft Read Access and user" +
                        " to not have Aircraft Write Access for Environment Info stored in ConfigSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 49
    testRunner.And("Generate new Get aircraft type by \"Id\" API URL and Execute Crud Get By Id request" +
                        " and Set DTO Objects for Crud API Aircraft Type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 50
    testRunner.And("Compare values from API response set to DB record set for a single returned recor" +
                        "d for Crud Aircraft Type API", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion