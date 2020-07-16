using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using LTICSharpAutoFramework.Utils;
using RestSharp;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using LTICSharpAutoFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LTICSharpAutoFramework
{
    [Binding]
    public class CreateProgram
    {

        RestClient restClient = null;
        RestApiHelper apiHelper = new RestApiHelper();
        String excelFilePath, sheetName, requestJsonFilePath = "";
        int rowIndex;
        public static String requestJson;

        String jsonString;
        String option;
        public static String refParameter;
        String referenceId;
        String referenceType;
        String duplicateCode, tokenValue;
        IRestResponse restResponse = null;


        [Given(@"base ""(.*)"" post uri is available")]
        public void GivenBasePostUriIsAvailable(string baseUrlKey)
        {
            String postBaseURI = WebActions.GetValue("BaseURI_PM_" + baseUrlKey, GlobalVariables.testData);
            restClient = apiHelper.SetUrl(postBaseURI, "");
        }
        
        [When(@"the ""(.*)"" request json is created from ""(.*)"" with Valid_CreateProgram_SCN(.*) data from ""(.*)""")]
        public void WhenTheRequestJsonIsCreatedFromWithValid_CreateProgram_SCNDataFrom(string notToUse, string jsonFileName, string optionFromFF, string excelFileName)
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            requestJsonFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\TestData\API\RequestJson\")) + jsonFileName;
            excelFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\TestData\API\Excel\")) + excelFileName;

            option = optionFromFF;

            //Selecting sheet
            if(option.Contains("Invalid"))
            {
                sheetName = "Invalid";

            }
            else if(option.Contains("Valid"))
            {
                sheetName = "Valid";
            }
            else if(option.Contains("Duplicate"))
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
            if(rowIndex > 0)
            {
                requestJson = RestApiHelperMethods.PutExcelRowDataToJSONReturnsString(requestJsonFilePath, excelFilePath,
                    sheetName, rowIndex);
            }
            else
            {
                Assert.Fail();
            }


        }
        
        [When(@"replace ""(.*)"" with unique code")]
        public void WhenReplaceWithUniqueCode(string parameter)
        {
           if(parameter.Equals("nothing"))
            {
                WebActions.AddLogToReport("Nothing to change to unique");

            }
           else
            {
                String[] indvParameter = parameter.Split(',');
                String oldParameter;


                ExcelUtils.SetExcelFile(excelFilePath, sheetName);
                rowIndex = ExcelUtils.GetRowIndexByCellValue(0, option);

                ExcelUtils.CloseWorkBook();

                Dictionary<String, string> map = new Dictionary<string, string>();
                map = RestApiHelperMethods.CreateMapFromExcelHeaders(excelFilePath, sheetName, rowIndex);

                for( int i = 0; i<indvParameter.Length;i++)
                {
                    long time = DateTime.Now.Ticks;

                    oldParameter = (String)map["$" + indvParameter[i]];

                    if(sheetName.Equals("Valid"))
                    {
                        map["$" + indvParameter[i]] = oldParameter + "AutomationTesting" + time;

                    }
                    if(rowIndex > 0)
                    {
                        requestJson = RestApiHelperMethods.CreateJsonReturnsString(requestJsonFilePath, map);

                    }
                    else
                    {
                        Assert.Fail();
                    }
                }

            }
        }
        

        [When(@"send POST request")]
        public void WhenSendPOSTRequest()
        {
            restResponse = apiHelper.GetResponse(apiHelper.CreatePostRequest(requestJson), restClient);
            WebActions.AddLogToReport("<b>JSON:-</b>" + restResponse.Content);

        }
        
        [Then(@"Expected reponse code should be ""(.*)""")]
        public void ThenExpectedReponseCodeShouldBe(int expectedStatusCode)

        {
            Assert.AreEqual(expectedStatusCode, (int)restResponse.StatusCode, "post request has failed and the returned status code is" + (int)restResponse.StatusCode);

        }
    }
}
