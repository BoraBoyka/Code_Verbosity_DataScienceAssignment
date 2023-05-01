Feature: CrudArrivalFlightsAPI

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIArrivalFlights @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Arrival Flight and validate the response with DB record set
    Then Execute Crud Arrival Flight API And Set DTO Objects for Crud API Arrival Flight for site claim "MGL"
    Then Establish Database Connection While Executing SQL Query "fetchArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Flight API

@CrudAPIArrivalFlights @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Arrival Flight by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightDataForSiteIdAsc" and "SQLConstants_ArrivalFlight"
    And Generate new Get Arrival flight by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Arrival Flight
    And Compare values from API response set to DB record set for a single returned record for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistArrivalFlight" and "SQLConstants_ArrivalFlight"
    Then Validate the GET API operation by "arrivalFlightId" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchArrivalFlightDetailsWithDesSiteId" and "SQLConstants_ArrivalFlight"
    And Generate new Get Arrival flight by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Arrival Flight 

@CrudAPIArrivalFlights @getAPIRequestResponseForFlightsByOperationalDateColumnValidation
Scenario: Validate Get Crud Arrival API Flight by FlightsByOperationalDate column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "updateOperDateArrFlightTable" and "SQLConstants_ArrivalFlight"
    Then Generate new get "GetByOperationDate" using "opDate" with values "2023-01-28" and Execute Crud Get By OperationalDate request and Set DTO Objects for Crud Arrival Flight API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForOpDateArrFlight" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "updateOperDateArrFlight" and "SQLConstants_ArrivalFlight"
    
@CrudAPIArrivalFlights @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Arrival Flight for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud Arrival Flight API And Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchArrFlightDataForAllSiteClaims" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Flight API

@CrudAPIArrivalFlights @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Arrival Flight Get Crud API and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "deleteRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteZRHForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "addDataArrivalFlightTable" and "SQLConstants_ArrivalFlight"
    Then Fetch value for site "MGL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteMGLForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "addDataArrivalFlightTable" and "SQLConstants_ArrivalFlight"
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteTXLForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "addDataArrivalFlightTable" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Arrival Flight API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIArrivalFlights @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Arrival Flight with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightDataWithSiteId" and "SQLConstants_ArrivalFlight"
    Then Generate new Get Arrival Flight by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the Arrival api should return error in the response body

@CrudAPIArrivalFlights @deleteAPIRequestResponseValidation
Scenario: Validate The Arrival Flight Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Arrival Flight API and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Arrival Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIArrivalFlights @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate The Arrival Flight Delete Crud API with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchArrivalFlightDetailsWithDesSiteId" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival Flight API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIArrivalFlights @getUnmatchedFlightsAPIRequestResponseValidation
Scenario: Validate Get Crud Arrival API Unmatched Flight by FlightsByOperationDate column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "updateCallSignArrFlightTable" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "updateOperDateArrFlightTable" and "SQLConstants_ArrivalFlight"
    Then Generate new get "GetUnmatchedFlights" using "opDate" with values "2023-01-26" and Execute Crud Get By Unmatched flight request and Set DTO Objects for Crud Arrival Flight API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForUnmatchedFlight" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "updateOperDateArrFlight" and "SQLConstants_ArrivalFlight"

@CrudAPIArrivalFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Arrival Flight and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Arrival Flight API with only sending Icao value in the request body and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    Then Execute Crud Post Arrival Flight API using the same request body and verify that the response should return the recent added Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type

@CrudAPIArrivalFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Arrival Flight with partial child without passing SiteId in the request body and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Arrival Flight API with partial child without SiteId and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    Then Execute Crud Post Arrival Flight API using the same request body and verify that the response should return the recent added Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type

@CrudAPIArrivalFlights @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Arrival Flight and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Arrival Flight API with only sending Icao value in the request body and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightDataForSiteIdAsc" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Arrival Flight API URL with only sending Iata in the request body and Execute Crud Put API Arrival Flight and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightDataForSiteIdAsc" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type

@CrudAPIAddArrivalFlightList @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Add Arrival Flight List and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post "AddArrivalList" API and Set DTO Objects for Crud API Add Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "deletedArrivalFlightDataForTop2Rows" and "SQLConstants_ArrivalFlight"
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type

@CrudAPIArrivalFlights @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API Arrival Flight insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataForSiteIdDesc" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Arrival Flight API using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Post Arrival Flight API and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight

@CrudAPIArrivalFlights @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Arrival Flight update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Arrival Flight API URL using different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIArrivalFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Arrival Flight with a SiteId that user doesn't have access to and validate the record should not be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Arrival Flight API for negative test and verify that error should be thrown via API

@CrudAPIArrivalFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Arrival Flight with an incorrect SiteId in the child object and validate the record should still be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Arrival Flight API with incorrect SiteId in the child object and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Arrival Flight API
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
 
@CrudAPIArrivalFlights @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Arrival Flight with a SiteId that user doesn't have access to and validate the record should not be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightDataForSiteIdAsc" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Arrival Flight API URL for negative test and verify that error should be thrown via API

