Feature: CrudAPIDepartureFlights

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIDepartureFlights @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Departure Flight and validate the response with DB record set
    Then Execute Crud Departure Flight API And Set DTO Objects for Crud API Departure Flight for site claim "MGL"
    Then Establish Database Connection While Executing SQL Query "fetchDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Flight API

@CrudAPIDepartureFlights @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Departure Flight by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightDataForSiteIdAsc" and "SQLConstants_DepartureFlight"
    And Generate new Get Departure flight by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Departure Flight
    And Compare values from API response set to DB record set for a single returned record for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistDepartureFlight" and "SQLConstants_DepartureFlight"
    Then Validate the GET API operation by "departureFlightId" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchDepartureFlightDetailsWithDesSiteId" and "SQLConstants_DepartureFlight"
    And Generate new Get Departure flight by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Departure Flight 

@CrudAPIDepartureFlights @getAPIRequestResponseForFlightsByTobtColumnValidation
Scenario: Validate Get Crud API Departure Flight by FlightsByTobt column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "updateTobtTimeDepartureFlightTable" and "SQLConstants_DepartureFlight"
    Then Generate new get "GetByTobt" using "startDateTime" and "endDateTime" with values "2023-01-28" and "2023-01-30" and Execute Crud Get By Tobt request and Set DTO Objects for Crud Departure Flight API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForTobtDepFlight" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Flight API

@CrudAPIDepartureFlights @getAPIRequestResponseForFlightsByOperationalDateColumnValidation
Scenario: Validate Get Crud Departure API Flight by FlightsByOperationalDate column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "updateOperDateDepFlightTable" and "SQLConstants_DepartureFlight"
    Then Generate new get "GetByOperationalDate" using "opDate" with values "2023-01-28" and Execute Crud Get By OperationalDate request and Set DTO Objects for Crud Departure Flight API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForOpDateDepFlight" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "updateOperDateDepFlight" and "SQLConstants_DepartureFlight"
    
@CrudAPIDepartureFlights @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Departure Flight for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud Departure Flight API And Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchDepFlightDataForAllSiteClaims" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Flight API

@CrudAPIDepartureFlights @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Departure Flight and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "deleteRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteZRHForTestData" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "addDataDepartureFlightTable" and "SQLConstants_DepartureFlight"
    Then Fetch value for site "MGL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteMGLForTestData" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "addDataDepartureFlightTable" and "SQLConstants_DepartureFlight"
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteTXLForTestData" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "addDataDepartureFlightTable" and "SQLConstants_DepartureFlight"
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Departure Flight API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIDepartureFlights @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Departure Flight with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightDataWithSiteId" and "SQLConstants_DepartureFlight"
    Then Generate new Get Departure Flight by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the departure api should return error in the response body

@CrudAPIDepartureFlights @deleteAPIRequestResponseValidation
Scenario: Validate The Departure Flight Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB   	
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"  
    Then Execute Crud Post Departure Flight API and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedDepartureFlightData" and "SQLConstants_DepartureFlight"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Departure Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIDepartureFlights @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate The Departure Flight Delete Crud API with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchDepartureFlightDetailsWithDesSiteId" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure Flight API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIDepartureFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Departure Flight and validate the record gets added into DB
   	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Departure Flight API with only sending Icao value in the request body and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    Then Execute Crud Post Departure Flight API using the same request body and verify that the response should return the recent added Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIDepartureFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Departure Flight with partial child without passing SiteId in the request body and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Departure Flight API with partial child without SiteId and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    Then Execute Crud Post Departure Flight API using the same request body and verify that the response should return the recent added Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type

@CrudAPIDepartureFlights @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Departure Flight and validate the record gets updated into DB
   	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Departure Flight API with only sending Icao value in the request body and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightDataForSiteIdAsc" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Departure Flight API URL with only sending Iata in the request body and Execute Crud Put API Departure Flight and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightDataForSiteIdAsc" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type

@CrudAPIAddDepartureFlightList @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Add Departure Flight List and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post "AddDepartureList" API and Set DTO Objects for Crud API Add Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "deletedDepartureFlightDataForTop2Rows" and "SQLConstants_DepartureFlight"
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIDepartureFlights @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API Departure Flight insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataForSiteIdDesc" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Departure Flight API using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Post Departure Flight API and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight

