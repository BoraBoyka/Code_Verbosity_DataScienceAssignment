Feature: AirsideOptimizerConfigurationScreen
Background:
	Given Launch Browser using URL
	Then Click "LogIn_Btn" displayed on "AirsideOptimizer"
	Then Enter UserName against "LogInUserName_Btn" and click on "Submit_Btn" to ensure that the logged in user is authenticated to the application displayed on "AirsideOptimizer"
	Then Enter Password against "Password_Btn" and click on "SignIn_Btn" and then click on "Submit_Btn" and ensure that the user is authenticated to the "AirsideOptimizer" application
	Then Click "ResourceData_Tab" displayed on "AirsideOptimizer"
	Then Click "ConfigTab_Btn" displayed on "ConfigurationScreen"

@AirsideOptimizerConfigurationScreen @configurationScreenWorkflows
Scenario: Click on Configuration tab and validate the Add, Delete, Edit Operation
	Then Establish Database Connection While Executing SQL Query "countNoOfRowsConfigurationTable" and "SQLConstants_Configuration"
	And Fetch value for field "Row_Count" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "countNoOfColumnsConfigurationTable" and "SQLConstants_Configuration"
	And Fetch value for field "Column_Count" from Database returned from Query Response 
	Then Establish Database Connection While Executing SQL Query "fetchConfigurationDataWithTrimSpaces" and "SQLConstants_Configuration"
	And Compare values from DB record set to Fetched values for field "Fetch_Id_Text_RowNumber" and "Fetch_Id_Text_ColValues" and "Fetch_IDColHeaderValue" "Fetch_NameColHeaderValue" "Fetch_ValueColHeaderValue" "Fetch_DescriptionColHeaderValue" "Fetch_SystemColHeaderValue" and "Fetch_GroupColHeaderValue" for "ID" "NAME" "VALUE" "DESCRIPTION" "SYSTEM" and "GROUP" displayed on "ConfigurationScreen" from application 

	# Warning Message Validation
	Then Click "Add_Btn" displayed on "ConfigurationScreen"
	Then Click "Update_Btn" displayed on "ConfigurationScreen"
	And Validate the "ErrorMessages_Screen" are displayed appropriately on screen fields displayed on "ConfigurationScreen"
	| ErrorMessages_ExpectedUI                                                                                                                         |
	| WarningValidation^'Name' must not be empty.^'Value' must not be empty.^'Description' must not be empty.^'System' must not be empty.^'Group' must not be empty. |
	And Enter values against "Configuration_Name" "Configuration_Value" "Configuration_Description" "Configuration_System" and "Configuration_Group" fields displayed on "ConfigurationScreen"
	 | Name			    | Value  | Description				| System	        | Group         |
     | EditConfig Name	| 15     | Test Description			| DMAN_Test123		| Test Group	|
	And Validate the "ErrorMessages_Screen" for System field is displayed appropriately on "ConfigurationScreen" 
	| ErrorMessage_SystemFieldUI                             |
	| The length of 'System' must be 10 characters or fewer. |
	Then Click "CancelBtn_AddConfigFlow" displayed on "ConfigurationScreen"

    # Multiple Add Operation
	Then Sort the Name column "Configuration_SortByNameColumn" "Configuration_NameDescendingSortValueSelect" in Descending order displayed on "ConfigurationScreen"
	Then Click "Add_Btn" And Enter values on the Add Screen against "Configuration_Name" "Configuration_Value" "Configuration_Description" "Configuration_System" and "Configuration_Group" and click "Update_Btn" and then click "ToastMessage_Close" popup displayed on "ConfigurationScreen"
	 | Name			| Value | Description				| System	| Group         |
     | ZTest Name11	| 7     | Test Description			| DMAN		| Test Group	|
	 | ZDummy Name	| 7     | Dummy Description			| DMAN		| Test Group	|
	 | ZTest Name2	| 7     | This is dummy Description | DMAN		| Test Group	|
	 | ZDummy Name2	| 7     | This is Test Description  | DMAN		| Test Group	|
	 | ZTest Name3	| 7     | Test Description again    | DMAN		| Test Group	|
	 | ZTest Name12	| 7     | Test Description			| DMAN		| Test Group	|
	 | ZDummy Name	| 7     | Dummy Description			| DMAN		| Test Group	|
	 | ZTest Name2	| 7     | This is dummy Description | DMAN		| Test Group	|
	 | ZDummy Name2	| 7     | This is Test Description  | DMAN		| Test Group	|
	 | ZTest Name3	| 7     | Test Description again    | DMAN		| Test Group	|

	Then Establish Database Connection While Executing SQL Query "fetchTop10ConfigurationData" and "SQLConstants_Configuration"
	And Validate all the Added values for "Value" "Description" "System" and "Group" fields in Application against Database 
	 | Name			| Value | Description				| System	| Group         |
     | ZTest Name11	| 7     | Test Description			| DMAN		| Test Group	|
	 | ZDummy Name	| 7     | Dummy Description			| DMAN		| Test Group	|
	 | ZTest Name2	| 7     | This is dummy Description | DMAN		| Test Group	|
	 | ZDummy Name2	| 7     | This is Test Description  | DMAN		| Test Group	|
	 | ZTest Name3	| 7     | Test Description again    | DMAN		| Test Group	|
	 | ZTest Name12	| 7     | Test Description			| DMAN		| Test Group	|
	 | ZDummy Name	| 7     | Dummy Description			| DMAN		| Test Group	|
	 | ZTest Name2	| 7     | This is dummy Description | DMAN		| Test Group	|
	 | ZDummy Name2	| 7     | This is Test Description  | DMAN		| Test Group	|
	 | ZTest Name3	| 7     | Test Description again    | DMAN		| Test Group	|

	# Add Operation
	Then Click "Add_Btn" displayed on "ConfigurationScreen"
	Then Establish Database Connection While Executing SQL Query "fetchMaxIDConfigurationTable" and "SQLConstants_Configuration"
	And Fetch value for field "Id" from Database returned from above sql Query
	Then Establish Database Connection While Executing SQL Query "fetchRandomNumberGenerator_UI" and "SQLConstants_AircraftTypeAndAirline"
	And Fetch value for field "Random_Number_Generator" from Database returned from Query Response 
	And Enter values against "Configuration_Name" "Configuration_Value" "Configuration_Description" "Configuration_System" and "Configuration_Group" fields displayed on "ConfigurationScreen"
	 | Name			| Value | Description				| System	| Group         |
     | ZZTest Name	| 7     | Test Description			| DMAN		| Test Group	|
	Then Click "Update_Btn" displayed on "ConfigurationScreen"
	And Validate the "ToastMessageValidation" for "Added successfully" displayed on "AirsideOptimizer"
	Then Establish Database Connection While Executing SQL Query "fetchRecentAddedConfigurationData" and "SQLConstants_Configuration"
	And Validate the Added values for "Id" "Name" "Value" "Description" "System" and "Group" fields in Application against Database 
	 | Name			| Value | Description				| System	| Group         |
     | ZZTest Name	| 7     | Test Description			| DMAN		| Test Group	|

	# Delete Operation
	Then Refresh the WebPage
	Then Click "ConfigTab_Btn" displayed on "ConfigurationScreen"
	Then Establish Database Connection While Executing SQL Query "countNoOfColumnsConfigurationTable" and "SQLConstants_Configuration"
	And Fetch value for field "Column_Count" from Database returned from Query Response 
	And Click "LastPage_Btn" displayed on "AirsideOptimizer"
	Then Fetch the record Id that the user is going to delete using "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "1" displayed on "AirsideOptimizer"
	Then "DeleteRecordDetails" "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "1" displayed on "AirsideOptimizer"
	And Validate the "Are you sure you would like to delete this Configuration?" "Warning_Message_Popup_Text" displayed on "AirsideOptimizer" 
	And Click "Delete_Btn" displayed on "AirsideOptimizer"
	And Validate the "ToastMessageValidation" for "Successfully Deleted" displayed on "AirsideOptimizer"
	Then Establish Database Connection While Executing SQL Query "fetchRecentDeletedConfigurationData" and "SQLConstants_Configuration"
	And Validate the deleted record details from the application against Database
	Then "DeleteRecordDetails" "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "1" displayed on "AirsideOptimizer"
	And Click "Cancel_Btn" displayed on "AirsideOptimizer"

	# Edit Operation
	Then Refresh the WebPage
	Then Click "ConfigTab_Btn" displayed on "ConfigurationScreen"
	Then Establish Database Connection While Executing SQL Query "countNoOfColumnsConfigurationTable" and "SQLConstants_Configuration"
	And Fetch value for field "Column_Count" from Database returned from Query Response 
	Then "EditRecordDetails" "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "2" displayed on "AirsideOptimizer"
	And Ensure that the element "Name_Textbox_DisableCheck" displayed on "ConfigurationScreen" is Disabled
	And Ensure that the element "Description_Textbox_DisableCheck" displayed on "ConfigurationScreen" is Disabled
	And Ensure that the element "System_Textbox_DisableCheck" displayed on "ConfigurationScreen" is Disabled
	And Ensure that the element "Group_Textbox_DisableCheck" displayed on "ConfigurationScreen" is Disabled
	And Update value "12" against "Value_Textbox_EditConfigScreen" field displayed on "ConfigurationScreen"
	Then Click "Update_Btn" displayed on "ConfigurationScreen" 
	And Validate the "ToastMessageValidation" for "Successfully Updated" displayed on "AirsideOptimizer"
	And Validate the Updated values for "12" using "Fetch_DynamicRowNumber" and "Fetch_DynamicColNumber" for "2" and "3" fields in Application against Database displayed on "AirsideOptimizer"