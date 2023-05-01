Feature: CrudAPIWakeTurbulenceCategory

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting 

@CrudAPIWakeTurbulenceCategory @getAPIRequestResponseValidation
Scenario: Validate Get Crud API WakeTurbulenceCategory and validate the response with DB record set
    Then Execute Crud WakeTurbulenceCategory API And Set DTO Objects for Crud API WakeTurbulenceCategory for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryOutput" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for Crud WakeTurbulenceCategory API

@CrudAPIWakeTurbulenceCategory @postAPIRequestResponseValidation
Scenario: Validate Crud API Post WakeTurbulenceCategory and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeTurbulenceCategory API and Set DTO Objects for Crud API WakeTurbulenceCategory 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for a single returned record for Crud WakeTurbulenceCategory API 
    Then again Execute Crud Post WakeTurbulenceCategory API with a duplicate Category and Category name value and verify that it should return error via API for Crud WakeTurbulenceCategory API 
    And Generate new Delete WakeTurbulenceCategory API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeTurbulenceCategory

@CrudAPIWakeTurbulenceCategory @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API WakeTurbulenceCategory by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Get WakeTurbulenceCategory by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API WakeTurbulenceCategory 
    And Compare values from API response set to DB record set for a single returned record for Crud WakeTurbulenceCategory API 
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistWakeTurbulenceCategory" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud WakeTurbulenceCategory 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordWakeTurbulenceCategoryTable" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryDetailsWithSiteId" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Get WakeTurbulenceCategory by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API WakeTurbulenceCategory   
    Then Establish Database Connection While Executing SQL Query "deleteWakeTurbulenceCategoryDetails" and "SQLConstants_WakeTurbulenceAndSeparation"

@CrudAPIWakeTurbulenceCategory @putAPIRequestResponseValidation
Scenario: Validate Put Crud API WakeTurbulenceCategory and validate the record gets updated into DB 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeTurbulenceCategory API and Set DTO Objects for Crud API WakeTurbulenceCategory 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Generate new put WakeTurbulenceCategory API URL using database fetched values and Execute Crud Put API WakeTurbulenceCategory and Set DTO Objects for Crud API WakeTurbulenceCategory
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for a single returned record for Crud WakeTurbulenceCategory API 
    And Generate new Delete WakeTurbulenceCategory API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeTurbulenceCategory

@CrudAPIWakeTurbulenceCategory @deleteAPIRequestResponseValidation
Scenario: Validate Delete Crud API WakeTurbulenceCategory for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeTurbulenceCategory API and Set DTO Objects for Crud API WakeTurbulenceCategory 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeTurbulenceCategory API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeTurbulenceCategory
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud WakeTurbulenceCategory

@CrudAPIWakeTurbulenceCategoryList @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Add WakeTurbulenceCategory List and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Execute Crud Post "AddWakeTurbulenceCategoryList" API and Set DTO Objects for Crud API Add WakeTurbulenceCategory
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for Crud WakeTurbulenceCategory API
    Then Establish Database Connection While Executing SQL Query "deletedWakeTurbulenceCategoryDataForTop2Rows" and "SQLConstants_WakeTurbulenceAndSeparation"

@CrudAPIWakeTurbulenceCategoryList @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Add WakeTurbulenceCategory List and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Execute Crud Post "AddWakeTurbulenceCategoryList" API and Set DTO Objects for Crud API Add WakeTurbulenceCategory
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Execute Crud Put "UpdateWakeTurbulenceCategoryList" API and Set DTO Objects for Crud API Add WakeTurbulenceCategory
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for Crud WakeTurbulenceCategory API
    Then Establish Database Connection While Executing SQL Query "deletedWakeTurbulenceCategoryDataForTop2Rows" and "SQLConstants_WakeTurbulenceAndSeparation"

@CrudAPIWakeTurbulenceCategory @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API WakeTurbulenceCategory and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting  
    Then Execute Crud WakeTurbulenceCategory API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIWakeTurbulenceCategory @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Get Crud API WakeTurbulenceCategory with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordWakeTurbulenceCategoryTable" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryDetailsWithSiteId" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Get WakeTurbulenceCategory by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting  
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeTurbulenceCategory API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeTurbulenceCategory 

@CrudAPIWakeTurbulenceCategory @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Delete Crud API WakeTurbulenceCategory with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordWakeTurbulenceCategoryTable" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeTurbulenceCategory API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting   
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeTurbulenceCategory API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeTurbulenceCategory 

@CrudAPIWakeTurbulenceCategory @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API WakeTurbulenceCategory insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeTurbulenceCategory API using different Site id than what is being used in the token and validate that the api should return error in the response body 
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting
    Then Execute Crud Post WakeTurbulenceCategory API and Set DTO Objects for Crud API WakeTurbulenceCategory 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeTurbulenceCategory API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeTurbulenceCategory 

@CrudAPIWakeTurbulenceCategory @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API WakeTurbulenceCategory update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordWakeTurbulenceCategoryTable" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Establish Database Connection While Executing SQL Query "fetchWakeTurbulenceCategoryDetailsWithSiteId" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Generate new put WakeTurbulenceCategory API URL using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeTurbulenceCategoryData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeTurbulenceCategory API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeTurbulenceCategory 

