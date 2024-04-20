using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowProject1.WebAutomation.PageObjects
{
    public class PageBase
    {
        protected IWebDriver webDriver;
        protected IWait<IWebDriver> wait;
        
        public PageBase(IWebDriver wd)
        {
            webDriver = wd;
            wait = new WebDriverWait(webDriver, TimeSpan.FromMinutes(5));
        }

        public bool ClickElement(IWebElement element)
        {
            int numberOfTries = 5; //can be customised from runsettings - not implemeted
            while (numberOfTries > 0)
            {
                try
                {
                    element.Click();
                    return true;
                }
                catch (Exception)
                {
                    numberOfTries--;
                    ScrollToElement(element);
                }
            }
            if (numberOfTries == 0)
            {
                throw new ElementNotInteractableException("Could not Click on Element");
            }
            return false;
        }

        public bool ClickElement(By mechanism)
        {
            int numberOfTries = 5; //can be customised from runsettings - not implemeted
            while (numberOfTries > 0)
            {
                try
                {
                    webDriver.FindElement(mechanism).Click();
                    return true;
                }
                catch (Exception)
                {
                    numberOfTries--;
                    ScrollToElement(mechanism);
                }
            }
            if (numberOfTries == 0)
            {
                throw new ElementNotInteractableException("Could not Click on Element");
            }
            return false;
        }

        protected IWebElement FindElementFromElements(By mechanism)
        {
            const int numOfItr = 1;
            const double defaultWait = 0; // sec
            for (var i = 0; i < numOfItr; i++)
            {
                try
                {
                    var element = webDriver.FindElement(mechanism);
                    return element;
                }
                catch (Exception)
                {
                    if (i == numOfItr - 1)
                    {
                        throw;
                    }
                }
            }
            return null;
        }

        protected IWebElement FindElementWithWait(By mechanism)
        {
            const int numOfItr = 2;
            const double defaultWait = 0.1; // sec
            for (var i = 0; i < numOfItr; i++)
            {
                try
                {
                    var element = webDriver.FindElement(mechanism);
                    return element;
                }
                catch (Exception)
                {
                    wait.Until(d => webDriver.FindElement(mechanism).Displayed);
                    if (i == numOfItr - 1)
                    {
                        throw;
                    }
                }
            }
            return null;
        }

        protected bool EnterValueToInputBox(IWebElement element, string value)
        {
            try
            {
                element.SendKeys(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool EnterValueToInputBox(By mechanism, string value)
        {
            try
            {
                webDriver.FindElement(mechanism).SendKeys(value);
                webDriver.FindElement(mechanism).SendKeys(Keys.Enter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ScrollToElement(IWebElement element)
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor) webDriver;
            javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public void ScrollToElement(By mechanism)
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)webDriver;
            javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", webDriver.FindElement(mechanism));
        }
    }
}
