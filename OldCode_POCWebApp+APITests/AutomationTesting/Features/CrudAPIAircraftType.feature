Feature: CrudAPIAircraftType

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
And Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIAircraftType @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Aircraft Type and validate the response with DB record set
    Then Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeData"
    And Compare values from API response set to DB record set for Crud Aircraft Type API

@CrudAPIAircraftType @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Aircraft Type and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API
    Then again Execute Crud Post Aircraft Type API with a duplicate Iata value and verify that it should return error via API for Crud Aircraft Type API

@CrudAPIAircraftType @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Aircraft Type and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    Then Generate new put aircraft type API URL using database fetched values and Execute Crud Put API Aircraft Type and Set DTO Objects for Crud API Aircraft Type
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API
 
@CrudAPIAircraftType @deleteAPIRequestResponseValidation
Scenario: Validate Delete Crud API Aircraft Type for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedAircraftTypeData"
    And Validate that DB record set for the deleted row from API to not exist in DB
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Aircraft Type

@CrudAPIAircraftType @getAPIRequestResponseForIataColumnValidation
Scenario: Validate Get Crud API Aircraft Type by Iata column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new "GetAircraftTypeByIata" using "IATA" in the API URL and Execute Crud Get By "iata" request and Set DTO Objects for Crud API Aircraft Type
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForIATAAircraftType" 
    And Compare values from API response set to DB record set for Crud Aircraft Type API

@CrudAPIAircraftType @getAPIRequestResponseForIcaoColumnValidation
Scenario: Validate Get Crud API Aircraft Type by Icao column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new "GetAircraftTypeByIcao" using "ICAO" in the API URL and Execute Crud Get By "icao" request and Set DTO Objects for Crud API Aircraft Type
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForICAOAircraftType" 
    And Compare values from API response set to DB record set for Crud Aircraft Type API
  
@CrudAPIAircraftType @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Aircraft Type by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new Get aircraft type by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Aircraft Type
    And Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistAircraftType"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Aircraft Type

@CrudAPIAircraftType @getAPIRequestResponseForTypeNameColumnValidation
Scenario: Validate Get Crud API Aircraft Type by TypeName column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new "GetAircraftTypeByName" using "TypeName" in the API URL and Execute Get By "name" API request and Set DTO Objects for Crud API Aircraft Type
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForTypeNameAircraftType" 
    And Compare values from API response set to DB record set for Crud Aircraft Type API
    
@CrudAPIAircraftType @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Aircraft Type and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite"
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Execute Crud Aircraft Type API with an incorrect Site id and validate that the api should return error in the response body

@CrudAPIAircraftType @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Aircraft Type with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId"
    And Generate new Get aircraft type by "Id" API URL using different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIAircraftType @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Delete Crud API Aircraft Type with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new Delete aircraft type API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending"
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type
    
@CrudAPIAircraftType @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API Aircraft Type insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending"
    Then Execute Crud Post Aircraft Type API using different Site id than what is being used in the token and validate that the api should return error in the response body
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type

@CrudAPIAircraftType @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Aircraft Type update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId"
    Then Generate new put aircraft type API URL using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending"
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type



   
