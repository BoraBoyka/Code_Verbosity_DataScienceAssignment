Feature: CrudAPIFlightPlanUnmatched

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseValidation
Scenario: Validate Get Crud API All Unmatched FlightPlans and validate the response with DB record set
    Then Execute Crud UnmatchedFlightPlan API And Set DTO Objects for Crud API FlightPlan for site claim "ZRH" for "AllUnmatchedFlightplans"
    Then Establish Database Connection While Executing SQL Query "fetchUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API

@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Unmatched FlightPlan Id by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanDataForSiteIdAsc" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new get "UnmatchedFlightplanbyId" using "id" "Id" value fetched from DB and Execute Crud Get By Unmatched Flights by Id request and Set DTO Objects for Crud Unmatched FlightPlan API
    And Compare values from API response set to DB record set for a single returned record for Crud Unmatched Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Validate the GET API operation by "UnmatchedFlightplanbyId" API URL using "id" "unmatchedFlightPlanId" that does not exist fetched from DB and it should return error via API for Crud Unmatched Flight Plan
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "updateTXLSiteForUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchUnmatchedFlightPlanDetailsWithDesSiteId" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new get "UnmatchedFlightplanbyId" using "id" "Id" value fetched from DB and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Unmatched Flight Plan 
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "updateZRHSiteForUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"

@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseForFlightPlanIdColumnValidation
Scenario: Validate Get Crud API Unmatched FlightPlan by FlightPlan Id column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanDataForSiteIdAsc" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new get "UnmatchedFlightsbyFlightPlanId" using "flightplanid" "FlightPlanId" value fetched from DB and Execute Crud Get By Unmatched Flights by FlightPlanId request and Set DTO Objects for Crud Unmatched FlightPlan API
    And Compare values from API response set to DB record set for flightplanId for Crud Unmatched Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Validate the GET API operation by "UnmatchedFlightsbyFlightPlanId" API URL using "unmatchedFlightPlanId" "flightplanid" that does not exist fetched from DB and it should return empty list via API for Crud Unmatched Flight Plan
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "updateTXLSiteForUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchUnmatchedFlightPlanDetailsWithDesSiteId" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new get "UnmatchedFlightplanbyId" using "id" "Id" value fetched from DB and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Unmatched Flight Plan 
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "updateZRHSiteForUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    
@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseForFlightsByOperationalDateColumnValidation
Scenario: Validate Get Crud UnmatchedFlightPlan API by FlightsByOperationalDate column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "updateOperDateUnmatchedFlightPlanTable" and "SQLConstants_UnmatchedFlightPlan"
    Then Generate new get "UnmatchedFlightPlansByOperationDate" using "opDate" with values "2023-03-08" and Execute Crud Get By OperationalDate request and Set DTO Objects for Crud Unmatched FlightPlan API 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForOpDateUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API
    Then Establish Database Connection While Executing SQL Query "updateOperDateFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    
@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseValidation
Scenario: Validate Get Crud API UnmatchedFlightPlan for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud UnmatchedFlightPlan API And Set DTO Objects for Crud API UnmatchedFlightPlan for "AllUnmatchedFlightplans"
    Then Establish Database Connection While Executing SQL Query "fetchUnmatchedFlightPlanDataForAllSiteClaims" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API

@CrudAPIUnmatchedFlightPlan @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Crud Get API UnmatchedFlightPlan and pass incorrect siteId in the token and validate that the api should not return any response
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteZRHForTestData" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "addDataFlightPlanTable" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud UnmatchedFlightPlan API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIUnmatchedFlightPlan @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Unmatched Flight Plan with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "updateTXLSiteForUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchUnmatchedFlightPlanDetailsWithDesSiteId" and "SQLConstants_UnmatchedFlightPlan"
    Then Generate new Get Unmatched FlightPlan using "id" "Id" for different Site id "TXL" than what is being used in the token and validate that the unmatched flight plan api should return error in the response body for "UnmatchedFlightplanbyId"
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "updateZRHSiteForUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"

@CrudAPIUnmatchedFlightPlan @deleteAPIRequestResponseValidation
Scenario: Validate The Unmatched Flight Plan Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB   	
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"  
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Delete Unmatched flight plan API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIUnmatchedFlightPlan @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate The Unmatched Flight Plan Delete Crud API with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "updateTXLSiteForUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchUnmatchedFlightPlanDetailsWithDesSiteId" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Delete Unmatched Flight Plan API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "updateZRHSiteForUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"

