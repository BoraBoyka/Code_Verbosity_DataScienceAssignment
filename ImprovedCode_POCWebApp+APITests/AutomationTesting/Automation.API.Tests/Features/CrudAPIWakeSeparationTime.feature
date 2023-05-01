Feature: CrudAPIWakeSeparationTime

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting 

@CrudAPIWakeSeparationTime @getAPIRequestResponseValidation
Scenario: Validate Get Crud API WakeSeparationTime and validate the response with DB record set
    Then Execute Crud WakeSeparationTime API And Set DTO Objects for Crud API WakeSeparationTime for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchWakeSeparationTimeData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for Crud WakeSeparationTime API

@CrudAPIWakeSeparationTime @postAPIRequestResponseValidation
Scenario: Validate Post Crud API WakeSeparationTime and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeSeparationTime API and Set DTO Objects for Crud API WakeSeparationTime 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for a single returned record for Crud WakeSeparationTime API
    Then again Execute Crud Post WakeSeparationTime API with a duplicate Leader and Follower Id value and verify that it should return error via API for Crud WakeSeparationTime API 
    And Generate new Delete WakeSeparationTime API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeSeparationTime

@CrudAPIWakeSeparationTime @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API WakeSeparationTime by specific column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Get WakeSeparationTime by "Id" API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API WakeSeparationTime 
    And Compare values from API response set to DB record set for a single returned record for Crud WakeSeparationTime API 
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistWakeSeparation" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud WakeSeparationTime 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordWakeSeparationTimesTable" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Establish Database Connection While Executing SQL Query "fetchWakeSeparationTimesDetailsWithSiteId" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Get WakeSeparationTime by "Id" API URL and Execute Crud Get By Id request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API WakeSeparationTime  
    Then Establish Database Connection While Executing SQL Query "deleteWakeSeparationDetails" and "SQLConstants_WakeTurbulenceAndSeparation"

@CrudAPIWakeSeparationTime @getAPIRequestResponseForIdColumnValidation
Scenario: Validate Get Crud API WakeSeparationTime by leader and follower column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeSeparationTime API and Set DTO Objects for Crud API WakeSeparationTime 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Get WakeSeparationTime using Leader Category and Follower Category "LeaderWakeTurbulenceCategoryId" in the API URL and Execute Crud Get By Id request and Set DTO Objects for Crud API WakeSeparationTime
    Then Establish Database Connection While Executing SQL Query "fetchSeparationTimeForRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set for the one field to DB record set for Crud WakeSeparationTime API 
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistWakeSeparation" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Validate the GET API operation by "Id" API URL again using the ID that doesn't exist fetched from DB and it should return error via API for Crud WakeSeparationTime 
    
@CrudAPIWakeSeparationTime @putAPIRequestResponseValidation
Scenario: Validate Put Crud API WakeSeparationTime and validate the record gets updated into DB 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeSeparationTime API and Set DTO Objects for Crud API WakeSeparationTime
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Generate new put WakeSeparationTime API URL using database fetched values and Execute Crud Put API WakeSeparationTime and Set DTO Objects for Crud API WakeSeparationTime
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for a single returned record for Crud WakeSeparationTime API
    Then again Execute Crud Put WakeSeparationTime API and try to update the Leader Id and verify that it should return error via API for Crud WakeSeparationTime API
    Then again Execute Crud Put WakeSeparationTime API and try to update the Follower Id and verify that it should return error via API for Crud WakeSeparationTime API
    Then again Execute Crud Put WakeSeparationTime API and try to update the Leader Id and Separation Seconds and verify that it should return error via API for Crud WakeSeparationTime API
    Then again Execute Crud Put WakeSeparationTime API and try to update the Follower Id and Separation Seconds and verify that it should return error via API for Crud WakeSeparationTime API
    Then again Execute Crud Put WakeSeparationTime API and try to update Separation Seconds and keep Leader and Follower details the same and verify that this time the update should happen correctly
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for a single returned record for Crud WakeSeparationTime API
    And Generate new Delete WakeSeparationTime API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeSeparationTime

@CrudAPIWakeSeparationTime @deleteAPIRequestResponseValidation
Scenario: Validate WakeSeparationTime Delete Crud API for specific row and validate the fetched details for the row to be NULL in DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeSeparationTime API and Set DTO Objects for Crud API WakeSeparationTime 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeSeparationTime API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeSeparationTime
    Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Validate the GET API operation again using the same deleted ID and it should return error via API for Crud WakeSeparationTime

