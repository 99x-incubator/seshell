using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using Selenium;
using SeShell.Test.Enums;

namespace SeShell.Test.Core
{
    /// <summary>
    /// Base class is used to make the driver initiate and set the base URL
    /// for other page classes to use 
    /// </summary>
    public class BaseClass: EventLogger
    {
        public static IWebDriver Driver;
        public static string BaseUrl;
        public static WebBrowsers WebBrws = WebBrowsers.None;
        public static ISelenium Selenium ;

        
        static BaseClass()
        {
            //Constructor set the Base Url and bfowser 
            BaseUrl = Configuration.BaseSiteUrl;

        }

         /// <summary>
         /// Navigate the browser to the passed url
         /// </summary>
         /// <param name="url"></param>
        public void NavigateTo(string url)
        {
            var navigateToThisUrl = BaseUrl + url;
            Driver.Navigate().GoToUrl(navigateToThisUrl);
        }

        //Start the browser depending on the setting
        public void GetDriver(WebBrowsers webBrws)
        {
            WebBrws = webBrws;
            if (webBrws == WebBrowsers.Ie)
            {
                //Secutiry setting for IE
                var capabilitiesInternet = new InternetExplorerOptions { IntroduceInstabilityByIgnoringProtectedModeSettings = true };
                Driver = new InternetExplorerDriver(capabilitiesInternet);
            }
            else
                if (webBrws == WebBrowsers.FireFox)
                {
                    //FirefoxBinary binary = new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                    FirefoxProfile profile = new FirefoxProfile();
                    // profile.SetPreference("webdriver.firefox.bin", "C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe");
                    Driver = new FirefoxDriver(profile);
                }
                else
                    if (webBrws == WebBrowsers.Chrome)
                    {
                        //Chrome driver requires the ChromeDriver.exe
                        Driver = new ChromeDriver(@"..\..\..\lib\BrowserDriver\Chrome");
                    }
                    else { throw new WebDriverException(); }

            Selenium = new WebDriverBackedSelenium(Driver, BaseUrl);
        }
    }
}
