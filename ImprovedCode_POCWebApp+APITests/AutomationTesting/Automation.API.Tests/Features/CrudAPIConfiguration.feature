Feature: CrudAPIConfiguration

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPIConfiguration @getAPIRequestResponseValidation
Scenario: Validate Crud API Get Configuration and validate the response with DB record set
    Then Execute Crud Configuration API And Set DTO Objects for Crud API Configuration for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationData" and "SQLConstants_Configuration"
    And Compare values from API response set to DB record set

@CrudAPIConfiguration @postAPIRequestResponseValidation
Scenario: Validate Crud API Post Configuration and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post API Configuration and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationData" and "SQLConstants_Configuration"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Compare values from API response set to DB record set for a single returned record
    And Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration
   
@CrudAPIConfiguration @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Configuration and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    Then Generate new put configuration API URL using database fetched values and Execute Crud Put API Configuration and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Compare values from API response set to DB record set for a single returned record

@CrudAPIConfiguration @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API Configuration by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Generate new Get configuration by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API Configuration
    And Compare values from API response set to DB record set for a single returned record
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExist" and "SQLConstants_Configuration"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithSiteId" and "SQLConstants_Configuration"
    And Generate new Get configuration by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Configuration 

@CrudAPIConfiguration @deleteAPIRequestResponseValidation
Scenario: Validate The Delete Crud API Configuration for specific row and validate the fetched details for the row to be NULL in DB
Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordConfigurationTable" and "SQLConstants_Configuration"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedConfigurationData" and "SQLConstants_Configuration"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API

@CrudAPIConfiguration @getAPIRequestResponseForNameColumnValidation
Scenario: Validate Get Crud API Configuration by Name column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Generate new "ConfigurationByName" using "Name" in the API URL and Execute Crud Get By "name" request and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForNameConfiguration" and "SQLConstants_Configuration" 
    And Compare values from API response set to DB record set for a single returned record
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithSiteId" and "SQLConstants_Configuration"
    And Generate new "ConfigurationByName" using "Name" in the API URL and Execute Get By "name" API request for a different Site Id than what is being used in the token and validate that the api should return No Content in the response body for Crud API Configuration 

@CrudAPIConfiguration @getAPIRequestResponseForSystemColumnValidation
Scenario: Validate Get Crud API Configuration by System column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Generate new "ConfigurationBySystem" using "System" in the API URL and Execute Crud Get By "system" request and Set DTO Objects for Crud API Configuration data
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForSystemConfiguration" and "SQLConstants_Configuration" 
    And Compare values from API response set to DB record set
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithSiteId" and "SQLConstants_Configuration"
    And Generate new "ConfigurationBySystem" using "System" in the API URL and Execute Get By "system" API request for a different Site Id than what is being used in the token and validate that the api should return empty list in the response body for Crud API Configuration 

@CrudAPIConfiguration @getAPIRequestResponseForSystemAndGroupColumnValidation
Scenario: Validate Get Crud API Configuration by System and Group column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Generate new "ConfigurationBySystemGroup" using "Group" and "System" in the API URL and Execute Crud Get By "system" and "group" request and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForSystemAndGroup" and "SQLConstants_Configuration" 
    And Compare values from API response set to DB record set
   
@CrudAPIConfiguration @getOrAddAPIRequestResponseForSiteIdColumnValidation
Scenario: Validate Get Or Add Crud API Post Configuration by SiteId method and validate the fetched or add details 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Generate new "GetOrAddConfiguration" API URL and Execute Crud Post request for existing record and Set DTO Objects for Crud API Configuration
    And Compare values from API response set to DB record set for a single returned record
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Generate new "GetOrAddConfiguration" API URL and Execute Crud Post request for new record and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
    And Compare values from API response set to DB record set for a single returned record
    And Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration
    
@CrudAPIConfiguration @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Configuration for all the site claims that user has access to and validate the response with DB record set
    Then Execute Crud Configuration API And Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDataForAllSiteClaims" and "SQLConstants_Configuration"
    And Compare values from API response set to DB record set
    
@CrudAPIConfiguration @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API Configuration and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Configuration API with an incorrect Site id  for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIConfiguration @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API Configuration with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate new Get Configuration by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body

@CrudAPIConfiguration @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate The Delete Crud API Configuration with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query 
    Then Establish Database Connection While Executing SQL Query "insertNewRecordConfigurationTable" and "SQLConstants_Configuration"
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithDesSiteId" and "SQLConstants_Configuration"
    And Generate new Delete Configuration API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithDesSiteId" and "SQLConstants_Configuration"
    And Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration

@CrudAPIConfiguration @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Crud API Post Configuration insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post Configuration API using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Post API Configuration and Set DTO Objects for Crud API Configuration
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithDesSiteId" and "SQLConstants_Configuration"
    And Generate new Delete configuration API URL and Execute Crud Delete API request and Set DTO Objects for Crud API Configuration

@CrudAPIConfiguration @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API Configuration update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator" and "SQLConstants_AircraftTypeAndAirline"
    And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchConfigurationDetailsWithSiteId" and "SQLConstants_Configuration"
    Then Generate new put Configuration API URL using different Site id than what is being used in the token and validate that the api should return error in the response body
