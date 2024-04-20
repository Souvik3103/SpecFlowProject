using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using SpecFlowProject1.WebAutomation.PageObjects.Home;

namespace SpecFlowProject1.Helpers
{
    internal class WebInit
    {
        public static IWebDriver InitWebDriver(string selectedBrowser)
        {
            IWebDriver driver;
            if (string.IsNullOrEmpty(selectedBrowser))
            {
                throw new ArgumentNullException("Please select a browser in runsettings");
            }
            switch (selectedBrowser.ToUpper())
            {
                case "CHROME":
                    bool mode = TestContext.Parameters["ChromeHeadless"] == "true" ? true : false;
                    driver = InitChromeDriver(mode); break;
                case "EDGE":
                    driver = InitEdgeDriver(); break;
                default:
                    driver = new ChromeDriver();    //default case to default setting chromedriver
                    break;
            }
            return driver;
        }

        private static IWebDriver InitChromeDriver(bool mode)
        {
            
            ChromeOptions chromeOptions = new ChromeOptions();
            if (mode)
            {
                chromeOptions.AddArgument("--headless");
            }

            chromeOptions.AddArgument("--start-maximized");


            chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AddArgument("--disable-notifications");
            chromeOptions.AddArgument("--disable-web-security");
            chromeOptions.AddArgument("--no-sandbox");


            IWebDriver webDriver;
            var cService = ChromeDriverService.CreateDefaultService();
            cService.HideCommandPromptWindow = true;
            webDriver = new ChromeDriver(cService, chromeOptions, TimeSpan.FromMinutes(5));
            Console.WriteLine($"Chrome process id: {cService.ProcessId}");

            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(5);
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Manage().Window.Maximize();
            return webDriver;
        }

        private static IWebDriver InitEdgeDriver()
        {
            //Not implementing Edge Options
            //Using Chrome for tests
            return new EdgeDriver();
        }

        public void startScenario(IWebDriver webDriver, SystemUnderTest systemUnderTest)
        {
            try
            {
                HomePage objHomePage = new HomePage(webDriver, systemUnderTest.AppUrl);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
