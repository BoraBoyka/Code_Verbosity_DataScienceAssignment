Feature: CrudAPIRunways

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIRunways @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Runways and validate the response with DB record set
    Then Execute Crud Runway API And Set DTO Objects for Crud API Runway for site claim "MGL"
    Then Establish Database Connection While Executing SQL Query "fetchRunwayData" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for Crud Runway API

@CrudAPIRunways @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Runways by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    And Generate new Get runway by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Runway
    And Compare values from API response set to DB record set for a single returned record for Crud Runway API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistRunway" and "SQLConstants_StandStandAreaRunway"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Runway
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetailsWithSiteId" and "SQLConstants_StandStandAreaRunway"
    And Generate new Get runway by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Runway 
   
@CrudAPIRunways @getAPIRequestResponseForNameColumnValidation
Scenario: Validate Get Crud API Runways by Name column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    And Generate new "GetRunwayByName" using "Name" in the API URL and Execute Get By "name" API request and Set DTO Objects for Crud API Runway
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForNameRunway" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for Crud Runway API
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetailsWithSiteId" and "SQLConstants_StandStandAreaRunway"
    And Generate new "GetRunwayByName" using "Name" in the API URL and Execute Get By "name" API request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Runway 
   
@CrudAPIRunways @deleteAPIRequestResponseValidation
Scenario: Validate Runway Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB
Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordRunwayTable" and "SQLConstants_StandStandAreaRunway"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayData" and "SQLConstants_StandStandAreaRunway"
    And Generate new Delete runway API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Runway
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedRunwayData" and "SQLConstants_StandStandAreaRunway"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Runway

@CrudAPIRunways @postAPIRequestResponseValidation
Scenario: Validate Crud API Post Runways and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post Runway API and Set DTO Objects for Crud API Runway
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for a single returned record for Crud Runway API
    And Generate new Delete runway API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Runway

@CrudAPIRunways @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Runways and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    Then Generate new put runway API URL using database fetched values and Execute Crud Put API Runway and Set DTO Objects for Crud API Runway
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayDataForSiteIdAsc" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for a single returned record for Crud Runway API

@CrudAPIRunways @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Runways for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud Runway API And Set DTO Objects for Crud API Runway
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDataForAllSiteClaims" and "SQLConstants_StandStandAreaRunway"
    And Compare values from API response set to DB record set for Crud Runway API

@CrudAPIRunways @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Runways and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Runway API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIRunways @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Runways with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new Get Runway by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body

@CrudAPIRunways @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Runway Delete Crud API with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordRunwayTable" and "SQLConstants_StandStandAreaRunway"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayData" and "SQLConstants_StandStandAreaRunway"
    And Generate new Delete Runway API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting  
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayData" and "SQLConstants_StandStandAreaRunway"
    And Generate new Delete runway API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Runway

@CrudAPIRunways @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API Runways insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post Runway API using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Post Runway API and Set DTO Objects for Crud API Runway
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayData" and "SQLConstants_StandStandAreaRunway"
    And Generate new Delete runway API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Runway

@CrudAPIRunways @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Runways update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetailsWithSiteId" and "SQLConstants_StandStandAreaRunway"
    Then Generate new put Runway API URL using different Site id than what is being used in the token and validate that the api should return error in the response body


