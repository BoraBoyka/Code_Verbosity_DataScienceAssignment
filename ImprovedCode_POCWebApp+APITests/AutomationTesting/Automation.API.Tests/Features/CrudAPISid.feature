Feature: CrudAPISid

Background:
Given Read API Config values for Env Info stored in ConfigSetting and Set DTO Objects
Then Generate JWT Authorization Token for Environment Info stored in ConfigSetting

@CrudAPISid @getAPIRequestResponseValidation
Scenario: Validate Get Crud API Sid and validate the response with DB record set
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteZRHForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Execute Get Crud Sid API And Set DTO Objects for Crud API Sid for site claim "ZRH"
    Then Establish Database Connection While Executing SQL Query "fetchSidDataForDynamicSite" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for Crud Sid Get API
    Then Execute Get Crud Sid API And Set DTO Objects for Crud API Sid for site claim "MGL" 
    And Validate that the response returned from the above step should be an empty List

@CrudAPISid @getAPIRequestResponseForSidShortNameColumnValidation
Scenario: Validate Get Crud API Sid by Short Name column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayData" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "insertSidDataForTXLSite" and "SQLConstants_SidAndSidSeparation"
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteZRHForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchTop1SidShortNameForDynamicSite" and "SQLConstants_SidAndSidSeparation"
    And Generate new "GetByShortName" using "SidShortName" in the API URL and Execute Get By "sidShortName" API request and Set DTO Objects for Crud API Sid 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForShortNameSid" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for a single returned record for Crud Sid API
    Then Establish Database Connection While Executing SQL Query "fetchSidDetailsWithSiteId" and "SQLConstants_SidAndSidSeparation"
    And Generate new "GetByShortName" using "SidShortName" in the API URL and Execute Get By "sidShortName" API request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Sid 
    Then Establish Database Connection While Executing SQL Query "deleteRecentAddedSidShortName" and "SQLConstants_SidAndSidSeparation"

@CrudAPISid @getAPIRequestResponseForSidFullNameColumnValidation
Scenario: Validate Get Crud API Sid by Full Name column and validate the fetched details for the column
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedRunwayData" and "SQLConstants_StandStandAreaRunway"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "insertSidDataForTXLSite" and "SQLConstants_SidAndSidSeparation"
    Then Fetch value for site "ZRH" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteZRHForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchTop1SidFullNameForDynamicSite" and "SQLConstants_SidAndSidSeparation"
    And Generate new "GetByFullName" using "SidFullName" in the API URL and Execute Get By "sidFullName" API request and Set DTO Objects for Crud API Sid 
    Then Establish Database Connection While Executing SQL Query "fetchRecordDetailsForFullNameSid" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for a single returned record for Crud Sid API
    Then Establish Database Connection While Executing SQL Query "fetchSidDetailsWithSiteId" and "SQLConstants_SidAndSidSeparation"
    And Generate new "GetByFullName" using "SidFullName" in the API URL and Execute Get By "sidFullName" API request for a different Site Id than what is being used in the token and validate that the api should return error in the response body for Crud API Full Name Sid 
    Then Establish Database Connection While Executing SQL Query "deleteRecentAddedSidShortName" and "SQLConstants_SidAndSidSeparation"

@CrudAPISid @putAPIRequestResponseValidation
Scenario: Validate Put Crud Sid API and validate the record gets added into DB
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetails" and "SQLConstants_StandStandAreaRunway"
    Then Execute Crud Put Sid API and Set DTO Objects for Crud Sid API
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSidShortName" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for Crud Sid Get API
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetails" and "SQLConstants_StandStandAreaRunway"
    Then again Execute Crud Put Sid API using the same sid short and full name and validate that it should return the same result in the response body as we have above
    Then Establish Database Connection While Executing SQL Query "fetchRecentAddedSidShortName" and "SQLConstants_SidAndSidSeparation"
    And Compare values from API response set to DB record set for Crud Sid Get API
    Then Establish Database Connection While Executing SQL Query "deleteRecentAddedSidShortName" and "SQLConstants_SidAndSidSeparation"

@CrudAPISid @putAPIRequestResponseValidation
Scenario: Validate Put Crud Sid API for a site that user doesn't have access to and validate that the response should throw an error
    Then Fetch value for site "TXL" and use it in the below sql query
    Then Establish Database Connection While Executing SQL Query "fetchSiteTXLForTestData" and "SQLConstants_ArrivalFlight"
    And Fetch value for field "Id" from Database returned from Query Response 
    Then Establish Database Connection While Executing SQL Query "fetchRunwayDetails" and "SQLConstants_StandStandAreaRunway"
    Then Execute Crud Put Sid API for incorrect Site and validate that user should get an error message in the response body

