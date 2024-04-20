using OpenQA.Selenium;
using System.Configuration;

namespace SpecFlowProject1.WebAutomation.PageObjects.Cart
{
    internal class CartPage : PageBase
    {
        public CartPage(IWebDriver wd) : base(wd) { }

        private readonly By byCartBtn = By.XPath("//a[@id='nav-cart']");
        private readonly By byCartPge = By.XPath("//title[contains(text(), 'Shopping Cart')]");
        private readonly By byCartItemPrice = By.XPath("//div[@data-name='Active Items']//span[contains(@class,'sc-price')]");
        private By byCartItem(string ItemName) => By.XPath($"//div[@data-name='Active Items']//span[@class='a-truncate-cut' and contains(text(), '{ItemName}')]");

        public bool NavigateToCart()
        {
            ClickElement(byCartBtn);
            if (webDriver.FindElement(byCartPge).Displayed)
            {
                return true;
            }
            return false;
        }

        public Tuple<string, float> VerifyCartItem(string productName)
        {
            var product = FindElementWithWait(byCartItem(productName));
            var price = FindElementWithWait(byCartItemPrice);
            string expectedProductName = product.Text;
            float expectedPrice = float.Parse(price.Text);
            return new Tuple<string, float>(expectedProductName, expectedPrice);
        }


    }
}