@CrudAPIDepartureFlights @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Departure Flight update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Departure Flight API URL using different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIDepartureFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Departure Flight with a SiteId that user doesn't have access to and validate the record should not be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Departure Flight API for negative test and verify that error should be thrown via API

@CrudAPIDepartureFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Departure Flight with an incorrect SiteId in the child object and validate the record should still be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Departure Flight API with incorrect SiteId in the child object and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for a single returned record for Crud Departure Flight API
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
 
@CrudAPIDepartureFlights @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Departure Flight with a SiteId that user doesn't have access to and validate the record should not be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightDataForSiteIdAsc" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Departure Flight API URL for negative test and verify that error should be thrown via API

@CrudAPIDepartureFlights @getUnmatchedFlightsAPIRequestResponseValidation
Scenario: Validate Get Crud Departure API Unmatched Flight by FlightsByOperationDate column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "updateCallSignDepFlightTable" and "SQLConstants_DepartureFlight"
    Then Establish Database Connection While Executing SQL Query "updateOperDateDepFlightTable" and "SQLConstants_DepartureFlight"
    Then Generate new get "GetUnmatchedFlights" using "opDate" with values "2023-01-26" and Execute Crud Get By Unmatched flight request and Set DTO Objects for Crud Departure Flight API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForUnmatchedDepFlight" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Flight API    
    Then Establish Database Connection While Executing SQL Query "updateOperDateDepFlight" and "SQLConstants_DepartureFlight"

@CrudAPIDepartureFlights @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate Record Addition using Get Crud API Departure Flight API For Audit By Id API Validation For Insert Operation
   	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Departure Flight API and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    And Generate new Get Departure flight by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and Set DTO Objects for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIDepartureFlights @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate Record Addition using Get Crud API Departure Flight API For Audit By Id API Validation For Update Operation
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Departure Flight API and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightDataForSiteIdAsc" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Departure Flight API URL using database fetched values and Execute Crud Put API Departure Flight and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    And Generate new Get Departure flight by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and verify the details for recent updated departure record for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchAuditUpdateDepartureDetails" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Audit Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIDepartureFlights @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate Record Addition using Get Crud API Departure Flight API For Audit By Id API Validation For Delete Operation
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Departure Flight API and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
    And Generate new Get Departure flight by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and verify the details for recent updated departure record for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Audit Flight API For Delete record
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIDepartureFlights @getAPIRequestResponseValidationForAuditByTimeStampAPI
Scenario: Validate Record Addition using Get Crud API Departure Flight API For Audit By TimeStamp API Validation For Insert Operation
   	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Departure Flight API and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceSpaceinTimestampForAudit" and "SQLConstants_DepartureFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceColoninTimestampForAudit" and "SQLConstants_DepartureFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceAddinTimestampForAudit" and "SQLConstants_DepartureFlight"
    And Fetch value for field "ChangeTime" from Database returned from above sql Query    
    And Generate new Get Departure flight by "Audit" and "Time" using "timestamp" with value fetched from DB response and Set DTO Objects for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type  

@CrudAPIDepartureFlights @getAPIRequestResponseValidationForAuditByTimeStampAPI
Scenario: Validate Record Addition using Get Crud API Departure Flight API For Audit By TimeStamp API Validation For Update Operation
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Departure Flight API and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceSpaceinTimestampForAudit" and "SQLConstants_DepartureFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceColoninTimestampForAudit" and "SQLConstants_DepartureFlight"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceAddinTimestampForAudit" and "SQLConstants_DepartureFlight"
    And Fetch value for field "ChangeTime" from Database returned from above sql Query       
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightDataForSiteIdAsc" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Departure Flight API URL using database fetched values and Execute Crud Put API Departure Flight and Set DTO Objects for Crud API Departure Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightDataForSiteIdAsc" and "SQLConstants_DepartureFlight"
    And Generate new Get Departure flight by "Audit" and "Time" using "timestamp" with value fetched in above step and verify the details for recent updated departure record for Crud Departure Flight API
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertDepartureDetails" and "SQLConstants_DepartureFlight"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditUpdateDepartureDetails" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Audit Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedDepartureFlightData" and "SQLConstants_DepartureFlight"
    And Generate new Delete Departure flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Departure Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type  

