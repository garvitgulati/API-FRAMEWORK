using LTICSharpAutoFramework.Utils;
using Newtonsoft.Json.Linq;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using RestSharp;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;



namespace LTICSharpAutoFramework
{
    [Binding]
    public class APIOperationsTestingCallsApiForAllBasicOperationsStep
    {
        public static String petId;
        //   public static Response response;
        IRestResponse restResponse = null;
        RestApiHelper restApiHelper = new RestApiHelper();
        public RestRequest restRequest;
        public static String requestJson;
        [Given(@"user provide pet ID to retrive employee information")]
        public void GivenUserProvidePetIDToRetriveEmployeeInformation()
        {
            restRequest.AddParameter("petId", petId); ///DOUBT//
        }
        
        [When(@"user update pet Id in request json")]
        public string WhenUserUpdatePetIdInRequestJson()
        {
            Random random = new Random();
            int i,length=2;
            for(i=0;i<length;i++)
            {
                petId += random.Next(1, 9).ToString("0");
            }
            return petId;

            requestJson = requestJson.Replace("\"@unique-id\"", petId);
        }
        
        [When(@"user update existing pet Id in request json")]
        public void WhenUserUpdateExistingPetIdInRequestJson()
        {
            requestJson = requestJson.Replace("\"$id\"", petId);
        }
        
        [When(@"user update invalid pet Id in request json")]
        public string WhenUserUpdateInvalidPetIdInRequestJson()
        {
            Random random = new Random();
            int i, length = 2;
            for (i = 0; i < length; i++)
            {
                petId += random.Next(1, 9).ToString("0");
            }
            return petId;

            requestJson = requestJson.Replace("\"@unique-id\"", petId);
        }
        
        [Then(@"user verify pet name")]
        public void ThenUserVerifyPetName()
        {
           // String expectedPetName = JsonPath.parse(RestApiUtils.requestJson).read("$.name").toString();
            JObject obj = JObject.Parse(requestJson);
            String expectedPetName = Convert.ToString(obj.SelectToken("$.name"));
            String responseBody = restApiHelper.GetResponseContent(restResponse);

            JObject obj1 = JObject.Parse(responseBody);

            String actualPetName = Convert.ToString(obj1.SelectToken("$.name"));

            Hooks.stepNode.Info("Pet ID: " + petId);
            Hooks.stepNode.Info("Expected Pet Name: " + expectedPetName);
            Hooks.stepNode.Info("Actual Pet Name: " + actualPetName);

            Assert.AreEqual(actualPetName, expectedPetName, "Pet Name are NOT as expected.");

           // JSONAssert.assertEquals(requestJson, responseBody, JSONCompareMode.LENIENT);

        }

        [Then(@"verify response message as ""(.*)""")]
        public void ThenVerifyResponseMessageAs(String rspMessage)
        {
            String actualMsg = restApiHelper.GetResponseContent(restResponse);

            Hooks.stepNode.Info("Pet ID: " + petId);
            Hooks.stepNode.Info("Expected Error Message: " + rspMessage);
            Hooks.stepNode.Info("Actual Error Message: " + actualMsg);

            Assert.AreEqual(actualMsg, rspMessage, "Error message is not as expected.");
        }
    }
}
