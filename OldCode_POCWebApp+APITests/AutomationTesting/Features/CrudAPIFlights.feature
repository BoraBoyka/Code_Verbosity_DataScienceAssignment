#Feature: CrudAPIFlights
#
#Background:
#Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
#Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
#And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
#
#@CrudAPIFlights @getAPIRequestResponseValidation
#Scenario: Validate Get Crud API Flight and validate the response with DB record set
#    Then Execute Crud Flight API And Set DTO Objects for Crud API Flight
#    Then Establish Database Connection While Executing SQL Query "fetchFlightData"
#    And Compare values from API response set to DB record set for Crud Flight API
#
#@CrudAPIFlights @getAPIRequestResponseForIdColumnValidation
#Scenario: Validate Get Crud API Flight by specific column and validate the fetched details for the column
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Generate new Get flight by "FlightId" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Flight
#    And Compare values from API response set to DB record set for a single returned record for Crud Flight API
#    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistFlight"
#    Then Validate the GET API operation by "FlightId" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Flight
#
#@CrudAPIFlights @deleteAPIRequestResponseValidation
#Scenario: Validate Delete Crud API Flight for specific row and validate the fetched details for the row to be NULL in DB
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Generate new Delete flight API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Flight
#    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedFlightData"
#    And Validate that DB record set for the deleted row from API to not exist in DB
#    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Flight
#
#@CrudAPIFlights @getAPIRequestResponseForFlightsByNumberColumnValidation
#Scenario: Validate Get Crud API Flight by FlightsByNumber column and validate the fetched details for the column
#    Then Establish Database Connection While Executing SQL Query "fetchSiteName"
#    And Fetch value for field "Name" from Database returned from Query Response
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Generate new "GetFlightsByNumber" using "FlightNumber" and "Name" in the API URL and Execute Crud Get By "number" and "site" request and Set DTO Objects for Crud API Flight
#    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForFlightNumber" 
#    And Compare values from API response set to DB record set for Crud Flight API
#
#@CrudAPIFlights @getAPIRequestResponseForFlightsByCallSignColumnValidation
#Scenario: Validate Get Crud API Flight by FlightsByCallSign column and validate the fetched details for the column
#    Then Establish Database Connection While Executing SQL Query "fetchSiteName"
#    And Fetch value for field "Name" from Database returned from Query Response
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Generate new "GetFlightsByCallSign" using "CallSign" and "Name" in the API URL and Execute Crud Get By "callSign" and "site" request and Set DTO Objects for Crud API Flight
#    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForCallSign" 
#    And Compare values from API response set to DB record set for Crud Flight API
#
#
#@CrudAPIFlights @postAPIRequestResponseValidation
#Scenario: Validate Post Crud API Flight and validate the record gets added into DB
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
#    Then Execute Crud Post Flight API and Set DTO Objects for Crud API Flight
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Compare values from API response set to DB record set for a single returned record for Crud Flight API
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
#    Then Execute Crud Post Flight API using a new aircraft type Id and error should be thrown via API
#    
#@CrudAPIFlights @putAPIRequestResponseValidation
#Scenario: Validate Put Crud API Flight and validate the record gets updated into DB
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Fetch value for field "FlightId" from Database returned from Query Response
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
#    Then Generate new put Flight API URL using database fetched values and Execute Crud Put API Flight and Set DTO Objects for Crud API Flight
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Compare values from API response set to DB record set for a single returned record for Crud Flight API
#
#@CrudAPIAddFlightList @postAPIRequestResponseValidation
#Scenario: Validate Post Crud API Add Flight List and validate the record gets added into DB
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
#    Then Execute Crud Post "AddFlightList" API and Set DTO Objects for Crud API Add Flight
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoFlightData"
#    And Compare values from API response set to DB record set for Crud Flight API
#
#@CrudAPIUpdateFlightList @putAPIRequestResponseValidation
#Scenario: Validate Put Crud API Update Flight List and validate the record gets updated into DB
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Fetch value for field "FlightId" from Database returned from Query Response
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData"
#    Then Generate new put "UpdateFlightList" API URL using database fetched values and Execute Crud Put API Update Flight List and Set DTO Objects for Crud API Flight
#    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedFlightData"
#    And Compare values from API response set to DB record set for Crud Flight API 
#
#@CrudAPIFlights @getAPIRequestResponseForFlightsByTimeWindowColumnValidation
#Scenario: Validate Get Crud API Flight by FlightsByTimeWindow column and validate the fetched details for the column
#    Then Establish Database Connection While Executing SQL Query "fetchSiteName"
#    And Fetch value for field "Name" from Database returned from Query Response
#    Then Generate new get "GetFlightsByTimeWindow" using "startDateTime" and "endDateTime" and "site" with values "2022-05-01" and "2022-05-08" and Execute Crud Get By Time window request and Set DTO Objects for Crud API Flight
#    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForTargetOffBlockTime" 
#    And Compare values from API response set to DB record set for Crud Flight API
#  