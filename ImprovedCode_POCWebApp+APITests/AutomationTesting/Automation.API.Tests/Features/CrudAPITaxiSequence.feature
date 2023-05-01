Feature: CrudAPITaxiSequence

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPITaxiSequence @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Taxi Sequence and validate the response with DB record set
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdSpecificValue" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRunwayId" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordTaxiSequenceTableForNullLineUpId" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Taxi Sequence API And Set DTO Objects for Crud API Taxi Sequence for site claim "MGL"
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceData" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for Crud Taxi Sequence API
    And Generate new Delete Taxi Sequence API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Taxi Sequence

@CrudAPITaxiSequence @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Taxi Sequence by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceDataForSiteIdAsc" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new Get Taxi Sequence by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Taxi Sequence
    And Compare values from API response set to DB record set for a single returned record for Crud Taxi Sequence API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistTaxiSequence" and "SQLConstants_TaxiSequenceAndSite"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Taxi Sequence
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceDetailsWithSiteId" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new Get Taxi Sequence by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Taxi Sequence 

@CrudAPITaxiSequence @getAPIRequestResponseForRunwayAndStandColumnValidation
Scenario: Validate Get Crud API Taxi Sequence by Runway and Stand column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "deleteTaxiStandsData" and "SQLConstants_TaxiSequenceAndSite"
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchStandAreaId" and "SQLConstants_StandStandAreaRunway" 
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetails" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandDataForStandAreaId" and "SQLConstants_StandStandAreaRunway"
    Then Execute Crud Post Taxi Sequence API and Set DTO Objects for Crud API Taxi Sequence
    Then Establish Database Connection While Executing SQL Query "fetchRunwayNameForTaxiSequence" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "RunwayName" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchStandNameForTaxiSequence" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "StandName" from Database returned from Query Response 
    And Generate new "GetTaxiSequenceByRunwayAndStand" API URL and Execute Crud Get By "runwayName" and "standName" request and Set DTO Objects for Crud API Taxi Sequence
    Then Establish Database Connection While Executing SQL Query "fetchStandIdForTaxiSequence" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "StandId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceDataWithRunwayAndStandId" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for a single returned record for Crud Taxi Sequence API
    And Generate new Delete Taxi Sequence API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Taxi Sequence

@CrudAPITaxiSequence @deleteAPIRequestResponseValidation
Scenario: Validate Delete Taxi Sequence Crud API for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRunwayId" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceId" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" against Taxi Sequence Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordTaxiSequenceTable" and "SQLConstants_TaxiSequenceAndSite"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceDataForSiteIdAsc" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new Delete Taxi Sequence API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Taxi Sequence
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedTaxiSequenceData" and "SQLConstants_TaxiSequenceAndSite"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Taxi Sequence

@CrudAPITaxiSequence @postAPIRequestResponseValidation
Scenario: Validate Crud API Post Taxi Sequence and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "deleteTaxiStandsData" and "SQLConstants_TaxiSequenceAndSite"
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchStandAreaId" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetails" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedStandDataForStandAreaId" and "SQLConstants_StandStandAreaRunway"
    Then Execute Crud Post Taxi Sequence API and Set DTO Objects for Crud API Taxi Sequence
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceDataForSiteIdAsc" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for a single returned record for Crud Taxi Sequence API
    And Generate new Delete Taxi Sequence API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Taxi Sequence
    Then Establish Database Connection While Executing SQL Query "fetchDistinctStandData" and "SQLConstants_StandStandAreaRunway"
    Then again Execute Crud Post Taxi Sequence API with a duplicate Name value and verify that it should return error via API for Crud Taxi Sequence API
    Then again Execute Crud Post Taxi Sequence API with a 0 LineUpId value and verify that it should return error via API for Crud Taxi Sequence API
    Then Establish Database Connection While Executing SQL Query "deleteTaxiStandsData" and "SQLConstants_TaxiSequenceAndSite"
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceIdForRunwayAndSite" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordTaxiStandsTable" and "SQLConstants_TaxiSequenceAndSite"
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetails" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then again Execute Crud Post Taxi Sequence API with the same runway and stand id and verify that it should return error via API for Crud Taxi Sequence API
    Then Establish Database Connection While Executing SQL Query "deleteTaxiStandsData" and "SQLConstants_TaxiSequenceAndSite"

@CrudAPITaxiSequence @putAPIRequestResponseValidation
Scenario: Validate Crud API Put Taxi Sequence and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceDataForSiteIdAsc" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new put Taxi Sequence API URL using database fetched values and Execute Crud Put API Taxi Sequence and Set DTO Objects for Crud API Taxi Sequence
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceDataForSiteIdAsc" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for a single returned record for Crud Taxi Sequence API
    
@CrudAPITaxiSequence @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Taxi Sequence for all the site claims that user has access to and validate the response with DB record set
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRunwayId" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordTaxiSequenceTableForNullLineUpId" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Taxi Sequence API And Set DTO Objects for Crud API Taxi Sequence 
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceDataForAllSiteClaims" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for Crud Taxi Sequence API
    And Generate new Delete Taxi Sequence API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Taxi Sequence

@CrudAPITaxiSequence @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Taxi Sequence and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Taxi Sequence API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPITaxiSequence @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Taxi Sequence with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new Get Taxi Sequence by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body

@CrudAPITaxiSequence @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Taxi Sequence Delete Crud API with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRunwayId" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceId" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" against Taxi Sequence Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordTaxiSequenceTable" and "SQLConstants_TaxiSequenceAndSite"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceData" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new Delete Taxi Sequence API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting  
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceData" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new Delete Taxi Sequence API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Taxi Sequence

@CrudAPITaxiSequence @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Crud API Post Taxi Sequence insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRunwayId" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceId" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" against Taxi Sequence Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordTaxiSequenceTable" and "SQLConstants_TaxiSequenceAndSite"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceData" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post Taxi Sequence API using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTaxiSequenceData" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new Delete Taxi Sequence API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Taxi Sequence

@CrudAPITaxiSequence @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Crud API Put Taxi Sequence update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchTaxiSequenceDetailsWithSiteId" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new put Taxi Sequence API URL using different Site id than what is being used in the token and validate that the api should return error in the response body


