Feature: CrudAPIAuthenticationTests

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects

@CrudAPIAuthenticationTests @apiForbiddenNoTokenSent
Scenario: Validate Get Crud API Aircraft Type when no token is sent and validate the api response should return forbidden
    Then Execute Crud Aircraft Type API and validate that when no token is sent it should return forbidden error via API response

@CrudAPIAuthenticationTests @apiForbiddenInvalidTokenSent
Scenario: Validate Get Crud API Aircraft Type when an invalid token is sent and validate the api response should return forbidden
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Execute Crud Aircraft Type API and validate that when an invalid token is sent it should return forbidden error via API response

@CrudAPIAuthenticationTests @apiForbiddenExpiredTokenSent
Scenario: Validate Get Crud API Aircraft Type when a expired token is sent and validate the api response should return forbidden
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Expired Token for Env Info stored in ConfigSetting
    Then Execute Crud Aircraft Type API and validate that when an expired token is sent it should return forbidden error via API response

