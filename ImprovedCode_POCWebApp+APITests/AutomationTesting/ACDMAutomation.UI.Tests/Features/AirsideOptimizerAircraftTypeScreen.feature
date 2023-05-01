Feature: AirsideOptimizerAircraftTypeScreen
Background:
	Given Launch Browser using URL
	Then Click "LogIn_Btn" displayed on "AirsideOptimizer"
	Then Enter UserName against "LogInUserName_Btn" and click on "Submit_Btn" to ensure that the logged in user is authenticated to the application displayed on "AirsideOptimizer"
	Then Enter Password against "Password_Btn" and click on "SignIn_Btn" and then click on "Submit_Btn" and ensure that the user is authenticated to the "AirsideOptimizer" application
	Then Click "ResourceData_Tab" displayed on "AirsideOptimizer"
	Then Click "AircraftTab_Btn" displayed on "AircraftTypeScreen"

@AirsideOptimizerAircraftTypeScreen @aircraftTypeScreenWorkflows
Scenario: Click on AircraftType tab and validate the Add, Delete, Edit Operation
	Then Establish Database Connection While Executing SQL Query "updateTypeNameAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
	Then Establish Database Connection While Executing SQL Query "deleteRecentAddedRowsAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
	Then Establish Database Connection While Executing SQL Query "fetchTotalRowsAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Row_Count" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "countNoOfColumnsTableAircraftType" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Column_Count" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchTop10AircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
	And Compare values from DB record set to Fetched values for field "Fetch_Id_Text_RowNumber" and "Fetch_Id_Text_ColValues" "Fetch_Id_Text_Col_HeaderValues" "Fetch_IDColHeaderValue" "Fetch_IATAColHeaderValue" "Fetch_ICAOColHeaderValue" "Fetch_EngineColHeaderValue" "Fetch_TypeNameColHeaderValue" "Fetch_NoOfEnginesColHeaderValue" "Fetch_SizeCodeColHeaderValue" "Fetch_SpeedClassColHeaderValue" "Fetch_WidthColHeaderValue" and "Fetch_CategoryNameColHeaderValue" displayed on "AirsideOptimizer" from application 
	
	# Warning Message Validation
	Then Establish Database Connection While Executing SQL Query "deleteRecentAddedRowsAircraftTable" and "SQLConstants_AircraftTypeAndAirline"
	Then Click "Add_Btn" displayed on "AircraftTypeScreen"
	Then Click "Update_Btn" displayed on "AircraftTypeScreen"
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator_UI" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	And Validate the "ErrorMessages_Screen" are displayed appropriately on screen fields displayed on "ConfigurationScreen"
	| ErrorMessages_ExpectedUI                                                                                                                                                                                                                                                                                       |  
	| WarningValidation^'Icao' must not be empty.^'Engine' must not be empty.^'TypeName' must not be empty.^'Size Code' must not be empty.^'Speed Class' must not be empty.^'Category Name' must not be empty. |
	And Enter values against "AircraftType_IATA" "AircraftType_ICAO" "AircraftType_Engine" "AircraftType_TypeName" "AircraftType_SizeCode" "AircraftType_SpeedClass" "AircraftType_CategoryName" "AircraftType_CategoryNameValueSelection" "AircraftType_NoOfEngines" and "AircraftType_Width" fields displayed on "AircraftTypeScreen"
	| IATA | ICAO | Engine     | TypeName | SizeCode | SpeedClass | 
    | M    | M    | HELI_Test1 | Airbus   | D        | V6_Jet     | 
	And Enter duplicate value "A320" against "AircraftType_ICAO" and update a negative value against "AircraftType_NoOfEngines_Negative" and validate the "ErrorMessages_Screen" are displayed appropriately on screen fields displayed on "AircraftTypeScreen"
	| ErrorMessages_ExpectedUI                                                         |
	| WarningValidation^'Icao' already exists. It must be Unique.^'# of Engines' must be greater than or equal to 0. |
	Then Click "CancelBtn_AddConfigFlow" displayed on "ConfigurationScreen"

	# Multiple Add Operation
	Then Double click the "AircraftType_SortTypeNameColumn" "AircraftType_TypeNameDescendingSortValueSelect" displayed on "AircraftTypeScreen"
	Then Click "Add_Btn" And Enter values on the Add Screen against "AircraftType_IATA" "AircraftType_ICAO" "AircraftType_Engine" "AircraftType_TypeName" "AircraftType_NoOfEngines" "AircraftType_SizeCode" "AircraftType_SpeedClass" "AircraftType_Width" "AircraftType_CategoryName" and "AircraftType_CategoryNameValueSelection" and click "Update_Btn" and then click "ToastMessage_Close" popup displayed on "AircraftTypeScreen"
	 | IATA	 | ICAO	 | Engine		 | TypeName			| SizeCode   | SpeedClass |  
	 | O1    | 01	 | HELI_Test1    | ZZIVKO_Test1		| D          | V6_Jet     | 
	 | Z2    | Z2    | HELI_Test2    | ZZIVKO_Test2		| NIL        | V6_Jet     |
	 | W3    | W3    | HELI_Test3    | ZZIVKO_Test3		| D          | V6_Jet     |
	 | Y4    | Y4    | HELI_Test4    | ZZIVKO_Test4		| NIL        | V6_Jet     |
	 | U5    | U5    | HELI_Test5    | ZZIVKO_Test5		| D          | V6_Jet     |
	 | N6    | N6    | HELI_Test6    | ZZIVKO_Test6		| NIL        | V6_Jet     |
	 | N7    | N7    | HELI_Test7    | ZZIVKO_Test7		| D          | V6_Jet     |
	 | M8    | M8	 | HELI_Test8    | ZZIVKO_Test8		| NIL        | V6_Jet     |
	 | M9    | M9	 | HELI_Test9    | ZZIVKO_Test9		| D          | V6_Jet     |
	 | M0    | M0	 | HELI_Test10   | ZZIVKO_Test10	| NIL        | V6_Jet     |

	Then Establish Database Connection While Executing SQL Query "fetchTop10DescAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
	And Validate the Added values for "Engine" "TypeName" "NumberOfEngines" "SizeCode" "SpeedClass" "Width" and "CategoryName" fields in Application against Database 
	 | IATA | ICAO  | Engine      | TypeName		| SizeCode  | SpeedClass | CategoryName | NumberOfEngines | Width |
	 | O1	| O1	| HELI_Test1  | ZZIVKO_Test1	| D         | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | Z2	| Z2	| HELI_Test2  | ZZIVKO_Test2	| NIL       | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | W3	| W3	| HELI_Test3  | ZZIVKO_Test3	| D         | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | Y4	| Y4	| HELI_Test4  | ZZIVKO_Test4	| NIL       | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | U5	| U5	| HELI_Test5  | ZZIVKO_Test5	| D         | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | N6	| N6	| HELI_Test6  | ZZIVKO_Test6	| NIL       | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | N7	| N7	| HELI_Test7  | ZZIVKO_Test7	| D         | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | M8	| M8	| HELI_Test8  | ZZIVKO_Test8	| NIL       | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | M9	| M9	| HELI_Test9  | ZZIVKO_Test9	| D         | V6_Jet     | Super Heavy  | 2               | 1.00  |
	 | M0	| M0	| HELI_Test10 | ZZIVKO_Test10	| NIL       | V6_Jet     | Super Heavy  | 2               | 1.00  |
	
	# Add Operation
	Then Click "Add_Btn" displayed on "AircraftTypeScreen"
	Then Establish Database Connection While Executing SQL Query "fetchMaxIDAircraftTypeTable" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator_UI" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	And Enter values against "AircraftType_IATA" "AircraftType_ICAO" "AircraftType_Engine" "AircraftType_TypeName" "AircraftType_SizeCode" "AircraftType_SpeedClass" "AircraftType_CategoryName" "AircraftType_CategoryNameValueSelection" "AircraftType_NoOfEngines" and "AircraftType_Width" fields displayed on "AircraftTypeScreen"
	 | IATA | ICAO | Engine     | TypeName	| SizeCode | SpeedClass | 
     | U0   | U9   | HELI_Test1 | ZZZIVKO   | D        | V6_Jet     | 
	Then Click "Update_Btn" displayed on "AircraftTypeScreen"
	And Validate the "ToastMessageValidation" for "Added successfully" displayed on "AirsideOptimizer"
	Then Establish Database Connection While Executing SQL Query "fetchTop1AircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
	And Validate the Added values for "Id" "IATA" "ICAO" "Engine" "TypeName" "SizeCode" "SpeedClass" "CategoryName" "NumberOfEngines" and "Width" fields in Application against Database 
	 | IATA | ICAO | Engine     | TypeName	| SizeCode | SpeedClass | CategoryName | NumberOfEngines | Width    |
     | U0   | U9   | HELI_Test1 | ZZZIVKO   | D        | V6_Jet     | Super Heavy  | 2               | 1.00     |

	# Delete Operation
	Then Refresh the WebPage
	Then Click "AircraftTab_Btn" displayed on "AircraftTypeScreen"
	Then Establish Database Connection While Executing SQL Query "countNoOfColumnsTableAircraftType" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Column_Count" from Database returned from Query Response 
	And Click "LastPage_Btn" displayed on "AirsideOptimizer"
	Then Fetch the record Id that the user is going to delete using "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "1" displayed on "AirsideOptimizer"
	Then "DeleteRecordDetails" "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "1" displayed on "AirsideOptimizer"
	And Validate the "Are you sure you would like to delete this Aircraft Type?" "Warning_Message_Popup_Text" displayed on "AirsideOptimizer" 
	And Click "Delete_Btn" displayed on "AirsideOptimizer"
	And Validate the "ToastMessageValidation" for "Successfully Deleted" displayed on "AirsideOptimizer"
	Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
	And Validate the deleted record details from the application against Database
	Then "DeleteRecordDetails" "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "1" displayed on "AirsideOptimizer"
	And Click "Cancel_Btn" displayed on "AirsideOptimizer"

	# Edit Operation
	Then Refresh the WebPage
	Then Click "AircraftTab_Btn" displayed on "AircraftTypeScreen"
	Then Establish Database Connection While Executing SQL Query "countNoOfColumnsTableAircraftType" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Column_Count" from Database returned from Query Response 
	And Click "LastPage_Btn" displayed on "AirsideOptimizer"
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator_UI" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Random_Number_Generator" from Database returned from above sql Query
	Then "EditRecordDetails" "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "9" displayed on "AirsideOptimizer"
	And Update value "EE" and "F9" against "AircraftType_IATA" and "AircraftType_ICAO" fields displayed on "AircraftTypeScreen"
	And Update value "HELI_Test8" against "AircraftType_Engine" field displayed on "AircraftTypeScreen"
	And Update value "ZZZZ_Test" against "AircraftType_TypeName" field displayed on "AircraftTypeScreen"
	And Update value "D" against "AircraftType_SizeCode" field displayed on "AircraftTypeScreen"
	And Update value "V6_Jet" against "AircraftType_SpeedClass" field displayed on "AircraftTypeScreen"
	And Update value "AircraftType_CategoryName" against "AircraftType_CategoryNameValueSelection" dropdown field displayed on "AircraftTypeScreen"
	Then Click "Update_Btn" displayed on "AircraftTypeScreen" 
	And Validate the "ToastMessageValidation" for "Successfully Updated" displayed on "AirsideOptimizer"
	Then Establish Database Connection While Executing SQL Query "fetchTop1DescAircraftTypeData" and "SQLConstants_AircraftTypeAndAirline"
	And Validate the Updated values for "IATA" "EE" "ICAO" "F9" "Engine" "HELI_Test8" "TypeName" "ZZZZ_Test" "SizeCode" "D" "SpeedClass" "V6_Jet" and "CategoryName" "Super Heavy" fields in Application against Database 
	Then Establish Database Connection While Executing SQL Query "deleteRecentAddedRowsAircraftTable" and "SQLConstants_AircraftTypeAndAirline"