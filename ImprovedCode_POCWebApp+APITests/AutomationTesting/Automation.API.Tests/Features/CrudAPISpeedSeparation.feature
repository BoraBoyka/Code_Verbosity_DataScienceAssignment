Feature: CrudAPISpeedSeparation

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPISpeedSeparation @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Speed Separation and validate the response with DB record set
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteZRHForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Execute Get Crud Speed Separation API And Set DTO Objects for Crud API Speed Separation for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchSpeedSeparationDataForDynamicSite" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for Crud Speed Separation Get API
    Then Execute Get Crud Speed Separation API And Set DTO Objects for Crud API Speed Separation for site claim "MGL" 
    And Validate that the response returned from the above step for Speed Separation matrix should be an empty List

@CrudAPISpeedSeparation @putAPIRequestResponseValidation
Scenario: Validate Put Crud Speed Separation API and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchTop1SpeedMatrix" and "SQLConstants_SidAndSidSeparation"
    Then Execute Crud Put Speed Matrix API and Set DTO Objects for Crud Speed Matrix API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSpeedSeparationData" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for Crud Speed Separation Get API
    Then Establish Database Connection While Executing SQL Query "deleteRecentAddedSpeedSeparationData" and "SQLConstants_SidAndSidSeparation"

@CrudAPISpeedSeparation @putAPIRequestResponseValidation
Scenario: Validate Put Crud Speed Separation API for a site that user doesn't have access to and validate that the response should throw an error
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteTXLForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchTop1SpeedMatrix" and "SQLConstants_SidAndSidSeparation"
    Then Execute Crud Put Speed Separation API for incorrect Site and validate that user should get an error message in the response body