@CrudAPIUnmatchedFlightPlan @deleteAPIRequestForFlightPlanIdResponseValidation
Scenario: Validate The Unmatched Flight Plan by flight plan id Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB   	
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline"  
    And Fetch value for field "Id" against Aircraft Type Id fetched from database response to the above query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordUnmatchedFlightPlan" and "SQLConstants_UnmatchedFlightPlan"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    Then Generate new Delete "FlightPlanid" using "flightPlanid" with value "AT0894977" and "opDate" with value "2023-03-08" and Execute Crud Delete flight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API 
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIUnmatchedFlightPlan @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Unmatched Flight Plan and validate the record gets added into DB
	Then Establish Database Connection While Executing SQL Query "fetchAllArrivalFlightDetailsWithZRHSiteId" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "updateArrivalFlightDetailsOperationDate" and "SQLConstants_ArrivalFlight"
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    Then Execute Crud Post Unmatched Flight API using the same request body and verify that the response should return the recent added Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    Then Generate new Delete "FlightPlanid" using "flightPlanid" with value "AT08998877" and "opDate" with value "2022-05-06" and Execute Crud Delete flight plan id request and Set DTO Objects for Crud Unmatched FlightPlan API 
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIUnmatchedFlightPlan @putAPIRequestResponseValidation
Scenario: Validate Crud Put API Unmatched Flight Plan and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanDataForSiteIdAsc" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Unmatched Flight Plan API URL using database fetched values and Execute Crud Put API Unmatched Flight Plan and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanDataWithoutFieldValue" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    Then Establish Database Connection While Executing SQL Query "updateFlightNumber" and "SQLConstants_UnmatchedFlightPlan"

#@CrudAPIUnmatchedFlightPlan @putAPIRequestDifferentSiteIdThanTokenValidation
#Scenario: Validate Put Crud API Unmatched Flight Plan update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
#    And Fetch value for field "Id" from Database returned from Query Response
#    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithSiteId" and "SQLConstants_AircraftTypeAndAirline"
#    Then Generate new put Unmatched Flight Plan API URL using different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIUnmatchedFlightPlan @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Unmatched Flight Plan with an incorrect SiteId in the child object and validate the record should still be added into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Execute Crud Post Unmatched Flight Plan API with incorrect SiteId in the child object and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud UnmatchedFlightPlan API Post request
    And Generate new Delete Unmatched flight plan API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Unmatched Flight Plan

@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate Record Addition using Get Crud API Unmatched Flight Plan API For Audit By Id API Validation For Insert Operation
   	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    And Generate new Get Unmatched flight plan by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and Set DTO Objects for Crud Unmatched Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Delete Unmatched flight plan API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Unmatched Flight Plan
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate Record Addition using Get Crud API Unmatched Flight Plan API For Audit By Id API Validation For Update Operation
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanDataForSiteIdAsc" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Unmatched Flight API URL and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    And Generate new Get Unmatched flight plan by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and verify the details for recent updated unmatched flight plan record for Crud Unmatched Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchAuditUpdateUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud Unmatched Audit Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Delete Unmatched flight plan API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Unmatched Flight Plan
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseValidationForAuditByIdAPI
Scenario: Validate The Record Addition using Get Crud API Unmatched Flight Plan API For Audit By Id API Validation For Delete Operation
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Delete Unmatched flight plan API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Unmatched Flight Plan
    And Generate new Get Unmatched flight plan by "Audit" and "Id" using "historyId" with value fetched from DB response in above step and verify the details for recent updated unmatched flight plan record for Crud Unmatched Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud Unmatched Audit Flight Plan API For Delete record
	And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseValidationForAuditByTimeStampAPI
Scenario: Validate Record Addition using Get Crud API Unmatched Flight Plan API For Audit By TimeStamp API Validation For Insert Operation
   	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceSpaceinTimestampForAudit" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceColoninTimestampForAudit" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceAddinTimestampForAudit" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "ChangeTime" from Database returned from above sql Query    
    And Generate new Get Unmatched flight plan by "Audit" and "Time" using "timestamp" with value fetched from DB response and Set DTO Objects for Crud Unmatched Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Delete Unmatched flight plan API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Unmatched Flight Plan
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type  

@CrudAPIUnmatchedFlightPlan @getAPIRequestResponseValidationForAuditByTimeStampAPI
Scenario: Validate Record Addition using Get Crud API Unmatched Flight Plan API For Audit By TimeStamp API Validation For Update Operation
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Post Aircraft Type API and Set DTO Objects for Crud API Aircraft Type 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeDataWithDynamicId" and "SQLConstants_AircraftTypeAndAirline" 
    Then Execute Crud Post Unmatched Flight Plan API and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceSpaceinTimestampForAudit" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceColoninTimestampForAudit" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "ChangeTime" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "replaceAddinTimestampForAudit" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "ChangeTime" from Database returned from above sql Query       
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanDataForSiteIdAsc" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "Id" from Database returned from Query Response
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    Then Generate new put Unmatched Flight API URL and Set DTO Objects for Crud API Unmatched Flight Plan
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanDataForSiteIdAsc" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Get Unmatched flight plan by "Audit" and "Time" using "timestamp" with value fetched in above step and verify the details for recent updated unmatched flight plan record for Crud Unmatched Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchAuditInsertUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Fetch value for field "HistoryId" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchAuditUpdateUnmatchedFlightPlanDetails" and "SQLConstants_UnmatchedFlightPlan"
    And Compare values from API response set to DB record set for Crud Unmatched Audit Flight Plan API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedUnmatchedFlightPlanData" and "SQLConstants_UnmatchedFlightPlan"
    And Generate new Delete Unmatched flight plan API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Unmatched Flight Plan
    And Generate new Delete aircraft type API URL using the added aircraft Id and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type  



