Feature: CrudAPISidSeparation

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPISidSeparation @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Sid Separation and validate the response with DB record set
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteZRHForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Execute Get Crud Sid Separation API And Set DTO Objects for Crud API Sid Separation for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchSidSeparationDataForDynamicSite" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for Crud Sid Separation Get API
    Then Execute Get Crud Sid Separation API And Set DTO Objects for Crud API Sid Separation for site claim "MGL" 
    And Validate that the response returned from the above step for Sid Separation matrix should be an empty List

@CrudAPISidSeparation @putAPIRequestResponseValidation
Scenario: Validate Put Crud Sid Separation API and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchTop1SidDetailsWithRunway" and "SQLConstants_SidAndSidSeparation"
    Then Execute Crud Put Sid Separation API and Set DTO Objects for Crud Sid Separation API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSidSeparationData" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for Crud Sid Separation Get API
    Then Establish Database Connection While Executing SQL Query "fetchTop1SidDetailsWithExistingRunway" and "SQLConstants_SidAndSidSeparation"
    Then again Execute Crud Put Sid Separation API using the existing Leader Sid and Follower Sid name and validate that it should return error in the response body
    Then Establish Database Connection While Executing SQL Query "deleteRecentAddedSidSeparation" and "SQLConstants_SidAndSidSeparation"
    Then Establish Database Connection While Executing SQL Query "deleteRecentAddedSidShortName" and "SQLConstants_SidAndSidSeparation"

@CrudAPISidSeparation @putAPIRequestResponseValidation
Scenario: Validate Put Crud Sid Separation API for a site that user doesn't have access to and validate that the response should throw an error
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteTXLForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchTop1SidDetailsWithRunway" and "SQLConstants_SidAndSidSeparation"
    Then Execute Crud Put Sid Separation API for incorrect Site and validate that user should get an error message in the response body
