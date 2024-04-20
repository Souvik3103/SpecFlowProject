using OpenQA.Selenium;

namespace SpecFlowProject1.WebAutomation.PageObjects.Home
{
    internal class HomePage : PageBase
    {
        private string myAppUrl;
        public HomePage(IWebDriver wd, string appurl) : base(wd)
        {
            myAppUrl = appurl;
        }

        private readonly By byHomePageBanner = By.XPath("//div[@id='nav-logo']/a[@aria-label='Amazon.in']");
        private readonly By bySearchBar = By.XPath("//*[@id='twotabsearchtextbox']");
        private readonly By bySearchBtn = By.XPath("//*[@id='nav-search-submit-button']");

        public HomePage(IWebDriver wd) : base(wd)
        {

        }
        
        public void NavigateToHomePage()
        {
            if (myAppUrl == null)
            {
                throw new Exception("Application URl is empty/null");
            }
            webDriver.Navigate().GoToUrl(myAppUrl);
        }

        public bool PageExists()
        {
            IWebElement homePageBanner = FindElementWithWait(byHomePageBanner);
            return homePageBanner != null ? true : false;
        }

        public bool SearchItem(string search)
        {
            IWebElement searchBox = FindElementWithWait(bySearchBar);
            if (EnterValueToInputBox(searchBox, search)){
                ClickElement(bySearchBtn);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
