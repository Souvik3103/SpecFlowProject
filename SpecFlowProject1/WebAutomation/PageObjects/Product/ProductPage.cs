using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject1.WebAutomation.PageObjects.Product
{
    internal class ProductPage : PageBase
    {
        private string? _productName;
        private By byProductName(string productName) => By.XPath($"//*[@id='productTitle' and contains(text(), '{productName}')]");
        private By byMetaContent(string productName) => By.XPath($"//meta[@name='title' and contains(@content, '{productName}')]"); 
        private readonly By byProductPrice = By.XPath("//div[@id='corePriceDisplay_desktop_feature_div']//span[@class='a-price-whole']");
        private readonly By byAddToCart = By.XPath("//*[@id='add-to-cart-button']");
        private readonly By byAddToCartError = By.XPath("//*[@id='attach-string-cart-generic-error' and @class!='aok-hidden']");
        private readonly By byRetryAddingToCart = By.XPath("//*[@id='attach-string-cart-generic-error' and @class!='aok-hidden']/parent::div/a");
        private readonly By byAddToCartSuccess = By.XPath("//*[@id='add-to-cart-confirmation-image']");
        public ProductPage(IWebDriver wd) : base(wd)
        {

        }

        public string GetProductValueAndAddToCart(string productName)
        {
            _productName = productName;
            string productPrice = string.Empty;
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            IWebElement productElement = FindElementWithWait(byProductName(productName));
            if (productElement != null)
            {
                productPrice = webDriver.FindElement(byProductPrice).Text;
            }
            if (productPrice != string.Empty)
            {
                ClickElement(byAddToCart);
                
                CheckCartError();
            }
            return productPrice;
        }
        
        private void CheckCartError()
        {
            if (FindElementWithWait(byAddToCartError) != null)
            {
                ClickElement(byRetryAddingToCart);
            }
        }

        public bool AddToCartSuccessful()
        {
            if (webDriver.FindElement(byAddToCartSuccess).Displayed)
            {
                Console.WriteLine($"Product {_productName} added to cart successfully");
                return true;
            }
            return false;
        }

    }
}
