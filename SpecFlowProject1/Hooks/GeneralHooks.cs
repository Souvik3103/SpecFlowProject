using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProject1.Helpers;
using SpecFlowProject1.WebAutomation.PageObjects.Home;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Hooks
{
    [Binding]
    public class GeneralHooks
    {
        private static IWebDriver? myWebDriver;
        private static ScenarioContext myScenarioContext;
        private static FeatureContext myFeatureContext;
        static string selectedBrowser;

        public static GeneralHooks()
        {
            myScenarioContext = ScenarioContext();
            myFeatureContext = featureContext;
            selectedBrowser = TestContext.Parameters["selectedBrowser"].ToString();
        }

        [BeforeScenario]
        public static void SetUp() 
        {
            SystemUnderTest systemUnderTest = new SystemUnderTest();
            Console.WriteLine($"Scenario: {myScenarioContext.ScenarioInfo.Title}");
            ScenarioSetup(systemUnderTest);
        }

        public static void ScenarioSetup(SystemUnderTest systemUnderTest)
        {
            
            myScenarioContext.Set(systemUnderTest, "currentSystemUnderTest");
            int numberOfBrowserProcesses = NumberOfProcess(selectedBrowser);

            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.WriteLine("number of webdriver beforeScenario " + numberOfBrowserProcesses);

            

            Console.WriteLine($"Set driver for context {myScenarioContext.ScenarioInfo.Title}");
            //bool chromeHeadlessMode = myTestContext.Properties["chromeHeadlessMode"].ToString() == "true";
            //bool chromeLocal = myTestContext.Properties["chromeLocal"].ToString() == "true";

            myWebDriver = WebInit.InitWebDriver(selectedBrowser);
            myScenarioContext.Set(myWebDriver, "currentDriver");
            HomePage homePage = new HomePage(myWebDriver, systemUnderTest.AppUrl);
            
        }

        [AfterFeature] 
        public static void TearDown()
        {
            
            Console.WriteLine("**After scenario:");
           
            if (myWebDriver != null)
            {
                TakeScreenShot();
                Console.WriteLine($"Cleanup driver for scenario [{myScenarioContext.ScenarioInfo.Title}]");
                myWebDriver.Close();
                myWebDriver.Quit();
                myWebDriver.Dispose();
            }

            int numberOfBrowserProcesses = NumberOfProcess(selectedBrowser);
            Trace.WriteLine("number of webdriver afterScenario " + numberOfBrowserProcesses);
            Console.WriteLine($"Test outcome: {TestContext.CurrentContext.Result}");
        }

        private static int NumberOfProcess(string selectedDriver)
        {
            string processname = selectedDriver == "Chrome" ? "chromedriver" : "msedgedriver";
            Process[] chromeDriverProcesses = Process.GetProcessesByName(processname);
            return chromeDriverProcesses.Length;
        }

        private static void TakeScreenShot()
        {
            try
            {
                string fileNameBase = string.Format("Finished_Screenshot_{0:yyyyMMdd_HHmmss}", DateTime.Now);
                ITakesScreenshot takesScreenshot = myWebDriver as ITakesScreenshot;

                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();
                    string screenshotFilePath = Path.Combine(fileNameBase + ".png");
                    screenshot.SaveAsFile(screenshotFilePath);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Screenshot was not possible");
            }
        }
    }
}
