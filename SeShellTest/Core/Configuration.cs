using System;
using System.Configuration;
using SeShell.Test.Enums;

namespace SeShell.Test.Core
{
    class Configuration {         

        //Application base URL
        public static string BaseSiteUrl         
        {
            get { return ConfigurationManager.AppSettings["BaseUrl"]; }         
        }

        //Storage path location of Error image snapshots
        public static string ErrorImagePath
        {
            get { return ConfigurationManager.AppSettings["ErrorImagePath"]; }
        }

        //Data driven test data path specifined in confige
        public static string TestDataFilePath
        {
            get { return ConfigurationManager.AppSettings["TestDataDirectory"]; }
        }

        //upload data path specifined in confige
        public static string TestDataUploadDirectory
        {
            get { return ConfigurationManager.AppSettings["TestDataUploadDirectory"]; }
        }

        //Retunrs the Login page name
        public static string LoginPage
        {
            get { return ConfigurationManager.AppSettings["LoginPage"]; }
        }

        //System setting to wait for a response
        //Returns the configured values else returns 10 seconds
        public static int WaitForResponse
        {
            get
            {
                try { return Int16.Parse(ConfigurationManager.AppSettings["ResponseTimeInSecons"]); }
                catch
                { return 10; }
            }
        }

        //Returns AdminSiteURL if given
        public static string AdminSiteUrl         
        {             
            get { return ConfigurationManager.AppSettings["AdminSiteUrl"]; }         
        }
        
        //Retunrs the PortNumber
        public static int PortNumber       
        {             
            get { return Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]); }         
        }

        //Retuns the configured web browser  
        public static WebBrowsers BrowserType        
        {             
            get {
                try { 
                    return (WebBrowsers) Enum.Parse(typeof(WebBrowsers), ConfigurationManager.AppSettings["BrowserType"],true); 
                }  
                catch 
                { return WebBrowsers.Ie; }
            }
                       
        }     
    } 
} 
