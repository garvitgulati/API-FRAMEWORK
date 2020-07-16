using LTICSharpAutoFramework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;

namespace LTICSharpAutoFramework.StepDefs
{
    [Binding]
    public sealed class Google
    {

        IWebDriver driver  = GlobalVariables.driver;
        [Given("Google Chrome is avaialable to open")]
        public void GoogleChromeisavaialabletoopen()
        {
            WebActions.OpenURL("https://www.google.com/");
            var X= driver.FindElement(By.Name("q"));
            X.SendKeys("Automation Testing");
            X.SendKeys(Keys.Enter);
            Thread.Sleep(2000);

           

        }
        
    }
}
