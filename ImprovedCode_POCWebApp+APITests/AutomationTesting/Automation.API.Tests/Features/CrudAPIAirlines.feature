Feature: CrudAPIAirlines

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting 

@CrudAPIAirlines @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Airlines and validate the response with DB record set
    Then Execute Crud Airline API And Set DTO Objects for Crud API Airline for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for Crud Airline API

@CrudAPIAirlines @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Airlines and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post Airline API and Set DTO Objects for Crud API Airline 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for a single returned record for Crud Airline API 
    Then again Execute Crud Post Airline API with a duplicate Iata value and verify that it should return error via API for Crud Airline API 
    And Generate new Delete airline API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Airline

@CrudAPIAirlines @getAPIRequestResponseForNameColumnValidation
Scenario: Validate Get Crud API Airline by Name column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAirlineByName" using "Name" in the API URL and Execute Get By "name" API request and Set DTO Objects for Crud API Airline 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForNameAirline" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for Crud Airline API
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAirlineByName" using "Name" in the API URL and Execute Get By "name" API request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body for Crud API Airline 

@CrudAPIAirlines @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Airlines by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Get airline by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Airline 
    And Compare values from API response set to DB record set for a single returned record for Crud Airline API 
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistAirline" and "SQLConstants_AircraftTypeAndAirline"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Airline 
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Get airline by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Airline 
   
@CrudAPIAirlines @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Airlines and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response  
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put airline API URL using database fetched values and Execute Crud Put API Airline and Set DTO Objects for Crud API Airline
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for a single returned record for Crud Airline API 

@CrudAPIAirlines @deleteAPIRequestResponseValidation
Scenario: Validate The Delete Crud API Airlines for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAirlineTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Delete airline API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Airline
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Airline

@CrudAPIAirlines @getAPIRequestResponseForIataColumnValidation
Scenario: Validate Get Crud API Airline by Iata column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAirlineByIata" using "Iata" in the API URL and Execute Crud Get By "iata" request and Set DTO Objects for Crud API Airline 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForIataAirline" and "SQLConstants_AircraftTypeAndAirline" 
    And Compare values from API response set to DB record set for a single returned record for Crud Airline API 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAirlineTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAirlineByIata" using "Iata" in the API URL and Execute Crud Get By "iata" request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body for Crud API Airline 
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" from Database returned from above sql Query
    And Establish Database Connection While Executing SQL Query "deleteRecentAddedRowAirlineTable" and "SQLConstants_AircraftTypeAndAirline"

@CrudAPIAirlines @getAPIRequestResponseForIcaoColumnValidation
Scenario: Validate Get Crud API Airline by Icao column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAirlineByIcao" using "Icao" in the API URL and Execute Crud Get By "icao" request and Set DTO Objects for Crud API Airline 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForIcaoAirline" and "SQLConstants_AircraftTypeAndAirline" 
    And Compare values from API response set to DB record set for a single returned record for Crud Airline API
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAirlineTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAirlineByIcao" using "Icao" in the API URL and Execute Crud Get By "icao" request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body for Crud API Airline  
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" from Database returned from above sql Query
    And Establish Database Connection While Executing SQL Query "deleteRecentAddedRowAirlineTable" and "SQLConstants_AircraftTypeAndAirline"

@CrudAPIAirlines @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Airline for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud Airline API And Set DTO Objects for Crud API Airline
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDataForAllSiteClaims" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for Crud Airline API
   
@CrudAPIAirlines @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Airline and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Airline API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIAirlines @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Airline with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new Get Airline by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body

@CrudAPIAirlines @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate The Delete Crud API Airline with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAirlineTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Delete Airline API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body 
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting  
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Delete airline API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Airline 

@CrudAPIAirlines @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API Airline insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post Airline API using different Site id than what is being used in the token and validate that the api should return error in the response body 
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Post Airline API and Set DTO Objects for Crud API Airline
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAirlineData" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Delete airline API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Airline 

@CrudAPIAirlines @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Airline update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchAirlineDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Airline API URL using different Site id than what is being used in the token and validate that the api should return error in the response body 