@CrudAPIWakeSeparationTime @postAPIRequestResponseValidation
Scenario: Validate Post Crud API Add WakeSeparationTime List and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Execute Crud Post "AddWakeSeparationTimeList" API and Set DTO Objects for Crud API Add WakeSeparationTime
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for Crud WakeSeparationTime API
    Then Establish Database Connection While Executing SQL Query "deletedWakeSeparationDataForTop2Rows" and "SQLConstants_WakeTurbulenceAndSeparation"

@CrudAPIWakeSeparationTime @putAPIRequestResponseValidation
Scenario: Validate Put Crud API Add WakeSeparationTime List and validate the record gets updated into DB
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdAscending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Execute Crud Post "AddWakeSeparationTimeList" API and Set DTO Objects for Crud API Add WakeSeparationTime
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Generate new put "UpdateWakeSeparationTimeList" API and Set DTO Objects for Crud API WakeSeparationTime
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedTopTwoWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Compare values from API response set to DB record set for Crud WakeSeparationTime API
    Then Establish Database Connection While Executing SQL Query "deletedWakeSeparationDataForTop2Rows" and "SQLConstants_WakeTurbulenceAndSeparation"

@CrudAPIWakeSeparationTime @getAPIRequestIncorrectSiteIdValidation
Scenario: Validate Get Crud API WakeSeparationTime and pass incorrect siteId in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchIdDoesntExistSite" and "SQLConstants_TaxiSequenceAndSite"
    And Generate JWT Authorization Token for Site Claim that doesnot exist for Environment Info stored in ConfigSetting  
    Then Execute Crud WakeSeparationTime API with an incorrect Site id for site claim "BadSite" and validate that the api should return error in the response body

@CrudAPIWakeSeparationTime @getAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate WakeSeparationTime Get Crud API with a different siteId than what is being passed in the token and validate that the api should not return any response
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordWakeSeparationTimesTable" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Establish Database Connection While Executing SQL Query "fetchWakeSeparationTimesDetailsWithSiteId" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Get WakeSeparationTime by "Id" API URL using different Site id "TXL" than what is being used in the token and validate that the api should return error in the response body
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting  
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeSeparationTime API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeSeparationTime 

@CrudAPIWakeSeparationTime @deleteAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate WakeSeparationTime Delete Crud API with a different Site Id passed in the token and validate that the record with another site id should not be deleted via api
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordWakeSeparationTimesTable" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeSeparationTime API URL with an different Site id than what is being used in the token and validate that the api should return error in the response body 
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting  
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeSeparationTime API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeSeparationTime 

@CrudAPIWakeSeparationTime @postAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Post Crud API WakeSeparationTime insert with a different Site Id than what is being passed in the token and validate that the record should not be inserted via api
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Execute Crud Post WakeSeparationTime API using different Site id than what is being used in the token and validate that the api should return error in the response body 
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Execute Crud Post WakeSeparationTime API and Set DTO Objects for Crud API WakeSeparationTime 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeSeparationTime API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeSeparationTime 

@CrudAPIWakeSeparationTime @putAPIRequestDifferentSiteIdThanTokenValidation
Scenario: Validate Put Crud API WakeSeparationTime update with a different Site Id than what is being passed in the token and validate that the record should not be updated via api
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    And Fetch value for field "Id" from Database returned from above sql Query
    Then Establish Database Connection While Executing SQL Query "insertNewRecordWakeSeparationTimesTable" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Establish Database Connection While Executing SQL Query "fetchWakeSeparationTimesDetailsWithSiteId" and "SQLConstants_WakeTurbulenceAndSeparation"
    Then Generate new put WakeSeparationTime API URL using different Site id than what is being used in the token and validate that the api should return error in the response body
    Then Establish Database Connection While Executing SQL Query "fetchSiteIdDescending" and "SQLConstants_TaxiSequenceAndSite"
    Then Generate JWT Authorization Token for one particular Site Claim that exist for Environment Info stored in ConfigSetting 
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedWakeSeparationData" and "SQLConstants_WakeTurbulenceAndSeparation"
    And Generate new Delete WakeSeparationTime API URL and Execute Crud Delete API request and Set DTO Objects for Crud API WakeSeparationTime 