@CrudAPIDepartureFlights @getAPIRequestResponseForDepartureUnmatchedFlightsValidation
Scenario: Validate Get Crud UnmatchedFlightPlan API for Departure Unmatched Flights and validate the fetched details for the column
    Then Generate new get "DepartureUnmatchedFlights" using "opDate" with values "2023-01-26" and Execute Crud Get All Departure Unmatched Flights request and Set DTO Objects for Crud Unmatched FlightPlan API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForDepartureWithNoFlightPlan" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud Departure Flight API

@CrudAPIDepartureFlights @patchAPIRequestResponseValidationForDepartureFlights
Scenario: Validate The Patch Crud API Unmatched Flight Plan API and update the flight plan body based on Departure Flight plan Id and Operation date and validate the record gets updated into Departure DB	
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchAllDepartureFlightDetailsWithZRHSiteId" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanIdOperationDateDepartureTable" and "SQLConstants_DepartureFlight"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Generate new patch Unmatched Flight Plan API URL using fetched values and Execute Crud Patch Unmatched Flight Plan API and validate that the record in Arrival table is updated
    Then Establish Database Connection While Executing SQL Query "fetchAllDepartureFlightDetailsWithZRHSiteId" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "updateDepFlightDetailsOperationDate" and "SQLConstants_DepartureFlight"
    Then Generate new Delete "FlightPlanid" using "flightPlanid" with value "AT08998877" and "opDate" with value "2022-05-06" and Execute Crud Delete flight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API 
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type
    
@CrudAPIDepartureFlights @patchAPIRequestResponseValidationForDepartureFlights
Scenario: Validate The Patch Crud API Unmatched Flight Plan API and update the flight plan body based on Departure Flight plan Id and Operation date and validate the record gets updated into Departure DB and also validate that the record should be deleted from unmatched flight plan
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
    Then Establish Database Connection While Executing SQL Query "fetchAllDepartureFlightDetailsWithZRHSiteId" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanIdOperationDateDepartureTableWithUnmatchedData" and "SQLConstants_DepartureFlight"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Generate new patch Unmatched Flight Plan API URL using fetched values and Execute Crud Patch Unmatched Flight Plan API and validate that the record in Departure table is updated and the same in unmatched table is deleted
    Then Establish Database Connection While Executing SQL Query "fetchAllDepartureFlightDetailsWithZRHSiteIdWithFieldValueNull" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "updateDepartureFlights" and "SQLConstants_DepartureFlight"
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIDepartureFlights @putAPIRequestResponseValidationForDepartureFlights
Scenario: Validate The Put Crud API Unmatched Flight Plan API and update the flight plan body based on combination of Departure Site && Carrier && FlightNumber &&  OperationDate && Destination and validate the record gets updated into Departure DB and should be deleted from unmatched flight plan
    Then Establish Database Connection While Executing SQL Query "fetchAllDepartureFlightDetailsWithZRHSiteId" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "updateDepartureFlightDetails" and "SQLConstants_DepartureFlight"
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
    Then Establish Database Connection While Executing SQL Query "fetchAllDepartureFlightDetailsWithZRHSiteId" and "SQLConstants_DepartureFlight"   
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "updateCarrierFlightNumOpdateDestinationDepartureTableWithUnmatchedData" and "SQLConstants_DepartureFlight"
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeDataForSiteIdAsc" and "SQLConstants_AircraftTypeAndAirline" 
    Then Generate new put Unmatched Flight Plan API URL using fetched values and Execute Crud Put Unmatched Flight Plan API and validate that the record in Arrival table is updated and the same in unmatched table is deleted
    Then Establish Database Connection While Executing SQL Query "fetchAllDepartureFlightDetailsWithZRHSiteIdWithFieldValueNull" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "updateDepartureFlightDetailsFieldValues" and "SQLConstants_DepartureFlight"
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIDepartureFlights @getAPIRequestResponseForALLFlightPlansColumnValidation
Scenario: Validate Get Crud UnmatchedFlightPlan API for All Flight Plans column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "updateOperDateUnmatchedFlightPlanTable" and "SQLConstants_UnmatchedFlightPlan"
    Then Generate new get "AllFlightPlans" using "opDate" with values "2023-03-08" and Execute Crud Get By All Flight Plans request and Set DTO Objects for Crud Unmatched FlightPlan API 
    Then Establish Database Connection While Executing SQL Query "fetchAllUnmatchedFlightPlanDataWithFlightPlanId" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API
    Then Establish Database Connection While Executing SQL Query "updateOperDateFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanArrFlightTable" and "SQLConstants_ArrivalFlight"
    Then Generate new get "AllFlightPlans" using "opDate" with values "2022-08-22" and Execute Crud Get By All Flight Plans request and Set DTO Objects for Crud Unmatched FlightPlan API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForFlightPlanArrival" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanArrFlight" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanDepFlightTable" and "SQLConstants_DepartureFlight"
    Then Generate new get "AllFlightPlans" using "opDate" with values "2022-08-25" and Execute Crud Get By All Flight Plans request and Set DTO Objects for Crud Unmatched FlightPlan API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForFlightPlanDeparture" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanDepFlight" and "SQLConstants_DepartureFlight"