@CrudAPIArrivalFlights @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate Record Addition using Get Crud API Arrival Flight API For Audit By Id API Validation For Insert Operation
   	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Arrival Flight API and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    And Generate new Get Arrival flight by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and Set DTO Objects for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIArrivalFlights @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate Arrival Flight Record Addition using Get Crud API Arrival Flight API For Audit By Id API Validation For Update Operation
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Arrival Flight API and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightDataForSiteIdAsc" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Arrival Flight API URL using database fetched values and Execute Crud Put API Arrival Flight and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    And Generate new Get Arrival flight by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and verify the details for recent updated arrival record for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchAuditUpdateArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Audit Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIArrivalFlights @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate Record Addition using Get Crud API Arrival Flight API For Audit By Id API Validation For Delete Operation
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Arrival Flight API and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
    And Generate new Get Arrival flight by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and verify the details for recent updated arrival record for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Audit Flight API For Delete record
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIArrivalFlights @getAPIRequestResponseValidationForAuditByTimeStampAPI
Scenario: Validate Record Addition using Get Crud API Arrival Flight API For Audit By TimeStamp API Validation For Insert Operation
   	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Arrival Flight API and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceSpaceinTimestampForAudit" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceColoninTimestampForAudit" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceAddinTimestampForAudit" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "ChangeTime" from Database returned from above sql Query    
    And Generate new Get Arrival flight by "Audit" and "Time" using "timestamp" with value fetched from DB response and Set DTO Objects for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type  

@CrudAPIArrivalFlights @getAPIRequestResponseValidationForAuditByTimeStampAPI
Scenario: Validate Record Addition using Get Crud API Arrival Flight API For Audit By TimeStamp API Validation For Update Operation
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Arrival Flight API and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceSpaceinTimestampForAudit" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceColoninTimestampForAudit" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceAddinTimestampForAudit" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "ChangeTime" from Database returned from above sql Query       
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightDataForSiteIdAsc" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Arrival Flight API URL using database fetched values and Execute Crud Put API Arrival Flight and Set DTO Objects for Crud API Arrival Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightDataForSiteIdAsc" and "SQLConstants_ArrivalFlight"
    And Generate new Get Arrival flight by "Audit" and "Time" using "timestamp" with value fetched in above step and verify the details for recent updated arrival record for Crud Arrival Flight API
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditUpdateArrivalDetails" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Audit Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedArrivalFlightData" and "SQLConstants_ArrivalFlight"
    And Generate new Delete Arrival flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Arrival Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type  

@CrudAPIArrivalFlights @getAPIRequestResponseForArrivalUnmatchedFlightsValidation
Scenario: Validate Get Crud UnmatchedFlightPlan API for Arrival Unmatched Flights and validate the fetched details for the column
    Then Generate new get "ArrivalUnmatchedFlights" using "opDate" with values "2023-01-26" and Execute Crud Get All Arrival Unmatched Flights request and Set DTO Objects for Crud Unmatched FlightPlan API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForArrivalWithNoFlightPlan" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud Arrival Flight API

@CrudAPIArrivalFlights @patchAPIRequestResponseValidationForArrivalFlights
Scenario: Validate The Patch Crud API Unmatched Flight Plan API and update the flight plan body based on Arrival Flight plan Id and Operation date and validate the record gets updated into Arrival DB
	Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanIdOperationDateArrivalTable" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Generate new patch Unmatched Flight Plan API URL using fetched values and Execute Crud Patch Unmatched Flight Plan API and validate that the record in Arrival table is updated
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"
    Then Generate new Delete "FlightPlanid" using "flightPlanid" with value "AT08998877" and "opDate" with value "2022-05-06" and Execute Crud Delete flight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API 
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIArrivalFlights @patchAPIRequestResponseValidationForArrivalFlights
Scenario: Validate The Patch Crud API Unmatched Flight Plan API and update the flight plan body based on Arrival Flight plan Id and Operation date and validate the record gets updated into Arrival DB and also validate that the record should be deleted from unmatched flight plan
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanIdOperationDateArrivalTableWithUnmatchedData" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Generate new patch Unmatched Flight Plan API URL using fetched values and Execute Crud Patch Unmatched Flight Plan API and validate that the record in Arrival table is updated and the same in unmatched table is deleted
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteIdWithFieldValueNull" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIArrivalFlights @putAPIRequestResponseValidationForArrivalFlights
Scenario: Validate The Put Crud API Unmatched Flight Plan API and update the flight plan body based on combination of Arrival Site && Carrier && FlightNumber &&  OperationDate && Origin and validate the record gets updated into Arrival DB and should be deleted from unmatched flight plan
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"   
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "updateCarrierFlightNumOpdateOriginArrivalTableWithUnmatchedData" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Generate new put Unmatched Flight Plan API URL using fetched values and Execute Crud Put Unmatched Flight Plan API and validate that the record in Arrival table is updated and the same in unmatched table is deleted
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteIdWithFieldValueNull" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsFieldValues" and "SQLConstants_ArrivalFlight"
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIArrivalFlights @postAPIRequestResponseValidationForArrivalFlights
Scenario: Validate The Post Crud Unmatched Flight Plan API without a Callsign value in the request body and update the flight plan body based on Arrival Flight plan Id and Operation date and Callsign value and validate the record gets updated into Arrival DB and also validate that it should be deleted from unmatched flight plan
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanIdOpDateCallsignNullArrivalTableWithUnmatchedData" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Generate new post Unmatched Flight Plan API URL using fetched values and Execute Crud Post Unmatched Flight Plan API and validate that the record in Arrival table is updated with the CallSign and the same in unmatched table is deleted
    Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteIdWithFieldValueNull" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

