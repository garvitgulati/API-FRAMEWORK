@td_AD_PM_API_CreateProgram
Feature: Creation of a Program Service
	

Scenario Outline: Verify Creation of a Program Service API for Product master with valid test data
		Given base "CreateProgram" post uri is available
		When the "create" request json is created from "CreateProgram.json" with <Valid_Data> data from "CreateProgram.xlsx"
		And replace "programCode" with unique code
		And send POST request
		Then Expected reponse code should be "200"

		Examples: 
			| Valid_Data                   |
			| Valid_CreateProgram_SCN01_01 |


