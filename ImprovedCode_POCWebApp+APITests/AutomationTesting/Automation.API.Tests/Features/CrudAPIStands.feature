Feature: CrudAPIStands

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIStands @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Stands and validate the response with DB record set
    Then Execute Crud Stand API And Set DTO Objects for Crud API Stand for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchStandData" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for Crud Stand API

@CrudAPIStands @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Stands by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    And Generate new Get Stand by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Stand
    And Compare values from API response set to DB record set for a single returned record for Crud Stand API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistStand" and "SQLConstants_StandStandAreaRunway"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Stand
    Then Establish Database Connection While Executing SQL Query "fetchStandDetailsWithSiteId" and "SQLConstants_StandStandAreaRunway"
    And Generate new Get Stand by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Stand 

@CrudAPIStands @getAPIRequestResponseForStandsByNameColumnValidation
Scenario: Validate Get Crud API Stand by StandsByName column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    And Generate new "GetStandByName" using "Name" in the API URL and Execute Crud Get By "standName" request and Set DTO Objects for Crud API Stand
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForStandName" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for Crud Stand API
    Then Establish Database Connection While Executing SQL Query "fetchStandDetailsWithSiteId" and "SQLConstants_StandStandAreaRunway"
    And Generate new "GetStandByName" using "Name" in the API URL and Execute Crud Get By "standName" request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Stand 

@CrudAPIStands @deleteAPIRequestResponseValidation
Scenario: Validate Stand Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchStandAreaId" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordStandTable" and "SQLConstants_StandStandAreaRunway"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandData" and "SQLConstants_StandStandAreaRunway"
    And Generate new Delete Stand API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Stand
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedStandData" and "SQLConstants_StandStandAreaRunway"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Stand

@CrudAPIStands @postAPIRequestResponseValidation
Scenario: Validate Crud API Post Stand and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandAreaData" and "SQLConstants_StandStandAreaRunway"
    Then Execute Crud Post Stand API and Set DTO Objects for Crud API Stand
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandAreaData" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandData" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for a single returned record for Crud Stand API
    Then again Execute Crud Post Stand API with a duplicate Name value and verify that it should return error via API for Crud Stand API
    And Generate new Delete Stand API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Stand
 
@CrudAPIStands @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Stand and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
     And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchStandAreaDetailsWithAscendingSiteId" and "SQLConstants_StandStandAreaRunway"
    Then Generate new put Stand API URL using database fetched values and Execute Crud Put API Stand and Set DTO Objects for Crud API Stand
    Then Establish Database Connection While Executing SQL Query "fetchStandData" and "SQLConstants_StandStandAreaRunway"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for a single returned record for Crud Stand API

@CrudAPIStands @postAPIAddStandsRequestResponseValidation
Scenario: Validate Post Crud API Add Stands List and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandAreaData" and "SQLConstants_StandStandAreaRunway"
    Then Execute Crud Post "AddStands" API and Set DTO Objects for Crud API Add Stands
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoStandData" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for Crud Stand API
    Then Establish Database Connection While Executing SQL Query "deletedStandDataForTop2Rows" and "SQLConstants_StandStandAreaRunway"

@CrudAPIStands @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Stands for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud Stand API And Set DTO Objects for Crud API Stand 
    Then Establish Database Connection While Executing SQL Query "fetchStandDataForAllSiteClaims" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for Crud Stand API

@CrudAPIStands @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Stand and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Stand API with an incorrect Site id  for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIStands @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Stand with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new Get Stand by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body

@CrudAPIStands @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate The Stand Delete Crud API with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandAreaDataForSiteIdDesc" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordStandTable" and "SQLConstants_StandStandAreaRunway"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandData" and "SQLConstants_StandStandAreaRunway"
    And Generate new Delete Stand API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandData" and "SQLConstants_StandStandAreaRunway"
    And Generate new Delete Stand API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Stand

@CrudAPIStands @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API Stands insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandAreaDataForSiteIdDesc" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordStandTable" and "SQLConstants_StandStandAreaRunway"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandData" and "SQLConstants_StandStandAreaRunway"
    Then Execute Crud Post Stand API using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Post Stand API and Set DTO Objects for Crud API Stand
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandData" and "SQLConstants_StandStandAreaRunway"
    And Generate new Delete Stand API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Stand

@CrudAPIStands @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Stands update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandData" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchStandAreaDetailsWithSiteId" and "SQLConstants_StandStandAreaRunway"
    Then Generate new put Stand API URL using different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIStands @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Stands with a SiteId different from what stand area table has and validate the record should not be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchStandAreaDetailsWithSiteId" and "SQLConstants_StandStandAreaRunway"
    Then Execute Crud Post Stand API for negative test and verify that error should be thrown via API

@CrudAPIStands @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Stands with a SiteId different from what stand area table has and validate the record should not be updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchStandAreaDetailsWithSiteId" and "SQLConstants_StandStandAreaRunway"
    Then Generate new put Stand API URL for negative test and verify that error should be thrown via API
