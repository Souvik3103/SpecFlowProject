using OpenQA.Selenium;

namespace SpecFlowProject1.WebAutomation.PageObjects.Home
{
    internal class SearchPage : PageBase
    {
        private readonly By bySearchResultRows = By.XPath("//div[@data-component-type='s-search-result']");
        private By byItemDesc(string itemName) => By.XPath($"//h2/a/span[contains(text(), '{itemName}')]");

        public SearchPage(IWebDriver wd) : base(wd)
        {

        }

        public bool SearchResultAvailable()
        {
            return webDriver.FindElements(bySearchResultRows).Count() > 0 ? true : false;
        }

        public bool OpenItem(string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                Console.WriteLine("Item Cannot be Null or empty");
                return false;
            }
            var selectedElement = FindElementFromElements(byItemDesc(itemName));
            if (selectedElement != null)
            {
                ClickElement(selectedElement);
                return true;
            }
            return false;
        }
        
    }
}
