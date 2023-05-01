Feature: CrudAPIFlights

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIFlights @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Flight and validate the response with DB record set
    Then Establish Database Connection While Executing SQL Query "UpdateParkPositionFlightTable" and "SQLConstants_Flight"
    Then Establish Database Connection While Executing SQL Query "UpdateCommentFlightTable" and "SQLConstants_Flight"
    Then Establish Database Connection While Executing SQL Query "UpdateTaxiTimetoPadFlightTable" and "SQLConstants_Flight"
    Then Execute Crud Flight API And Set DTO Objects for Crud API Flight for site claim "MGL"
    Then Establish Database Connection While Executing SQL Query "UpdateParkPositionFlightTable" and "SQLConstants_Flight"
    Then Establish Database Connection While Executing SQL Query "UpdateCommentFlightTable" and "SQLConstants_Flight"
    Then Establish Database Connection While Executing SQL Query "UpdateTaxiTimetoPadFlightTable" and "SQLConstants_Flight"
    Then Establish Database Connection While Executing SQL Query "fetchFlightData" and "SQLConstants_Flight"
    And Compare values from API response set to DB record set for Crud Flight API

@CrudAPIFlights @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Flight by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightDataForSiteidAsc" and "SQLConstants_Flight"
    And Generate new Get flight by "FlightId" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Flight
    And Compare values from API response set to DB record set for a single returned record for Crud Flight API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistFlight" and "SQLConstants_Flight"
    Then Validate the GET API operation by "FlightId" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Flight
    Then Establish Database Connection While Executing SQL Query "fetchFlightDetailsWithDesSiteId" and "SQLConstants_Flight"
    And Generate new Get flight by "FlightId" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Flight 

@CrudAPIFlights @deleteAPIRequestResponseValidation
Scenario: Validate The Flight Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordFlightTable" and "SQLConstants_Flight"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Generate new Delete flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedFlightData" and "SQLConstants_Flight"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Flight
    And Generate new Delete aircraft type API URL using the above added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIFlights @getAPIRequestResponseForFlightsByNumberColumnValidation
Scenario: Validate Get Crud API Flight by FlightsByNumber column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Generate new "GetFlightsByNumber" using "FlightNumber" in the API URL and Execute Crud Get By "number" request and Set DTO Objects for Crud API Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForFlightNumber" and "SQLConstants_Flight"
    And Compare values from API response set to DB record set for Crud Flight API
    Then Establish Database Connection While Executing SQL Query "fetchFlightDetailsWithSiteId" and "SQLConstants_Flight"
    And Generate new "GetFlightsByNumber" using "FlightNumber" in the API URL and Execute Crud Get By "number" request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Flight 

@CrudAPIFlights @getAPIRequestResponseForFlightsByCallSignColumnValidation
Scenario: Validate Get Crud API Flight by FlightsByCallSign column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Generate new "GetFlightsByCallSign" using "CallSign" in the API URL and Execute Crud Get By "callSign" request and Set DTO Objects for Crud API Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForCallSign" and "SQLConstants_Flight" 
    And Compare values from API response set to DB record set for Crud Flight API
    Then Establish Database Connection While Executing SQL Query "fetchFlightDetailsWithSiteId" and "SQLConstants_Flight"
    And Generate new "GetFlightsByCallSign" using "CallSign" in the API URL and Execute Crud Get By "callSign" request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Flight

@CrudAPIFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Flight and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Flight API and Set DTO Objects for Crud API Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Compare values from API response set to DB record set for a single returned record for Crud Flight API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Execute Crud Post Flight API using a new aircraft type Id and error should be thrown via API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Generate new Delete flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Flight
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 
   
@CrudAPIFlights @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Flight and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightDataForSiteidAsc" and "SQLConstants_Flight"
    And Fetch value for field "FlightId" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Flight API URL using database fetched values and Execute Crud Put API Flight and Set DTO Objects for Crud API Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightDataForSiteidAsc" and "SQLConstants_Flight"
    And Compare values from API response set to DB record set for a single returned record for Crud Flight API

@CrudAPIAddFlightList @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Add Flight List and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post "AddFlightList" API and Set DTO Objects for Crud API Add Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoFlightData" and "SQLConstants_Flight"
    And Compare values from API response set to DB record set for Crud Flight API
    Then Establish Database Connection While Executing SQL Query "deletedFlightDataForTop2Rows" and "SQLConstants_Flight"
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 
   
@CrudAPIFlights @getAPIRequestResponseForFlightsByTimeWindowColumnValidation
Scenario: Validate Get Crud API Flight by FlightsByTimeWindow column and validate the fetched details for the column
Then Establish Database Connection While Executing SQL Query "fetchSiteName" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Name" from Database returned from Query Response
    Then Generate new get "GetFlightsByTimeWindow" using "startDateTime" and "endDateTime" and "site" with values "2022-05-01" and "2022-05-08" and Execute Crud Get By Time window request and Set DTO Objects for Crud API Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForTargetOffBlockTime" and "SQLConstants_Flight"
    And Compare values from API response set to DB record set for Crud Flight API
    Then Establish Database Connection While Executing SQL Query "fetchFlightDetailsWithSiteId" and "SQLConstants_Flight"
    And Generate new get "GetFlightsByTimeWindow" using "startDateTime" and "endDateTime" and "site" with values "2022-09-20" and "2022-09-20" and Execute Crud Get By Time window request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Flight 
    
@CrudAPIFlights @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Flight for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud Flight API And Set DTO Objects for Crud API Flight
    Then Establish Database Connection While Executing SQL Query "fetchFlightDataForAllSiteClaims" and "SQLConstants_Flight"
    And Compare values from API response set to DB record set for Crud Flight API

@CrudAPIFlights @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Flight and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Flight API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIFlights @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Flight with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "UpdateSiteIdFlightTable" and "SQLConstants_Flight"
    Then Establish Database Connection While Executing SQL Query "fetchFlightDetailsWithSiteId" and "SQLConstants_Flight"
    Then Generate new Get Flight by "FlightId" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body

@CrudAPIFlights @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate The Flight Delete Crud API with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataForSiteIdDesc" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordFlightTable" and "SQLConstants_Flight"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Generate new Delete Flight API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Generate new Delete flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Flight

@CrudAPIFlights @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API Flight insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataForSiteIdDesc" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Flight API using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Post Flight API and Set DTO Objects for Crud API Flight
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Generate new Delete flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Flight

@CrudAPIFlights @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Flight update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData" and "SQLConstants_Flight"
    And Fetch value for field "FlightId" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Flight API URL using different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIFlights @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Flight with a SiteId different from what aircraft table has and validate the record should not be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Flight API for negative test and verify that error should be thrown via API
  
@CrudAPIFlights @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Flight with a SiteId different from what aircraft table has and validate the record should not be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightDataForSiteidAsc" and "SQLConstants_Flight"
    And Fetch value for field "FlightId" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Flight API URL for negative test and verify that error should be thrown via API

