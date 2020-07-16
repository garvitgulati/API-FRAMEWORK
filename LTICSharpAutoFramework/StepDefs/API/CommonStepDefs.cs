using LTICSharpAutoFramework.Utils;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using RestSharp;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace LTICSharpAutoFramework
{
    [Binding]
    public class APIOperationsTestingCallsApiForAllBasicOperationsSteps
    {
      //  String baseUrl = "https://petstore.swagger.io/#/pet";
        RestApiHelper restApiHelper = new RestApiHelper();
        String option;
        int rowIndex;
        string requestJsonFilePath = "";
        IRestResponse restResponse = null;

        public static String requestJson;

        [Given(@"user set base uri for ""(.*)"" api")]
        public void GivenUserSetBaseUriForApi(String baseUriKey)
        {
            String baseUrl = WebActions.GetTestDataFromDB(baseUriKey);


            restApiHelper.SetUrl(baseUrl," ");
        }
        
        [Given(@"user set base path for ""(.*)"" endpoint")]
        public void GivenUserSetBasePathForEndpoint(String basePathKey)
        {
            String basePath = WebActions.GetTestDataFromDB(basePathKey);

          restApiHelper.SetUrl(" ",basePath);
        }
        /// <summary>
        ///TO MAKE METHOD FOR SETBASE
        /// </summary>
        /// <param name="methodType"></param>
        /// <param name="jsonCondition"></param>


        [When(@"user execute ""(.*)"" request ""(.*)"" json")]
        public void WhenUserExecuteRequestJson(String methodType, String jsonCondition)
        {
            if (methodType == "POST" )
            {
                if (jsonCondition == "with")
                {
                    restApiHelper.CreatePostRequest(requestJson);
                }
            }
            else if(methodType == "PUT")
            {
                if (jsonCondition == "with")
                {
                    restApiHelper.CreatePutRequest(requestJson);
                }
            }
            else if (methodType == "GET")
            {
                if (jsonCondition == "without")
                {
                    restApiHelper.CreateGetRequest();
                }
            }
            else if (methodType == "DELETE")
            {
                if (jsonCondition == "without")
                {
                    restApiHelper.CreateDeleteRequest();
                }
            }

        }    

            
     
    
        
        
        [When(@"user form request json ""(.*)"" with test data in excel ""(.*)"" and sheet ""(.*)"" for scneario Valid_UpdatePet_(.*)")]
        public void WhenUserFormRequestJsonWithTestDataInExcelAndSheetForScnearioValid_UpdatePet_(String jsonFileName, String excelFileName, String sheetName, String optionFromFF)
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string excelFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"TestData\API\Excel\")) + excelFileName;
            string jsonFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"TestData\API\requestJson")) + jsonFileName;

            // String jsonFilePath = FileManager.getFileManagerObj().searchFile(userDir, jsonFileName);
            // String excelFilePath = FileManager.getFileManagerObj().searchFile(userDir, excelFileName);


            /* ExcelUtils objExcel = new ExcelUtils(ExcelOperation.LOAD, excelFilePath);
             XSSFSheet sheet = objExcel.getSheet(sheetName);

             /* getting row index 
             int scenarioColumnIndex = objExcel.getCellIndexByCellValue(sheet, 0, "Scenario");
             int rowIndex = objExcel.getRowIndexByCellValue(sheet, scenarioColumnIndex, scenario);

             RestApiUtils.requestJson = RestApiUtils.putExcelRowDataToJSON(jsonFilePath, excelFilePath, sheetName, rowIndex);

             objExcel.CloseWorkBook();*/

            option = optionFromFF;

            //Selecting sheet
            if (option.Contains("Invalid"))
            {
                sheetName = "Invalid";

            }
            else if (option.Contains("Valid"))
            {
                sheetName = "Valid";
            }
            else if (option.Contains("Duplicate"))
            {
                sheetName = "Duplicate";
            }
            else
            {
                Assert.Fail();
            }

            ExcelUtils.SetExcelFile(excelFilePath, sheetName);
            rowIndex = ExcelUtils.GetRowIndexByCellValue(0, option);
            ExcelUtils.CloseWorkBook();
            if (rowIndex > 0)
            {
                requestJson = RestApiHelperMethods.PutExcelRowDataToJSONReturnsString(requestJsonFilePath, excelFilePath,
                    sheetName, rowIndex);
            }
            else
            {
                Assert.Fail();
            }
        }
        
       
        
        [Then(@"user verifies response code as ""(.*)""")]
        public void ThenUserVerifiesResponseCodeAs(int statusCode)
        {

          //  restApiHelper.verifyStatusCode(RestApiUtils.response, Long.parseLong(statusCode));
            Assert.AreEqual(statusCode, (int)restResponse.StatusCode, "post request failed");
        }
       
        
        [Then(@"user reset rest assured parameters")]
        public void ThenUserResetRestAssuredParameters()
        {
            //RestApiUtils.reset();
        }
        
      
    }
}