@CrudAPIDepartureFlights @getAPIRequestResponseForALLFlightPlansbyFlightPlanIdColumnValidation
Scenario: Validate Get Crud UnmatchedFlightPlan API for All Flight Plans by Flight Plan Id column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "updateOperDateUnmatchedFlightPlanTable" and "SQLConstants_UnmatchedFlightPlan"
    Then Generate new get "FlightPlansbyFlightPlanId" using "flightplanid" with value "AT0894970" and "opDate" with values "2023-03-08" and Execute Crud Get By All Flight Plans by FLight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API 
    Then Establish Database Connection While Executing SQL Query "fetchUnmatchedFlightPlanDataWithFlightPlanId" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API
    Then Establish Database Connection While Executing SQL Query "updateOperDateFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanArrFlightTable" and "SQLConstants_ArrivalFlight"
    Then Generate new get "FlightPlansbyFlightPlanId" using "flightplanid" with value "1" and "opDate" with values "2022-08-22" and Execute Crud Get By All Flight Plans by FLight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForFlightPlanArrival" and "SQLConstants_ArrivalFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanArrFlight" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanDepFlightTable" and "SQLConstants_DepartureFlight"
    Then Generate new get "FlightPlansbyFlightPlanId" using "flightplanid" with value "2" and "opDate" with values "2022-08-25" and Execute Crud Get By All Flight Plans by FLight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForFlightPlanDeparture" and "SQLConstants_DepartureFlight"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API
    Then Establish Database Connection While Executing SQL Query "updateFlightPlanDepFlight" and "SQLConstants_DepartureFlight" 

@CrudAPIDepartureFlights @deleteAPIRequestForFlightPlanIdResponseValidationForArrivalAndDepartureFlights
Scenario: Validate The Unmatched Flight Plan by flight plan id Delete for Arrival and Departure Flights and validate the fetched details for the row to be NULL in DB   	
    Then Establish Database Connection While Executing SQL Query "fetchDepartureFlightDetailsWithSiteId" and "SQLConstants_DepartureFlight"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "updateDepartureFlightDetailsForFlightPlanId" and "SQLConstants_DepartureFlight"
    Then Establish Database Connection While Executing SQL Query "fetchArrivalFlightDetailsWithSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsForFlightPlanId" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchFlightPlanIdDepartureTable" and "SQLConstants_DepartureFlight"
    And Validate that the fetched "FlightPlanId" value from DB is NULL post deleting the flightplan Id using delete API
    Then Establish Database Connection While Executing SQL Query "fetchFlightPlanIdArrivalTable" and "SQLConstants_ArrivalFlight"
    And Validate that the fetched "FlightPlanId" value from DB is NULL post deleting the flightplan Id using delete API
    Then Establish Database Connection While Executing SQL Query "updateDepartureFlightDetailsOperationDate" and "SQLConstants_DepartureFlight"
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"