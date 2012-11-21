using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace SeShell.Test.Core
{
    /// <summary>
    /// Utility is being used to keep the actions waiting for a specified time or 
    /// till the next required object/contorls avaiable 
    /// </summary>
    public class ThreadWait : BaseClass
    {

        //Waits for a system configured time in the configuration file
        //if it is not configured it takes a 10 sencond value
        public static void WaitUntilElementAppears(string xpathOfUI)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Configuration.WaitForResponse));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
                {
                    return d.FindElement(By.XPath(xpathOfUI));
                });
        }

        //Waits for a control to be visible for a user spcified time
        public static void WaitUntilElementAppears(string xpathOfUI, int maxWait)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(maxWait));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.XPath(xpathOfUI));
            });
        }

        //Waits for a system configured time in the configuration file
        //if it is not configured it takes a 10 sencond value
        public static void WaitUntilElementFillWithContentUsingXpath(string xpathOfUi)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Configuration.WaitForResponse));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.XPath(xpathOfUi));
            });
            bool foundContent = wait.Until<bool>((b) =>
            {
                return myDynamicElement.Text.Length > 0;
            });

        }

        //Waits for a system configured time in the configuration file
        //if it is not configured it takes a 10 sencond value
        public static void WaitUntilPopupWindowAndClose(string windowId, string xpathOfUI)
        {
            
                foreach (String locationPopUpHandle in Driver.WindowHandles)
                {
                    
                        Driver.SwitchTo().Window(locationPopUpHandle);
                        try
                        {
                            Driver.FindElement(By.XPath(xpathOfUI)).Click();
                            return;
                        }
                        catch (NoSuchElementException ex)
                        {
                            // ignore as it is trying various popup windows
                        }
                }
        }

        public static void WaitUntilForElementPresent(String elementName)
        {
            DateTime sDateTime = DateTime.Now;
            //int t1 = stratTime.Second;

            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - sDateTime.Ticks);

            while (ts.TotalSeconds <= Configuration.WaitForResponse)
            {
                if (IsElementPresent(elementName))
                {
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        private static bool IsElementPresent(string xpath)
        {
            try
            {
                Driver.FindElement(By.XPath(xpath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
