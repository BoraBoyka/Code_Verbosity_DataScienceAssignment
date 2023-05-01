﻿Feature: CrudAPIConfiguration

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
And Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIConfiguration @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Configuration and validate the response with DB record set
    Then Execute Crud Configuration API And Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationData"
    And Compare values from API response set to DB record set

@CrudAPIConfiguration @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Configuration and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
    Then Execute Crud Post API Configuration and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Compare values from API response set to DB record set for a single returned record
   
@CrudAPIConfiguration @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Configuration and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    Then Generate new put configuration API URL using database fetched values and Execute Crud Put API Configuration and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Compare values from API response set to DB record set for a single returned record

@CrudAPIConfiguration @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Configuration by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new Get configuration by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Configuration
    And Compare values from API response set to DB record set for a single returned record
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExist"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API

@CrudAPIConfiguration @deleteAPIRequestResponseValidation
Scenario: Validate Delete Crud API Configuration for specific row and validate the fetched details for the row to be NULL in DB
Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordConfigurationTable"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedConfigurationData"
    And Validate that DB record set for the deleted row from API to not exist in DB
    Then Validate the GET API operation again using the same deleted ID and it should return error via API

@CrudAPIConfiguration @getAPIRequestResponseForNameColumnValidation
Scenario: Validate Get Crud API Configuration by Name column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new "ConfigurationByName" using "Name" in the API URL and Execute Crud Get By "name" request and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForNameConfiguration" 
    And Compare values from API response set to DB record set for a single returned record

@CrudAPIConfiguration @getAPIRequestResponseForSystemColumnValidation
Scenario: Validate Get Crud API Configuration by System column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new "ConfigurationBySystem" using "System" in the API URL and Execute Crud Get By "system" request and Set DTO Objects for Crud API Configuration data
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForSystemConfiguration" 
    And Compare values from API response set to DB record set

@CrudAPIConfiguration @getAPIRequestResponseForSystemAndGroupColumnValidation
Scenario: Validate Get Crud API Configuration by System and Group column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new "ConfigurationBySystemGroup" using "Group" and "System" in the API URL and Execute Crud Get By "system" and "group" request and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForSystemAndGroup" 
    And Compare values from API response set to DB record set
   
@CrudAPIConfiguration @getOrAddAPIRequestResponseForSiteIdColumnValidation
Scenario: Validate Get Or Add Crud API Post Configuration by SiteId method and validate the fetched or add details 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new "GetOrAddConfiguration" API URL and Execute Crud Post request for existing record and Set DTO Objects for Crud API Configuration
    And Compare values from API response set to DB record set for a single returned record
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending"
    And Generate new "GetOrAddConfiguration" API URL and Execute Crud Post request for new record and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Compare values from API response set to DB record set for a single returned record

@CrudAPIConfiguration @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Configuration and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite"
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Execute Crud Configuration API with an incorrect Site id and validate that the api should return error in the response body

@CrudAPIConfiguration @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Configuration with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithSiteId"
    And Generate new Get Configuration by "Id" API URL using different Site id than what is being used in the token and validate that the api should return error in the response body

@CrudAPIConfiguration @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Delete Crud API Configuration with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordConfigurationTable"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new Delete Configuration API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending"
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration

@CrudAPIConfiguration @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API Configuration insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending"
    Then Execute Crud Post Configuration API using different Site id than what is being used in the token and validate that the api should return error in the response body
    And Generate JWT Authorization Token for Environment Info stored in ConfigSetting
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData"
    And Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration

@CrudAPIConfiguration @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Configuration update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithSiteId"
    Then Generate new put Configuration API URL using different Site id than what is being used in the token and validate that the api should return error in the response body
 