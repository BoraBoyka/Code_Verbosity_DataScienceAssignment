Feature: CrudAPIRoleBasedAuthorizationTests

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for No Role Access

@CrudAPIRoleBasedAuthorizationTests @getAPIRequestForALLAPIsNotAuthorizedErrorValidation
Scenario: Validate All Get Crud API and validate that the user should get 403 Forbidden error in the response
    Then Execute all Get Crud APIs and verify that the response body returns unauthorized error

@CrudAPIRoleBasedAuthorizationTests @postAPIRequestForAircraftWithoutWriteAccess
Scenario: Validate Post Crud Aircraft Type API request without having Aircraft Write Role Access and validate that the user should get 403 Forbidden error in the response
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate JWT Authorization Token for User with with Aircraft Read Access and user to not have Aircraft Write Access for Environment Info stored in ConfigSetting 
    Then Execute Crud Post Aircraft Type API and validate that the user to get unauthorized error in the response body 

@CrudAPIRoleBasedAuthorizationTests @deleteAPIRequestResponseValidationForAircraftWithoutWriteAccess
Scenario: Validate Delete Crud API Aircraft Type request without having Aircraft Write Role Access and validate that the user should get 403 Forbidden error in the response
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    And Generate JWT Authorization Token for User with with Aircraft Read Access and user to not have Aircraft Write Access for Environment Info stored in ConfigSetting 
    Then Execute Crud Delete Aircraft Type API and validate that the user to get unauthorized Forbidden error in the response body 
    Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 

@CrudAPIRoleBasedAuthorizationTests @deleteAPIRequestResponseValidationForAircraftWithWriteAccess
Scenario: Validate Delete Crud API Aircraft Type request with Aircraft Write Role Access and validate that the user should be able to successfully delete the record
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
    And Generate JWT Authorization Token for User with Aircraft Write Access and user to not have Aircraft Read Access for Environment Info stored in ConfigSetting 
    And Generate new Delete aircraft type API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Aircraft Type 
 
@CrudAPIRoleBasedAuthorizationTests @getAPIRequestResponseForIdColumnValidationForAircraftReadAccessRole
Scenario: Validate Get Crud API Aircraft Type by Id with Aircraft Read Role Access and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchAircraftDetailsWithAscendingSiteId" and "SQLConstants_AircraftTypeAndAirline"
    And Generate JWT Authorization Token for User with with Aircraft Read Access and user to not have Aircraft Write Access for Environment Info stored in ConfigSetting  
    And Generate new Get aircraft type by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Aircraft Type 
    And Compare values from API response set to DB record set for a single returned record for Crud Aircraft Type API 
    