using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using AventStack.ExtentReports.Gherkin.Model;
using ACDMAutomation.Shared.API.DTO_AuthAPI;
using ACDMAutomation.Shared.PageObjects;
using System.Linq;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acdm.InformationServices.Dto;
using System.Threading;
using ACDMAutomation.Shared.Hooks;

namespace ACDMAutomation.API.Tests.Steps
{
    [Binding]
    public class CrudAPITaxiSequenceSteps : CrudAPIBaseMethods<TaxiSequenceDto>
    {
        public CrudAPITaxiSequenceSteps(ScenarioContext scenarioContext, IWebDriver driver) : base(scenarioContext, driver)
        { }
        public RunwayDto Runway { get; private set; }
        public List<StandDto> Stands { get; private set; }
        public StandAreaDto StandAreaDto { get; private set; }

        [Then(@"Execute Crud Taxi Sequence API And Set DTO Objects for Crud API Taxi Sequence for site claim ""([^""]*)""")]
        public void ThenExecuteCrudTaxiSequenceAPIAndSetDTOObjectsForCrudAPITaxiSequenceForSiteClaim(string siteClaim)
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.TaxiSequenceAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup, siteClaim);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }

        [Then(@"Execute Crud Taxi Sequence API And Set DTO Objects for Crud API Taxi Sequence")]
        public void ThenExecuteCrudTaxiSequenceAPIAndSetDTOObjectsForCrudAPITaxiSequence()
        {
            var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.TaxiSequenceAPIURL);
            var dtoResult = ExecuteGetAPIWithDeserialize(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
            dtoResultList = dtoResult.OrderBy(x => x.Id).ToList();
        }
        [Then(@"Compare values from API response set to DB record set for Crud Taxi Sequence API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForCrudTaxiSequenceAPI()
        {
            ThenCompareValuesFromAPIResponseSetToDBRecordSetForAPI();
        }
        public override void APIDBComparison<T>(Dictionary<string, string> compareDictionary, T compareObject)
        {
            try
            {
                if (compareObject is TaxiSequenceDto TaxiSequenceDto)
                {
                    Assert.AreEqual(TaxiSequenceDto.Id, Int32.Parse(compareDictionary["Id"]), "StandId did not match:" + TaxiSequenceDto.Id + " != " + compareDictionary["Id"]);
                    Assert.AreEqual(TaxiSequenceDto.SiteId, Int32.Parse(compareDictionary["SiteId"]), "SiteId did not match:" + TaxiSequenceDto.SiteId + " != " + compareDictionary["SiteId"]);
                    Assert.AreEqual(TaxiSequenceDto.SequenceName, compareDictionary["SequenceName"], "SequenceName did not match:" + TaxiSequenceDto.SequenceName + " != " + compareDictionary["SequenceName"]);
                    Assert.AreEqual(TaxiSequenceDto.Runway.Id, Int32.Parse(compareDictionary["RunwayId"]), "RunwayId did not match:" + TaxiSequenceDto.Runway.Id + " != " + compareDictionary["RunwayId"]);
                    if (compareDictionary["LineUpId"] == null)
                    {
                        Assert.AreEqual(null, compareDictionary["LineUpId"], "LineUpId did not match:" + TaxiSequenceDto.LineUpId + " != " + compareDictionary["LineUpId"]);
                    }
                    else
                    {
                        Assert.AreEqual(TaxiSequenceDto.LineUpId, Int32.Parse(compareDictionary["LineUpId"]), "LineUpId did not match:" + TaxiSequenceDto.LineUpId + " != " + compareDictionary["LineUpId"]);
                    }
                }
                else
                {
                    Assert.Fail("Return values which are not taxi sequence type");
                }
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Compare_DB_API_values");
            }
        }
        [Then(@"Generate new Get Taxi Sequence by ""([^""]*)"" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Taxi Sequence")]
        public void ThenGenerateNewGetTaxiSequenceByAPIURLAndExecuteCrudGetByIdRequestAndSetDTOObjectsForCrudAPITaxiSequence(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.TaxiSequenceAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restResponse = ExecuteGetAPI(apiConfigDTO.JWT_TOKEN, "Failed_To_Execute_Get_API", restOBJSetup);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<TaxiSequenceDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_TaxiSequenceType");
            }
        }
        [Then(@"Compare values from API response set to DB record set for a single returned record for Crud Taxi Sequence API")]
        public void ThenCompareValuesFromAPIResponseSetToDBRecordSetForASingleReturnedRecordForCrudTaxiSequenceAPI()
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> taxiSequenceDetails = (Dictionary<string, string>)(resultData).ElementAt<object>(0);
            APIDBComparison(taxiSequenceDetails, dtoResultSingleRecord);
        }
        [Then(@"Validate the GET API operation by ""([^""]*)"" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Taxi Sequence")]
        public void ThenValidateTheGETAPIOperationByAPIURLAgainUsingTheIDThatDoesntExistFetchedFromDBAndItShouldReturnErrorViaAPIForCrudTaxiSequence(string colName)
        {
            ValidateIDDoesntExistErrorMessage(APITests.apiConfigDTO.TaxiSequenceAPIURL, colName, "Failed_To_Execute_Get_By_API", "No Content");
        }
        [Then(@"Generate new ""([^""]*)"" API URL and Execute Crud Get By ""([^""]*)"" and ""([^""]*)"" request and Set DTO Objects for Crud API Taxi Sequence")]
        public void ThenGenerateNewAPIURLAndExecuteCrudGetByAndRequestAndSetDTOObjectsForCrudAPITaxiSequence(string apiReqName, string colNameAPI, string colNameAPI1)
        {
            try
            {
                string valueSQLDataRunwayName = (_scenarioContext.Get<string>("valueSQLResponse"));
                string valueSQLDataStandName = (_scenarioContext.Get<string>("valueSQLData"));
                var restResponse = ExecuteGetByAPIMultiParameterRequestWithDeserialize(apiReqName, colNameAPI, colNameAPI1, APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Execute_Get_By_API_Request", valueSQLDataRunwayName, valueSQLDataStandName);
                dtoResultSingleRecord = JsonConvert.DeserializeObject<TaxiSequenceDto>(restResponse.Content);
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_TaxiSequenceAPI");
            }
        }
        [Then(@"Fetch value for field ""([^""]*)"" against Taxi Sequence Id fetched from database response to the above query")]
        public void ThenFetchValueForFieldAgainstTaxiSequenceIdFetchedFromDatabaseResponseToTheAboveQuery(string keyColName)
        {
            var resultData = _scenarioContext.Get<List<Object>>("sqlResponseList");
            Dictionary<string, string> sqlQueryDetails = (Dictionary<string, string>)resultData.ElementAt<object>(0);
            AddOrUpdateScenarioContext("outputSQLResponseData", sqlQueryDetails[keyColName]);
            string outputSQLResponseData = (_scenarioContext.Get<string>("outputSQLResponseData"));
            Console.WriteLine(outputSQLResponseData);
            if (outputSQLResponseData.Equals(null) || outputSQLResponseData.Equals(""))
            {
                Assert.IsFalse(true, "Failed_To_Fetch_FieldValue_Database");
            }
        }
        [Then(@"Generate new Delete Taxi Sequence API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Taxi Sequence")]
        public IRestResponse ThenGenerateNewDeleteTaxiSequenceAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPITaxiSequence()
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.TaxiSequenceAPIURL, "Id", "Failed_To_Execute_Get_By_API");
                IRestResponse restResponse = ExecuteDeleteAPI(restOBJSetup, "Failed_To_Update_Crud_API_Delete_TaxiSequence");
                return restResponse;
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_TaxiSequence");
                return null;
            }
        }
        [Then(@"Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Taxi Sequence")]
        public void ThenValidateTheGETAPIOperationAgainUsingTheSameDeletedIDAndItShouldReturnErrorViaAPIForCrudTaxiSequence()
        {
            ExecuteGetDeleteErrorMessageAPI(APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Get_Crud_API_Delete_ResponseCodeMessage");
        }
        [Then(@"Execute Crud Post Taxi Sequence API and Set DTO Objects for Crud API Taxi Sequence")]
        public void ThenExecuteCrudPostTaxiSequenceAPIAndSetDTOObjectsForCrudAPITaxiSequence()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int Id = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
            string Name = sqlResponseDetails["Name"];
            var postRequestBody = new TaxiSequenceDto(0, "Dummy" + (_scenarioContext.Get<string>("valueSQLData")), Runway = new RunwayDto(Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")), 1, "test") { DependencyGroup = 1 }, null, SiteId,
            Stands = new List<StandDto>() { new StandDto(Id, SiteId, Name, StandAreaDto = new StandAreaDto(1, 1, "Test")) });
            dtoResultSingleRecord = PostAPIWithDeserialize(postRequestBody, APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Update_Crud_API_Post_TaxiSequence_Configuration");
        }
        [Then(@"again Execute Crud Post Taxi Sequence API with a duplicate Name value and verify that it should return error via API for Crud Taxi Sequence API")]
        public void ThenAgainExecuteCrudPostTaxiSequenceAPIWithADuplicateNameValueAndVerifyThatItShouldReturnErrorViaAPIForCrudTaxiSequenceAPI()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int Id = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Name = sqlResponseDetails["Name"];
                var postRequestBody = new TaxiSequenceDto(0, "Test", Runway = new RunwayDto(Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")), 1, "test") { DependencyGroup = 1 }, null, SiteId,
                Stands = new List<StandDto>() { new StandDto(Id, SiteId, Name, StandAreaDto = new StandAreaDto(1, 1, "Test")) });
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Update_Crud_API_Post_TaxiSequence_Configuration");
                APIResponseErrorValidation(restResponse, "Violation of UNIQUE KEY constraint 'AK_TaxiSequence_SequenceName'. Cannot insert duplicate key in object 'resources.TaxiSequence'. The duplicate key value is (1, Test).", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_TaxiSequence");
            }
        }
        [Then(@"again Execute Crud Post Taxi Sequence API with a (.*) LineUpId value and verify that it should return error via API for Crud Taxi Sequence API")]
        public void ThenAgainExecuteCrudPostTaxiSequenceAPIWithALineUpIdValueAndVerifyThatItShouldReturnErrorViaAPIForCrudTaxiSequenceAPI(int p0)
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int Id = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Name = sqlResponseDetails["Name"];
                var postRequestBody = new TaxiSequenceDto(0, "Dummy" + (_scenarioContext.Get<string>("valueSQLData")), Runway = new RunwayDto(Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")), 1, "test") { DependencyGroup = 1 }, 0, SiteId,
                Stands = new List<StandDto>() { new StandDto(Id, SiteId, Name, StandAreaDto = new StandAreaDto(1, 1, "Test")) });
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Update_Crud_API_Post_TaxiSequence_Configuration");
                APIResponseErrorValidation(restResponse, "The INSERT statement conflicted with the FOREIGN KEY SAME TABLE constraint \"FK_TaxiSequence\". The conflict occurred in database \"acdm-data\", table \"resources.TaxiSequence\", column 'Id'.", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_TaxiSequence");
            }
        }
        [Then(@"Generate new put Taxi Sequence API URL using database fetched values and Execute Crud Put API Taxi Sequence and Set DTO Objects for Crud API Taxi Sequence")]
        public void ThenGenerateNewPutTaxiSequenceAPIURLUsingDatabaseFetchedValuesAndExecuteCrudPutAPITaxiSequenceAndSetDTOObjectsForCrudAPITaxiSequence()
        {
            Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
            int Id = Int32.Parse(sqlResponseDetails["Id"]);
            int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
            string Name = sqlResponseDetails["SequenceName"];
            int runwayId = Int32.Parse(sqlResponseDetails["RunwayId"]);

            var putRequestBody = new TaxiSequenceDto(Id, Name, Runway = new RunwayDto(runwayId, 1, "test") { DependencyGroup = 1 }, null, SiteId,
            Stands = new List<StandDto>() { });
            Thread.Sleep(200);
            dtoResultSingleRecord = PutAPIWithDeserialize(putRequestBody, APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Update_Crud_API_Put_TaxiSequence_Configuration");
        }
        [Then(@"Execute Crud Taxi Sequence API with an incorrect Site id for site claim ""([^""]*)"" and validate that the api should return error in the response body")]
        public void ThenExecuteCrudTaxiSequenceAPIWithAnIncorrectSiteIdForSiteClaimAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string siteClaim)
        {
            try
            {
                var restOBJSetup = RestAPICommonMethods.SetURL(APITests.apiConfigDTO.BASE_URL, APITests.apiConfigDTO.TaxiSequenceAPIURL);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "SelectedSite : " + siteClaim + " not exist in the database. BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_InvalidSiteId_Crud_API_TaxiSequence_MessageVerification");
            }
        }
        [Then(@"Generate new Get Taxi Sequence by ""([^""]*)"" API URL using different Site id ""([^""]*)"" than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewGetTaxiSequenceByAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody(string colName, string siteClaim)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.TaxiSequenceAPIURL, colName, "Failed_To_Execute_Get_By_API");
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN, siteClaim);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User does not have an access to the SelectedSite : " + siteClaim + ". BadRequest", "MessageCode", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Get_DifferentSiteId_Crud_API_TaxiSequence_MessageVerification");
            }
        }
        [Then(@"Generate new Delete Taxi Sequence API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewDeleteTaxiSequenceAPIURLWithAnDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                IRestResponse restResponse = ThenGenerateNewDeleteTaxiSequenceAPIURLAndExecuteCrudDeleteAPIRequestAndSetDTOObjectsForCrudAPITaxiSequence();
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                ResponseErrorMessageVerification(restResponse, "No Site Access for TaxiSequence Id: " + fetchedIDValueAPI + ". No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Delete_TaxiSequence");
            }
        }
        [Then(@"Execute Crud Post Taxi Sequence API using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenExecuteCrudPostTaxiSequenceAPIUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int Id = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Name = sqlResponseDetails["SequenceName"];
                var postRequestBody = new TaxiSequenceDto(0, "Dummy" + (_scenarioContext.Get<string>("valueSQLData")), Runway = new RunwayDto(1, 1, "test") { DependencyGroup = 1 }, 1, SiteId,
                Stands = new List<StandDto>() { new StandDto(Id, SiteId, Name, StandAreaDto = new StandAreaDto(1, 1, "Test")) });
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Update_Crud_API_Post_TaxiSequence_Configuration");
                dtoResultSingleRecord = JsonConvert.DeserializeObject<TaxiSequenceDto>(restResponse.Content);
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Post_API_DifferentSiteId");
            }
        }
        [Then(@"Generate new put Taxi Sequence API URL using different Site id than what is being used in the token and validate that the api should return error in the response body")]
        public void ThenGenerateNewPutTaxiSequenceAPIURLUsingDifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBody()
        {
            try
            {
                Dictionary<string, string> sqlResponseDetails = GetSQLResponseDetails();
                int Id = Int32.Parse(sqlResponseDetails["Id"]);
                int SiteId = Int32.Parse(sqlResponseDetails["SiteId"]);
                string Name = sqlResponseDetails["SequenceName"];

                var putRequestBody = new TaxiSequenceDto(Id, Name, Runway = new RunwayDto(1, 1, "test") { DependencyGroup = 1 }, null, SiteId,
                Stands = new List<StandDto>() { });
                Thread.Sleep(200);
                var restResponse = PutAPI(putRequestBody, APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Update_Crud_API_Put_TaxiSequence_Configuration");
                ResponseErrorMessageVerification(restResponse, "User has no access to the site SiteIdMismatch", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Execute_Crud_Put_API_DifferentSiteId");
            }
        }
        [Then(@"again Execute Crud Post Taxi Sequence API with the same runway and stand id and verify that it should return error via API for Crud Taxi Sequence API")]
        public void ThenAgainExecuteCrudPostTaxiSequenceAPIWithTheSameRunwayAndStandIdAndVerifyThatItShouldReturnErrorViaAPIForCrudTaxiSequenceAPI()
        {
            try
            {
                var postRequestBody = new TaxiSequenceDto(0, "Dummy" + (_scenarioContext.Get<string>("valueSQLData")), Runway = new RunwayDto(Int32.Parse(_scenarioContext.Get<string>("valueSQLResponse")), 1, "test") { DependencyGroup = 1 }, null, 1,
                Stands = new List<StandDto>() { new StandDto(21, 1, "Test34", StandAreaDto = new StandAreaDto(1, 1, "Test")) });
                var restResponse = PostAPI(postRequestBody, APITests.apiConfigDTO.TaxiSequenceAPIURL, "Failed_To_Update_Crud_API_Post_TaxiSequence_Configuration");
                APIResponseErrorValidation(restResponse, "Cannot insert duplicate key row in object 'resources.TaxiSequenceRunwayStand' with unique index 'IX_TaxiSequence_RunwayStand'. The duplicate key value is (473, 21).\nThe statement has been terminated.", "Message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Post_TaxiSequence");
            }
        }
        [Then(@"Generate new Get Taxi Sequence by ""([^""]*)"" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Taxi Sequence")]
        public void ThenGenerateNewGetTaxiSequenceByAPIURLAndExecuteCrudGetByIdRequestForADifferentSiteIdThanWhatIsBeingUsedInTheTokenAndValidateThatTheApiShouldReturnErrorInTheResponseBodyForCrudAPITaxiSequence(string colName)
        {
            try
            {
                var restOBJSetup = ExecuteGetByAPI(APITests.apiConfigDTO.TaxiSequenceAPIURL, colName, "Failed_To_Execute_Get_By_API");
                string fetchedIDValueAPI = (_scenarioContext.ContainsKey("idValueDB") ? _scenarioContext.Get<string>("idValueDB") : string.Empty);
                var restOBJRequest = restAPIUtil.CreateGetRequest(apiConfigDTO.JWT_TOKEN);
                GetResponseAndErrorMessageVerification(restOBJSetup, restOBJRequest, "User has no access to the site with taxi sequence Id: " + fetchedIDValueAPI + " No Site Access", "messageCode", "message");
            }
            catch (Exception)
            {
                Assert.IsFalse(true, "Failed_To_Update_Crud_API_Get_By_Column_Configuration_TaxiSequence");
            }
        }
    }
}
