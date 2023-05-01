Feature: CrudAPIAircraftType

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIAircraftType @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Aircraft Type and validate the response with DB record set
    Then Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for Crud Aircraft Type API

@CrudAPIAircraftType @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Aircraft Type and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API
    Then again Execute Crud Post Aircraft Type API with a duplicate Iata value and verify that it should return error via API for Crud Aircraft Type API 
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIAircraftType @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Aircraft Type and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put aircraft type API URL using database fetched values and Execute Crud Put API Aircraft Type and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API 
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIAircraftType @deleteAPIRequestResponseValidation
Scenario: Validate Delete Crud API Aircraft Type for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Aircraft Type 

@CrudAPIAircraftType @getAPIRequestResponseForIataColumnValidation
Scenario: Validate Get Crud API Aircraft Type by Iata column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response  
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteIdForIATA" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAircraftTypeByIata" using "IATA" in the API URL and Execute Crud Get By "iata" request and Set DTO Objects for Crud API Aircraft Type
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForIATAAircraftType" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" from Database returned from above sql Query
    And Compare values from API response set to DB record set for Crud Aircraft Type API 
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAircraftTypeByIata" using "IATA" in the API URL and Execute Crud Get By "iata" request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body 
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" from Database returned from above sql Query
    And Establish Database Connection While Executing SQL Query "deleteRecentAddedRowAircraftTable" and "SQLConstants_AircraftTypeAndAirline"

@CrudAPIAircraftType @getAPIRequestResponseForIcaoColumnValidation
Scenario: Validate Get Crud API Aircraft Type by Icao column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAircraftTypeByIcao" using "ICAO" in the API URL and Execute Crud Get By "icao" request and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForICAOAircraftType" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for Crud Aircraft Type API
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAircraftTypeByIcao" using "ICAO" in the API URL and Execute Crud Get By "icao" request for a different Site Id than what is being used in the token and validate that the api should return No Content in the response body 
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" from Database returned from above sql Query
    And Establish Database Connection While Executing SQL Query "deleteRecentAddedRowAircraftTable" and "SQLConstants_AircraftTypeAndAirline"

@CrudAPIAircraftType @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Aircraft Type by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Get aircraft type by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Aircraft Type 
    And Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API 
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistAircraftType" and "SQLConstants_AircraftTypeAndAirline"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Get aircraft type by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body 

@CrudAPIAircraftType @getAPIRequestResponseForTypeNameColumnValidation
Scenario: Validate Get Crud API Aircraft Type by TypeName column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAircraftTypeByName" using "TypeName" in the API URL and Execute Get By "name" API request and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForTypeNameAircraftType" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for Crud Aircraft Type API
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new "GetAircraftTypeByName" using "TypeName" in the API URL and Execute Get By "name" API request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body 
    
@CrudAPIAircraftType @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Aircraft Type for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForAllSiteClaims" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for Crud Aircraft Type API
   
@CrudAPIAircraftType @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Aircraft Type and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Aircraft Type API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body 
   
@CrudAPIAircraftType @getAPIRequestNoSiteIdValidation
Scenario: Validate Get Crud API Aircraft Type and pass No siteId in the request header and user have access to only Bad site and validate that the api should return empty list in the response
    Then Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType and validate that the api should return empty list in the response body 

@CrudAPIAircraftType @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Crud API Get Aircraft Type and pass site claim in the token and validate that the api should only return response for that particular site
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType for site claim "TXL"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForTXLSite" and "SQLConstants_AircraftTypeAndAirline"
    And Compare values from API response set to DB record set for Crud Aircraft Type API

@CrudAPIAircraftType @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Aircraft Type with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new Get aircraft type by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body 

@CrudAPIAircraftType @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate The Delete Crud API Aircraft Type with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    And Generate new Delete aircraft type API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" from Database returned from above sql Query
    And Establish Database Connection While Executing SQL Query "deleteRecentAddedRowAircraftTable" and "SQLConstants_AircraftTypeAndAirline"

@CrudAPIAircraftType @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Crud API Post Aircraft Type insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API using different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIAircraftType @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Aircraft Type update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put aircraft type API URL using different Site id than what is being used in the token and validate that the api should return error in the response body 

@CrudAPIAircraftType @getAPIRequestNoSiteClaimsValidation
Scenario: Validate Get Crud API Aircraft Type and pass no site claims in the token and validate that the api should only return error in the response
    Then Generate JWT Authorization Token for no Site Claims for the user for Environment Info stored in ConfigSetting 
    Then Execute Crud Aircraft Type API for "NoSiteClaim" and validate that the api should return error in the response body 

@CrudAPIAircraftType @getAPIRequestNoSiteClaimsValidation
Scenario: Validate Get Crud API Aircraft Type and pass a site claim in the token but the user doesn't have access to any sites and validate that the api should only return error in the response
    Then Generate JWT Authorization Token for no Site Claims for the user for Environment Info stored in ConfigSetting 
    Then Execute Crud Aircraft Type API for "ABC" and validate that the api should return error in the response body 

@CrudAPIAircraftType @getAPIRequestForSiteClaimsValidationWithInvalidSitePassedInTheToken
Scenario: Validate Get Crud API Aircraft Type and pass an invalid site claim in the token irrespective of user having access to valid site claims and validate that the api should only return error in the response
    Then Execute Crud Aircraft Type API using invalid Site id "BadSite" in the token and validate that the api should return error in the response body 

@CrudAPIAircraftType @getAPIRequestForSiteClaimsValidationWithASiteNotInDB
Scenario: Validate Get Crud API Aircraft Type and pass a site claim in the token that user has access to but doesn't exist in database and validate that the api should only return error in the response
    Then Generate JWT Authorization Token for Site Claims for the user that does not exist in database for Environment Info stored in ConfigSetting 
    Then Execute Crud Aircraft Type API for "XYZ" Site Claim and validate that the api should return empty list in the response body 

  




   
