Feature: CrudAPIAuthenticationTests

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects

@CrudAPIAuthenticationTests @apiSuccessValidTokenSent
Scenario: Validate Get Crud API Aircraft Type when a valid token is sent and validate the response with DB record set
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Execute Crud Aircraft Type API And Set DTO Objects for Crud API AircraftType
    Then Establish Database Connection While Executing SQL Query "fetchAircraftTypeData"
    And Compare values from API response set to DB record set for Crud Aircraft Type API

@CrudAPIAuthenticationTests @apiForbiddenNoTokenSent
Scenario: Validate Get Crud API Aircraft Type when no token is sent and validate the api response should return forbidden
    Then Execute Crud Aircraft Type API and validate that when no token is sent it should return forbidden error via API response

@CrudAPIAuthenticationTests @apiForbiddenInvalidTokenSent
Scenario: Validate Get Crud API Aircraft Type when an invalid token is sent and validate the api response should return forbidden
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Execute Crud Aircraft Type API and validate that when an invalid token is sent it should return forbidden error via API response

@CrudAPIAuthenticationTests @apiForbiddenExpiredTokenSent
Scenario: Validate Get Crud API Aircraft Type when a expired token is sent and validate the api response should return forbidden
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
    And Generate JWT Authorization Expired Token for Env Info stored in ConfigSetting
    Then Execute Crud Aircraft Type API and validate that when an expired token is sent it should return forbidden error via API response
    


    

