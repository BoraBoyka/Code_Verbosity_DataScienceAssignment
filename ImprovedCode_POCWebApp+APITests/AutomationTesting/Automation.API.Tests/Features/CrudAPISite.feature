Feature: CrudAPISite

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPISite @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Site and validate the response with DB record set
    Then Execute Crud Site API And Set DTO Objects for Crud API Site for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for Crud Site API

@CrudAPISite @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Site by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new Get site by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Site
    And Compare values from API response set to DB record set for a single returned record for Crud Site API
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud Site

@CrudAPISite @getAPIRequestResponseForNameColumnValidation
Scenario: Validate Get Crud API Site by Name column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new "GetSiteByName" using "Name" in the API URL and Execute Get By "siteName" API request and Set DTO Objects for Crud API Site
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForNameSite" and "SQLConstants_TaxiSequenceAndSite" 
    And Compare values from API response set to DB record set for Crud Site API

@CrudAPISite @getAPIRequestResponseForIataColumnValidation
Scenario: Validate Get Crud API Site by Iata column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new "GetSiteByIata" using "Iata" in the API URL and Execute Crud Get By "iata" request and Set DTO Objects for Crud API Site
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForIataSite" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for Crud Site API

@CrudAPISite @getAPIRequestResponseForIcaoColumnValidation
Scenario: Validate Get Crud API Site by Icao column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new "GetSiteByIcao" using "Icao" in the API URL and Execute Crud Get By "icao" request and Set DTO Objects for Crud API Site
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForIcaoSite" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for Crud Site API

@CrudAPISite @deleteAPIRequestResponseValidation
Scenario: Validate Site Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "insertNewRecordSiteTable" and "SQLConstants_TaxiSequenceAndSite"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new Delete site API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Site
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud Site

@CrudAPISite @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Site and validate the record gets added into DB
    Then Execute Crud Post Site API and Set DTO Objects for Crud API Site
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for a single returned record for Crud Site API
    And Generate new Delete site API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Site
    
@CrudAPISite @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Site and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new put site API URL using database fetched values and Execute Crud Put API Site and Set DTO Objects for Crud API Site
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    And Compare values from API response set to DB record set for a single returned record for Crud Site API
    Then Establish Database Connection While Executing SQL Query "updateNameForSiteData" and "SQLConstants_TaxiSequenceAndSite"

@CrudAPISite @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Site Get Crud API and pass incorrect siteId in the token and validate that the api should still return valid response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Site API with an incorrect Site id and validate that the api should still return valid response
    Then Establish Database Connection While Executing SQL Query "fetchSiteData" and "SQLConstants_TaxiSequenceAndSite"
    And Compare values from API response set to DB record set for Crud Site API





