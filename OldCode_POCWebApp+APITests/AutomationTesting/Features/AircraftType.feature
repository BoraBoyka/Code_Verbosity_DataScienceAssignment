#Feature: AircraftType
#Background:
#        Given Launch Chrome in headless mode
#
#@AircraftType  @regressionUpdateOperation
#Scenario: Validate Update AircraftType Operation
#        Then Click "AircraftDetails" for <RowNumber> displayed on "Aircraft"
#        And Update value <Engine> against "AircraftTypeEngine" field displayed on "Aircraft"
#        And Update value <NoOfEngine> against "AircraftTypeNoOfEngines" field displayed on "Aircraft"
#        And Update value <Icao> against "AircraftTypeIcao" field displayed on "Aircraft"
#        And Update value <Iata> against "AircraftTypeIata" field displayed on "Aircraft"
#        And Update value <Type_Name> against "AircraftTypeTypeName" field displayed on "Aircraft"
#        And Update value <Size_Code> against "AircraftTypeSizeCode" field displayed on "Aircraft"
#        And Update value <Width> against "AircraftTypeWidth" field displayed on "Aircraft"
#        And Update value <Wvc> against "AircraftTypeWvc" field displayed on "Aircraft"
#        And Update value <Speed_Class> against "AircraftTypeSpeedClass" field displayed on "Aircraft"
#        Then Click "Submit_Btn" displayed on "Aircraft" 
#        And Fetch value for field "Fetch_Id_Text_Row" and "Fetch_Id_Text_Col" displayed on "Aircraft" for <RowNumber> from application
#        Then Establish Database Connection While Executing SQL Query "selectTableAircraftType"
#        And validate if SQL Query response has records or not
#        And Validate the Updated values for "Engine" <Engine> "ICAO" <Icao> "IATA" <Iata> "TypeName" <Type_Name> "SizeCode" <Size_Code> "Width" <Width> "Wvc" <Wvc> "SpeedClass" <Speed_Class> and "NumberOfEngines" <NoOfEngine> fields in Application against Database 
#       
#        Examples: 
#        | Engine       | NoOfEngine | Icao | Iata | Type_Name      | RowNumber  | Size_Code | Width | Wvc | Speed_Class |
#        | HELI_Test1   | 1          | A124 | A1F  | Augusta_Test1  | 1          | NUL       | 1.00  | L   | V6_Jet      |
#        #| HELI_Test2   | 2          | A1%$ | A2F  | Augusta_Test2  | 2          | NUL       | 2.00  | L   | V6_Jet      |
#        #| HELI_Test3   | 3          | A122 | A3F  | Augusta_Test3  | 3          | NUL       | 3.00  | M   | V6_Jet      |
#        #| HELI_Test4   | 4          | A123 | A4F  | Augusta_Test4  | 4          | NUL       | 4.00  | M   | V6_Jet      |
#        #| HELI_Test5   | 5          | A12@ | A5F  | Augusta_Test5  | 5          | NUL       | 5.00  | L   | V6_Jet      |
#        #| HELI_Test6   | 6          | A125 | A6F  | Augusta_Test6  | 6          | NUL       | 6.00  | L   | V6_Jet      |
#        #| HELI_Test7   | 7          | A126 | A7F  | Augusta_Test7  | 7          | NUL       | 7.00  | M   | V6_Jet      |
#        #| HELI_Test8   | 8          | A127 | A8F  | Augusta_Test8  | 8          | NUL       | 8.00  | M   | V6_Jet      |
#        #| HELI_Test9   | 9          | A128 | A9F  | Augusta_Test9  | 9          | NUL       | 9.00  | L   | V6_Jet      |
#        #| HELI_Test10  | 10         | A129 | A10F | Augusta_Test10 | 10         | NUL       | 10.00 | L   | V6_Jet      |
#        
#@AircraftType @regressionWarningMessagesOperation
#Scenario: Validate Warning messages validation displayed on Aircraft Type screen
#         Then Click "AircraftDetails" for <RowNumber> displayed on "Aircraft"
#         And Clear value against "AircraftTypeIcao" displayed on "Aircraft"
#         And Clear value against "AircraftTypeEngine" displayed on "Aircraft"
#         And Clear value against "AircraftTypeTypeName" displayed on "Aircraft"
#         And Clear value against "AircraftTypeWidth" displayed on "Aircraft"
#         And Clear value against "AircraftTypeNoOfEngines" displayed on "Aircraft"
#         And Clear value against "AircraftTypeSizeCode" displayed on "Aircraft"
#         And Clear value against "AircraftTypeWvc" displayed on "Aircraft"
#         And Clear value against "AircraftTypeSpeedClass" displayed on "Aircraft"
#         Then Fetch "ErrorMessageText" for all the <SequenceNumbers> displayed on "Aircraft"
#         And Validate the <ErrorMessages_Screen> are displayed appropriately on screen
#
#         Examples: 
#         | RowNumber | SequenceNumbers  | ErrorMessages_Screen    |                                                                                                              
#         | 1         | 8                | Icao Is Required^Engine Is Required^Type Name Is Required^Width Cannot Convert To Decimal : Input string was not in a correct format.^NumberOfEngines Cannot Convert To Int32 : Input string was not in a correct format.^Size Code Is Required^Wvc Is Required^Speed Class Is Required   |
#
#
#@AircraftType  @regressionAddOperation
#Scenario: Validate Add AircraftType Operation 
#        Then Click "Add_Btn" displayed on "Aircraft"
#        Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator"
#        And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
#        And Enter values against "AircraftTypeEngine" <Engine> "AircraftTypeIcao" <Icao> "AircraftTypeIata" <Iata> "AircraftTypeTypeName" <Type_Name> "AircraftTypeSizeCode" <Size_Code> "AircraftTypeWidth" <Width> "AircraftTypeWvc" <Wvc> "AircraftTypeSpeedClass" <Speed_Class> and "AircraftTypeNoOfEngines" <NoOfEngine> fields displayed on "Aircraft"
#        And Double Click "Submit_Btn" displayed on "Aircraft"
#        Then Establish Database Connection While Executing SQL Query "fetchTotalRows"
#        And Fetch value for field "totalRows" from Database
#        And Fetch value for field ID against entered record using "Fetch_Id_Text_Row" and "Fetch_Id_Text_Col" displayed on "Aircraft" application
#        Then Establish Database Connection While Executing SQL Query "selectTableAircraftType"
#        And validate if SQL Query response has records or not
#        And Validate the Added values for "Id" "Engine" <Engine> "NumberOfEngines" <NoOfEngine> "ICAO" <Icao> "IATA" <Iata> "TypeName" <Type_Name> "SizeCode" <Size_Code> "Width" <Width> "Wvc" <Wvc> and "SpeedClass" <Speed_Class> fields in Application against Database 
#       
#        Examples: 
#        | Engine        | NoOfEngine | Icao     | Iata  | Type_Name         | Size_Code | Width | Wvc | Speed_Class |
#        | DummyValue26  | 26         | A14026   | A4026 | Antonov AN-14026  | NUL       | 1.00  | L   | V6_Jet      |
#
#@AircraftType @regressionDeleteOperation
#Scenario: Validate Delete AircraftType Operation 
#        Then Delete "DeleteAircraftDetails" for <RowNumber> displayed on "Aircraft"
#        And Click "Delete_Btn" displayed on "Aircraft"
#        And Fetch value for field "Fetch_Id_Text_Row" and "Fetch_Id_Text_Col" displayed on "Aircraft" for <RowNumber> from application
#        Then Establish Database Connection While Executing SQL Query "selectTableAircraftType"
#        And Validate the deleted record details from the application against Database
#        And Delete "DeleteAircraftDetails" for <RowNumber> displayed on "Aircraft"
#        And Click "Cancel_Btn" displayed on "Aircraft"
#        And Fetch value for field "Fetch_Id_Text_Row" and "Fetch_Id_Text_Col" displayed on "Aircraft" for <RowNumber> from application
#        Then Establish Database Connection While Executing SQL Query "selectTableAircraftType"
#        And Validate the deleted record details from the application against Database
#        
#        Examples: 
#        | RowNumber |
#        | 1         |
#        #| 5         |
#
#@AircraftType @regressionTestMessagesOperation
#Scenario: Validate tests Validation on AircraftType Screen
#       Then Fetch value for Icao against <RowNumber> <ColNumber> displayed on "Aircraft", "Fetch_Id_Text_Row" and "Fetch_Id_Text_Col" from application
#       Then Fetch value for Iata against <RowNumber> <ColNumber> displayed on "Aircraft", "Fetch_Id_Text_Row" and "Fetch_Id_Text_Col" from application
#       Then Click "Add_Btn" displayed on "Aircraft"
#       And Enter values against "AircraftTypeEngine" <Engine> "AircraftTypeIcao" "AircraftTypeIata" "AircraftTypeTypeName" <Type_Name> "AircraftTypeSizeCode" <Size_Code> "AircraftTypeWidth" <Width> "AircraftTypeWvc" <Wvc> "AircraftTypeSpeedClass" <Speed_Class> and "AircraftTypeNoOfEngines" <NoOfEngine> fields displayed on "Aircraft"
#       And Double Click "Submit_Btn" displayed on "Aircraft"
#       And Validate the Error "MessageValidation" displayed on "Aircraft"
#       And Clear value against "AircraftTypeIcao" displayed on "Aircraft"
#       Then Fetch "ErrorMessageText" displayed on "Aircraft"
#       And Validate the <ErrorMessages_Screen> displayed on screen
#       Then Enter value against "AircraftTypeIcao" and Clear value against "AircraftTypeIata" displayed on "Aircraft"
#       And Double Click "Submit_Btn" displayed on "Aircraft"
#       And Validate the Error "MessageValidation" displayed on "Aircraft"
#       And Clear value against "AircraftTypeIcao" displayed on "Aircraft"
#       Then Fetch "ErrorMessageText" displayed on "Aircraft"
#       And Validate the <ErrorMessages_Screen> displayed on screen
#       
#      
#       Examples: 
#       | Engine       | NoOfEngine | Type_Name        | Size_Code | Width | Wvc | Speed_Class | RowNumber | ColNumber | ColNumber1 | ErrorMessages_Screen |
#       | DummyValue26 | 26         | Antonov AN-14026 | NUL       | 1.00  | L   | V6_Jet      | 1         | 2         | 3          | Icao Is Required     |
#
#
##@AircraftType @TestData
##Scenario: LoginApplication
##    And Enter User name fetched from data sheet for iteration "a"
##    And Enter User password fetched from data sheet for iteration "a"
##    And Click on LOG IN button
#
#@AircraftType @IterationTestDataFetchPOC
#Scenario: Validate_Update_AircraftType
#        Then "a" Click "AircraftDetails" displayed on "Aircraft" for RowNumber fetched from data sheet 
#        And "a" Update values "Engine" "NoOfEngine" "Icao"  "Iata" "Type_Name" against "AircraftTypeEngine" "AircraftTypeNoOfEngines" "AircraftTypeIcao" "AircraftTypeIata" "AircraftTypeTypeName" field displayed on "Aircraft"
#        And "a" Update value "Size_Code" "Width" "Wvc" "Speed_Class" against "AircraftTypeSizeCode" "AircraftTypeWidth" "AircraftTypeWvc" "AircraftTypeSpeedClass" field displayed on "Aircraft"
#        Then Click "Submit_Btn" displayed on "Aircraft" 
#        And "a" Fetch value for field "Fetch_Id_Text_Row" and "Fetch_Id_Text_Col" displayed on "Aircraft" for RowNumber fetched from data sheet
#        Then Establish Database Connection While Executing SQL Query "selectTableAircraftType"
#        And Validate Updated values for "Engine" "Engine" "ICAO" "Icao" "IATA" "Iata" "TypeName" "Type_Name" "SizeCode" "Size_Code" "Width" "Width" "Wvc" "Wvc" "SpeedClass" "Speed_Class" and "NumberOfEngines" "NoOfEngine" fields in Application against Database 
#        Then "b" Click "AircraftDetails" displayed on "Aircraft" for RowNumber fetched from data sheet 
#        And "b" Update values "Engine" "NoOfEngine" "Icao"  "Iata" "Type_Name" against "AircraftTypeEngine" "AircraftTypeNoOfEngines" "AircraftTypeIcao" "AircraftTypeIata" "AircraftTypeTypeName" field displayed on "Aircraft"
#        And "b" Update value "Size_Code" "Width" "Wvc" "Speed_Class" against "AircraftTypeSizeCode" "AircraftTypeWidth" "AircraftTypeWvc" "AircraftTypeSpeedClass" field displayed on "Aircraft"
#        Then Click "Submit_Btn" displayed on "Aircraft" 
#        And "b" Fetch value for field "Fetch_Id_Text_Row" and "Fetch_Id_Text_Col" displayed on "Aircraft" for RowNumber fetched from data sheet
#        Then Establish Database Connection While Executing SQL Query "selectTableAircraftType"
#        And Validate Updated values for "Engine" "Engine" "ICAO" "Icao" "IATA" "Iata" "TypeName" "Type_Name" "SizeCode" "Size_Code" "Width" "Width" "Wvc" "Wvc" "SpeedClass" "Speed_Class" and "NumberOfEngines" "NoOfEngine" fields in Application against Database 
     
    