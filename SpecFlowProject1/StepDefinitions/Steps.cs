using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProject1.Helpers;
using SpecFlowProject1.WebAutomation.PageObjects.Cart;
using SpecFlowProject1.WebAutomation.PageObjects.Home;
using SpecFlowProject1.WebAutomation.PageObjects.Product;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class Steps
    {
        private static ScenarioContext myScenarioContext;
        private static IWebDriver webDriver;
        private static HomePage myHomePage;
        private static SearchPage mySearchPage;
        private static ProductPage myProductPage;
        private static CartPage myCartPage;
        static string selectedBrowser;
        private string productPrice;
        private string productName;

        public Steps(ScenarioContext scenarioContext)
        { 
            
        }

        [Given(@"User Opens Amazon Home Page")]
        public void UserOpensAmazonHomePage()
        {
            Assert.IsNotNull(myHomePage, "Home page could not be instantiated");
            Assert.IsTrue(myHomePage.PageExists(), "Application Home page does not exists");
        }

        [When(@"User Searches for (.*)")]
        public void WhenUserSearchesFor(string searchString)
        {
            productName = searchString;
            Assert.IsTrue(myHomePage.SearchItem(searchString), $"Could not search for item {searchString}");
        }

        [Then(@"User is displayed the search result")]
        public void ThenUserIsDisplayedTheSearchResult()
        {
            mySearchPage.SearchResultAvailable();
        }

        [When(@"User Selects the item (.*) from search page")]
        public void WhenUserSelectsTheItemTP_LinkNWiFiRouter_WirelessInternetRouterForHomeTL_WRNFromSearchPage(string searchString)
        {
            Assert.IsTrue(mySearchPage.OpenItem(searchString), $"Item with description: {searchString} not found");
        }

        [Then(@"User Checks the price and adds the product to cart")]
        public void ThenUserChecksThePriceAndAddsTheProductToCart()
        {
            productPrice = myProductPage.GetProductValueAndAddToCart(productName);
            Assert.IsTrue(myProductPage.AddToCartSuccessful(), "Item Could not be added to cart");
        }

        [When(@"User navigates to the Cart")]
        public void WhenUserNavigatesToTheCart()
        {
            myCartPage.NavigateToCart();
        }

        [Then(@"User Verifies the (.*) and price")]
        public void ThenUserVerifiesTheProductNameAndPrice(string productName)
        {
            Tuple<string, float> productPricePair = myCartPage.VerifyCartItem(productName);
            Assert.That(productPricePair.Item1.Contains(this.productName), $"Product Name in Cart {productPricePair.Item1} is not equal to Expected Price {this.productName}");
            var expectedPrice = float.Parse(productPrice);
            Assert.IsTrue(productPricePair.Item2 == expectedPrice, $"Product Price in Cart {productPricePair.Item2} is not equal to Expected Price {expectedPrice}");
        }









        [BeforeScenario]
        public static void SetUp(ScenarioContext scenarioContext)
        {
            myScenarioContext = scenarioContext;
            SystemUnderTest systemUnderTest = new SystemUnderTest();
            Console.WriteLine($"Scenario: {myScenarioContext.ScenarioInfo.Title}");
            ScenarioSetup(systemUnderTest);
        }

        public static void ScenarioSetup(SystemUnderTest systemUnderTest)
        {

            myScenarioContext.Set(systemUnderTest, "currentSystemUnderTest");
            selectedBrowser = TestContext.Parameters["selectedBrowser"].ToString();
            int numberOfBrowserProcesses = NumberOfProcess(selectedBrowser);

            //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Console.WriteLine("number of webdriver beforeScenario " + numberOfBrowserProcesses);



            Console.WriteLine($"Set driver for context {myScenarioContext.ScenarioInfo.Title}");

            webDriver = WebInit.InitWebDriver(selectedBrowser);
            myScenarioContext.Set(webDriver, "currentDriver");
            myHomePage = new HomePage(webDriver, systemUnderTest.AppUrl);
            mySearchPage = new SearchPage(webDriver);
            myProductPage = new ProductPage(webDriver);
            myCartPage = new CartPage(webDriver);
            myHomePage.NavigateToHomePage();

        }

        [AfterScenario]
        public static void TearDown()
        {

            Console.WriteLine("**After scenario:");

            if (webDriver != null)
            {
                TakeScreenShot();
                Console.WriteLine($"Cleanup driver for scenario [{myScenarioContext.ScenarioInfo.Title}]");
                webDriver.Close();
                webDriver.Quit();
                webDriver.Dispose();
            }

            int numberOfBrowserProcesses = NumberOfProcess(selectedBrowser);
            Console.WriteLine("number of webdriver afterScenario " + numberOfBrowserProcesses);
            Console.WriteLine($"Test outcome: {TestContext.CurrentContext.Result.Outcome}");
        }

        private static int NumberOfProcess(string selectedDriver)
        {
            string processname = selectedDriver.ToUpper() == "CHROME" ? "chromedriver" : "msedgedriver";
            Process[] chromeDriverProcesses = Process.GetProcessesByName(processname);
            return chromeDriverProcesses.Length;
        }

        private static void TakeScreenShot()
        {
            try
            {
                string fileNameBase = string.Format("Finished_Screenshot_{0:yyyyMMdd_HHmmss}", DateTime.Now);
                ITakesScreenshot takesScreenshot = webDriver as ITakesScreenshot;

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
