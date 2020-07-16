using LTICSharpAutoFramework.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Unicorn.Browsers;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Gherkin.Model;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using LTICSharpAutoFramework;

namespace LTICSharpAutoFramework
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private static ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;
        bool flagUI = false;
        private static ExtentTest featureName;
        private static ExtentTest scenario;

        private static ExtentReports extent;
        public static ExtentTest stepNode;

        [BeforeTestRun]
        public static void InitializeExtentReports()
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var HtmlReporter = new ExtentHtmlReporter(assemblyPath);
            extent = new ExtentReports();
            extent.AttachReporter(HtmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
        }

        [BeforeStep]
        public void BeforeStep()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    stepNode = scenario.CreateNode<Given>("Given: " + _scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    stepNode = scenario.CreateNode<When>("When: " + _scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    stepNode = scenario.CreateNode<Then>("Then: " + _scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    stepNode = scenario.CreateNode<And>("And: " + _scenarioContext.StepContext.StepInfo.Text);
            }
        }

        [AfterStep]
        public void AfterStep()
        {
            if (_scenarioContext.TestError != null)
            {
                if (flagUI == true)
                    stepNode.Fail("This step has failed and the Error is ::" + _scenarioContext.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(WebActions.TakeScreenshot()).Build());
                else
                    stepNode.Fail("This step has failed and the Error is :: " + _scenarioContext.TestError.Message);
            }

            //if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
            //{
            //    if (stepType == "Given")
            //    scenario.CreateNode<Given>("Given: " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            //    else if (stepType == "When")
            //        scenario.CreateNode<When>("When: " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            //    else if (stepType == "Then")
            //        scenario.CreateNode<Then>("Then: " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            //    else if (stepType == "And")
            //        scenario.CreateNode<And>("And: " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            //}
        }
        [BeforeScenario]
        public void BeforeScenarioLoadTestData(ScenarioContext scenarioContext)
        {
            String tdTags = "";

            String[] featureTagNames = _featureContext.FeatureInfo.Tags;
            foreach (String tagName in featureTagNames)
            {
                if (tagName.Contains("td_"))
                {
                    String temp = tagName.Substring(3);
                    tdTags = tdTags + "'" + temp + "'" + ",";
                }
            }

            if (tdTags.Length > 0)
            {
                /* removing last ',' */
                tdTags = tdTags.Substring(0, tdTags.Length - 1);
             //   GlobalVariables.testData = DBUtils.GetData(tdTags);
            }

            //  GlobalVariables.testData = DBUtils.GetData("FXLApp_FXL_API_TRUSTFinancialInformation");
            //  GlobalVariables.testData.AddRange(DBUtils.GetData("AD_PM_API_CreateProgram"));
            // GlobalVariables.testData = DBUtils.GetData("AD_PM_API_CreateProgram");
        }
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

            String[] featureTagNames = _featureContext.FeatureInfo.Tags;
            foreach (String tagName in featureTagNames)
            {
                if (tagName.Contains("UI"))
                {
                    flagUI = true;
                    /* Initializing Driver */
                    GlobalVariables.browserName = FXLConfig.GetConfiguration().BrowserName;
                    GlobalVariables.browser = new Browser(GlobalVariables.browserName);
                    GlobalVariables.driver = GlobalVariables.browser.GetDriver();

                    /* Initializing Action class */
                    WebActions.Initialize(GlobalVariables.driver, GlobalVariables.waitTime);
                    //Clearing History
                    //  SignInPage signInPage = new SignInPage();
                    //    if (tagName.Contains("ClearData"))
                    //    {
                    //         WebActions.OpenURL("chrome://settings/clearBrowserData");
                    //         signInPage.ClickOnClearDataAndWaitToClear();
                }

                //Navgating to FXL App
                //      WebActions.OpenURL(FXLConfig.GetConfiguration().AppUrl);

                /*login into the FXP APP */
                //SignInPage signInPage = new SignInPage();
                //   signInPage.cancelNotification();
                //   new HomePage().ClickOnFXL_APPLICATIONTab();
                //    signInPage.SignIN();
            }

            //   GlobalVariables.testData = DBUtils.GetData("FXLApp_FXL_API_TRUSTFinancialInformation");
            //   GlobalVariables.testData.AddRange(DBUtils.GetData("AD_PM_API_CreateProgram"))
        }
        [AfterScenario(Order = 2)]
        public void BrowserClean()
        {
            if (flagUI == true)
            {
                GlobalVariables.driver.Manage().Cookies.DeleteAllCookies();
                GlobalVariables.driver.Quit();
                GlobalVariables.driver = null;
            }
        }

        [AfterScenario(Order = 1)]
        public void TakeScreenshot()
        {
            if (flagUI == true)
            {
                if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                {
                    WebActions.TakeScreenshot();
                }
            }
        }
    }
}
