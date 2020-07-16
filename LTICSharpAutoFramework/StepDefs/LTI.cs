using LTICSharpAutoFramework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;

namespace LTICSharpAutoFramework
{
    [Binding]
    public class UnknownSteps
    {
        IWebDriver driver = GlobalVariables.driver;
        [Given(@"LTI Site is available")]
        public void GivenLTISiteIsAvailable()
        {
            WebActions.OpenURL("https://www.lntinfotech.com/");
        }
        
        [When(@"I clicl on Contact us")]
        public void WhenICliclOnContactUs()
        {
            // var X = driver.FindElement(By.LinkText("Contact Us"));
            var X = driver.FindElement(By.XPath("/html/body/div[5]/header/div/div[1]/nav/ul/li[3]/a"));
            WebActions.Click(X);
            Thread.Sleep(5000);
        }
        
        [Then(@"the data should be entered on the screen")]
        public void ThenTheDataShouldBeEnteredOnTheScreen()
        {
            //    driver.FindElement(By.Name("firstName")).SendKeys("Sneha");
            //    driver.FindElement(By.Name("lastName")).SendKeys("Pagadala");
            //    driver.FindElement(By.Name("emailAddress")).SendKeys("snehap20g22@gmail.com");
            //    driver.FindElement(By.Name("tel-48")).SendKeys("1234567890");
            //    Thread.Sleep(2000);

        }
    }
}
